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
                @ErrorCode              AS INT;

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
                SELECT @PRODNUM = Value FROM TipWebHostedChicagoPS.dbo.tblUnvCounter WHERE CounterUID = 4;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to get the Next Value for ProductNumber', 1;
                    END

                --Get all 
                DECLARE NewItems CURSOR READ_ONLY
                FOR
                    SELECT DISTINCT
                        TargetItem.ProductName,
                        TargetItem.ProductDescription,
                        TargetItem.ItemTypeUID,
                        TargetItem.ModelNumber,
                        TargetItem.ManufacturerUID,
                        TargetItem.PurchasePrice,
                        TargetItem.AreaUID
                    FROM
                        IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
                    WHERE
                        TargetItem.ItemUID = 0
                    AND TargetItem.ProcessTaskUID = @ProcessTaskUid
                    AND TargetItem.Rejected = 0;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to create the Cursor of Products to Create', 1;
                    END

                OPEN NewItems;

                FETCH NEXT FROM NewItems
                    INTO @ProductName, @ProductDescription, @ProductTypeUID, @Model, @ManufacturerUID, @SuggestedPrice, @AreaUID;

                WHILE @@FETCH_STATUS = 0
                    BEGIN
                        SET @ItemNumber = @PRODNUM + @COUNT;

                        WHILE EXISTS(SELECT 1 FROM [TipWebHostedChicagoPS].[dbo].[tblTechItems] WHERE [tblTechItems].[ItemNumber] = CAST(@ItemNumber AS VARCHAR))
                            BEGIN
                                SET @COUNT = @COUNT + 1;
                                SET @ItemNumber = @PRODNUM + @COUNT;
                            END

                        IF @@ERROR <> 0
                            BEGIN
                                SET @ErrorCode = @@ERROR + 100000;
                                --SET @ErrorMessage = ;
                                --RETURN @ErrorCode;
                                THROW @ErrorCode, 'Failed to determine the next available ItemNumber', 1;
                            END

                        INSERT INTO TipWebHostedChicagoPS.dbo.tblTechItems
                            ([ItemNumber], [ItemName], [ItemDescription], [ItemTypeUID], [ModelNumber], [ManufacturerUID], [ItemSuggestedPrice],
                             [AreaUID], [ItemNotes], [SKU], [SerialRequired], [ProjectedLife], [CustomField1], [CustomField2], [CustomField3],
                             [Active], [CreatedByUserID], [CreatedDate], [LastModifiedByUserID], [LastModifiedDate], [AllowUntagged])
                        SELECT
                            CAST(@ItemNumber AS VARCHAR),
                            @ProductName,
                            @ProductDescription,
                            @ProductTypeUID,
                            @Model,
                            @ManufacturerUID,
                            @SuggestedPrice,
                            @AreaUID,
                            @Notes,
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
                            0;

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

                UPDATE TargetItem
                SET TargetItem.ItemUID = SourceItem.ItemUID
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
                INNER JOIN
                    TipWebHostedChicagoPS.dbo.tblTechItems SourceItem
                    ON UPPER(LTRIM(RTRIM(TargetItem.ProductName))) = UPPER(LTRIM(RTRIM(SourceItem.ItemName))) AND
                       UPPER(LTRIM(RTRIM(ISNULL(TargetItem.ProductDescription, '')))) = UPPER(LTRIM(RTRIM(ISNULL(SourceItem.ItemDescription, '')))) AND
                       TargetItem.ItemTypeUID = SourceItem.ItemTypeUID AND
                       UPPER(LTRIM(RTRIM(ISNULL(TargetItem.ModelNumber, '')))) = UPPER(LTRIM(RTRIM(ISNULL(SourceItem.ModelNumber, '')))) AND
                       TargetItem.ManufacturerUID = SourceItem.ManufacturerUID AND
                       TargetItem.PurchasePrice = SourceItem.ItemSuggestedPrice AND
                       TargetItem.AreaUID = SourceItem.AreaUID
                WHERE
                    TargetItem.ItemUID = 0
                AND TargetItem.ProcessTaskUID = @ProcessTaskUid
                AND TargetItem.Rejected = 0;

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

                UPDATE SourceItem
                SET SourceItem.ItemName = UPPER(LTRIM(RTRIM(TargetItem.ProductName)))
                FROM
                    TipWebHostedChicagoPS.dbo.tblTechItems SourceItem
                INNER JOIN
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
                    ON SourceItem.ItemUID = TargetItem.ItemUID
                WHERE
                    TargetItem.ItemUID > 0
                AND UPPER(LTRIM(RTRIM(SourceItem.ItemName))) <> UPPER(LTRIM(RTRIM(TargetItem.ProductName)))
                AND TargetItem.ProcessTaskUID = @ProcessTaskUid
                AND TargetItem.Rejected = 0;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to update the Product Name of existing Items', 1;
                    END

            END

    END -- End Procedure