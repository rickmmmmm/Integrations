

----------- These fields have a display order of 0 where MDMSourceTypeUID = 1, so they would not display in the application.  Updating the Display Order

--update tblTechMDMField set DisplayOrder = 9 where mdmfielduid = 9   ---MACAddress
--update tblTechMDMField set DisplayOrder = 8 where mdmfielduid = 10	---OS
--update tblTechMDMField set DisplayOrder = 5 where mdmfielduid = 4	---LastSeenDate
--update tblTechMDMField set DisplayOrder = 6 where mdmfielduid = 6	---ExternalIP
--update tblTechMDMField set DisplayOrder = 7 where mdmfielduid = 7	---Internal



/* The code below should take care of the following requirements:
Part 2: Update Logic for Existing Field
•	InsideGeofence
•	Delete the custom field associated to all Product Types in the db.
•	Send this data to the Lat/Long Field instead
*/


--DECLARE @MDMFieldUID INT
--SELECT @MDMFieldUID = MDMFieldUID FROM tblTechMDMField WHERE MDMSourceTypeUID = 3 AND FieldUniqueName = 'LATLONG'

--INSERT INTO [dbo].[tblTechInventoryMDM]
--([InventoryUID],[MDMFieldUID],[Value], CreatedByUserID, createddate, LastModifiedByUserID, lastmodifieddate)


select Distinct ti.InventoryUID, @MDMFieldUID, ext.InventoryExtValue AS Value , 0 as CreatedByUserID, Getdate() as createddate, 0 as LastModifiedByUserID, getdate() as lastmodifieddate 
from [dbo].[tblTechInventory] ti
inner join
(
	select  inv.InventoryUID, iext.InventoryExtValue
	from tblTechInventory inv  
	inner join tblTechInventoryExt iext on inv.InventoryUID = iext.InventoryUID
	inner join tblTechInventoryMeta m on iext.InventoryMetaUID = m.InventoryMetaUID
	join tblTechItemTypes tty on m.ItemTypeUID = tty.ItemTypeuid
	where itemtypename in ('2 in 1', 'all in 1 desktop pc', 'CELLULAR PHONE', 'CHROMEBOOK','DESKTOP', 'LAPTOP (CHROMEBOOK)', 'LAPTOP (MACBOOK)', 'LAPTOP (PC)', 'TABLET/IPAD' )
	and m.InventoryMetaLabel = 'Inside GeoFence'
) ext
on ti.InventoryUID = ext.InventoryUID

---52749

---------- Delete values from custom field data for Inside GeoFence

select *
---delete x
from tblTechInventoryExt x
where InventoryMetaUID in (
	select m.InventoryMetaUID  --m.*, 
	from tblTechItemtypes  ttm
	join tblTechInventoryMeta m on ttm.ItemTYpeUID = m.ItemTypeUID
	where InventoryMetaLabel like '%geo%'
	)


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

----- rename the old custom fiedl

select ttm.ItemTypeName, m.*
--update m set InventoryMetaLabel = 'LTE Connection', InventoryMetaType = 'Boolean'
from tblTechItemtypes  ttm
join tblTechInventoryMeta m on ttm.ItemTYpeUID = m.ItemTypeUID
where ItemTypeName in ('2 in 1', 'all in 1 desktop pc', 'CELLULAR PHONE', 'CHROMEBOOK','DESKTOP', 'LAPTOP (CHROMEBOOK)', 'LAPTOP (MACBOOK)', 'LAPTOP (PC)', 'TABLET/IPAD' )
and InventoryMetaLabel like '%geo%'
and InventoryMetaOrder = '4'

order by itemtypename, InventoryMetaOrder


--------Insert new custom fields


--insert tblTechInventoryMeta (ItemTYpeuid, InventoryMetaLabel, InventoryMetaType, InventoryMetaRequired, InventoryMetaOrder)
select tty.ItemTypeUID, 'User Group', 'String', 0, '1'
from tblTechInventoryMeta m
right join tblTechItemTypes tty on m.ItemTypeUID = tty.ItemTypeUID and m.InventoryMetaLabel = 'User Group'
where itemtypename in ('2 in 1', 'all in 1 desktop pc', 'CELLULAR PHONE', 'CHROMEBOOK','DESKTOP', 'LAPTOP (CHROMEBOOK)', 'LAPTOP (MACBOOK)', 'LAPTOP (PC)', 'TABLET/IPAD' )
and m.InventoryMetaUID is null
union
select tty.ItemTypeUID, 'Take Home', 'Boolean', 0, '2'
from tblTechInventoryMeta m
right join tblTechItemTypes tty on m.ItemTypeUID = tty.ItemTypeUID and m.InventoryMetaLabel = 'Take Home'
where itemtypename in ('2 in 1', 'all in 1 desktop pc', 'CELLULAR PHONE', 'CHROMEBOOK','DESKTOP', 'LAPTOP (CHROMEBOOK)', 'LAPTOP (MACBOOK)', 'LAPTOP (PC)', 'TABLET/IPAD' )
and m.InventoryMetaUID is null
union
select tty.ItemTypeUID, 'At Risk', 'String', 0, '3'
from tblTechInventoryMeta m
right join tblTechItemTypes tty on m.ItemTypeUID = tty.ItemTypeUID and m.InventoryMetaLabel = 'At Risk'
where itemtypename in ('2 in 1', 'all in 1 desktop pc', 'CELLULAR PHONE', 'CHROMEBOOK','DESKTOP', 'LAPTOP (CHROMEBOOK)', 'LAPTOP (MACBOOK)', 'LAPTOP (PC)', 'TABLET/IPAD' )
and m.InventoryMetaUID is null
union
select tty.ItemTypeUID, 'LTE Connection', 'Boolean', 0, '4'
from tblTechInventoryMeta m
right join tblTechItemTypes tty on m.ItemTypeUID = tty.ItemTypeUID and m.InventoryMetaLabel = 'LTE Connection'
where itemtypename in ('2 in 1',  'CELLULAR PHONE', 'LAPTOP (CHROMEBOOK)', 'LAPTOP (MACBOOK)', 'LAPTOP (PC)', 'TABLET/IPAD' )
and m.InventoryMetaUID is null




