/*
 *  sp_AddUpdate_Items
 *  
 *  
 */
CREATE PROCEDURE [dbo].[sp_AddUpdate_Items]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateProducts         AS BIT,
                @UpdateProductName      AS BIT,
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @Notes                  AS VARCHAR(100),
                @ErrorCode              AS INT,
                @Statement              AS NVARCHAR(MAX),
                @ParamDefinition        AS NVARCHAR(MAX);

        SET NOCOUNT ON;

        SET @CreateProducts = 0;
        SET @UpdateProductName = 0;
        SET @Notes = '';

        SET @TargetDatabase = [dbo].[fn_GetTargetDatabaseName](@ProcessUid);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to determine the Target Database for the Process', 1;
            END

        SET @SourceTable = [dbo].[fn_GetSourceTable](@SourceProcess);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to determine the Source Table for the Process', 1;
            END

        --Check that Target Database is not null or empty
        IF @TargetDatabase IS NULL OR LEN(@TargetDatabase) = 0
            BEGIN
                ;
                THROW 100000, 'Target Database Name is empty.', 1;
            END;

        --Check that Source Table is not null or empty
        IF @SourceTable IS NULL OR LEN(@SourceTable) = 0
            BEGIN
                ;
                THROW 100000, 'Source Table could not be verified.', 1;
            END

        SELECT 
            @CreateProducts = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'CreateProducts'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateProducts', 1;
            END

        SELECT 
            @UpdateProductName = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'UpdateProductName'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for UpdateProductName', 1;
            END

        SELECT
            @Notes = REPLACE(ISNULL(ConfigurationValue, 'MiddleWay Integration - {DATE}'), '{DATE}', CONVERT(VARCHAR(8), GETDATE(), 1))
        FROM [Configurations]
        WHERE ConfigurationName = 'InsertNotes'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for InsertNotes', 1;
            END

        IF @CreateProducts = 1
            BEGIN
                DECLARE @ItemNumber AS INT,
                        @ProductName AS VARCHAR(100),
                        @ProductDescription AS VARCHAR(1000),
                        @ProductTypeUID AS INT,
                        @Model AS VARCHAR(100),
                        @ManufacturerUID AS INT,
                        @SuggestedPrice AS DECIMAL,
                        @AreaUID AS INT,
                        @PRODNUM AS INT,
                        @COUNT AS INT;

                SET @COUNT = 0;

                SET @ParamDefinition = N'@PRODNUM INT OUTPUT';
                SET @Statement = N'
                SELECT @PRODNUM = Value FROM ' + @TargetDatabase + '.dbo.tblUnvCounter WHERE CounterUID = 4';
                EXECUTE sp_executesql @statement, @ParamDefinition, @PRODNUM = @PRODNUM OUTPUT;
                --PRINT @Statement;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to get the Next Value for ProductNumber', 1;
                    END

                IF EXISTS(SELECT 1 FROM tempdb.sys.tables WHERE name LIKE '#NewItems%')
                    BEGIN
                        TRUNCATE TABLE #Tags;
                    END
                ELSE
                    BEGIN
                        CREATE TABLE #NewItems (
                            ProductName VARCHAR(100),
                            ProductDescription VARCHAR(1000),
                            ItemTypeUID INT,
                            ModelNumber VARCHAR(100),
                            ManufacturerUID INT,
                            PurchasePrice DECIMAL,
                            AreaUID INT, 
                            CONSTRAINT [PK_ProductName] PRIMARY KEY CLUSTERED ( ProductName ASC ));

                    END

                --Get all 
                SET @Statement = '
                INSERT INTO #NewItems
                    (ProductName, ProductDescription, ItemTypeUID, ModelNumber, ManufacturerUID, PurchasePrice, AreaUID)
                SELECT DISTINCT
                    TargetItem.ProductName,
                    ISNULL(TargetItem.ProductDescription, TargetItem.ProductName),
                    TargetItem.ItemTypeUID,
                    TargetItem.ModelNumber,
                    TargetItem.ManufacturerUID,
                    MAX(ISNULL(TargetItem.PurchasePrice, 0)),
                    TargetItem.AreaUID
                FROM
                    IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetItem
                WHERE
                    TargetItem.ItemUID = 0
                AND TargetItem.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                AND TargetItem.Rejected = 0
                GROUP BY 
                    TargetItem.ProductName,
                    ISNULL(TargetItem.ProductDescription, TargetItem.ProductName),
                    TargetItem.ItemTypeUID,
                    TargetItem.ModelNumber,
                    TargetItem.ManufacturerUID,
                    TargetItem.AreaUID';
                EXECUTE (@statement);
                --PRINT @Statement;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to Insert data into the NewItems temp table', 1;
                    END

                DECLARE NewItems CURSOR READ_ONLY
                FOR
                    SELECT
                        ProductName, ProductDescription, ItemTypeUID, ModelNumber, ManufacturerUID, PurchasePrice, AreaUID
                    FROM
                        #NewItems

                OPEN NewItems;

                FETCH NEXT FROM NewItems
                    INTO @ProductName, @ProductDescription, @ProductTypeUID, @Model, @ManufacturerUID, @SuggestedPrice, @AreaUID;

                WHILE @@FETCH_STATUS = 0
                    BEGIN
                        SET @ItemNumber = @PRODNUM + @COUNT;

                        SET @ParamDefinition = N'@PRODNUM INT, @COUNT INT OUTPUT, @ItemNumber INT OUTPUT';
                        SET @Statement = '
                        WHILE EXISTS(SELECT 1 FROM [' + @TargetDatabase + '].[dbo].[tblTechItems] WHERE [tblTechItems].[ItemNumber] = CAST(@ItemNumber AS VARCHAR))
                            BEGIN
                                SET @COUNT = @COUNT + 1;
                                SET @ItemNumber = @PRODNUM + @COUNT;
                            END';
                        EXECUTE sp_executesql @statement, @ParamDefinition, @PRODNUM = @PRODNUM, @COUNT = @COUNT OUTPUT, @ItemNumber = @ItemNumber OUTPUT;
                        --PRINT @Statement;

                        IF @@ERROR <> 0
                            BEGIN
                                SET @ErrorCode = @@ERROR + 100000;
                                --SET @ErrorMessage = ;
                                --RETURN @ErrorCode;
                                THROW @ErrorCode, 'Failed to determine the next available ItemNumber', 1;
                            END

                        SET @Statement = '
                        INSERT INTO ' + @TargetDatabase + '.dbo.tblTechItems
                            ([ItemNumber], [ItemName], [ItemDescription], [ItemTypeUID], [ModelNumber], [ManufacturerUID], [ItemSuggestedPrice],
                             [AreaUID], [ItemNotes], [SKU], [SerialRequired], [ProjectedLife], [CustomField1], [CustomField2], [CustomField3],
                             [Active], [CreatedByUserID], [CreatedDate], [LastModifiedByUserID], [LastModifiedDate], [AllowUntagged])
                        SELECT
                            ''' + CAST(@ItemNumber AS VARCHAR) + ''',
                            ''' + @ProductName + ''',
                            ''' + @ProductDescription + ''',
                            ' + CAST(@ProductTypeUID AS VARCHAR) + ',
                            ''' + @Model + ''',
                            ' + CAST(@ManufacturerUID AS VARCHAR) + ',
                            ' + CAST(@SuggestedPrice AS VARCHAR) + ',
                            ' + CAST(@AreaUID AS VARCHAR) + ',
                            ''' + @Notes + ''',
                            NULL,
                            0,
                            0,
                            NULL,
                            NULL,
                            NULL,
                            1,
                            0,
                            GETDATE(),
                            0,
                            GETDATE(),
                            0';
                        EXECUTE (@Statement);
                        --PRINT @Statement;

                        IF @@ERROR <> 0
                            BEGIN
                                SET @ErrorCode = @@ERROR + 100000;
                                --SET @ErrorMessage = ;
                                --RETURN @ErrorCode;
                                THROW @ErrorCode, 'Failed to create Insert a new Product', 1;
                            END

                        SET @COUNT = @COUNT + 1;

                        FETCH NEXT FROM NewItems
                            INTO @ProductName, @ProductDescription, @ProductTypeUID, @Model, @ManufacturerUID, @SuggestedPrice, @AreaUID;

                    END

                CLOSE NewItems;

                DEALLOCATE NewItems;

                SET @Statement = '
                UPDATE TargetItem
                SET TargetItem.ItemUID = SourceItem.ItemUID
                FROM
                    IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetItem
                INNER JOIN
                    ' + @TargetDatabase + '.dbo.tblTechItems SourceItem
                    ON UPPER(LTRIM(RTRIM(TargetItem.ProductName))) = UPPER(LTRIM(RTRIM(SourceItem.ItemName))) AND
                       UPPER(LTRIM(RTRIM(ISNULL(TargetItem.ProductDescription, TargetItem.ProductName)))) = UPPER(LTRIM(RTRIM(ISNULL(SourceItem.ItemDescription, TargetItem.ProductName)))) AND
                       TargetItem.ItemTypeUID = SourceItem.ItemTypeUID AND
                       UPPER(LTRIM(RTRIM(ISNULL(TargetItem.ModelNumber, '''')))) = UPPER(LTRIM(RTRIM(ISNULL(SourceItem.ModelNumber, '''')))) AND
                       TargetItem.ManufacturerUID = SourceItem.ManufacturerUID AND
                       --TargetItem.PurchasePrice = SourceItem.ItemSuggestedPrice AND
                       TargetItem.AreaUID = SourceItem.AreaUID
                WHERE
                    TargetItem.ItemUID = 0
                AND TargetItem.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                AND TargetItem.Rejected = 0';
                EXECUTE (@Statement);
                --PRINT @Statement;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match New Products to their Origin Record', 1;
                    END

            END

        IF @UpdateProductName = 1
            BEGIN

                SET @Statement = '
                UPDATE SourceItem
                SET SourceItem.ItemName = UPPER(LTRIM(RTRIM(TargetItem.ProductName)))
                FROM
                    ' + @TargetDatabase + '.dbo.tblTechItems SourceItem
                INNER JOIN
                    IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetItem
                    ON SourceItem.ItemUID = TargetItem.ItemUID
                WHERE
                    TargetItem.ItemUID > 0
                AND UPPER(LTRIM(RTRIM(SourceItem.ItemName))) <> UPPER(LTRIM(RTRIM(TargetItem.ProductName)))
                AND TargetItem.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                AND TargetItem.Rejected = 0';
                EXECUTE (@Statement);
                --PRINT @Statement;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to update the Product Name of existing Items', 1;
                    END

            END

    END -- End Procedure