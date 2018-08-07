/*
 *  sp_ValidateItemTypes
 *  Perform Product Type searches by Product Type Name and Product Type Description
 *  If the CreateProductsTypes is not allowed use a defualt value.
 *  If hte Default value is nto found reject
 *  Reject sets ItemTypeUID to -1 and Rejected to true, add Reject Notes
 */
CREATE PROCEDURE [dbo].[sp_ValidateItemTypes]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateProductTypes     AS BIT,
                @DefaultProductType     AS VARCHAR(50),
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @AllowStackingErrors    AS BIT,
                @ErrorCode              AS INT;

        SET NOCOUNT ON;

        SET @CreateProductTypes = 0;
        SET @DefaultProductType = 'Unassigned';
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
            @CreateProductTypes = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'CreateProductTypes'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateProductTypes', 1;
            END

        SELECT @DefaultProductType = UPPER(LTRIM(RTRIM(ConfigurationValue)))
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultProductType'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for DefaultProductType', 1;
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
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for AllowStackingErrors', 1;
            END

        /*
         * Check if there is a match by Product Type Name, then by Produt Type Description (if not null)
         * For the remaining if CreateProductTypes is false set them to the default product type (if found)
         *     else reject Product Types where Product Type Name is NULL
         */

        --Get Matches by ProductTypeName
        --SELECT TargetItemTypes.ProductTypeName, SourceItemTypes.ItemTypeName, SourceItemTypes.ItemTypeUID
        UPDATE TargetItemTypes SET TargetItemTypes.ItemTypeUID = SourceItemTypes.ItemTypeUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetItemTypes
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechItemTypes SourceItemTypes
            ON UPPER(LTRIM(RTRIM(TargetItemTypes.ProductTypeName))) = UPPER(LTRIM(RTRIM(SourceItemTypes.ItemTypeName)))
        WHERE 
            TargetItemTypes.ProductTypeName IS NOT NULL
        AND SourceItemTypes.ItemTypeUID IS NOT NULL
        AND TargetItemTypes.ItemTypeUID = 0
        AND TargetItemTypes.ProcessTaskUID = @ProcessTaskUid
        AND (TargetItemTypes.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match ProductTypes by Name', 1;
            END

        --Get Matches by ProductTypeDescription
        --SELECT TargetItemTypes.ProductTypeName, TargetItemTypes.ProductTypeDescription, SourceItemTypes.ItemTypeName, SourceItemTypes.ItemTypeDescription, SourceItemTypes.ItemTypeUID
        UPDATE TargetItemTypes SET TargetItemTypes.ItemTypeUID = SourceItemTypes.ItemTypeUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetItemTypes
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechItemTypes SourceItemTypes
            ON UPPER(LTRIM(RTRIM(TargetItemTypes.ProductTypeDescription))) = UPPER(LTRIM(RTRIM(SourceItemTypes.ItemTypeDescription)))
        WHERE 
            TargetItemTypes.ProductTypeDescription IS NOT NULL
        AND SourceItemTypes.ItemTypeUID IS NOT NULL
        AND TargetItemTypes.ItemTypeUID = 0
        AND TargetItemTypes.ProcessTaskUID = @ProcessTaskUid
        AND (TargetItemTypes.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match ProductTypes by Description', 1;
            END

        PRINT N'@CreateProductTypes: ' + CAST(@CreateProductTypes AS VARCHAR)

        IF @CreateProductTypes = 1
            BEGIN
                --If Product Type Name is null or empty reject
                --SELECT TargetItemTypes.ProductTypeName, TargetItemTypes.ProductTypeDescription
                UPDATE TargetItemTypes SET Rejected = 1, ItemTypeUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: ProductTypeName; ProductTypeName is NULL or Empty'
                FROM 
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetItemTypes
                WHERE 
                    (TargetItemTypes.ProductTypeName IS NULL OR
                     LTRIM(RTRIM(TargetItemTypes.ProductTypeName)) = '')
                AND TargetItemTypes.ItemTypeUID = 0
                AND TargetItemTypes.ProcessTaskUID = @ProcessTaskUid
                AND (TargetItemTypes.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where the ProductTypeName is Null or Empty', 1;
                    END

            END
        ELSE
            BEGIN
                --Get the ItemTypeUID of the default Product Type, if not found reject remainders
                DECLARE @ItemTypeUID AS INT;
                SET @ItemTypeUID = -1;

                SELECT @ItemTypeUID = ItemTypeUID
                FROM TipWebHostedChicagoPS.dbo.tblTechItemTypes SourceItemTypes
                WHERE UPPER(LTRIM(RTRIM(SourceItemTypes.ItemTypeName))) = @DefaultProductType

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match the DefaultProductType by Name', 1;
                    END

                PRINT N'DefaultProduct ItemTypeUID: ' + CAST(@ItemTypeUID AS VARCHAR)

                IF @ItemTypeUID = -1
                    BEGIN
                        PRINT N'Default Product Type not found, Rejecting'

                        --If Product Type Name is null or empty reject
                        --SELECT TargetItemTypes.ProductTypeName, TargetItemTypes.ProductTypeDescription
                        UPDATE TargetItemTypes
                        SET Rejected = 1, ItemTypeUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: ProductTypeName; ProductTypeName is NULL or Empty'
                        FROM 
                            IntegrationMiddleWay.dbo._ETL_Inventory TargetItemTypes
                        WHERE 
                            (TargetItemTypes.ProductTypeName IS NULL OR
                             LTRIM(RTRIM(TargetItemTypes.ProductTypeName)) = '')
                        AND TargetItemTypes.ItemTypeUID = 0
                        AND TargetItemTypes.ProcessTaskUID = @ProcessTaskUid
                        AND (TargetItemTypes.Rejected = 0 OR @AllowStackingErrors = 1);

                        IF @@ERROR <> 0
                            BEGIN
                                SET @ErrorCode = @@ERROR + 100000;
                                --SET @ErrorMessage = ;
                                --RETURN @ErrorCode;
                                THROW @ErrorCode, 'Failed to reject records where the ProductTypeName is Null or Empty', 1;
                            END

                    END
                ELSE
                    BEGIN
                        PRINT N'Default Product Type Found, Setting Default ItemTypeUID'

                        --Set the Default ItemTypeUID and ProductTypeName to the default value
                        --SELECT TargetItemTypes.ProductTypeName, TargetItemTypes.ProductTypeDescription
                        UPDATE TargetItemTypes SET TargetItemTypes.ItemTypeUID = @ItemTypeUID, ProductTypeName = @DefaultProductType
                        FROM
                            IntegrationMiddleWay.dbo._ETL_Inventory TargetItemTypes
                        WHERE
                            TargetItemTypes.ItemTypeUID = 0
                        AND TargetItemTypes.ProcessTaskUID = @ProcessTaskUid
                        AND (TargetItemTypes.Rejected = 0 OR @AllowStackingErrors = 1);

                        IF @@ERROR <> 0
                            BEGIN
                                SET @ErrorCode = @@ERROR + 100000;
                                --SET @ErrorMessage = ;
                                --RETURN @ErrorCode;
                                THROW @ErrorCode, 'Failed to set the DefaultProductType on ProductTypes not matched', 1;
                            END
                    END
            END

        SET NOCOUNT OFF;

        RETURN 0;

    END --End Procedure