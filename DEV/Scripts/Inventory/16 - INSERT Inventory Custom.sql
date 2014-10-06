DECLARE @Label VARCHAR(50)
DECLARE @Type VARCHAR(50)

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 7 AND ETLSettingValue <> '') = 1
	BEGIN
		SELECT @Label = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 7
		SELECT @Type = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 8
		INSERT INTO tblTechInventoryMeta (ItemTypeUID, InventoryMetaLabel,
		InventoryMetaType, InventoryMetaRequired, InventoryMetaOrder)
		SELECT Tags.ItemTypeUID, @Label, @Type, 0, 1
		FROM _ETL_Inventory AS Tags
		LEFT JOIN tblTechInventoryMeta ON Tags.ItemTypeUID = tblTechInventoryMeta.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 1
		WHERE Tags.ItemTypeUID > 0
		AND tblTechInventoryMeta.InventoryMetaUID IS NULL
		GROUP BY Tags.ItemTypeUID
		
		UPDATE _ETL_Inventory
		SET InventoryMeta1UID = tblTechInventoryMeta.InventoryMetaUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventoryMeta ON Tags.ItemTypeUID = tblTechInventoryMeta.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 1
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryMeta1UID IS NULL
		OR Tags.InventoryMeta1UID <> tblTechInventoryMeta.InventoryMetaUID)
		
		UPDATE _ETL_Inventory
		SET InventoryMeta1UID = 0
		WHERE InventoryMeta1UID IS NULL
		
		INSERT INTO tblTechInventoryExt (InventoryUID, InventoryMetaUID, InventoryExtValue)
		SELECT Tags.InventoryUID, Tags.InventoryMeta1UID, Tags.CustomField1
		FROM _ETL_Inventory AS Tags
		LEFT JOIN tblTechInventoryExt ON Tags.InventoryUID = tblTechInventoryExt.InventoryUID
		AND Tags.InventoryMeta1UID = tblTechInventoryExt.InventoryMetaUID
		WHERE Tags.InventoryUID > 0
		AND Tags.InventoryMeta1UID > 0
		AND tblTechInventoryExt.InventoryExtUID IS NULL
		
		UPDATE _ETL_Inventory
		SET InventoryExt1UID = tblTechInventoryExt.InventoryExtUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventoryExt ON Tags.InventoryMeta1UID = tblTechInventoryExt.InventoryMetaUID
		AND Tags.InventoryUID = tblTechInventoryExt.InventoryUID
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryExt1UID IS NULL
		OR Tags.InventoryExt1UID <> tblTechInventoryExt.InventoryExtUID)
		
		UPDATE _ETL_Inventory
		SET InventoryExt1UID = 0
		WHERE InventoryExt1UID IS NULL
	END

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 9 AND ETLSettingValue <> '') = 1
	BEGIN
		SELECT @Label = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 9
		SELECT @Type = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 10
		INSERT INTO tblTechInventoryMeta (ItemTypeUID, InventoryMetaLabel,
		InventoryMetaType, InventoryMetaRequired, InventoryMetaOrder)
		SELECT Tags.ItemTypeUID, @Label, @Type, 0, 2
		FROM _ETL_Inventory AS Tags
		LEFT JOIN tblTechInventoryMeta ON Tags.ItemTypeUID = tblTechInventoryMeta.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 2
		WHERE Tags.ItemTypeUID > 0
		AND tblTechInventoryMeta.InventoryMetaUID IS NULL
		GROUP BY Tags.ItemTypeUID
		
		UPDATE _ETL_Inventory
		SET InventoryMeta2UID = tblTechInventoryMeta.InventoryMetaUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventoryMeta ON Tags.ItemTypeUID = tblTechInventoryMeta.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 2
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryMeta2UID IS NULL
		OR Tags.InventoryMeta2UID <> tblTechInventoryMeta.InventoryMetaUID)
		
		UPDATE _ETL_Inventory
		SET InventoryMeta2UID = 0
		WHERE InventoryMeta1UID IS NULL
		
		INSERT INTO tblTechInventoryExt (InventoryUID, InventoryMetaUID, InventoryExtValue)
		SELECT Tags.InventoryUID, Tags.InventoryMeta2UID, Tags.CustomField2
		FROM _ETL_Inventory AS Tags
		LEFT JOIN tblTechInventoryExt ON Tags.InventoryUID = tblTechInventoryExt.InventoryUID
		AND Tags.InventoryMeta2UID = tblTechInventoryExt.InventoryMetaUID
		WHERE Tags.InventoryUID > 0
		AND Tags.InventoryMeta2UID > 0
		AND tblTechInventoryExt.InventoryExtUID IS NULL
		
		UPDATE _ETL_Inventory
		SET InventoryExt2UID = tblTechInventoryExt.InventoryExtUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventoryExt ON Tags.InventoryMeta2UID = tblTechInventoryExt.InventoryMetaUID
		AND Tags.InventoryUID = tblTechInventoryExt.InventoryUID
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryExt2UID IS NULL
		OR Tags.InventoryExt2UID <> tblTechInventoryExt.InventoryExtUID)
		
		UPDATE _ETL_Inventory
		SET InventoryExt2UID = 0
		WHERE InventoryExt2UID IS NULL
	END

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 11 AND ETLSettingValue <> '') = 1
	BEGIN
		SELECT @Label = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 11
		SELECT @Type = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 12
		INSERT INTO tblTechInventoryMeta (ItemTypeUID, InventoryMetaLabel,
		InventoryMetaType, InventoryMetaRequired, InventoryMetaOrder)
		SELECT Tags.ItemTypeUID, @Label, @Type, 0, 3
		FROM _ETL_Inventory AS Tags
		LEFT JOIN tblTechInventoryMeta ON Tags.ItemTypeUID = tblTechInventoryMeta.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 3
		WHERE Tags.ItemTypeUID IS NOT NULL
		AND Tags.ItemTypeUID > 0
		AND tblTechInventoryMeta.InventoryMetaUID IS NULL
		GROUP BY Tags.ItemTypeUID
		
		UPDATE _ETL_Inventory
		SET InventoryMeta3UID = tblTechInventoryMeta.InventoryMetaUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventoryMeta ON Tags.ItemTypeUID = tblTechInventoryMeta.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 3
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryMeta3UID IS NULL
		OR Tags.InventoryMeta3UID <> tblTechInventoryMeta.InventoryMetaUID)
		
		UPDATE _ETL_Inventory
		SET InventoryMeta3UID = 0
		WHERE InventoryMeta3UID IS NULL
		
		INSERT INTO tblTechInventoryExt (InventoryUID, InventoryMetaUID, InventoryExtValue)
		SELECT Tags.InventoryUID, Tags.InventoryMeta3UID, Tags.CustomField3
		FROM _ETL_Inventory AS Tags
		LEFT JOIN tblTechInventoryExt ON Tags.InventoryUID = tblTechInventoryExt.InventoryUID
		AND Tags.InventoryMeta3UID = tblTechInventoryExt.InventoryMetaUID
		WHERE Tags.InventoryUID > 0
		AND Tags.InventoryMeta3UID > 0
		AND tblTechInventoryExt.InventoryExtUID IS NULL
		
		UPDATE _ETL_Inventory
		SET InventoryExt3UID = tblTechInventoryExt.InventoryExtUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventoryExt ON Tags.InventoryMeta3UID = tblTechInventoryExt.InventoryMetaUID
		AND Tags.InventoryUID = tblTechInventoryExt.InventoryUID
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryExt3UID IS NULL
		OR Tags.InventoryExt3UID <> tblTechInventoryExt.InventoryExtUID)
		
		UPDATE _ETL_Inventory
		SET InventoryExt3UID = 0
		WHERE InventoryExt3UID IS NULL
	END

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 13 AND ETLSettingValue <> '') = 1
	BEGIN
		SELECT @Label = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 13
		SELECT @Type = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 14
		INSERT INTO tblTechInventoryMeta (ItemTypeUID, InventoryMetaLabel,
		InventoryMetaType, InventoryMetaRequired, InventoryMetaOrder)
		SELECT Tags.ItemTypeUID, @Label, @Type, 0, 4
		FROM _ETL_Inventory AS Tags
		LEFT JOIN tblTechInventoryMeta ON Tags.ItemTypeUID = tblTechInventoryMeta.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 4
		WHERE Tags.ItemTypeUID IS NOT NULL
		AND Tags.ItemTypeUID > 0
		AND tblTechInventoryMeta.InventoryMetaUID IS NULL
		GROUP BY Tags.ItemTypeUID
		
		UPDATE _ETL_Inventory
		SET InventoryMeta4UID = tblTechInventoryMeta.InventoryMetaUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventoryMeta ON Tags.ItemTypeUID = tblTechInventoryMeta.ItemTypeUID
		AND tblTechInventoryMeta.InventoryMetaOrder = 4
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryMeta4UID IS NULL
		OR Tags.InventoryMeta4UID <> tblTechInventoryMeta.InventoryMetaUID)
		
		UPDATE _ETL_Inventory
		SET InventoryMeta4UID = 0
		WHERE InventoryMeta4UID IS NULL
		
		INSERT INTO tblTechInventoryExt (InventoryUID, InventoryMetaUID, InventoryExtValue)
		SELECT Tags.InventoryUID, Tags.InventoryMeta4UID, Tags.CustomField4
		FROM _ETL_Inventory AS Tags
		LEFT JOIN tblTechInventoryExt ON Tags.InventoryUID = tblTechInventoryExt.InventoryUID
		AND Tags.InventoryMeta4UID = tblTechInventoryExt.InventoryMetaUID
		WHERE Tags.InventoryUID > 0
		AND Tags.InventoryMeta4UID > 0
		AND tblTechInventoryExt.InventoryExtUID IS NULL
		
		UPDATE _ETL_Inventory
		SET InventoryExt4UID = tblTechInventoryExt.InventoryExtUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventoryExt ON Tags.InventoryMeta4UID = tblTechInventoryExt.InventoryMetaUID
		AND Tags.InventoryUID = tblTechInventoryExt.InventoryUID
		WHERE Tags.ItemTypeUID > 0
		AND (Tags.InventoryExt4UID IS NULL
		OR Tags.InventoryExt4UID <> tblTechInventoryExt.InventoryExtUID)
		
		UPDATE _ETL_Inventory
		SET InventoryExt4UID = 0
		WHERE InventoryExt4UID IS NULL
	END
