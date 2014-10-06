DECLARE @Label VARCHAR(50)
DECLARE @Type VARCHAR(50)

UPDATE _ETL_Inventory
SET ItemTypeUID = tblTechItems.ItemTypeUID
FROM _ETL_Inventory AS Tags
JOIN tblTechItems ON Tags.ItemUID = tblTechItems.ItemUID
WHERE Tags.ItemTypeUID IS NULL
OR (Tags.ItemTypeUID <> tblTechItems.ItemTypeUID)

UPDATE _ETL_Inventory
SET ItemTypeUID = 0
WHERE ItemTypeUID IS NULL

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 7 AND ETLSettingValue <> '') = 1
	BEGIN
		UPDATE _ETL_Inventory
		SET InventoryMeta1UID = tblTechInventoryMeta.InventoryMetaUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventoryMeta ON Tags.ItemTypeUID = tblTechInventoryMeta.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 1
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryMeta1UID IS NULL
		OR Tags.InventoryMeta1UID <> tblTechInventoryMeta.InventoryMetaUID)
		
		SELECT @Label = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 7
		SELECT @Type = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 8
		UPDATE tblTechInventoryMeta
		SET InventoryMetaLabel = @Label, InventoryMetaType = @Type
		FROM tblTechInventoryMeta
		JOIN _ETL_Inventory AS Tags ON tblTechInventoryMeta.ItemTypeUID = Tags.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 1
		WHERE Tags.ItemTypeUID > 0
		AND (tblTechInventoryMeta.InventoryMetaLabel <> @Label
		OR tblTechInventoryMeta.InventoryMetaType <> @Type)
		
		UPDATE _ETL_Inventory
		SET InventoryExt1UID = tblTechInventoryExt.InventoryExtUID
		FROM _ETL_Inventory as Tags
		JOIN tblTechInventoryExt ON Tags.InventoryMeta1UID = tblTechInventoryExt.InventoryMetaUID
		AND Tags.InventoryUID = tblTechInventoryExt.InventoryUID
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryExt1UID IS NULL
		OR Tags.InventoryExt1UID <> tblTechInventoryExt.InventoryExtUID)
		
		UPDATE tblTechInventoryExt
		SET InventoryExtValue = Tags.CustomField1
		FROM tblTechInventoryExt
		JOIN _ETL_Inventory AS Tags ON tblTechInventoryExt.InventoryMetaUID = Tags.InventoryMeta1UID
		AND tblTechInventoryExt.InventoryUID = Tags.InventoryUID
		WHERE Tags.ItemTypeUID > 0
		AND tblTechInventoryExt.InventoryExtValue <> Tags.CustomField1
		AND Tags.CustomField1 IS NOT NULL
		
		DELETE old
		FROM tblTechInventoryExt AS old
		JOIN _ETL_Inventory AS Tags ON old.InventoryUID = Tags.InventoryUID
		AND old.InventoryMetaUID = Tags.InventoryMeta1UID
		WHERE Tags.ItemTypeUID > 0
		AND old.InventoryExtUID <> Tags.InventoryExt1UID
		AND Tags.InventoryExt1UID IS NOT NULL
		
		DELETE old
		FROM tblTechInventoryMeta AS old
		JOIN _ETL_Inventory AS Tags ON old.ItemTypeUID = Tags.ItemTypeUID
		AND old.InventoryMetaOrder = 1
		WHERE Tags.ItemTypeUID > 0
		AND old.InventoryMetaUID <> Tags.InventoryMeta1UID
		AND Tags.InventoryMeta1UID IS NOT NULL
	END

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 9 AND ETLSettingValue <> '') = 1
	BEGIN
		UPDATE _ETL_Inventory
		SET InventoryMeta2UID = tblTechInventoryMeta.InventoryMetaUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventoryMeta ON Tags.ItemTypeUID = tblTechInventoryMeta.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 2
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryMeta2UID IS NULL
		OR Tags.InventoryMeta2UID <> tblTechInventoryMeta.InventoryMetaUID)
		
		SELECT @Label = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 9
		SELECT @Type = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 10
		UPDATE tblTechInventoryMeta
		SET InventoryMetaLabel = @Label, InventoryMetaType = @Type
		FROM tblTechInventoryMeta
		JOIN _ETL_Inventory AS Tags ON tblTechInventoryMeta.ItemTypeUID = Tags.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 2
		WHERE Tags.ItemTypeUID > 0
		AND (tblTechInventoryMeta.InventoryMetaLabel <> @Label
		OR tblTechInventoryMeta.InventoryMetaType <> @Type)
		
		UPDATE _ETL_Inventory
		SET InventoryExt2UID = tblTechInventoryExt.InventoryExtUID
		FROM _ETL_Inventory as Tags
		JOIN tblTechInventoryExt ON Tags.InventoryMeta2UID = tblTechInventoryExt.InventoryMetaUID
		AND Tags.InventoryUID = tblTechInventoryExt.InventoryUID
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryExt2UID IS NULL
		OR Tags.InventoryExt2UID <> tblTechInventoryExt.InventoryExtUID)
		
		UPDATE tblTechInventoryExt
		SET InventoryExtValue = Tags.CustomField2
		FROM tblTechInventoryExt
		JOIN _ETL_Inventory AS Tags ON tblTechInventoryExt.InventoryMetaUID = Tags.InventoryMeta2UID
		AND tblTechInventoryExt.InventoryUID = Tags.InventoryUID
		WHERE Tags.ItemTypeUID > 0
		AND tblTechInventoryExt.InventoryExtValue <> Tags.CustomField2
		AND Tags.CustomField2 IS NOT NULL
		
		DELETE old
		FROM tblTechInventoryExt AS old
		JOIN _ETL_Inventory AS Tags ON old.InventoryUID = Tags.InventoryUID
		AND old.InventoryMetaUID = Tags.InventoryMeta2UID
		WHERE Tags.ItemTypeUID > 0
		AND old.InventoryExtUID <> Tags.InventoryExt2UID
		AND Tags.InventoryExt2UID IS NOT NULL
		
		DELETE old
		FROM tblTechInventoryMeta AS old
		JOIN _ETL_Inventory AS Tags ON old.ItemTypeUID = Tags.ItemTypeUID
		AND old.InventoryMetaOrder = 2
		WHERE Tags.ItemTypeUID > 0
		AND old.InventoryMetaUID <> Tags.InventoryMeta2UID
		AND Tags.InventoryMeta2UID IS NOT NULL
	END

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 11 AND ETLSettingValue <> '') = 1
	BEGIN
		UPDATE _ETL_Inventory
		SET InventoryMeta3UID = tblTechInventoryMeta.InventoryMetaUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventoryMeta ON Tags.ItemTypeUID = tblTechInventoryMeta.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 3
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryMeta3UID IS NULL
		OR Tags.InventoryMeta3UID <> tblTechInventoryMeta.InventoryMetaUID)
		
		SELECT @Label = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 11
		SELECT @Type = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 12
		UPDATE tblTechInventoryMeta
		SET InventoryMetaLabel = @Label, InventoryMetaType = @Type
		FROM tblTechInventoryMeta
		JOIN _ETL_Inventory AS Tags ON tblTechInventoryMeta.ItemTypeUID = Tags.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 3
		WHERE Tags.ItemTypeUID > 0
		AND (tblTechInventoryMeta.InventoryMetaLabel <> @Label
		OR tblTechInventoryMeta.InventoryMetaType <> @Type)
		
		UPDATE _ETL_Inventory
		SET InventoryExt3UID = tblTechInventoryExt.InventoryExtUID
		FROM _ETL_Inventory as Tags
		JOIN tblTechInventoryExt ON Tags.InventoryMeta3UID = tblTechInventoryExt.InventoryMetaUID
		AND Tags.InventoryUID = tblTechInventoryExt.InventoryUID
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryExt3UID IS NULL
		OR Tags.InventoryExt3UID <> tblTechInventoryExt.InventoryExtUID)
		
		UPDATE tblTechInventoryExt
		SET InventoryExtValue = Tags.CustomField3
		FROM tblTechInventoryExt
		JOIN _ETL_Inventory AS Tags ON tblTechInventoryExt.InventoryMetaUID = Tags.InventoryMeta3UID
		AND tblTechInventoryExt.InventoryUID = Tags.InventoryUID
		WHERE Tags.ItemTypeUID > 0
		AND tblTechInventoryExt.InventoryExtValue <> Tags.CustomField3
		AND Tags.CustomField3 IS NOT NULL
		
		DELETE old
		FROM tblTechInventoryExt AS old
		JOIN _ETL_Inventory AS Tags ON old.InventoryUID = Tags.InventoryUID
		AND old.InventoryMetaUID = Tags.InventoryMeta3UID
		WHERE Tags.ItemTypeUID > 0
		AND old.InventoryExtUID <> Tags.InventoryExt3UID
		AND Tags.InventoryExt3UID IS NOT NULL
		
		DELETE old
		FROM tblTechInventoryMeta AS old
		JOIN _ETL_Inventory AS Tags ON old.ItemTypeUID = Tags.ItemTypeUID
		AND old.InventoryMetaOrder = 3
		WHERE Tags.ItemTypeUID > 0
		AND old.InventoryMetaUID <> Tags.InventoryMeta3UID
		AND Tags.InventoryMeta3UID IS NOT NULL
	END

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 13 AND ETLSettingValue <> '') = 1
	BEGIN
		UPDATE _ETL_Inventory
		SET InventoryMeta4UID = tblTechInventoryMeta.InventoryMetaUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventoryMeta ON Tags.ItemTypeUID = tblTechInventoryMeta.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 4
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryMeta4UID IS NULL
		OR Tags.InventoryMeta4UID <> tblTechInventoryMeta.InventoryMetaUID)
		
		SELECT @Label = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 13
		SELECT @Type = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 14
		UPDATE tblTechInventoryMeta
		SET InventoryMetaLabel = @Label, InventoryMetaType = @Type
		FROM tblTechInventoryMeta
		JOIN _ETL_Inventory AS Tags ON tblTechInventoryMeta.ItemTypeUID = Tags.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 4
		WHERE Tags.ItemTypeUID > 0
		AND (tblTechInventoryMeta.InventoryMetaLabel <> @Label
		OR tblTechInventoryMeta.InventoryMetaType <> @Type)
		
		UPDATE _ETL_Inventory
		SET InventoryExt4UID = tblTechInventoryExt.InventoryExtUID
		FROM _ETL_Inventory as Tags
		JOIN tblTechInventoryExt ON Tags.InventoryMeta4UID = tblTechInventoryExt.InventoryMetaUID
		AND Tags.InventoryUID = tblTechInventoryExt.InventoryUID
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryExt4UID IS NULL
		OR Tags.InventoryExt4UID <> tblTechInventoryExt.InventoryExtUID)
		
		UPDATE tblTechInventoryExt
		SET InventoryExtValue = Tags.CustomField4
		FROM tblTechInventoryExt
		JOIN _ETL_Inventory AS Tags ON tblTechInventoryExt.InventoryMetaUID = Tags.InventoryMeta4UID
		AND tblTechInventoryExt.InventoryUID = Tags.InventoryUID
		WHERE Tags.ItemTypeUID > 0
		AND tblTechInventoryExt.InventoryExtValue <> Tags.CustomField4
		AND Tags.CustomField4 IS NOT NULL
		
		DELETE old
		FROM tblTechInventoryExt AS old
		JOIN _ETL_Inventory AS Tags ON old.InventoryUID = Tags.InventoryUID
		AND old.InventoryMetaUID = Tags.InventoryMeta4UID
		WHERE Tags.ItemTypeUID > 0
		AND old.InventoryExtUID <> Tags.InventoryExt4UID
		AND Tags.InventoryExt4UID IS NOT NULL
		
		DELETE old
		FROM tblTechInventoryMeta AS old
		JOIN _ETL_Inventory AS Tags ON old.ItemTypeUID = Tags.ItemTypeUID
		AND old.InventoryMetaOrder = 4
		WHERE Tags.ItemTypeUID > 0
		AND old.InventoryMetaUID <> Tags.InventoryMeta4UID
		AND Tags.InventoryMeta4UID IS NOT NULL
	END
