INSERT INTO tblTechPurchaseInventory (InventoryUID, PurchaseItemShipmentUID,
CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
SELECT Tags.InventoryUID, Tags.PurchaseItemShipmentUID,
0, GETDATE(), 0, GETDATE()
FROM _ETL_Inventory AS Tags
WHERE Tags.InventoryUID > 0
AND Tags.SiteUID > 0
AND Tags.PurchaseUID > 0
AND Tags.PurchaseItemDetailUID > 0
AND Tags.PurchaseItemShipmentUID > 0
AND (Tags.PurchaseInventoryUID IS NULL
OR Tags.PurchaseInventoryUID = 0)

UPDATE _ETL_Inventory
SET PurchaseInventoryUID = tblTechPurchaseInventory.PurchaseInventoryUID
FROM _ETL_Inventory AS Tags
JOIN tblTechPurchaseInventory ON Tags.InventoryUID = tblTechPurchaseInventory.InventoryUID
WHERE (Tags.PurchaseInventoryUID IS NULL
OR Tags.PurchaseInventoryUID <> tblTechPurchaseInventory.PurchaseInventoryUID)

UPDATE _ETL_Inventory
SET PurchaseInventoryUID = 0
WHERE PurchaseInventoryUID IS NULL

DELETE old
FROM tblTechPurchaseItemShipments AS old
JOIN tblTechPurchaseItemDetails ON old.PurchaseItemDetailUID = tblTechPurchaseItemDetails.PurchaseItemDetailUID
JOIN tblTechPurchases ON tblTechPurchaseItemDetails.PurchaseUID = tblTechPurchases.PurchaseUID
LEFT JOIN (SELECT PurchaseItemShipmentUID FROM _ETL_Inventory
GROUP BY PurchaseItemShipmentUID) AS Inv ON old.PurchaseItemShipmentUID = Inv.PurchaseItemShipmentUID
LEFT JOIN (SELECT PurchaseItemShipmentUID FROM _ETL_Purchases
GROUP BY PurchaseItemShipmentUID) AS Purch ON old.PurchaseItemShipmentUID = Purch.PurchaseItemShipmentUID
LEFT JOIN (SELECT PurchaseItemShipmentUID FROM tblTechPurchaseInventory
GROUP BY PurchaseItemShipmentUID) AS Tags ON old.PurchaseItemShipmentUID = Tags.PurchaseItemShipmentUID
WHERE tblTechPurchases.Notes LIKE '%DATA IMPORT%'
AND Inv.PurchaseItemShipmentUID IS NULL
AND Purch.PurchaseItemShipmentUID IS NULL
AND Tags.PurchaseItemShipmentUID IS NULL

DELETE old
FROM tblTechPurchaseItemDetails AS old
JOIN tblTechPurchases ON old.PurchaseUID = tblTechPurchases.PurchaseUID
LEFT JOIN (SELECT PurchaseItemDetailUID FROM _ETL_Inventory
GROUP BY PurchaseItemDetailUID) AS Inv ON old.PurchaseItemDetailUID = Inv.PurchaseItemDetailUID
LEFT JOIN (SELECT PurchaseItemDetailUID FROM _ETL_Purchases
GROUP BY PurchaseItemDetailUID) AS Purch ON old.PurchaseItemDetailUID = Purch.PurchaseItemDetailUID
LEFT JOIN (SELECT PurchaseItemDetailUID FROM tblTechPurchaseItemShipments
GROUP BY PurchaseItemDetailUID) AS Tags ON old.PurchaseItemDetailUID = Tags.PurchaseItemDetailUID
WHERE tblTechPurchases.Notes LIKE '%DATA IMPORT%'
AND Inv.PurchaseItemDetailUID IS NULL
AND Purch.PurchaseItemDetailUID IS NULL
AND Tags.PurchaseItemDetailUID IS NULL

UPDATE tblTechPurchaseItemShipments
SET QuantityShipped = Tags.c
FROM tblTechPurchaseItemShipments
JOIN (SELECT COUNT(*) AS c, PurchaseItemShipmentUID
FROM tblTechPurchaseInventory
GROUP BY PurchaseItemShipmentUID) AS Tags
ON Tags.PurchaseItemShipmentUID = tblTechPurchaseItemShipments.PurchaseItemShipmentUID
WHERE Tags.c > QuantityShipped

UPDATE tblTechPurchaseItemShipments
SET StatusUID = 33
FROM tblTechPurchaseItemShipments
JOIN (SELECT COUNT(*) AS c, PurchaseItemShipmentUID
FROM tblTechPurchaseInventory
GROUP BY PurchaseItemShipmentUID) AS Tags
ON Tags.PurchaseItemShipmentUID = tblTechPurchaseItemShipments.PurchaseItemShipmentUID
WHERE Tags.c = QuantityShipped
AND StatusUID = 32
OR StatusUID = 58

UPDATE tblTechPurchaseItemShipments
SET StatusUID = 58
FROM tblTechPurchaseItemShipments
JOIN (SELECT COUNT(*) AS c, PurchaseItemShipmentUID
FROM tblTechPurchaseInventory
GROUP BY PurchaseItemShipmentUID) AS Tags
ON Tags.PurchaseItemShipmentUID = tblTechPurchaseItemShipments.PurchaseItemShipmentUID
WHERE Tags.c < QuantityShipped
AND StatusUID = 33
