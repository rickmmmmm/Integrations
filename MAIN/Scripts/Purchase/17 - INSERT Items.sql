UPDATE _ETL_Purchases
SET ItemUID = _ETL_Items.ItemUID
FROM _ETL_Purchases AS Purchases
JOIN _ETL_Items ON UPPER(Purchases.Product) = UPPER(_ETL_Items.Product)
WHERE Purchases.Product IS NOT NULL AND Purchases.Product <> ''
AND Purchases.Product <> 'N/A' AND Purchases.Product <> 'NONE' AND Purchases.Product <> 'UNKNOWN'
AND (Purchases.ItemUID IS NULL
OR Purchases.ItemUID <> _ETL_Items.ItemUID)

UPDATE _ETL_Purchases
SET ItemUID = 0
WHERE ItemUID IS NULL
