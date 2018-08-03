/*
 *  sp_ValidateItems
 *  Perform Product searches by Product Name, Product Description and Product By Number
 *  Get the highest ItemUID for the matching product if multiples
 *  If the CreateProducts configuration is not set Reject 
 *  Reject sets ItemUID to -1 and Rejected to true, add Reject Notes
 */
CREATE PROCEDURE [dbo].[sp_ValidateItems]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateProducts                         AS BIT,
                @OverwriteProductManufacturerFromSource AS BIT,
                @TargetDatabase                         AS VARCHAR(100),
                @SourceTable                            AS VARCHAR(100),
                @AllowStackingErrors                    AS BIT,
                @ErrorCode                              AS INT;

        SET NOCOUNT ON;

        --Set a default starting value
        SET @CreateProducts = 0;
        SET @TargetDatabase = [dbo].[fn_GetTargetDatabaseName](@ProcessUid);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to determine the Target Database for the Process', 1;
            END

        SET @SourceTable = [dbo].[fn_GetSourceTable](@SourceProcess);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to determine the Source Table for the Process', 1;
            END

        --Check that Target Database is not null or empty
        IF @TargetDatabase IS NULL OR LEN(@TargetDatabase) = 0
            BEGIN
                ;
                THROW 50000, 'Target Database Name is empty.', 1;
            END

        --Check that Source Table is not null or empty
        IF @SourceTable IS NULL OR LEN(@SourceTable) = 0
            BEGIN
                ;
                THROW 50000, 'Source Table could not be verified.', 1;
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
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateProducts', 1;
            END

        SELECT
            @OverwriteProductManufacturerFromSource = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations] 
        WHERE ConfigurationName = 'OverwriteProductManufacturerFromSource'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for OverwriteProductManufacturerFromSource', 1;
            END

        SELECT
            @AllowStackingErrors = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'AllowStackingErrors' 
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for AllowStackingErrors', 1;
            END

        -- Match the Item by Name
        --SELECT TargetItem.ItemUID, TargetItem.ProductName, SourceItem.ItemName, SourceItem.ItemUID
        UPDATE TargetItem SET TargetItem.ItemUID = SourceItem.ItemUID, TargetItem.ProductNumber = SourceItem.ItemNumber
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
        INNER JOIN (
            SELECT
                MAX(SourceItem.ItemUID) ItemUID, SourceItem.ItemName, SourceItem.ItemNumber
            FROM 
                IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
            LEFT JOIN
                TipWebHostedChicagoPS.dbo.tblTechItems SourceItem
                ON UPPER(LTRIM(RTRIM(TargetItem.ProductName))) = UPPER(LTRIM(RTRIM(SourceItem.ItemName)))
            WHERE
                SourceItem.ItemName IS NOT NULL
            AND TargetItem.ProductName IS NOT NULL
            AND TargetItem.ProcessTaskUID = @ProcessTaskUid
            AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1)
            GROUP BY
                SourceItem.ItemName,
                SourceItem.ItemNumber
            ) SourceItem
            ON UPPER(LTRIM(RTRIM(TargetItem.ProductName))) = UPPER(LTRIM(RTRIM(SourceItem.ItemName)))
        WHERE 
            SourceItem.ItemName IS NOT NULL
        AND TargetItem.ProductName IS NOT NULL
        AND TargetItem.ItemUID = 0
        AND TargetItem.ProcessTaskUID = @ProcessTaskUid
        AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Items by Name', 1;
            END

        -- Match the Item by Description
        --SELECT TargetItem.ProductName, TargetItem.ProductDescription, SourceItem.ItemDescription, SourceItem.ItemUID
        UPDATE TargetItem SET TargetItem.ItemUID = SourceItem.ItemUID, TargetItem.ProductNumber = SourceItem.ItemNumber
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
        INNER JOIN (
            SELECT
                MAX(SourceItem.ItemUID) ItemUID, SourceItem.ItemNumber, SourceItem.ItemDescription
            FROM 
                IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
            LEFT JOIN
                TipWebHostedChicagoPS.dbo.tblTechItems SourceItem
                ON UPPER(LTRIM(RTRIM(TargetItem.ProductDescription))) = UPPER(LTRIM(RTRIM(SourceItem.ItemDescription)))
            WHERE
                SourceItem.ItemDescription IS NOT NULL
            AND TargetItem.ProductDescription IS NOT NULL
            AND TargetItem.ProcessTaskUID = @ProcessTaskUid
            AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1)
            GROUP BY
                SourceItem.ItemNumber,
                SourceItem.ItemDescription
            ) SourceItem
            ON UPPER(LTRIM(RTRIM(TargetItem.ProductDescription))) = UPPER(LTRIM(RTRIM(SourceItem.ItemDescription)))
        WHERE
            SourceItem.ItemDescription IS NOT NULL
        AND TargetItem.ProductDescription IS NOT NULL
        AND TargetItem.ItemUID = 0
        AND TargetItem.ProcessTaskUID = @ProcessTaskUid
        AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Items by Description', 1;
            END

        -- Match the Item by ItemNumber
        --SELECT TargetItem.ProductName, TargetItem.ProductByNumber, SourceItem.ItemNumber, SourceItem.ItemUID
        UPDATE TargetItem SET TargetItem.ItemUID = SourceItem.ItemUID, TargetItem.ProductNumber = SourceItem.ItemNumber
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
        INNER JOIN (
            SELECT
                SourceItem.ItemNumber, MAX(SourceItem.ItemUID) ItemUID
            FROM 
                IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
            LEFT JOIN
                TipWebHostedChicagoPS.dbo.tblTechItems SourceItem
                ON UPPER(LTRIM(RTRIM(TargetItem.ProductByNumber))) = UPPER(LTRIM(RTRIM(SourceItem.ItemNumber)))
            WHERE
                SourceItem.ItemNumber IS NOT NULL
            AND TargetItem.ProductByNumber IS NOT NULL
            AND TargetItem.ProcessTaskUID = @ProcessTaskUid
            AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1)
            GROUP BY
                SourceItem.ItemNumber
            ) SourceItem
            ON UPPER(LTRIM(RTRIM(TargetItem.ProductByNumber))) = UPPER(LTRIM(RTRIM(SourceItem.ItemNumber)))
        WHERE
            SourceItem.ItemNumber IS NOT NULL
        AND TargetItem.ProductByNumber IS NOT NULL
        AND TargetItem.ItemUID = 0
        AND TargetItem.ProcessTaskUID = @ProcessTaskUid
        AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Items by ProductByNumber', 1;
            END

        --Reject All Products Not matched where Product Name is NULL
        UPDATE TargetItem SET Rejected = 1, ItemUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: ProductName; ProductName is NULL or Empty'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
        WHERE
            TargetItem.ItemUID = 0
        AND (TargetItem.ProductName IS NULL OR
                LTRIM(RTRIM(TargetItem.ProductName)) = '')
        AND TargetItem.ProcessTaskUID = @ProcessTaskUid
        AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the ProductName is Null or Empty', 1;
            END

        IF @CreateProducts = 0
            BEGIN
                --Reject All Products Not matched
                UPDATE TargetItem SET Rejected = 1, ItemUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: ProductName; ProductName could not be matched'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
                WHERE
                    TargetItem.ItemUID = 0
                AND TargetItem.ProcessTaskUID = @ProcessTaskUid
                AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where the ProductName could not be matched', 1;
                    END

            END
        ELSE
            BEGIN
                IF @OverwriteProductManufacturerFromSource = 1
                    BEGIN
                        --Get the Manufacturer of the product and overwrite in staging
                        --SELECT TargetItem.ItemUID, TargetItem.ProductName, TargetItem.ManufacturerName, TargetItem.ManufacturerUID, SourceItem.ItemName, SourceItem.ItemUID, SourceManufacturer.ManufacturerUID, SourceManufacturer.ManufacturerName
                        UPDATE TargetItem SET TargetItem.ManufacturerUID = SourceItem.ManufacturerUID, TargetItem.ManufacturerName = SourceManufacturer.ManufacturerName
                        FROM
                            IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
                        INNER JOIN
                            TipWebHostedChicagoPS.dbo.tblTechItems SourceItem
                            ON TargetItem.ItemUID = SourceItem.ItemUID
                        INNER JOIN
                            TipWebHostedChicagoPS.dbo.tblUnvManufacturers SourceManufacturer
                            ON SourceItem.ManufacturerUID = SourceManufacturer.ManufacturerUID
                        WHERE
                            TargetItem.ItemUID <> 0
                        AND UPPER(LTRIM(RTRIM(TargetItem.ManufacturerName))) <> UPPER(LTRIM(RTRIM(SourceManufacturer.ManufacturerName)))
                        AND TargetItem.ProcessTaskUID = @ProcessTaskUid
                        AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1);

                        IF @@ERROR <> 0
                            BEGIN
                                SET @ErrorCode = @@ERROR;
                                --SET @ErrorMessage = ;
                                --RETURN @ErrorCode;
                                THROW @ErrorCode, 'Failed to overwrite the Manufacturer from the matched Product', 1;
                            END

                    END
            END

        SET NOCOUNT OFF;

        RETURN 0;

    END --End Procedure