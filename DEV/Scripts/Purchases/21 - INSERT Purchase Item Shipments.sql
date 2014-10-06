UPDATE _ETL_Purchases
SET TicketedByUserID = tblUser.UserID
FROM _ETL_Purchases AS Purchases
JOIN tblUser ON UPPER(Purchases.TicketedBy) = UPPER(tblUser.RealName)
AND tblUser.ApplicationUID = 2
WHERE Purchases.TicketedBy IS NOT NULL AND Purchases.TicketedBy <> ''
AND Purchases.TicketedBy <> 'N/A' AND Purchases.TicketedBy <> 'NONE' AND Purchases.TicketedBy <> 'UNKNOWN'
AND (Purchases.TicketedByUserID IS NULL
OR Purchases.TicketedByUserID <> tblUser.UserID)

UPDATE _ETL_Purchases
SET TicketedByUserID = 0
WHERE TicketedByUserID IS NULL

UPDATE tblTechPurchaseItemShipments
SET QuantityShipped = ISNULL(Tags.c, 0), LastModifiedByUserID = 0, LastModifiedDate = GETDATE()
FROM tblTechPurchaseItemShipments AS TPIS
JOIN (SELECT COUNT(*) AS c, PurchaseItemShipmentUID
FROM _ETL_Inventory
GROUP BY PurchaseItemShipmentUID) AS Tags
ON TPIS.PurchaseItemShipmentUID = Tags.PurchaseItemShipmentUID
JOIN tblTechPurchaseItemDetails ON TPIS.PurchaseItemDetailUID = tblTechPurchaseItemDetails.PurchaseItemDetailUID
WHERE TPIS.QuantityShipped < ISNULL(Tags.c, 0)

INSERT INTO tblTechPurchaseItemShipments (PurchaseItemDetailUID, ShippedToSiteUID, TicketNumber, QuantityShipped,
TicketedByUserID, TicketedDate, StatusUID, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
SELECT Purchases.PurchaseItemDetailUID, Purchases.ShippedToSiteUID, Purchases.TicketNumber, MAX(ISNULL(Purchases.QuantityShipped, 0)),
MAX(Purchases.TicketedByUserID), MIN(ISNULL(Purchases.TicketedDate, GETDATE())), 58, 0, GETDATE(), 0, GETDATE()
FROM _ETL_Purchases AS Purchases
LEFT JOIN tblTechPurchaseItemShipments ON Purchases.PurchaseItemDetailUID = tblTechPurchaseItemShipments.PurchaseItemDetailUID
AND Purchases.ShippedToSiteUID = tblTechPurchaseItemShipments.ShippedToSiteUID
AND ISNULL(Purchases.TicketNumber, '') = ISNULL(tblTechPurchaseItemShipments.TicketNumber, '')
WHERE Purchases.PurchaseItemDetailUID > 0
AND Purchases.ShippedToSiteUID > 0
AND purchases.PurchaseItemShipmentUID IS NULL
AND tblTechPurchaseItemShipments.PurchaseItemShipmentUID IS NULL
GROUP BY Purchases.PurchaseItemDetailUID, Purchases.ShippedToSiteUID, Purchases.TicketNumber

UPDATE _ETL_Purchases
SET PurchaseItemShipmentUID = tblTechPurchaseItemShipments.PurchaseItemShipmentUID
FROM _ETL_Purchases AS Purchases
JOIN tblTechPurchaseItemShipments ON Purchases.PurchaseItemDetailUID = tblTechPurchaseItemShipments.PurchaseItemDetailUID
AND Purchases.ShippedToSiteUID = tblTechPurchaseItemShipments.ShippedToSiteUID
AND ISNULL(Purchases.TicketNumber, '') = ISNULL(tblTechPurchaseItemShipments.TicketNumber, '')
WHERE (Purchases.PurchaseItemShipmentUID IS NULL
OR Purchases.PurchaseItemShipmentUID <> tblTechPurchaseItemShipments.PurchaseItemShipmentUID)

UPDATE _ETL_Purchases
SET PurchaseItemShipmentUID = 0
WHERE PurchaseItemShipmentUID IS NULL
