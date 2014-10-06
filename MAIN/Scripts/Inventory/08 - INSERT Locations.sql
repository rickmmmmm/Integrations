UPDATE _ETL_Inventory
SET Location = ''
WHERE Location IS NULL
OR Location = 'N/A'
OR Location = 'NONE'
OR Location = 'UNKNOWN'

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 3 AND ETLSettingValue = 'SiteID') = 0
	BEGIN
		UPDATE _ETL_Inventory
		SET SiteUID = tblTechSites.SiteUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechSites ON UPPER(Tags.Site) = UPPER(tblTechSites.SiteName)
		WHERE (Tags.SiteUID IS NULL
		OR Tags.SiteUID <> tblTechSites.SiteUID)
	END
ELSE
	BEGIN
		UPDATE _ETL_Inventory
		SET SiteUID = tblTechSites.SiteUID
		FROM _ETL_Inventory AS Tags
		JOIN tblTechSites ON UPPER(Tags.Site) = UPPER(tblTechSites.SiteID)
		WHERE (Tags.SiteUID IS NULL
		OR Tags.SiteUID <> tblTechSites.SiteUID)
	END

UPDATE _ETL_Inventory
SET SiteUID = 1
WHERE SiteUID IS NULL

DECLARE @LOCATION AS VARCHAR(50)
SELECT @LOCATION = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 4
IF @LOCATION IS NOT NULL
	BEGIN
		UPDATE _ETL_Inventory
		SET Location = @LOCATION
		WHERE Location = ''
	END

UPDATE _ETL_Inventory
SET EntityUID = tblUnvRooms.RoomUID, EntityTypeUID = 2
FROM _ETL_Inventory AS Tags
JOIN tblUnvRooms ON UPPER(Tags.Location) = UPPER(tblUnvRooms.RoomNumber)
AND Tags.SiteUID = tblUnvRooms.SiteUID
WHERE Tags.SiteUID > 1
AND (Tags.EntityUID IS NULL
OR Tags.EntityUID <> tblUnvRooms.RoomUID)

UPDATE _ETL_Inventory
SET EntityUID = 0, EntityTypeUID = 0
WHERE EntityUID IS NULL
