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
                @ErrorCode              AS INT,
                @Statement              AS VARCHAR(MAX);

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
        SET @Statement = '
        UPDATE SourceInventoryExt
        SET SourceInventoryExt.InventoryExtValue = ISNULL(TargetCustomFields.CustomField1Value, '''')
        FROM
            ' + @TargetDatabase + '.dbo.tblTechInventoryExt SourceInventoryExt
        LEFT JOIN
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetCustomFields
            ON SourceInventoryExt.InventoryExtUID = TargetCustomFields.InventoryExt1UID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt1UID > 0
        AND TargetCustomFields.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetCustomFields.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        PRINT N'Updated Ext1Value: ' + CAST(@@ROWCOUNT AS VARCHAR(100));

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to update the Ext1Value of the Source Inventory', 1;
            END

        SET @Statement = '
        UPDATE SourceInventoryExt
        SET SourceInventoryExt.InventoryExtValue = ISNULL(TargetCustomFields.CustomField2Value, '''')
        FROM
            ' + @TargetDatabase + '.dbo.tblTechInventoryExt SourceInventoryExt
        LEFT JOIN
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetCustomFields
            ON SourceInventoryExt.InventoryExtUID = TargetCustomFields.InventoryExt2UID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt2UID > 0
        AND TargetCustomFields.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetCustomFields.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        PRINT N'Updated Ext2Value: ' + CAST(@@ROWCOUNT AS VARCHAR(100));

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to update the Ext2Value of the Source Inventory', 1;
            END

        SET @Statement = '
        UPDATE SourceInventoryExt
        SET SourceInventoryExt.InventoryExtValue = ISNULL(TargetCustomFields.CustomField3Value, '''')
        FROM
            ' + @TargetDatabase + '.dbo.tblTechInventoryExt SourceInventoryExt
        LEFT JOIN
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetCustomFields
            ON SourceInventoryExt.InventoryExtUID = TargetCustomFields.InventoryExt3UID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt3UID > 0
        AND TargetCustomFields.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetCustomFields.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        PRINT N'Updated Ext3Value: ' + CAST(@@ROWCOUNT AS VARCHAR(100));

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to update the Ext3Value of the Source Inventory', 1;
            END

        SET @Statement = '
        UPDATE SourceInventoryExt
        SET SourceInventoryExt.InventoryExtValue = ISNULL(TargetCustomFields.CustomField4Value, '''')
        FROM
            ' + @TargetDatabase + '.dbo.tblTechInventoryExt SourceInventoryExt
        LEFT JOIN
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetCustomFields
            ON SourceInventoryExt.InventoryExtUID = TargetCustomFields.InventoryExt4UID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt4UID > 0
        AND TargetCustomFields.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetCustomFields.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        PRINT N'Updated Ext4Value: ' + CAST(@@ROWCOUNT AS VARCHAR(100));

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to update the Ext4Value of the Source Inventory', 1;
            END

        --Insert new Ext values for existing MetaUID records
        SET @Statement = '
        INSERT INTO ' + @TargetDatabase + '.dbo.tblTechInventoryExt --SourceInventoryExt
            (InventoryUID, InventoryMetaUID, InventoryExtValue)
        SELECT
            TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta1UID, ISNULL(TargetCustomFields.CustomField1Value, '''')
        FROM
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetCustomFields
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt1UID = 0
        AND TargetCustomFields.InventoryMeta1UID > 0
        AND TargetCustomFields.CustomField1Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField1Label)) <> ''''
        AND TargetCustomFields.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetCustomFields.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        PRINT N'Inserted new InventoryExt1 Fields: ' + CAST(@@ROWCOUNT AS VARCHAR(100));

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to create new Inventory Ext1 record', 1;
            END

        --Match new Ext records to the original record
        SET @Statement = '
        UPDATE TargetCustomFields
        SET TargetCustomFields.InventoryExt1UID = SourceInventoryExt.InventoryExtUID
        FROM
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetCustomFields
        INNER JOIN
            ' + @TargetDatabase + '.dbo.tblTechInventoryExt SourceInventoryExt
            ON TargetCustomFields.InventoryUID = SourceInventoryExt.InventoryUID
            AND TargetCustomFields.InventoryMeta1UID = SourceInventoryExt.InventoryMetaUID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt1UID = 0
        AND TargetCustomFields.InventoryMeta1UID > 0
        AND TargetCustomFields.CustomField1Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField1Label)) <> ''''
        AND TargetCustomFields.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetCustomFields.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        PRINT N'Matched InventoryExt1 records: ' + CAST(@@ROWCOUNT AS VARCHAR(100));

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match created Inventory Ext1 with Origin record', 1;
            END

        SET @Statement = '
        INSERT INTO ' + @TargetDatabase + '.dbo.tblTechInventoryExt --SourceInventoryExt
            (InventoryUID, InventoryMetaUID, InventoryExtValue)
        SELECT
            TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta2UID, ISNULL(TargetCustomFields.CustomField2Value, '''')
        FROM
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetCustomFields
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt2UID = 0
        AND TargetCustomFields.InventoryMeta2UID > 0
        AND TargetCustomFields.CustomField2Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField2Label)) <> ''''
        AND TargetCustomFields.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetCustomFields.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        PRINT N'Inserted new InventoryExt2 Fields: ' + CAST(@@ROWCOUNT AS VARCHAR(100));

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to create new Inventory Ext2 record', 1;
            END

        --Match new Ext records to the original record
        SET @Statement = '
        UPDATE TargetCustomFields
        SET TargetCustomFields.InventoryExt2UID = SourceInventoryExt.InventoryExtUID
        FROM
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetCustomFields
        INNER JOIN
            ' + @TargetDatabase + '.dbo.tblTechInventoryExt SourceInventoryExt
            ON TargetCustomFields.InventoryUID = SourceInventoryExt.InventoryUID
            AND TargetCustomFields.InventoryMeta2UID = SourceInventoryExt.InventoryMetaUID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt2UID = 0
        AND TargetCustomFields.InventoryMeta2UID > 0
        AND TargetCustomFields.CustomField2Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField2Label)) <> ''''
        AND TargetCustomFields.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetCustomFields.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        PRINT N'Matched InventoryExt2 records: ' + CAST(@@ROWCOUNT AS VARCHAR(100));

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match created Inventory Ext2 with Origin record', 1;
            END

        SET @Statement = '
        INSERT INTO ' + @TargetDatabase + '.dbo.tblTechInventoryExt --SourceInventoryExt
            (InventoryUID, InventoryMetaUID, InventoryExtValue)
        SELECT
            TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta3UID, ISNULL(TargetCustomFields.CustomField3Value, '''')
        FROM
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetCustomFields
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt3UID = 0
        AND TargetCustomFields.InventoryMeta3UID > 0
        AND TargetCustomFields.CustomField3Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField3Label)) <> ''''
        AND TargetCustomFields.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetCustomFields.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        PRINT N'Inserted new InventoryExt3 Fields: ' + CAST(@@ROWCOUNT AS VARCHAR(100));

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to create new Inventory Ext3 record', 1;
            END

        --Match new Ext records to the original record
        SET @Statement = '
        UPDATE TargetCustomFields
        SET TargetCustomFields.InventoryExt3UID = SourceInventoryExt.InventoryExtUID
        FROM
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetCustomFields
        INNER JOIN
            ' + @TargetDatabase + '.dbo.tblTechInventoryExt SourceInventoryExt
            ON TargetCustomFields.InventoryUID = SourceInventoryExt.InventoryUID
            AND TargetCustomFields.InventoryMeta3UID = SourceInventoryExt.InventoryMetaUID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt3UID = 0
        AND TargetCustomFields.InventoryMeta3UID > 0
        AND TargetCustomFields.CustomField3Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField3Label)) <> ''''
        AND TargetCustomFields.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetCustomFields.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        PRINT N'Matched InventoryExt3 records: ' + CAST(@@ROWCOUNT AS VARCHAR(100));

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match created Inventory Ext3 with Origin record', 1;
            END

        SET @Statement = '
        INSERT INTO ' + @TargetDatabase + '.dbo.tblTechInventoryExt --SourceInventoryExt
            (InventoryUID, InventoryMetaUID, InventoryExtValue)
        SELECT
            TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta4UID, ISNULL(TargetCustomFields.CustomField4Value, '''')
        FROM
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetCustomFields
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt4UID = 0
        AND TargetCustomFields.InventoryMeta4UID > 0
        AND TargetCustomFields.CustomField4Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField4Label)) <> ''''
        AND TargetCustomFields.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetCustomFields.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        PRINT N'Inserted new InventoryExt4 Fields: ' + CAST(@@ROWCOUNT AS VARCHAR(100));

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to create new Inventory Ext4 record', 1;
            END

        --Match new Ext records to the original record
        SET @Statement = '
        UPDATE TargetCustomFields
        SET TargetCustomFields.InventoryExt4UID = SourceInventoryExt.InventoryExtUID
        FROM
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetCustomFields
        INNER JOIN
            ' + @TargetDatabase + '.dbo.tblTechInventoryExt SourceInventoryExt
            ON TargetCustomFields.InventoryUID = SourceInventoryExt.InventoryUID
            AND TargetCustomFields.InventoryMeta4UID = SourceInventoryExt.InventoryMetaUID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.InventoryExt4UID = 0
        AND TargetCustomFields.InventoryMeta4UID > 0
        AND TargetCustomFields.CustomField4Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField4Label)) <> ''''
        AND TargetCustomFields.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetCustomFields.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        PRINT N'Matched InventoryExt4 records: ' + CAST(@@ROWCOUNT AS VARCHAR(100));

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match created Inventory Ext4 with Origin record', 1;
            END

    END -- End Procedure