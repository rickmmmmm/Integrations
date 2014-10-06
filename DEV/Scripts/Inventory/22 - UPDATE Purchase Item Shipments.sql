UPDATE _ETL_Inventory
SET PurchaseItemShipmentUID = tblTechPurchaseItemShipments.PurchaseItemShipmentUID
FROM _ETL_Inventory AS Tags
JOIN tblTechPurchaseItemShipments ON Tags.PurchaseItemDetailUID = tblTechPurchaseItemShipments.PurchaseItemDetailUID
AND Tags.SiteUID = tblTechPurchaseItemShipments.ShippedToSiteUID
WHERE (Tags.PurchaseItemShipmentUID IS NULL
OR Tags.PurchaseItemShipmentUID <> tblTechPurchaseItemShipments.PurchaseItemShipmentUID)

UPDATE tblTechPurchaseItemShipments
SET QuantityShipped = ISNULL(Tags.c, 0), LastModifiedByUserID = 0, LastModifiedDate = GETDATE()
FROM tblTechPurchaseItemShipments AS TPIS
JOIN (SELECT COUNT(*) AS c, PurchaseItemShipmentUID
FROM _ETL_Inventory
GROUP BY PurchaseItemShipmentUID) AS Tags
ON TPIS.PurchaseItemShipmentUID = Tags.PurchaseItemShipmentUID
JOIN tblTechPurchaseItemDetails ON TPIS.PurchaseItemDetailUID = tblTechPurchaseItemDetails.PurchaseItemDetailUID
WHERE TPIS.QuantityShipped < ISNULL(Tags.c, 0)
