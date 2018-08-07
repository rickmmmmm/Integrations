/*
 *  sp_AddUpdate_InventoryExt
 *  
 *  
 */
CREATE PROCEDURE [dbo].[sp_AddUpdate_InventoryExt]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @ErrorCode              AS INT;

        SET NOCOUNT ON;

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

        --Update Existing Ext values
        UPDATE SourceInventoryExt
        SET SourceInventoryExt.InventoryExtValue = TargetCustomFields.CustomField1Value
        FROM
            TipWebHostedChicagoPS.dbo.tblTechInventoryExt SourceInventoryExt
        LEFT JOIN
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
            ON SourceInventoryExt.InventoryExtUID = TargetCustomFields.InventoryExt1UID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt1UID > 0
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND TargetCustomFields.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to update the Ext1Value of the Source Inventory', 1;
            END
            
        UPDATE SourceInventoryExt
        SET SourceInventoryExt.InventoryExtValue = TargetCustomFields.CustomField2Value
        FROM
            TipWebHostedChicagoPS.dbo.tblTechInventoryExt SourceInventoryExt
        LEFT JOIN
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
            ON SourceInventoryExt.InventoryExtUID = TargetCustomFields.InventoryExt2UID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt2UID > 0
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND TargetCustomFields.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to update the Ext2Value of the Source Inventory', 1;
            END

        UPDATE SourceInventoryExt
        SET SourceInventoryExt.InventoryExtValue = TargetCustomFields.CustomField3Value
        FROM
            TipWebHostedChicagoPS.dbo.tblTechInventoryExt SourceInventoryExt
        LEFT JOIN
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
            ON SourceInventoryExt.InventoryExtUID = TargetCustomFields.InventoryExt3UID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt3UID > 0
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND TargetCustomFields.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to update the Ext3Value of the Source Inventory', 1;
            END

        UPDATE SourceInventoryExt
        SET SourceInventoryExt.InventoryExtValue = TargetCustomFields.CustomField4Value
        FROM
            TipWebHostedChicagoPS.dbo.tblTechInventoryExt SourceInventoryExt
        LEFT JOIN
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
            ON SourceInventoryExt.InventoryExtUID = TargetCustomFields.InventoryExt4UID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt4UID > 0
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND TargetCustomFields.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to update the Ext4Value of the Source Inventory', 1;
            END

        --Insert new Ext values for existing MetaUID records
        INSERT INTO TipWebHostedChicagoPS.dbo.tblTechInventoryExt --SourceInventoryExt
            (InventoryUID, InventoryMetaUID, InventoryExtValue)
        SELECT
            TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta1UID, TargetCustomFields.CustomField1Value
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt1UID = 0
        AND TargetCustomFields.InventoryMeta1UID > 0
        AND TargetCustomFields.CustomField1Value IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField1Value)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND TargetCustomFields.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to create new Inventory Ext1 record', 1;
            END

        --Match new Ext records to the original record
        UPDATE TargetCustomFields
        SET TargetCustomFields.InventoryExt1UID = SourceInventoryExt.InventoryExtUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryExt SourceInventoryExt
            ON TargetCustomFields.InventoryUID = SourceInventoryExt.InventoryUID
            AND TargetCustomFields.InventoryMeta1UID = SourceInventoryExt.InventoryMetaUID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt1UID = 0
        AND TargetCustomFields.InventoryMeta1UID > 0
        AND TargetCustomFields.CustomField1Value IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField1Value)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND TargetCustomFields.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match created Inventory Ext1 with Origin record', 1;
            END

        INSERT INTO TipWebHostedChicagoPS.dbo.tblTechInventoryExt --SourceInventoryExt
            (InventoryUID, InventoryMetaUID, InventoryExtValue)
        SELECT
            TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta2UID, TargetCustomFields.CustomField2Value
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt2UID = 0
        AND TargetCustomFields.InventoryMeta2UID > 0
        AND TargetCustomFields.CustomField2Value IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField2Value)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND TargetCustomFields.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to create new Inventory Ext2 record', 1;
            END

        --Match new Ext records to the original record
        UPDATE TargetCustomFields
        SET TargetCustomFields.InventoryExt2UID = SourceInventoryExt.InventoryExtUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryExt SourceInventoryExt
            ON TargetCustomFields.InventoryUID = SourceInventoryExt.InventoryUID
            AND TargetCustomFields.InventoryMeta2UID = SourceInventoryExt.InventoryMetaUID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt2UID = 0
        AND TargetCustomFields.InventoryMeta2UID > 0
        AND TargetCustomFields.CustomField2Value IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField2Value)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND TargetCustomFields.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match created Inventory Ext2 with Origin record', 1;
            END

        INSERT INTO TipWebHostedChicagoPS.dbo.tblTechInventoryExt --SourceInventoryExt
            (InventoryUID, InventoryMetaUID, InventoryExtValue)
        SELECT
            TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta3UID, TargetCustomFields.CustomField3Value
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt3UID = 0
        AND TargetCustomFields.InventoryMeta3UID > 0
        AND TargetCustomFields.CustomField3Value IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField3Value)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND TargetCustomFields.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to create new Inventory Ext3 record', 1;
            END

        --Match new Ext records to the original record
        UPDATE TargetCustomFields
        SET TargetCustomFields.InventoryExt3UID = SourceInventoryExt.InventoryExtUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryExt SourceInventoryExt
            ON TargetCustomFields.InventoryUID = SourceInventoryExt.InventoryUID
            AND TargetCustomFields.InventoryMeta3UID = SourceInventoryExt.InventoryMetaUID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt3UID = 0
        AND TargetCustomFields.InventoryMeta3UID > 0
        AND TargetCustomFields.CustomField3Value IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField3Value)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND TargetCustomFields.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match created Inventory Ext3 with Origin record', 1;
            END

        INSERT INTO TipWebHostedChicagoPS.dbo.tblTechInventoryExt --SourceInventoryExt
            (InventoryUID, InventoryMetaUID, InventoryExtValue)
        SELECT
            TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta4UID, TargetCustomFields.CustomField4Value
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt4UID = 0
        AND TargetCustomFields.InventoryMeta4UID > 0
        AND TargetCustomFields.CustomField4Value IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField4Value)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND TargetCustomFields.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to create new Inventory Ext4 record', 1;
            END

        --Match new Ext records to the original record
        UPDATE TargetCustomFields
        SET TargetCustomFields.InventoryExt4UID = SourceInventoryExt.InventoryExtUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryExt SourceInventoryExt
            ON TargetCustomFields.InventoryUID = SourceInventoryExt.InventoryUID
            AND TargetCustomFields.InventoryMeta4UID = SourceInventoryExt.InventoryMetaUID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt4UID = 0
        AND TargetCustomFields.InventoryMeta4UID > 0
        AND TargetCustomFields.CustomField4Value IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField4Value)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND TargetCustomFields.Rejected = 0;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match created Inventory Ext4 with Origin record', 1;
            END

    END -- End Procedure