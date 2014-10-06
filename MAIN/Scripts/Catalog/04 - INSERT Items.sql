UPDATE _ETL_Items
SET Model = ''
WHERE Model IS NULL
OR Model = 'N/A'
OR Model = 'NONE'
OR Model = 'UNKNOWN'

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 2 AND ETLSettingValue = 'False') = 0
	BEGIN
		DECLARE @PRODNUM AS INT
		DECLARE @COUNT AS INT
		SELECT @PRODNUM = Value - 1 FROM tblUnvCounter WHERE CounterUID = 4
		INSERT INTO tblTechItems (ItemNumber, ItemName, ItemDescription, ItemTypeUID, ModelNumber, ManufacturerUID,
		ItemSuggestedPrice, ItemNotes, SKU, SerialRequired, ProjectedLife, CustomField1, CustomField2,
		CustomField3, Active, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
		SELECT CONVERT(NVARCHAR, ROW_NUMBER() OVER(ORDER BY Items.ProductName, Items.Model) + @PRODNUM),
		Items.ProductName, MAX(ISNULL(Items.ProductDescription, '')), MAX(ISNULL(Items.ItemTypeUID, 0)),
		Items.Model, Items.ManufacturerUID, MAX(ISNULL(Items.SuggestedPrice, 0.0)),
		'DATA IMPORT ' + CONVERT(NVARCHAR(10), GETDATE(), 120),
		MAX(ISNULL(Items.SKU, '')), 0, MAX(ISNULL(Items.ProjectedLife, 0)), MAX(ISNULL(Items.OtherField1, '')),
		MAX(ISNULL(Items.OtherField2, '')), MAX(ISNULL(Items.OtherField3, '')), 1, 0, GETDATE(), 0, GETDATE()
		FROM _ETL_Items AS Items
		LEFT JOIN tblTechItems ON UPPER(Items.ProductName) = UPPER(tblTechItems.ItemName)
		AND UPPER(Items.Model) = UPPER(ISNULL(tblTechItems.ModelNumber, ''))
		AND Items.ManufacturerUID = tblTechItems.ManufacturerUID
		WHERE tblTechItems.ItemUID IS NULL
		GROUP BY Items.ProductName, Items.Model, Items.ManufacturerUID
		SELECT @COUNT = @@ROWCOUNT
		SELECT @PRODNUM = @PRODNUM + @COUNT
		IF @COUNT > 0
			BEGIN
				UPDATE tblUnvCounter SET Value = @PRODNUM + 1 WHERE CounterUID = 4
			END
	END
ELSE
	BEGIN
		INSERT INTO tblTechItems (ItemNumber, ItemName, ItemDescription, ItemTypeUID, ModelNumber, ManufacturerUID,
		ItemSuggestedPrice, ItemNotes, SKU, SerialRequired, ProjectedLife, CustomField1, CustomField2,
		CustomField3, Active, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
		SELECT MAX(ISNULL(Items.ProductNumber, '')),
		Items.ProductName, MAX(ISNULL(Items.ProductDescription, '')), MAX(ISNULL(Items.ItemTypeUID, 0)),
		Items.Model, Items.ManufacturerUID, MAX(ISNULL(Items.SuggestedPrice, 0.0)),
		'DATA IMPORT ' + CONVERT(NVARCHAR(10), GETDATE(), 120),
		MAX(ISNULL(Items.SKU, '')), 0, MAX(ISNULL(Items.ProjectedLife, 0)), MAX(ISNULL(Items.OtherField1, '')),
		MAX(ISNULL(Items.OtherField2, '')), MAX(ISNULL(Items.OtherField3, '')), 1, 0, GETDATE(), 0, GETDATE()
		FROM _ETL_Items AS Items
		LEFT JOIN tblTechItems ON UPPER(Items.ProductName) = UPPER(tblTechItems.ItemName)
		AND UPPER(Items.Model) = UPPER(ISNULL(tblTechItems.ModelNumber, ''))
		AND Items.ManufacturerUID = tblTechItems.ManufacturerUID
		WHERE tblTechItems.ItemUID IS NULL
		GROUP BY Items.ProductName, Items.Model, Items.ManufacturerUID
	END

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 2 AND ETLSettingValue = 'False') = 0
	BEGIN
		UPDATE _ETL_Items
		SET ItemUID = tblTechItems.ItemUID, ProductNumber = tblTechItems.ItemNumber
		FROM _ETL_Items AS Items
		JOIN tblTechItems ON UPPER(Items.ProductName) = UPPER(tblTechItems.ItemName)
		AND UPPER(Items.Model) = UPPER(ISNULL(tblTechItems.ModelNumber, ''))
		AND Items.ManufacturerUID = tblTechItems.ManufacturerUID
		WHERE (Items.ItemUID IS NULL
		OR Items.ItemUID <> tblTechItems.ItemUID
		OR Items.ProductNumber IS NULL
		OR Items.ProductNumber <> tblTechItems.ItemNumber)
	END
ELSE
	BEGIN
		UPDATE _ETL_Items
		SET ItemUID = tblTechItems.ItemUID
		FROM _ETL_Items AS Items
		JOIN tblTechItems ON UPPER(Items.ProductName) = UPPER(tblTechItems.ItemName)
		AND UPPER(Items.Model) = UPPER(ISNULL(tblTechItems.ModelNumber, ''))
		AND Items.ManufacturerUID = tblTechItems.ManufacturerUID
		WHERE (Items.ItemUID IS NULL
		OR Items.ItemUID <> tblTechItems.ItemUID)
	END
	
UPDATE _ETL_Items
SET ItemUID = 0
WHERE ItemUID IS NULL

IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 1 AND ETLSettingValue = 'ProductNumber') = 0
	BEGIN
		UPDATE _ETL_Items
		SET Product = ProductName
	END
ELSE
	BEGIN
		UPDATE _ETL_Items
		SET Product = ProductNumber
	END
