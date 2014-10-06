UPDATE _ETL_Inventory
SET Serial = ''
WHERE Serial IS NULL
OR Serial = 'N/A'
OR Serial = 'NONE'
OR Serial = 'UNKNOWN'

UPDATE _ETL_Inventory
SET InventoryUID = tblTechInventory.InventoryUID
FROM _ETL_Inventory AS Tags
JOIN tblTechInventory ON UPPER(Tags.AssetID) = UPPER(tblTechInventory.AssetID)
WHERE (Tags.InventoryUID IS NULL
OR Tags.InventoryUID <> tblTechInventory.InventoryUID)

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 15 AND ETLSettingValue = 'True') = 1
	BEGIN
		UPDATE _ETL_Inventory
		SET InventoryUID = tblTechInventory.InventoryUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechInventory ON UPPER(Tags.Tag) = UPPER(tblTechInventory.Tag)
		WHERE Tags.InventoryUID IS NULL

		UPDATE tblTechInventory
		SET AssetID = Tags.AssetID
		FROM tblTechInventory AS TI
		JOIN _ETL_Inventory AS Tags ON TI.InventoryUID = Tags.InventoryUID
		WHERE TI.AssetID IS NULL
	END

--InventoryUID set to -1 means Tag already exists in TIPWeb
UPDATE _ETL_Inventory
SET InventoryUID = -1
FROM _ETL_Inventory AS Tags
JOIN tblTechInventory ON UPPER(Tags.Tag) = UPPER(tblTechInventory.Tag)
WHERE Tags.InventoryUID IS NULL

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 6 AND ETLSettingValue = 'True') = 1
	BEGIN
		UPDATE tblTechInventory
		SET Tag = Tags.Tag
		FROM tblTechInventory AS TI
		JOIN _ETL_Inventory AS Tags ON TI.InventoryUID = Tags.InventoryUID
		WHERE TI.Tag <> Tags.Tag
	END

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 16 AND ETLSettingValue = 'True') = 1
	BEGIN
		UPDATE tblTechInventory
		SET ItemUID = Tags.ItemUID
		FROM tblTechInventory AS TI
		JOIN _ETL_Inventory AS Tags ON TI.InventoryUID = Tags.InventoryUID
		WHERE TI.ItemUID <> Tags.ItemUID
	END

UPDATE tblTechInventory
SET InventoryTypeUID = Tags.InventoryTypeUID,
Serial = Tags.Serial, FundingSourceUID = Tags.FundingSourceUID,
PurchasePrice = Tags.PurchasePrice, PurchaseDate = Tags.PurchaseDate,
LastModifiedByUserID = 0, LastModifiedDate = GETDATE()
FROM tblTechInventory AS TI
JOIN _ETL_Inventory AS Tags ON TI.InventoryUID = Tags.InventoryUID
WHERE TI.InventoryTypeUID <> Tags.InventoryTypeUID
OR TI.Serial <> Tags.Serial
OR TI.FundingSourceUID <> Tags.FundingSourceUID
OR TI.PurchasePrice <> Tags.PurchasePrice
OR TI.PurchaseDate <> Tags.PurchaseDate
OR (TI.PurchasePrice IS NULL AND Tags.PurchasePrice IS NOT NULL)
OR (TI.PurchasePrice IS NOT NULL AND Tags.PurchasePrice IS NULL)
OR (TI.PurchaseDate IS NULL AND Tags.PurchaseDate IS NOT NULL)
OR (TI.PurchaseDate IS NOT NULL AND Tags.PurchaseDate IS NULL)

UPDATE tblTechInventoryHistory
SET InventoryTypeUID = tblTechInventory.InventoryTypeUID,
LastModifiedByUserID = 0, LastModifiedDate = GETDATE()
FROM tblTechInventoryHistory AS TIH
JOIN tblTechInventory ON TIH.InventoryUID = tblTechInventory.InventoryUID
WHERE TIH.InventoryTypeUID <> tblTechInventory.InventoryTypeUID
