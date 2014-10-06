INSERT INTO _ETL_Export (New, Tag, Serial, FundingSource, PurchasePrice, PurchaseDate,
PurchaseOrder, Vendor, VendorAccountNumber, ProductName, Model,
Area, [Site], Location, [Status])
SELECT 1, Inv.Tag, Inv.Serial, tblFundingSources.FundingSource, Inv.PurchasePrice, Inv.PurchaseDate,
tblTechPurchases.OrderNumber, tblVendor.VendorName, tblVendor.AccountNumber, tblTechItems.ItemName, tblTechItems.ModelNumber,
tblUnvAreas.AreaName, tblTechSites.SiteID,
CASE
	WHEN Inv.EntityTypeUID = 2 THEN 'Room ' + tblUnvRooms.RoomNumber
	WHEN Inv.EntityTypeUID = 3 THEN 'Staff ' + tblTeachers.TeacherID
	WHEN Inv.EntityTypeUID = 4 THEN 'Student ' + tblStudents.StudentID
	ELSE ''
END, 'ACTIVE'
FROM tblTechInventory AS Inv
JOIN tblFundingSources ON Inv.FundingSourceUID = tblFundingSources.FundingSourceUID
JOIN tblTechPurchaseInventory ON Inv.InventoryUID = tblTechPurchaseInventory.InventoryUID
JOIN tblTechPurchaseItemShipments ON tblTechPurchaseInventory.PurchaseItemShipmentUID = tblTechPurchaseItemShipments.PurchaseItemShipmentUID
JOIN tblTechPurchaseItemDetails ON tblTechPurchaseItemShipments.PurchaseItemDetailUID = tblTechPurchaseItemDetails.PurchaseItemDetailUID
JOIN tblTechPurchases ON tblTechPurchaseItemDetails.PurchaseUID = tblTechPurchases.PurchaseUID
JOIN tblVendor ON tblTechPurchases.VendorUID = tblVendor.VendorID
JOIN tblTechItems ON Inv.ItemUID = tblTechItems.ItemUID
JOIN tblUnvAreas ON tblTechItems.AreaUID = tblUnvAreas.AreaUID
JOIN tblTechSites ON Inv.SiteUID = tblTechSites.SiteUID
LEFT JOIN tblUnvRooms ON Inv.EntityUID = tblUnvRooms.RoomUID
LEFT JOIN tblStudents ON Inv.EntityUID = tblStudents.StudentsUID
LEFT JOIN tblTeachers ON Inv.EntityUID = tblTeachers.TeachersUID
LEFT JOIN _ETL_Inventory ON Inv.AssetID = _ETL_Inventory.AssetID
WHERE (Inv.AssetID IS NULL
OR _ETL_Inventory.AssetID IS NULL)
AND Inv.ArchiveUID = 0

UPDATE _ETL_Inventory
SET Location = ''
WHERE Location IS NULL

UPDATE _ETL_Inventory
SET Modified = 1
FROM _ETL_Inventory AS Tags
JOIN tblTechInventory AS Inv ON Tags.InventoryUID = Inv.InventoryUID
LEFT JOIN tblUnvRooms ON Inv.EntityUID = tblUnvRooms.RoomUID
LEFT JOIN tblStudents ON Inv.EntityUID = tblStudents.StudentsUID
LEFT JOIN tblTeachers ON Inv.EntityUID = tblTeachers.TeachersUID
WHERE Tags.Tag <> Inv.Tag
OR Tags.Serial <> Inv.Serial
OR Tags.ItemUID <> Inv.ItemUID
OR Tags.SiteUID <> Inv.SiteUID
OR Tags.Location <>
CASE
	WHEN Inv.EntityTypeUID = 2 THEN 'Room ' + tblUnvRooms.RoomNumber
	WHEN Inv.EntityTypeUID = 3 THEN 'Staff ' + tblTeachers.TeacherID
	WHEN Inv.EntityTypeUID = 4 THEN 'Student ' + tblStudents.StudentID
	ELSE ''
END
OR Inv.ArchiveUID > 0

INSERT INTO _ETL_Export (New, Tag, Serial, FundingSource, PurchasePrice, PurchaseDate,
PurchaseOrder, Vendor, VendorAccountNumber, ProductName, Model,
Area, [Site], Location, [Status])
SELECT 0, Inv.Tag, Inv.Serial, tblFundingSources.FundingSource, Inv.PurchasePrice, Inv.PurchaseDate,
tblTechPurchases.OrderNumber, tblVendor.VendorName, tblVendor.AccountNumber, tblTechItems.ItemName, tblTechItems.ModelNumber,
tblUnvAreas.AreaName, tblTechSites.SiteID,
CASE
	WHEN Inv.EntityTypeUID = 2 THEN 'Room ' + tblUnvRooms.RoomNumber
	WHEN Inv.EntityTypeUID = 3 THEN 'Staff ' + tblTeachers.TeacherID
	WHEN Inv.EntityTypeUID = 4 THEN 'Student ' + tblStudents.StudentID
	ELSE ''
END, 'ACTIVE'
FROM tblTechInventory AS Inv
JOIN tblFundingSources ON Inv.FundingSourceUID = tblFundingSources.FundingSourceUID
JOIN tblTechPurchaseInventory ON Inv.InventoryUID = tblTechPurchaseInventory.InventoryUID
JOIN tblTechPurchaseItemShipments ON tblTechPurchaseInventory.PurchaseItemShipmentUID = tblTechPurchaseItemShipments.PurchaseItemShipmentUID
JOIN tblTechPurchaseItemDetails ON tblTechPurchaseItemShipments.PurchaseItemDetailUID = tblTechPurchaseItemDetails.PurchaseItemDetailUID
JOIN tblTechPurchases ON tblTechPurchaseItemDetails.PurchaseUID = tblTechPurchases.PurchaseUID
JOIN tblVendor ON tblTechPurchases.VendorUID = tblVendor.VendorID
JOIN tblTechItems ON Inv.ItemUID = tblTechItems.ItemUID
JOIN tblUnvAreas ON tblTechItems.AreaUID = tblUnvAreas.AreaUID
JOIN tblTechSites ON Inv.SiteUID = tblTechSites.SiteUID
LEFT JOIN tblUnvRooms ON Inv.EntityUID = tblUnvRooms.RoomUID
LEFT JOIN tblStudents ON Inv.EntityUID = tblStudents.StudentsUID
LEFT JOIN tblTeachers ON Inv.EntityUID = tblTeachers.TeachersUID
JOIN _ETL_Inventory AS Tags ON Inv.AssetID = Tags.AssetID
WHERE Tags.Modified = 1

UPDATE _ETL_Export
SET [Status] = 'MISSING'
FROM _ETL_Export AS Tags
JOIN tblTechInventory AS Inv ON Tags.Tag = Inv.Tag
WHERE Inv.ArchiveUID > 0
AND Inv.StatusUID IN (47, 48, 49)
AND UPPER(Tags.[Status]) <> 'MISSING'

UPDATE _ETL_Export
SET [Status] = 'STOLEN'
FROM _ETL_Export AS Tags
JOIN tblTechInventory AS Inv ON Tags.Tag = Inv.Tag
WHERE Inv.ArchiveUID > 0
AND Inv.StatusUID IN (63, 64, 65)
AND UPPER(Tags.[Status]) <> 'STOLEN'

UPDATE _ETL_Export
SET [Status] = 'RETURNED TO VENDOR'
FROM _ETL_Export AS Tags
JOIN tblTechInventory AS Inv ON Tags.Tag = Inv.Tag
WHERE Inv.ArchiveUID > 0
AND Inv.StatusUID IN (66, 67, 68)
AND UPPER(Tags.[Status]) <> 'RETURNED TO VENDOR'

UPDATE _ETL_Export
SET [Status] = 'AUCTION'
FROM _ETL_Export AS Tags
JOIN tblTechInventory AS Inv ON Tags.Tag = Inv.Tag
WHERE Inv.ArchiveUID > 0
AND Inv.StatusUID IN (77, 78, 79)
AND UPPER(Tags.[Status]) <> 'AUCTION'

UPDATE _ETL_Export
SET [Status] = 'ERRONEOUS ENTRY'
FROM _ETL_Export AS Tags
JOIN tblTechInventory AS Inv ON Tags.Tag = Inv.Tag
WHERE Inv.ArchiveUID > 0
AND Inv.StatusUID = 80
AND UPPER(Tags.[Status]) <> 'ERRONEOUS ENTRY'
