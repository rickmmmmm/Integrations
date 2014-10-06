IF (SELECT COUNT(*) FROM tblUnvManufacturers WHERE ManufacturerUID = 0) = 0
	BEGIN
		SET IDENTITY_INSERT tblUnvManufacturers ON 
		INSERT INTO tblUnvManufacturers (ManufacturerUID, ManufacturerName,
		CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
		VALUES (0, 'NONE', 0, GETDATE(), 0, GETDATE())
		SET IDENTITY_INSERT tblUnvManufacturers OFF
	END

INSERT INTO tblUnvManufacturers (ManufacturerName,
CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
SELECT Items.Manufacturer, 0, GETDATE(), 0, GETDATE()
FROM _ETL_Items AS Items
LEFT JOIN tblUnvManufacturers ON UPPER(Items.Manufacturer) = UPPER(tblUnvManufacturers.ManufacturerName)
WHERE Items.Manufacturer IS NOT NULL AND Items.Manufacturer <> ''
AND Items.Manufacturer <> 'N/A' AND Items.Manufacturer <> 'NONE' AND Items.Manufacturer <> 'UNKNOWN'
AND tblUnvManufacturers.ManufacturerUID IS NULL
GROUP BY Items.Manufacturer

UPDATE _ETL_Items
SET ManufacturerUID = tblUnvManufacturers.ManufacturerUID
FROM _ETL_Items AS Items
JOIN tblUnvManufacturers ON UPPER(Items.Manufacturer) = UPPER(tblUnvManufacturers.ManufacturerName)
WHERE Items.Manufacturer IS NOT NULL AND Items.Manufacturer <> ''
AND Items.Manufacturer <> 'N/A' AND Items.Manufacturer <> 'NONE' AND Items.Manufacturer <> 'UNKNOWN'
AND (Items.ManufacturerUID IS NULL
OR Items.ManufacturerUID <> tblUnvManufacturers.ManufacturerUID)

UPDATE _ETL_Items
SET ManufacturerUID = 0
WHERE ManufacturerUID IS NULL
