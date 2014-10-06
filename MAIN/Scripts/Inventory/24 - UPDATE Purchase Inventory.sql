UPDATE _ETL_Inventory
SET PurchaseInventoryUID = tblTechPurchaseInventory.PurchaseInventoryUID
FROM _ETL_Inventory AS Tags
JOIN tblTechPurchaseInventory ON Tags.InventoryUID = tblTechPurchaseInventory.InventoryUID
WHERE Tags.SiteUID > 0
AND (Tags.PurchaseInventoryUID IS NULL
OR Tags.PurchaseInventoryUID <> tblTechPurchaseInventory.PurchaseInventoryUID)

UPDATE tblTechPurchaseInventory
SET PurchaseItemShipmentUID = Tags.PurchaseItemShipmentUID
FROM tblTechPurchaseInventory AS TPI
JOIN _ETL_Inventory AS Tags ON TPI.PurchaseInventoryUID = Tags.PurchaseInventoryUID
AND Tags.PurchaseItemShipmentUID > 0
AND TPI.PurchaseItemShipmentUID <> Tags.PurchaseItemShipmentUID

DELETE old
FROM tblTechPurchaseInventory AS old
JOIN tblTechPurchaseItemShipments ON old.PurchaseItemShipmentUID = tblTechPurchaseItemShipments.PurchaseItemShipmentUID
JOIN tblTechPurchaseItemDetails ON tblTechPurchaseItemShipments.PurchaseItemDetailUID = tblTechPurchaseItemDetails.PurchaseItemDetailUID
JOIN tblTechPurchases ON tblTechPurchaseItemDetails.PurchaseUID = tblTechPurchases.PurchaseUID
LEFT JOIN _ETL_Inventory AS Tags ON old.PurchaseInventoryUID = Tags.PurchaseInventoryUID
AND old.PurchaseItemShipmentUID = Tags.PurchaseItemShipmentUID
WHERE tblTechPurchases.Notes LIKE '%DATA IMPORT%'
AND Tags.PurchaseInventoryUID IS NULL
