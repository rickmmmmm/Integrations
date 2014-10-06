INSERT INTO tblTechPurchaseItemDetails (PurchaseUID, ItemUID, FundingSourceUID,
StatusUID, SiteAddedSiteUID, QuantityOrdered, QuantityReceived, PurchasePrice, AccountCode,
CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
SELECT Tags.PurchaseUID, Tags.ItemUID, Tags.FundingSourceUID,
33, 1, COUNT(*), COUNT(*), Tags.PurchasePrice, NULL, 0, GETDATE(), 0, GETDATE()
FROM _ETL_Inventory AS Tags
WHERE Tags.InventoryUID > 0
AND Tags.SiteUID > 0
AND Tags.PurchaseUID > 0
AND (Tags.PurchaseItemDetailUID IS NULL
OR Tags.PurchaseItemDetailUID = 0)
GROUP BY Tags.PurchaseUID, Tags.ItemUID,
Tags.FundingSourceUID, Tags.PurchasePrice

UPDATE tblTechPurchaseItemDetails
SET QuantityOrdered = QuantityReceived
WHERE QuantityReceived > QuantityOrdered

UPDATE _ETL_Inventory
SET PurchaseItemDetailUID = tblTechPurchaseItemDetails.PurchaseItemDetailUID
FROM _ETL_Inventory AS Tags
JOIN tblTechPurchaseItemDetails ON Tags.PurchaseUID = tblTechPurchaseItemDetails.PurchaseUID
AND Tags.ItemUID = tblTechPurchaseItemDetails.ItemUID
AND Tags.FundingSourceUID = tblTechPurchaseItemDetails.FundingSourceUID
AND Tags.PurchasePrice = tblTechPurchaseItemDetails.PurchasePrice
WHERE Tags.InventoryUID > 0
AND Tags.SiteUID > 0
AND (Tags.PurchaseItemDetailUID IS NULL
OR Tags.PurchaseItemDetailUID <> tblTechPurchaseItemDetails.PurchaseItemDetailUID)

UPDATE _ETL_Inventory
SET PurchaseItemDetailUID = 0
WHERE PurchaseItemDetailUID IS NULL
