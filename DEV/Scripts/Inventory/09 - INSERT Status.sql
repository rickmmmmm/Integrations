DECLARE @STATUS AS VARCHAR(50)
SELECT @STATUS = ETLSettingValue FROM _ETL_Settings WHERE ETLSettingUID = 5
UPDATE _ETL_Inventory
SET Status = @STATUS
WHERE Status IS NULL
OR Status NOT IN ('In Use', 'Available')

UPDATE _ETL_Inventory
SET statusID = 26
WHERE Status = 'Available'

UPDATE _ETL_Inventory
SET statusID = 28
WHERE Status = 'In Use'

UPDATE _ETL_Inventory
SET TechDepartmentUID = tblTechDepartments.TechDepartmentUID
FROM _ETL_Inventory AS Tags
JOIN tblTechDepartments ON UPPER(Tags.Department) = UPPER(tblTechDepartments.DepartmentName)
WHERE Tags.Department IS NOT NULL AND Tags.Department <> ''
AND Tags.Department <> 'N/A' AND Tags.Department <> 'NONE' AND Tags.Department <> 'UNKNOWN'
AND (Tags.TechDepartmentUID IS NULL
OR Tags.TechDepartmentUID <> tblTechDepartments.TechDepartmentUID)

UPDATE _ETL_Inventory
SET TechDepartmentUID = 0
WHERE TechDepartmentUID IS NULL
