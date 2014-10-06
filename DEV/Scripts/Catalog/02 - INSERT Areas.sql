IF (SELECT COUNT(*) FROM tblUnvAreas WHERE AreaUID = 0) = 0
	BEGIN
		SET IDENTITY_INSERT tblUnvAreas ON 
		INSERT INTO tblUnvAreas (AreaUID, AreaName,
		CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
		VALUES (0, 'NONE', 0, GETDATE(), 0, GETDATE())
		SET IDENTITY_INSERT tblUnvAreas OFF
	END

INSERT INTO tblUnvAreas (AreaName,
CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
SELECT Items.Area, 0, GETDATE(), 0, GETDATE()
FROM _ETL_Items AS Items
LEFT JOIN tblUnvAreas ON UPPER(Items.Area) = UPPER(tblUnvAreas.AreaName)
WHERE Items.Area IS NOT NULL AND Items.Area <> ''
AND Items.Area <> 'N/A' AND Items.Area <> 'NONE' AND Items.Area <> 'UNKNOWN'
AND tblUnvAreas.AreaUID IS NULL
GROUP BY Items.Area

UPDATE _ETL_Items
SET AreaUID = tblUnvAreas.AreaUID
FROM _ETL_Items AS Items
JOIN tblUnvAreas ON UPPER(Items.Area) = UPPER(tblUnvAreas.AreaName)
WHERE Items.Area IS NOT NULL AND Items.Area <> ''
AND Items.Area <> 'N/A' AND Items.Area <> 'NONE' AND Items.Area <> 'UNKNOWN'
AND (Items.AreaUID IS NULL
OR Items.AreaUID <> tblUnvAreas.AreaUID)

UPDATE _ETL_Items
SET AreaUID = 0
WHERE AreaUID IS NULL
