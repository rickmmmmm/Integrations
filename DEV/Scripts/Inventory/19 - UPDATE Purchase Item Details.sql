UPDATE _ETL_Inventory
SET PurchaseItemDetailUID = tblTechPurchaseItemDetails.PurchaseItemDetailUID
FROM _ETL_Inventory AS Tags
JOIN tblTechPurchaseItemDetails ON Tags.PurchaseUID = tblTechPurchaseItemDetails.PurchaseUID
AND Tags.ItemUID = tblTechPurchaseItemDetails.ItemUID
AND Tags.FundingSourceUID = tblTechPurchaseItemDetails.FundingSourceUID
AND Tags.PurchasePrice = tblTechPurchaseItemDetails.PurchasePrice
WHERE (Tags.PurchaseItemDetailUID IS NULL
OR Tags.PurchaseItemDetailUID <> tblTechPurchaseItemDetails.PurchaseItemDetailUID)

UPDATE tblTechPurchaseItemDetails
SET QuantityOrdered = ISNULL(Tags.c, 0),
LastModifiedByUserID = 0, LastModifiedDate = GETDATE()
FROM tblTechPurchaseItemDetails AS TPID
JOIN (SELECT COUNT(*) AS c, PurchaseItemDetailUID
FROM _ETL_Inventory
GROUP BY PurchaseItemDetailUID) AS Tags
ON TPID.PurchaseItemDetailUID = Tags.PurchaseItemDetailUID
WHERE TPID.QuantityOrdered < ISNULL(Tags.c, 0)

UPDATE tblTechPurchaseItemDetails
SET QuantityReceived = ISNULL(Tags.c, 0),
LastModifiedByUserID = 0, LastModifiedDate = GETDATE()
FROM tblTechPurchaseItemDetails AS TPID
JOIN (SELECT COUNT(*) AS c, PurchaseItemDetailUID
FROM _ETL_Inventory
GROUP BY PurchaseItemDetailUID) AS Tags
ON TPID.PurchaseItemDetailUID = Tags.PurchaseItemDetailUID
WHERE TPID.QuantityReceived < ISNULL(Tags.c, 0)
