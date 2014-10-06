UPDATE _ETL_Inventory
SET ItemUID = _ETL_Items.ItemUID
FROM _ETL_Inventory AS Tags
JOIN _ETL_Items ON UPPER(Tags.Product) = UPPER(_ETL_Items.Product)
WHERE Tags.Product IS NOT NULL AND Tags.Product <> ''
AND Tags.Product <> 'N/A' AND Tags.Product <> 'NONE' AND Tags.Product <> 'UNKNOWN'
AND (Tags.ItemUID IS NULL
OR Tags.ItemUID <> _ETL_Items.ItemUID)

UPDATE _ETL_Inventory
SET ItemUID = 0
WHERE ItemUID IS NULL
