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
                @AllowStackingErrors    AS BIT;

        SET @CreateCustomFields = 0;
        SET @TargetDatabase = [dbo].[fn_GetTargetDatabaseName](@ProcessUid);
        SET @SourceTable = [dbo].[fn_GetSourceTable](@SourceProcess);
        
        --Check that Target Database is not null or empty
        IF @TargetDatabase IS NULL OR LEN(@TargetDatabase) = 0
            BEGIN
                ;
                THROW 50000, 'Target Database Name is empty.', 1;
            END;

        --Check that Source Table is not null or empty
        IF @SourceTable IS NULL OR LEN(@SourceTable) = 0
            BEGIN
                ;
                THROW 50000, 'Source Table could not be verified.', 1;
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

        SELECT UPPER(LTRIM(RTRIM(ConfigurationValue)))
        FROM [Configurations] 
        WHERE ConfigurationName = '' AND ProcessUid = @ProcessUid;

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

        --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta2UID, TargetCustomFields.InventoryExt2UID, SourceInventoryExt.InventoryExtUID, SourceInventoryExt.InventoryExtValue
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryExt1UID = SourceInventoryExt.InventoryExtUID
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

        --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta3UID, TargetCustomFields.InventoryExt3UID, SourceInventoryExt.InventoryExtUID, SourceInventoryExt.InventoryExtValue
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryExt1UID = SourceInventoryExt.InventoryExtUID
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

        --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta4UID, TargetCustomFields.InventoryExt4UID, SourceInventoryExt.InventoryExtUID, SourceInventoryExt.InventoryExtValue
        UPDATE TargetCustomFields SET TargetCustomFields.InventoryExt1UID = SourceInventoryExt.InventoryExtUID
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

        /*  IF CreateCustomFields is false 
         *      IF ItemTypeUid = 0 AND CustomLabel IS NOT NULL (or empty) reject rows
         *      IF ItemTypeUid > 0 AND MetaUID = 0 AND CustomLabel IS NOT NULL (or empty) reject rows
         */
        IF @CreateCustomFields = 0
            BEGIN
                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta1UID, TargetCustomFields.CustomField1Label
                UPDATE TargetCustomFields SET Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField1Label + '''; Custom Field ''' + CustomField1Label + ''' Cannot be created for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID = 0
                AND TargetCustomFields.CustomField1Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField1Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta2UID, TargetCustomFields.CustomField2Label
                UPDATE TargetCustomFields SET Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField2Label + '''; Custom Field ''' + CustomField2Label + ''' Cannot be created for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID = 0
                AND TargetCustomFields.CustomField2Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField2Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta3UID, TargetCustomFields.CustomField3Label
                UPDATE TargetCustomFields SET Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField3Label + '''; Custom Field ''' + CustomField3Label + ''' Cannot be created for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID = 0
                AND TargetCustomFields.CustomField3Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField3Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta4UID, TargetCustomFields.CustomField4Label
                UPDATE TargetCustomFields SET Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField4Label + '''; Custom Field ''' + CustomField4Label + ''' Cannot be created for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID = 0
                AND TargetCustomFields.CustomField4Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField4Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);


                --*  IF ItemTypeUID > 0 AND MetaUID = 0 AND CustomLabel IS NOT NULL (or empty) reject rows
                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta1UID, TargetCustomFields.CustomField1Label, TargetCustomFields.ProductTypeName
                UPDATE TargetCustomFields SET Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField1Label + '''; Custom Field ''' + CustomField1Label + ''' was not found for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID > 0
                AND TargetCustomFields.InventoryMeta1UID = 0
                AND TargetCustomFields.CustomField1Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField1Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta2UID, TargetCustomFields.CustomField2Label, TargetCustomFields.ProductTypeName
                UPDATE TargetCustomFields SET Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField2Label + '''; Custom Field ''' + CustomField2Label + ''' was not found for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID > 0
                AND TargetCustomFields.InventoryMeta2UID = 0
                AND TargetCustomFields.CustomField2Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField2Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta3UID, TargetCustomFields.CustomField3Label, TargetCustomFields.ProductTypeName
                UPDATE TargetCustomFields SET Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField3Label + '''; Custom Field ''' + CustomField3Label + ''' was not found for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID > 0
                AND TargetCustomFields.InventoryMeta3UID = 0
                AND TargetCustomFields.CustomField3Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField3Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

                --SELECT TargetCustomFields.InventoryUID, TargetCustomFields.InventoryMeta4UID, TargetCustomFields.CustomField4Label, TargetCustomFields.ProductTypeName
                UPDATE TargetCustomFields SET Rejected = 1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Custom Field ''' + CustomField4Label + '''; Custom Field ''' + CustomField4Label + ''' was not found for Product Type ''' + ProductTypeName + ''''
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetCustomFields
                WHERE
                    TargetCustomFields.ItemTypeUID > 0
                AND TargetCustomFields.InventoryMeta4UID = 0
                AND TargetCustomFields.CustomField4Label IS NOT NULL
                AND LTRIM(RTRIM(TargetCustomFields.CustomField4Label)) <> ''
                AND TargetCustomFields.ProcessTaskUID = @ProcessTaskUid
                AND (TargetCustomFields.Rejected = 0 OR @AllowStackingErrors = 1);

            END

    END --End Procedure