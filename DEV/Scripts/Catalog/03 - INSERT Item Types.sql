IF (SELECT COUNT(*) FROM tblTechItemTypes WHERE ItemTypeUID = 0) = 0
	BEGIN
		SET IDENTITY_INSERT tblTechItemTypes ON 
		INSERT INTO tblTechItemTypes (ItemTypeUID, ItemTypeName, ItemTypeDescription,
		CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
		VALUES (0, 'NONE', NULL, 0, GETDATE(), 0, GETDATE())
		SET IDENTITY_INSERT tblTechItemTypes OFF
	END

UPDATE _ETL_Items
SET ProductTypeDescription = ''
WHERE ProductTypeDescription IS NULL
OR ProductTypeDescription = 'NONE'
OR ProductTypeDescription = 'N/A'
OR ProductTypeDescription = 'UNKNOWN'

INSERT INTO tblTechItemTypes (ItemTypeName, ItemTypeDescription,
CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
SELECT Items.ProductType,
CASE WHEN MAX(Items.ProductTypeDescription) <> ''
THEN MAX(Items.ProductTypeDescription)
ELSE Items.ProductType END,
0, GETDATE(), 0, GETDATE()
FROM _ETL_Items AS Items
LEFT JOIN tblTechItemTypes ON UPPER(Items.ProductType) = UPPER(tblTechItemTypes.ItemTypeName)
WHERE Items.ProductType IS NOT NULL AND Items.ProductType <> ''
AND Items.ProductType <> 'N/A' AND Items.ProductType <> 'NONE' AND Items.ProductType <> 'UNKNOWN'
AND tblTechItemTypes.ItemTypeUID IS NULL
GROUP BY Items.ProductType

UPDATE _ETL_Items
SET ItemTypeUID = tblTechItemTypes.ItemTypeUID
FROM _ETL_Items AS Items
JOIN tblTechItemTypes ON UPPER(Items.ProductType) = UPPER(tblTechItemTypes.ItemTypeName)
WHERE Items.ProductType IS NOT NULL AND Items.ProductType <> ''
AND Items.ProductType <> 'N/A' AND Items.ProductType <> 'NONE' AND Items.ProductType <> 'UNKNOWN'
AND (Items.ItemTypeUID IS NULL
OR Items.ItemTypeUID <> tblTechItemTypes.ItemTypeUID)

UPDATE _ETL_Items
SET ItemTypeUID = 0
WHERE ItemTypeUID IS NULL
