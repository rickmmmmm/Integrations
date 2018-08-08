/*
 *  sp_AddUpdate_Inventory
 *  
 *  
 */
CREATE PROCEDURE [dbo].[sp_AddUpdate_Inventory]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @UpdateSerial           AS BIT,
                @UpdateAssetUID         AS BIT,
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @Notes                  AS VARCHAR(100),
                @ErrorCode              AS INT;

        SET NOCOUNT ON;

        SET @UpdateSerial = 0;
        SET @UpdateAssetUID = 0;
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
            @UpdateSerial = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'UpdateSerial'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for UpdateSerial', 1;
            END
            
        SELECT 
            @UpdateAssetUID = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'UpdateAssetUID'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for UpdateAssetUID', 1;
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

        IF @UpdateSerial = 1
            BEGIN

                --SELECT
                --    TargetInventory.InventoryUID, TargetInventory.Tag, TargetInventory.Serial, TargetInventory.AssetID, 
                --    SourceInventory.InventoryUID, SourceInventory.Tag, SourceInventory.Serial, SourceInventory.AssetID
                UPDATE SourceInventory
                SET SourceInventory.Serial = LTRIM(RTRIM(TargetInventory.Serial))
                FROM
                    TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                INNER JOIN
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
                    ON SourceInventory.InventoryUID = TargetInventory.InventoryUID
                WHERE
                    TargetInventory.InventoryUID > 0
                AND TargetInventory.Rejected = 0

                AND (TargetInventory.Serial IS NOT NULL AND
                     LTRIM(RTRIM(TargetInventory.Serial)) <> '')
                AND (SourceInventory.Serial IS NULL OR
                     LTRIM(RTRIM(SourceInventory.Serial)) = '')
                AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
                AND TargetInventory.Rejected = 0;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to set the Serial Number for Source Inventory Records', 1;
                    END

            END

        IF @UpdateAssetUID = 1
            BEGIN

                --SELECT
                --    TargetInventory.InventoryUID, TargetInventory.Tag, TargetInventory.Serial, TargetInventory.AssetID, 
                --    SourceInventory.InventoryUID, SourceInventory.Tag, SourceInventory.Serial, SourceInventory.AssetID
                UPDATE SourceInventory
                SET SourceInventory.AssetID = LTRIM(RTRIM(TargetInventory.AssetID))
                FROM
                    TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                INNER JOIN
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
                    ON SourceInventory.InventoryUID = TargetInventory.InventoryUID
                WHERE
                    TargetInventory.InventoryUID > 0
                AND TargetInventory.Rejected = 0

                AND (TargetInventory.AssetID IS NOT NULL AND
                     LTRIM(RTRIM(TargetInventory.AssetID)) <> '')
                AND (SourceInventory.AssetID IS NULL OR
                     LTRIM(RTRIM(SourceInventory.AssetID)) = '')
                AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
                AND TargetInventory.Rejected = 0;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to set the AssetID for Source Inventory Records', 1;
                    END

            END

        --Insert new Inventory
        INSERT INTO TipWebHostedChicagoPS.dbo.tblTechInventory --SourceInventory
            (InventoryTypeUID, ItemUID, SiteUID, EntityUID, EntityTypeUID, StatusUID, TechDepartmentUID, 
             Tag, Serial, FundingSourceUID, PurchasePrice, PurchaseDate, ExpirationDate, InventoryNotes, 
             CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate, ArchiveUID, ParentInventoryUID, 
             AssetID, BulkUpdated, InventorySourceUID, ContainerUID)
        SELECT
            InventoryTypeUID, ItemUID, SiteUID, EntityUID, EntityTypeUID, StatusID, TechDepartmentUID, 
            Tag, Serial, FundingSourceUID, PurchasePrice, PurchaseDate, ExpirationDate, ISNULL(InventoryNotes, '') + ' ' + @Notes, 
            0, GETDATE(), 0, GETDATE(), 0, ParentInventoryUID, 
            AssetID, 0, InventorySourceUID, ContainerUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        WHERE
            TargetInventory.InventoryUID = 0
        AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
        AND TargetInventory.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to create New Inventory', 1;
            END

        --Match the Created Inventory to origin records
        UPDATE TargetInventory
        SET TargetInventory.InventoryUID = SourceInventory.InventoryUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
            ON TargetInventory.InventoryTypeUID = SourceInventory.InventoryTypeUID AND
               TargetInventory.ItemUID = SourceInventory.ItemUID AND
               TargetInventory.SiteUID = SourceInventory.SiteUID AND
               TargetInventory.EntityUID = SourceInventory.EntityUID AND
               TargetInventory.EntityTypeUID = SourceInventory.EntityTypeUID AND
               TargetInventory.StatusID = SourceInventory.StatusUID AND
               TargetInventory.TechDepartmentUID = SourceInventory.TechDepartmentUID AND
               UPPER(LTRIM(RTRIM(TargetInventory.Tag))) = UPPER(LTRIM(RTRIM(SourceInventory.Tag))) AND
               UPPER(LTRIM(RTRIM(TargetInventory.Serial))) = UPPER(LTRIM(RTRIM(SourceInventory.Serial))) AND
               TargetInventory.FundingSourceUID = SourceInventory.FundingSourceUID AND
               TargetInventory.PurchasePrice = SourceInventory.PurchasePrice AND
               TargetInventory.PurchaseDate = SourceInventory.PurchaseDate AND
               TargetInventory.ExpirationDate = SourceInventory.ExpirationDate AND
               TargetInventory.ParentInventoryUID = SourceInventory.ParentInventoryUID AND
               TargetInventory.AssetID = SourceInventory.AssetID AND
               TargetInventory.InventorySourceUID = SourceInventory.InventorySourceUID AND
               TargetInventory.ContainerUID = SourceInventory.ContainerUID
        WHERE
            TargetInventory.InventoryUID = 0
        AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
        AND TargetInventory.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match New Inventory to their Origin record ', 1;
            END

    END -- End Procedure