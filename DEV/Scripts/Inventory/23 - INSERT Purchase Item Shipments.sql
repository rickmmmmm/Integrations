INSERT INTO tblTechPurchaseItemShipments (PurchaseItemDetailUID, ShippedToSiteUID, TicketNumber, QuantityShipped,
TicketedByUserID, TicketedDate, StatusUID, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
SELECT Tags.PurchaseItemDetailUID, Tags.SiteUID, NULL, COUNT(*),
NULL, NULL, 33, 0, GETDATE(), 0, GETDATE()
FROM _ETL_Inventory AS Tags
WHERE Tags.InventoryUID > 0
AND Tags.SiteUID > 0
AND Tags.PurchaseUID > 0
AND Tags.PurchaseItemDetailUID > 0
AND (Tags.PurchaseItemShipmentUID IS NULL
OR Tags.PurchaseItemShipmentUID = 0)
GROUP BY Tags.PurchaseItemDetailUID, Tags.SiteUID

UPDATE _ETL_Inventory
SET PurchaseItemShipmentUID = tblTechPurchaseItemShipments.PurchaseItemShipmentUID
FROM _ETL_Inventory AS Tags
JOIN tblTechPurchaseItemShipments ON tags.PurchaseItemDetailUID = tblTechPurchaseItemShipments.PurchaseItemDetailUID
AND Tags.SiteUID = tblTechPurchaseItemShipments.ShippedToSiteUID
WHERE Tags.InventoryUID > 0
AND Tags.SiteUID > 0
AND (Tags.PurchaseItemShipmentUID IS NULL
OR Tags.PurchaseItemShipmentUID <> tblTechPurchaseItemShipments.PurchaseItemShipmentUID)

UPDATE _ETL_Inventory
SET PurchaseItemShipmentUID = 0
WHERE PurchaseItemShipmentUID IS NULL
