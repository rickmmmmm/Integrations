/* The code below should take care of the following requirements:
Part 2: Update Logic for Existing Field
•	InsideGeofence
•	Delete the custom field associated to all Product Types in the db.
•	Send this data to the Lat/Long Field instead
*/

DECLARE @MDMFieldUID INT
SELECT @MDMFieldUID = MDMFieldUID FROM tblTechMDMField WHERE MDMSourceTypeUID = 3 AND FieldUniqueName = 'LATLONG'


INSERT INTO [dbo].[tblTechInventoryMDM]
([InventoryUID],[MDMFieldUID],[Value], CreatedByUserID, createddate, LastModifiedByUserID, lastmodifieddate)
select ti.InventoryUID, @MDMFieldUID as MDMFieldUID, ext.InventoryExtValue AS Value , 0 as CreatedByUserID, Getdate() as createddate, 0 as LastModifiedByUserID, getdate() as lastmodifieddate 
from [dbo].[tblTechInventory] ti
inner join
(
	select  inv.Serial, iext.InventoryExtValue
	from tblTechInventory inv  
	inner join tblTechInventoryExt iext on inv.InventoryUID = iext.InventoryUID
	inner join tblTechInventoryMeta m on iext.InventoryMetaUID = m.InventoryMetaUID
	join tblTechItemTypes tty on m.ItemTypeUID = tty.ItemTypeuid
	where m.InventoryMetaOrder = '4'
) ext
on ti.Serial = ext.Serial

/*
The code below should take care of the following requirements:
Part 3: Create New Custom Field
•	LTE Connection - needs to be added to the product types listed in the data mapping document
•	2 in 1
•	Cellular Phone
•	Laptop(chromebook)
•	Laptop(Macbook)
•	Laptop(PC)
•	Tablet/IPAD
•	LTE Connection - true or false field, NOT required
•	Double check all Product Types listed have the listed Custom Fields on the data mapping document

*/

insert tblTechInventoryMeta (ItemTYpeuid, InventoryMetaLabel, InventoryMetaType, InventoryMetaRequired, InventoryMetaOrder)
select ItemTypeUID, 'LTE Connection', 'Boolean', 0, '4'
from tblTechItemTypes 
where itemtypename in ('2 in 1', 'Cellular Phone', 'Laptop(chromebook)', 'LAPTOP (MACBOOK)', 'LAPTOP (PC)', 'TABLET/IPAD')