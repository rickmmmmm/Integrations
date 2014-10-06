UPDATE _ETL_Inventory
SET SiteUID = tblTechSites.SiteUID
FROM _ETL_Inventory AS Tags
JOIN tblTechSites ON Tags.SiteUID = tblTechSites.SiteUID
WHERE (Tags.SiteUID IS NULL
OR Tags.SiteUID <> tblTechSites.SiteUID)

UPDATE _ETL_Inventory
SET SiteUID = 0
WHERE SiteUID IS NULL

INSERT INTO tblTechInventory (InventoryTypeUID, ItemUID, SiteUID, EntityUID, EntityTypeUID, StatusUID,
Tag, Serial, FundingSourceUID, PurchasePrice, PurchaseDate, ExpirationDate, InventoryNotes,
CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate, AssetID)
SELECT CASE WHEN Tags.PurchaseUID = 0 THEN 1 ELSE 2 END,
Tags.ItemUID, Tags.SiteUID, Tags.EntityUID, 2, Tags.StatusID,
Tags.Tag, Tags.Serial, Tags.FundingSourceUID, Tags.PurchasePrice, Tags.PurchaseDate,
Tags.ExpirationDate, Tags.InventoryNotes, 0, GETDATE(), 0, GETDATE(), Tags.AssetID
FROM _ETL_Inventory AS Tags
WHERE Tags.SiteUID > 1
AND Tags.InventoryUID IS NULL

UPDATE _ETL_Inventory
SET InventoryUID = tblTechInventory.InventoryUID
FROM _ETL_Inventory AS Tags
JOIN tblTechInventory ON Tags.Tag = tblTechInventory.Tag
WHERE Tags.InventoryUID IS NULL
OR (Tags.InventoryUID > 0
AND Tags.InventoryUID <> tblTechInventory.InventoryUID)

--InventoryUID set to 0 means Unknown Error
UPDATE _ETL_Inventory
SET InventoryUID = 0
WHERE InventoryUID IS NULL

INSERT INTO tblTechInventoryHistory (InventoryUID, InventoryTypeUID,
SiteUID, EntityUID, EntityTypeUID, StatusUID,
OriginSiteUID, OriginStatusUID, OriginEntityUID, OriginEntityTypeUID,
InventoryHistoryNotes, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
SELECT tblTechInventory.InventoryUID, tblTechInventory.InventoryTypeUID,
tblTechInventory.SiteUID, tblTechInventory.EntityUID, 2, tblTechInventory.StatusUID,
tblTechInventory.SiteUID, tblTechInventory.StatusUID, tblTechInventory.EntityUID, 2,
'DATA IMPORT ' + CONVERT(NVARCHAR(10), GETDATE(), 120), 0, GETDATE(), 0, GETDATE()
FROM tblTechInventory
LEFT JOIN tblTechInventoryHistory ON tblTechInventory.InventoryUID = tblTechInventoryHistory.InventoryUID
WHERE tblTechInventoryHistory.InventoryHistoryUID IS NULL
