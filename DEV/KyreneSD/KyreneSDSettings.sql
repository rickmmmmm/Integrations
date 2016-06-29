
/*----------------------
--CONFIGURE SETTINGS--

Purpose:  Intgration settings configurations.
	one time deal.  Once you create the customer 
	and it is executed, it should never be changed, 
	unless the customer makes a change

----------------------*/
--ProductField: Which field is used to match asset to catalog
DECLARE @ProductField AS VARCHAR(50) = 'ProductName'--'ProductName' or 'ProductNumber'
--TIPWebProductNumbers: True if we generate autoincremented TIPWeb-IT Product Numbers
DECLARE @TIPWebProductNumbers AS VARCHAR(50) = 'True'--'True' or 'False'
--SiteField: Which field is provided to determine site
DECLARE @SiteField AS VARCHAR(50) = 'SiteID'--'SiteName' or 'SiteID'
--RoomDefault: Default room to put assets without location information
DECLARE @RoomDefault AS VARCHAR(50) = 'Receiving'--'<Anything>' Default Room
--StatusDefault: Default status to put assets without status information
DECLARE @StatusDefault AS VARCHAR(50) = 'Available'--'Available' or 'In Use'
--TagUpdate: True if we update Tag field from their data
DECLARE @TagUpdate AS VARCHAR(50) = 'False'--'True' or 'False'
--Custom Field 1: Definition of Custom Field 1
DECLARE @CustomField1Label AS VARCHAR(50) = 'Computer name'--'' or '<Anything>'
DECLARE @CustomField1Type AS VARCHAR(50) = 'String'--'String' or 'Integer' or 'Boolean' or 'DateTime' or 'Double' or 'Percent'
--Custom Field 2: Definition of Custom Field 2
DECLARE @CustomField2Label AS VARCHAR(50) = 'Last check in date'--'' or '<Anything>'
DECLARE @CustomField2Type AS VARCHAR(50) = 'String'--'String' or 'Integer' or 'Boolean' or 'DateTime' or 'Double' or 'Percent'
--Custom Field 3: Definition of Custom Field 3
DECLARE @CustomField3Label AS VARCHAR(50) = ''--'' or '<Anything>'
DECLARE @CustomField3Type AS VARCHAR(50) = 'String'--'String' or 'Integer' or 'Boolean' or 'DateTime' or 'Double' or 'Percent'
--Custom Field 4: Definition of Custom Field 4
DECLARE @CustomField4Label AS VARCHAR(50) = ''--'' or '<Anything>'
DECLARE @CustomField4Type AS VARCHAR(50) = 'String'--'String' or 'Integer' or 'Boolean' or 'DateTime' or 'Double' or 'Percent'
--TIPWebTags: True if assets can originate in TIPWeb-IT
DECLARE @TIPWebTags AS VARCHAR(50) = 'False'--'True' or 'False'
--ProductUpdate: True if we update catalog information for an asset
DECLARE @ProductUpdate AS VARCHAR(50) = 'False'--'True' or 'False'
--PurchaseDetailLineNumbers: True if line numbers are provided to match POs from their system to TIPWeb-IT
DECLARE @PurchaseDetailLineNumbers AS VARCHAR(50) = 'False'--'True' or 'False'
--FundingUpdate: True if we update funding information of an asset
DECLARE @FundingUpdate AS VARCHAR(50) = 'False'--'True' or 'False'
--Insert Products from Purchases: True if catalog information is included in a purchase integration
DECLARE @ProductsFromPurchases AS VARCHAR(50) = 'False'--'True' or 'False'
--Insert Products from Inventory: True if catalog information is included in an asset integration
DECLARE @ProductsFromInventory AS VARCHAR(50) = 'False'--'True' or 'False'
--Insert Rooms: True if Room Numbers are created by the integration
DECLARE @InsertRooms AS VARCHAR(50) = 'False'--'True' or 'False'

--ProductField
--This determines if the Inventory and Purchases
--Join to Items by ProductName or ProductNumber
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 1) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (1, 'ProductField', @ProductField)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'ProductField', ETLSettingValue = @ProductField
		WHERE ETLSettingUID = 1
	END

--TIPWebProductNumbers
--This determines if they will provide Product Numbers
--Or use TIPWeb autogenerated Product Numbers
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 2) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (2, 'TIPWebProductNumbers', @TIPWebProductNumbers)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'TIPWebProductNumbers', ETLSettingValue = @TIPWebProductNumbers
		WHERE ETLSettingUID = 2
	END

--SiteField
--This determines if the Inventory and Purchases
--Join to Sites by SiteName or SiteID
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 3) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (3, 'SiteField', @SiteField)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'SiteField', ETLSettingValue = @SiteField
		WHERE ETLSettingUID = 3
	END

--RoomDefault
--This determines the default Room to assign
--Inventory if Room is not supplied
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 4) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (4, 'RoomDefault', @RoomDefault)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'RoomDefault', ETLSettingValue = @RoomDefault
		WHERE ETLSettingUID = 4
	END

--StatusDefault
--This determines the default Status
--Inventory is given
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 5) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (5, 'StatusDefault', @StatusDefault)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'StatusDefault', ETLSettingValue = @StatusDefault
		WHERE ETLSettingUID = 5
	END

--TagUpdate
--This determines if we Update Tag
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 6) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (6, 'TagUpdate', @TagUpdate)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'TagUpdate', ETLSettingValue = @TagUpdate
		WHERE ETLSettingUID = 6
	END

--CustomField1Label
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 7) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (7, 'CustomField1Label', @CustomField1Label)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'CustomField1Label', ETLSettingValue = @CustomField1Label
		WHERE ETLSettingUID = 7
	END

--CustomField1Type
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 8) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (8, 'CustomField1Type', @CustomField1Type)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'CustomField1Type', ETLSettingValue = @CustomField1Type
		WHERE ETLSettingUID = 8
	END

--CustomField2Label
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 9) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (9, 'CustomField2Label', @CustomField2Label)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'CustomField2Label', ETLSettingValue = @CustomField2Label
		WHERE ETLSettingUID = 9
	END

--CustomField2Type
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 10) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (10, 'CustomField2Type', @CustomField2Type)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'CustomField2Type', ETLSettingValue = @CustomField2Type
		WHERE ETLSettingUID = 10
	END

--CustomField3Label
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 11) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (11, 'CustomField3Label', @CustomField3Label)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'CustomField3Label', ETLSettingValue = @CustomField3Label
		WHERE ETLSettingUID = 11
	END

--CustomField3Type
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 12) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (12, 'CustomField3Type', @CustomField3Type)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'CustomField3Type', ETLSettingValue = @CustomField3Type
		WHERE ETLSettingUID = 12
	END

--CustomField4Label
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 13) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (13, 'CustomField4Label', @CustomField4Label)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'CustomField4Label', ETLSettingValue = @CustomField4Label
		WHERE ETLSettingUID = 13
	END

--CustomField4Type
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 14) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (14, 'CustomField4Type', @CustomField4Type)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'CustomField4Type', ETLSettingValue = @CustomField4Type
		WHERE ETLSettingUID = 14
	END

--TIPWebTags
--This determines if tags can be originated in TIPWeb
--Then assigned AssetIDs from the integrated system later
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 15) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (15, 'TIPWebTags', @TIPWebTags)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'TIPWebTags', ETLSettingValue = @TIPWebTags
		WHERE ETLSettingUID = 15
	END

--ProductUpdate
--This determines if we Update Product
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 16) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (16, 'ProductUpdate', @ProductUpdate)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'ProductUpdate', ETLSettingValue = @ProductUpdate
		WHERE ETLSettingUID = 16
	END

--PurchaseDetailLineNumbers
--This determines if line numbers are provided for Purchase Details
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 17) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (17, 'PurchaseDetailLineNumbers', @PurchaseDetailLineNumbers)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'PurchaseDetailLineNumbers', ETLSettingValue = @PurchaseDetailLineNumbers
		WHERE ETLSettingUID = 17
	END

--FundingUpdate
--This determines if we Update Funding Source and Price
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 18) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (18, 'FundingUpdate', @FundingUpdate)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'FundingUpdate', ETLSettingValue = @FundingUpdate
		WHERE ETLSettingUID = 18
	END

--ProductsFromPurchases
--This determines if we add Products from the Purchases Import to the Catalog
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 19) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (19, 'ProductsFromPurchases', @ProductsFromPurchases)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'ProductsFromPurchases', ETLSettingValue = @ProductsFromPurchases
		WHERE ETLSettingUID = 19
	END

--ProductsFromInventory
--This determines if we add Products from the Inventory Import to the Catalog
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 20) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (20, 'ProductsFromInventory', @ProductsFromInventory)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'ProductsFromInventory', ETLSettingValue = @ProductsFromInventory
		WHERE ETLSettingUID = 20
	END

--InsertRooms
--This determines if we insert new rooms from the Inventory Import
IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 21) = 0
	BEGIN
		INSERT INTO [dbo].[_ETL_Settings] ([ETLSettingUID], [ETLSettingName], [ETLSettingValue])
		VALUES (21, 'InsertRooms', @InsertRooms)
	END
ELSE
	BEGIN
		UPDATE [dbo].[_ETL_Settings]
		SET ETLSettingName = 'InsertRooms', ETLSettingValue = @InsertRooms
		WHERE ETLSettingUID = 21
	END

GO
