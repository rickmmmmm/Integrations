UPDATE _ETL_Purchases
SET AccountCode = ''
WHERE AccountCode IS NULL
OR AccountCode = 'N/A'
OR AccountCode = 'NONE'
OR AccountCode = 'UNKNOWN'

UPDATE _ETL_Purchases
SET TechDepartmentUID = tblTechDepartments.TechDepartmentUID
FROM _ETL_Purchases AS Purchases
JOIN tblTechDepartments ON UPPER(Purchases.Department) = UPPER(tblTechDepartments.DepartmentName)
WHERE Purchases.Department IS NOT NULL AND Purchases.Department <> ''
AND Purchases.Department <> 'N/A' AND Purchases.Department <> 'NONE' AND Purchases.Department <> 'UNKNOWN'
AND (Purchases.TechDepartmentUID IS NULL
OR Purchases.TechDepartmentUID <> tblTechDepartments.TechDepartmentUID)

UPDATE _ETL_Purchases
SET TechDepartmentUID = 0
WHERE TechDepartmentUID IS NULL

UPDATE _ETL_Purchases
SET PurchaseItemDetailUID = tblTechPurchaseItemDetails.PurchaseItemDetailUID
FROM _ETL_Purchases AS Purchases
JOIN tblTechPurchaseItemDetails ON Purchases.PurchaseUID = tblTechPurchaseItemDetails.PurchaseUID
AND Purchases.ItemUID = tblTechPurchaseItemDetails.ItemUID
AND Purchases.FundingSourceUID = tblTechPurchaseItemDetails.FundingSourceUID
AND Purchases.PurchasePrice = tblTechPurchaseItemDetails.PurchasePrice
AND Purchases.AccountCode = tblTechPurchaseItemDetails.AccountCode
AND Purchases.LineNumber = tblTechPurchaseItemDetails.LineNumber
WHERE (Purchases.PurchaseItemDetailUID IS NULL
OR Purchases.PurchaseItemDetailUID <> tblTechPurchaseItemDetails.PurchaseItemDetailUID)

UPDATE tblTechPurchaseItemDetails
SET QuantityOrdered = ISNULL(Purchases.QuantityOrdered, 0),
LastModifiedByUserID = 0, LastModifiedDate = GETDATE()
FROM tblTechPurchaseItemDetails AS TPID
JOIN _ETL_Purchases AS Purchases
ON TPID.PurchaseItemDetailUID = Purchases.PurchaseItemDetailUID
WHERE TPID.QuantityOrdered <> ISNULL(Purchases.QuantityOrdered, 0)

UPDATE tblTechPurchaseItemDetails
SET QuantityReceived = ISNULL(Purchases.QuantityReceived, 0),
LastModifiedByUserID = 0, LastModifiedDate = GETDATE()
FROM tblTechPurchaseItemDetails AS TPID
JOIN _ETL_Purchases AS Purchases
ON TPID.PurchaseItemDetailUID = Purchases.PurchaseItemDetailUID
WHERE TPID.QuantityReceived <> ISNULL(Purchases.QuantityReceived, 0)

INSERT INTO tblTechPurchaseItemDetails (PurchaseUID, ItemUID, FundingSourceUID,
StatusUID, SiteAddedSiteUID, QuantityOrdered, QuantityReceived, PurchasePrice, AccountCode,
CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
SELECT Purchases.PurchaseUID, Purchases.ItemUID, Purchases.FundingSourceUID,
32, MIN(Purchases.SiteAddedSiteUID), MAX(ISNULL(Purchases.QuantityOrdered, 0)),
MAX(ISNULL(Purchases.QuantityReceived, 0)), Purchases.PurchasePrice, Purchases.AccountCode,
0, GETDATE(), 0, GETDATE()
FROM _ETL_Purchases AS Purchases
LEFT JOIN tblTechPurchaseItemDetails ON Purchases.PurchaseUID = tblTechPurchaseItemDetails.PurchaseUID
AND Purchases.ItemUID = tblTechPurchaseItemDetails.ItemUID
AND Purchases.FundingSourceUID = tblTechPurchaseItemDetails.FundingSourceUID
AND Purchases.PurchasePrice = tblTechPurchaseItemDetails.PurchasePrice
AND Purchases.AccountCode = tblTechPurchaseItemDetails.AccountCode
AND Purchases.LineNumber = tblTechPurchaseItemDetails.LineNumber
WHERE Purchases.PurchaseUID > 0
AND Purchases.ItemUID > 0
AND tblTechPurchaseItemDetails.PurchaseItemDetailUID IS NULL
GROUP BY Purchases.PurchaseUID, Purchases.ItemUID, Purchases.FundingSourceUID,
Purchases.PurchasePrice, Purchases.AccountCode, Purchases.LineNumber

UPDATE tblTechPurchaseItemDetails
SET QuantityOrdered = QuantityReceived
WHERE QuantityReceived > QuantityOrdered

UPDATE tblTechPurchaseItemDetails
SET StatusUID = 32
WHERE QuantityOrdered > QuantityReceived
AND StatusUID = 33

UPDATE _ETL_Purchases
SET PurchaseItemDetailUID = tblTechPurchaseItemDetails.PurchaseItemDetailUID
FROM _ETL_Purchases AS Purchases
JOIN tblTechPurchaseItemDetails ON Purchases.PurchaseUID = tblTechPurchaseItemDetails.PurchaseUID
AND Purchases.ItemUID = tblTechPurchaseItemDetails.ItemUID
AND Purchases.FundingSourceUID = tblTechPurchaseItemDetails.FundingSourceUID
AND Purchases.PurchasePrice = tblTechPurchaseItemDetails.PurchasePrice
AND Purchases.AccountCode = tblTechPurchaseItemDetails.AccountCode
WHERE (Purchases.PurchaseItemDetailUID IS NULL
OR Purchases.PurchaseItemDetailUID <> tblTechPurchaseItemDetails.PurchaseItemDetailUID)

UPDATE _ETL_Purchases
SET PurchaseItemDetailUID = 0
WHERE PurchaseItemDetailUID IS NULL
