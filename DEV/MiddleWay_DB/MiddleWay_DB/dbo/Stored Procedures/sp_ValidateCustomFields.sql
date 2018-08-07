/*
 *  sp_ValidateCustomFields
 *  Get the MetaUIDs that correspond to the Custom Field Labels of each item type
 *  Get the ExtUID of the custom field values for existing Inventory (if the Ext record exists)
 *  Reject Custom fields where the value is null or empty and the Custom Meta is required
 *  If Custom Fields cannot be created (for new Item Types) reject rows where the Custom Field Label
 *      is not empty or null and the Item Type was not found or the MetaUID of the field was not found
 */
CREATE PROCEDURE [dbo].[sp_ValidateCustomFields]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateCustomFields     AS BIT,
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @AllowStackingErrors    AS BIT,
                @ErrorCode              AS INT;

        SET NOCOUNT ON;

        SET @CreateCustomFields = 0;
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
            @CreateCustomFields = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'CreateCustomFields'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateCustomFields', 1;
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
         *  For rows that have a valid ItemTypeUID
         *  IF ItemTypeUid > 0 
         *      Get the Meta Fields for the ItemType
         *      Set the MetaUID for each of the Custom Labels (not null and not empty)
         *      IF InventoryUID > 0 get the ExtUID of the fields
         *      IF MetaUID > 0 AND SourceInventoryMeta.InventoryMetaRequired is true and CustomFieldValue IS NULL reject rows
         *      IF CreateCustomFields is false AND MetaUID = 0 AND CustomLabel IS NOT NULL (or empty) reject rows
         *  IF ItemTypeUid = 0 
         *      IF CreateCustomFields is false AND CustomLabel IS NOT NULL (or empty) reject rows
         */

         -- Set the MetaUID of the Custom Labels
         --SELECT TargetCustomFields.ItemTypeUID, TargetCustomFields.ProductTypeName, TargetCustomFields.CustomField1Label, TargetCustomFields.InventoryMeta1UID, SourceItemTypes.ItemTypeUID, SourceItemTypes.ItemTypeName, SourceInventoryMeta.InventoryMetaUID, SourceInventoryMeta.InventoryMetaLabel, SourceInventoryMeta.InventoryMetaOrder, SourceInventoryMeta.InventoryMetaRequired
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryMeta1UID = SourceInventoryMeta.InventoryMetaUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechItemTypes SourceItemTypes
            ON TargetCustomFields.ItemTypeUID = SourceItemTypes.ItemTypeUID
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryMeta SourceInventoryMeta
            ON SourceItemTypes.ItemTypeUID = SourceInventoryMeta.ItemTypeUID
        WHERE
            TargetCustomFields.ItemTypeUID > 0
        AND TargetCustomFields.CustomField1Label IS NOT NULL
        AND UPPER(LTRIM(RTRIM(TargetCustomFields.CustomField1Label))) = UPPER(LTRIM(RTRIM(SourceInventoryMeta.InventoryMetaLabel)))
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match MetaUID1 by Label', 1;
            END

        --SELECT TargetCustomFields.ItemTypeUID, TargetCustomFields.ProductTypeName, TargetCustomFields.CustomField2Label, TargetCustomFields.InventoryMeta2UID, SourceItemTypes.ItemTypeUID, SourceItemTypes.ItemTypeName, SourceInventoryMeta.InventoryMetaUID, SourceInventoryMeta.InventoryMetaLabel, SourceInventoryMeta.InventoryMetaOrder, SourceInventoryMeta.InventoryMetaRequired
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryMeta2UID = SourceInventoryMeta.InventoryMetaUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechItemTypes SourceItemTypes
            ON TargetCustomFields.ItemTypeUID = SourceItemTypes.ItemTypeUID
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryMeta SourceInventoryMeta
            ON SourceItemTypes.ItemTypeUID = SourceInventoryMeta.ItemTypeUID
        WHERE
            TargetCustomFields.ItemTypeUID > 0
        AND TargetCustomFields.CustomField2Label IS NOT NULL
        AND UPPER(LTRIM(RTRIM(TargetCustomFields.CustomField2Label))) = UPPER(LTRIM(RTRIM(SourceInventoryMeta.InventoryMetaLabel)))
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match MetaUID2 by Label', 1;
            END

        --SELECT TargetCustomFields.ItemTypeUID, TargetCustomFields.ProductTypeName, TargetCustomFields.CustomField3Label, TargetCustomFields.InventoryMeta3UID, SourceItemTypes.ItemTypeUID, SourceItemTypes.ItemTypeName, SourceInventoryMeta.InventoryMetaUID, SourceInventoryMeta.InventoryMetaLabel, SourceInventoryMeta.InventoryMetaOrder, SourceInventoryMeta.InventoryMetaRequired
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryMeta3UID = SourceInventoryMeta.InventoryMetaUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechItemTypes SourceItemTypes
            ON TargetCustomFields.ItemTypeUID = SourceItemTypes.ItemTypeUID
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryMeta SourceInventoryMeta
            ON SourceItemTypes.ItemTypeUID = SourceInventoryMeta.ItemTypeUID
        WHERE
            TargetCustomFields.ItemTypeUID > 0
        AND TargetCustomFields.CustomField3Label IS NOT NULL
        AND UPPER(LTRIM(RTRIM(TargetCustomFields.CustomField3Label))) = UPPER(LTRIM(RTRIM(SourceInventoryMeta.InventoryMetaLabel)))
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match MetaUID3 by Label', 1;
            END

        --SELECT TargetCustomFields.ItemTypeUID, TargetCustomFields.ProductTypeName, TargetCustomFields.CustomField4Label, TargetCustomFields.InventoryMeta4UID, SourceItemTypes.ItemTypeUID, SourceItemTypes.ItemTypeName, SourceInventoryMeta.InventoryMetaUID, SourceInventoryMeta.InventoryMetaLabel, SourceInventoryMeta.InventoryMetaOrder, SourceInventoryMeta.InventoryMetaRequired
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryMeta4UID = SourceInventoryMeta.InventoryMetaUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechItemTypes SourceItemTypes
            ON TargetCustomFields.ItemTypeUID = SourceItemTypes.ItemTypeUID
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryMeta SourceInventoryMeta
            ON SourceItemTypes.ItemTypeUID = SourceInventoryMeta.ItemTypeUID
        WHERE
            TargetCustomFields.ItemTypeUID > 0
        AND TargetCustomFields.CustomField4Label IS NOT NULL
        AND UPPER(LTRIM(RTRIM(TargetCustomFields.CustomField4Label))) = UPPER(LTRIM(RTRIM(SourceInventoryMeta.InventoryMetaLabel)))
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match MetaUID4 by Label', 1;
            END

        -- Get the ExtUID (if any) of existing Assets
        --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta1UID, TargetCustomFields.InventoryExt1UID, SourceInventoryExt.InventoryExtUID, SourceInventoryExt.InventoryExtValue
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryExt1UID = SourceInventoryExt.InventoryExtUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryExt SourceInventoryExt
            ON TargetCustomFields.InventoryUID = SourceInventoryExt.InventoryUID
            AND TargetCustomFields.InventoryMeta1UID = SourceInventoryExt.InventoryMetaUID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.ItemTypeUID > 0
        AND TargetCustomFields.InventoryMeta1UID > 0
        AND SourceInventoryExt.InventoryExtUID IS NOT NULL
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match ExtUID1 by Inventory and MetaUID', 1;
            END

        --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta2UID, TargetCustomFields.InventoryExt2UID, SourceInventoryExt.InventoryExtUID, SourceInventoryExt.InventoryExtValue
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryExt2UID = SourceInventoryExt.InventoryExtUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryExt SourceInventoryExt
            ON TargetCustomFields.InventoryUID = SourceInventoryExt.InventoryUID
            AND TargetCustomFields.InventoryMeta2UID = SourceInventoryExt.InventoryMetaUID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.ItemTypeUID > 0
        AND TargetCustomFields.InventoryMeta2UID > 0
        AND SourceInventoryExt.InventoryExtUID IS NOT NULL
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match ExtUID2 by Inventory and MetaUID', 1;
            END

        --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta3UID, TargetCustomFields.InventoryExt3UID, SourceInventoryExt.InventoryExtUID, SourceInventoryExt.InventoryExtValue
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryExt3UID = SourceInventoryExt.InventoryExtUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryExt SourceInventoryExt
            ON TargetCustomFields.InventoryUID = SourceInventoryExt.InventoryUID
            AND TargetCustomFields.InventoryMeta3UID = SourceInventoryExt.InventoryMetaUID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.ItemTypeUID > 0
        AND TargetCustomFields.InventoryMeta3UID > 0
        AND SourceInventoryExt.InventoryExtUID IS NOT NULL
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match ExtUID3 by Inventory and MetaUID', 1;
            END

        --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta4UID, TargetCustomFields.InventoryExt4UID, SourceInventoryExt.InventoryExtUID, SourceInventoryExt.InventoryExtValue
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryExt4UID = SourceInventoryExt.InventoryExtUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryExt SourceInventoryExt
            ON TargetCustomFields.InventoryUID = SourceInventoryExt.InventoryUID
            AND TargetCustomFields.InventoryMeta4UID = SourceInventoryExt.InventoryMetaUID
        WHERE
            TargetCustomFields.InventoryUID > 0
        AND TargetCustomFields.ItemTypeUID > 0
        AND TargetCustomFields.InventoryMeta4UID > 0
        AND SourceInventoryExt.InventoryExtUID IS NOT NULL
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match ExtUID4 by Inventory and MetaUID', 1;
            END

        --IF MetaUID > 0 AND SourceInventoryMeta.InventoryMetaRequired is true and CustomFieldValue IS NULL OR Empty reject rows
        --SELECT TargetCustomFields.ItemTypeUID, TargetCustomFields.ProductTypeName, TargetCustomFields.InventoryMeta1UID, TargetCustomFields.CustomField1Label, TargetCustomFields.CustomField1Value, SourceInventoryMeta.InventoryMetaUID, SourceInventoryMeta.InventoryMetaRequired
        UPDATE TargetCustomFields SET Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField1Label + '''; The Value for ' + CustomField1Label + ' is NULL or Empty'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryMeta SourceInventoryMeta
            ON TargetCustomFields.ItemTypeUID = SourceInventoryMeta.ItemTypeUID
            AND TargetCustomFields.InventoryMeta1UID = SourceInventoryMeta.InventoryMetaUID
        WHERE
            TargetCustomFields.ItemTypeUID > 0
        AND TargetCustomFields.InventoryMeta1UID > 0
        AND SourceInventoryMeta.InventoryMetaRequired = 1
        AND (TargetCustomFields.CustomField1Value IS NULL OR
                LTRIM(RTRIM(TargetCustomFields.CustomField1Value)) = '')
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the Meta1 Value is required but Empty', 1;
            END

        --SELECT TargetCustomFields.ItemTypeUID, TargetCustomFields.ProductTypeName, TargetCustomFields.InventoryMeta2UID, TargetCustomFields.CustomField2Label, TargetCustomFields.CustomField2Value, SourceInventoryMeta.InventoryMetaUID, SourceInventoryMeta.InventoryMetaRequired
        UPDATE TargetCustomFields SET Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField2Label + '''; The Value for ' + CustomField2Label + ' is NULL or Empty'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryMeta SourceInventoryMeta
            ON TargetCustomFields.ItemTypeUID = SourceInventoryMeta.ItemTypeUID
            AND TargetCustomFields.InventoryMeta2UID = SourceInventoryMeta.InventoryMetaUID
        WHERE
            TargetCustomFields.ItemTypeUID > 0
        AND TargetCustomFields.InventoryMeta2UID > 0
        AND SourceInventoryMeta.InventoryMetaRequired = 1
        AND (TargetCustomFields.CustomField2Value IS NULL OR
                LTRIM(RTRIM(TargetCustomFields.CustomField2Value)) = '')
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the Meta2 Value is required but Empty', 1;
            END

        --SELECT TargetCustomFields.ItemTypeUID, TargetCustomFields.ProductTypeName, TargetCustomFields.InventoryMeta3UID, TargetCustomFields.CustomField3Label, TargetCustomFields.CustomField3Value, SourceInventoryMeta.InventoryMetaUID, SourceInventoryMeta.InventoryMetaRequired
        UPDATE TargetCustomFields SET Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField3Label + '''; The Value for ' + CustomField3Label + ' is NULL or Empty'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryMeta SourceInventoryMeta
            ON TargetCustomFields.ItemTypeUID = SourceInventoryMeta.ItemTypeUID
            AND TargetCustomFields.InventoryMeta3UID = SourceInventoryMeta.InventoryMetaUID
        WHERE
            TargetCustomFields.ItemTypeUID > 0
        AND TargetCustomFields.InventoryMeta3UID > 0
        AND SourceInventoryMeta.InventoryMetaRequired = 1
        AND (TargetCustomFields.CustomField3Value IS NULL OR
                LTRIM(RTRIM(TargetCustomFields.CustomField3Value)) = '')
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the Meta3 Value is required but Empty', 1;
            END

        --SELECT TargetCustomFields.ItemTypeUID, TargetCustomFields.ProductTypeName, TargetCustomFields.InventoryMeta4UID, TargetCustomFields.CustomField4Label, TargetCustomFields.CustomField4Value, SourceInventoryMeta.InventoryMetaUID, SourceInventoryMeta.InventoryMetaRequired
        UPDATE TargetCustomFields SET Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField4Label + '''; The Value for ' + CustomField4Label + ' is NULL or Empty'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventoryMeta SourceInventoryMeta
            ON TargetCustomFields.ItemTypeUID = SourceInventoryMeta.ItemTypeUID
            AND TargetCustomFields.InventoryMeta4UID = SourceInventoryMeta.InventoryMetaUID
        WHERE
            TargetCustomFields.ItemTypeUID > 0
        AND TargetCustomFields.InventoryMeta4UID > 0
        AND SourceInventoryMeta.InventoryMetaRequired = 1
        AND (TargetCustomFields.CustomField4Value IS NULL OR
                LTRIM(RTRIM(TargetCustomFields.CustomField4Value)) = '')
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the Meta4 Value is required but Empty', 1;
            END


        --Set the MetaUID of all Assets to zero where the Label is not null and the MetaUID is NULL
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryMeta1UID = 0
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        WHERE
            TargetCustomFields.InventoryMeta1UID IS NULL
        AND TargetCustomFields.CustomField1Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField1Label)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to set Meta1UID to zero', 1;
            END

        UPDATE TargetCustomFields SET TargetCustomFields.InventoryMeta2UID = 0
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        WHERE
            TargetCustomFields.InventoryMeta2UID IS NULL
        AND TargetCustomFields.CustomField2Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField2Label)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to set Meta2UID to zero', 1;
            END

        UPDATE TargetCustomFields SET TargetCustomFields.InventoryMeta3UID = 0
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        WHERE
            TargetCustomFields.InventoryMeta3UID IS NULL
        AND TargetCustomFields.CustomField3Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField3Label)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to set Meta3UID to zero', 1;
            END

        UPDATE TargetCustomFields SET TargetCustomFields.InventoryMeta4UID = 0
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        WHERE
            TargetCustomFields.InventoryMeta4UID IS NULL
        AND TargetCustomFields.CustomField4Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField4Label)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to set Meta4UID to zero', 1;
            END


        -- Set the ExtUID of unrejected existing Assets to 0 where the label is not null
        --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta1UID, TargetCustomFields.InventoryExt1UID, SourceInventoryExt.InventoryExtUID, SourceInventoryExt.InventoryExtValue
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryExt1UID = 0
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        WHERE
            TargetCustomFields.InventoryExt1UID IS NULL
        AND TargetCustomFields.CustomField1Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField1Label)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to set the Ext1UID to zero', 1;
            END

        UPDATE TargetCustomFields SET TargetCustomFields.InventoryExt2UID = 0
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        WHERE
            TargetCustomFields.InventoryExt2UID IS NULL
        AND TargetCustomFields.CustomField2Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField2Label)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to set the Ext2UID to zero', 1;
            END

        UPDATE TargetCustomFields SET TargetCustomFields.InventoryExt3UID = 0
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        WHERE
            TargetCustomFields.InventoryExt3UID IS NULL
        AND TargetCustomFields.CustomField3Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField3Label)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to set the Ext3UID to zero', 1;
            END

        UPDATE TargetCustomFields SET TargetCustomFields.InventoryExt4UID = 0
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
        WHERE
            TargetCustomFields.InventoryExt4UID IS NULL
        AND TargetCustomFields.CustomField4Label IS NOT NULL
        AND LTRIM(RTRIM(TargetCustomFields.CustomField4Label)) <> ''
        AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
        AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to set the Ext4UID to zero', 1;
            END


        /*  IF CreateCustomFields is false 
         *      IF ItemTypeUid = 0 AND CustomLabel IS NOT NULL (or empty) reject rows
         *      IF ItemTypeUid > 0 AND MetaUID = 0 AND CustomLabel IS NOT NULL (or empty) reject rows
         */
        IF @CreateCustomFields = 0
            BEGIN
                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta1UID, TargetCustomFields.CustomField1Label
                UPDATE TargetCustomFields SET InventoryMeta1UID = -1, Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField1Label + '''; Custom Field ''' + CustomField1Label + ''' Cannot be created for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID = 0
                AND TargetCustomFields.CustomField1Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField1Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where CustomField1 cannot be created', 1;
                    END

                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta2UID, TargetCustomFields.CustomField2Label
                UPDATE TargetCustomFields SET InventoryMeta2UID = -1, Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField2Label + '''; Custom Field ''' + CustomField2Label + ''' Cannot be created for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID = 0
                AND TargetCustomFields.CustomField2Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField2Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where CustomField2 cannot be created', 1;
                    END

                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta3UID, TargetCustomFields.CustomField3Label
                UPDATE TargetCustomFields SET InventoryMeta3UID = -1, Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField3Label + '''; Custom Field ''' + CustomField3Label + ''' Cannot be created for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID = 0
                AND TargetCustomFields.CustomField3Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField3Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where CustomField3 cannot be created', 1;
                    END

                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta4UID, TargetCustomFields.CustomField4Label
                UPDATE TargetCustomFields SET InventoryMeta4UID = -1, Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField4Label + '''; Custom Field ''' + CustomField4Label + ''' Cannot be created for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID = 0
                AND TargetCustomFields.CustomField4Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField4Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where CustomField4 cannot be created', 1;
                    END

                --*  IF ItemTypeUID > 0 AND MetaUID = 0 AND CustomLabel IS NOT NULL (or empty) reject rows
                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta1UID, TargetCustomFields.CustomField1Label, TargetCustomFields.ProductTypeName
                UPDATE TargetCustomFields SET InventoryExt1UID = -1, Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField1Label + '''; Custom Field ''' + CustomField1Label + ''' was not found for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID > 0
                AND TargetCustomFields.InventoryMeta1UID = 0
                AND TargetCustomFields.CustomField1Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField1Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where CustomField1 could not be found', 1;
                    END

                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta2UID, TargetCustomFields.CustomField2Label, TargetCustomFields.ProductTypeName
                UPDATE TargetCustomFields SET InventoryExt2UID = -1, Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField2Label + '''; Custom Field ''' + CustomField2Label + ''' was not found for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID > 0
                AND TargetCustomFields.InventoryMeta2UID = 0
                AND TargetCustomFields.CustomField2Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField2Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where CustomField2 could not be found', 1;
                    END

                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta3UID, TargetCustomFields.CustomField3Label, TargetCustomFields.ProductTypeName
                UPDATE TargetCustomFields SET InventoryExt3UID = -1, Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField3Label + '''; Custom Field ''' + CustomField3Label + ''' was not found for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID > 0
                AND TargetCustomFields.InventoryMeta3UID = 0
                AND TargetCustomFields.CustomField3Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField3Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where CustomField3 could not be found', 1;
                    END

                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta4UID, TargetCustomFields.CustomField4Label, TargetCustomFields.ProductTypeName
                UPDATE TargetCustomFields SET InventoryExt4UID = -1, Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField4Label + '''; Custom Field ''' + CustomField4Label + ''' was not found for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID > 0
                AND TargetCustomFields.InventoryMeta4UID = 0
                AND TargetCustomFields.CustomField4Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField4Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where CustomField4 could not be found', 1;
                    END

            END

        SET NOCOUNT OFF;

        RETURN 0;

    END --End Procedure