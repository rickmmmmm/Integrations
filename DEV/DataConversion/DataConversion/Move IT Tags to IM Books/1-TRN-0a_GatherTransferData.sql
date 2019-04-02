

select count(*) from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM where [action]= 'transfer'
--22905

select distinct tm.ORderNumber, --v.VendorName, 
m.ManufacturerName, ttm.ItemName, ttm.ItemNumber, 
tty.ItemTypeName, tti.PurchasePrice, f.FundingSource, tti.PurchaseDate,  tm.*
--update tm set PublisherName = m.ManufacturerName, 
--VendorName = v.VendorName, FundingSource = f.FundingSource, Title= ttm.ItemName, ItemName= ttm.ItemName,
--ISBN=ttm.ItemNumber, ItemNumber = ttm.ItemNumber, ItemType = tty.ItemTypeName, PurchasePrice = tti.PurchasePrice
from tblTechInventory tti
join Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm on tti.InventoryUID = tm.InventoryUID
join tblTechItems ttm on tti.ItemUID = ttm.ItemUID
join tblTechItemTypes tty on ttm.ItemTypeUID = tty.ItemTypeUID
join tblUnvManufacturers m on ttm.ManufacturerUID = m.ManufacturerUID
join tblTechPurchases tpur on tm.PUrchaseUID = tpur.PurchaseUID
join tblVendor v on tpur.VendorUID = v.VendorID
join tblFundingSources f on tti.FundingSourceUID = f.FundingSourceUID
where tm.Action = 'TRANSFER'
and tm.PurchaseUID is not null
--21942


select distinct tm.ORderNumber, 
m.ManufacturerName, ttm.ItemName, ttm.ItemNumber, 
tty.ItemTypeName, tti.PurchasePrice, f.FundingSource, tti.PurchaseDate,  tm.*
--update tm set PublisherName = m.ManufacturerName,  FundingSource = f.FundingSource, Title= ttm.ItemName, ItemName= ttm.ItemName,
--ISBN=ttm.ItemNumber, ItemNumber = ttm.ItemNumber, ItemType = tty.ItemTypeName, PurchasePrice = tti.PurchasePrice
from tblTechInventory tti
join Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm on tti.InventoryUID = tm.InventoryUID
join tblTechItems ttm on tti.ItemUID = ttm.ItemUID
join tblTechItemTypes tty on ttm.ItemTypeUID = tty.ItemTypeUID
join tblUnvManufacturers m on ttm.ManufacturerUID = m.ManufacturerUID
join tblFundingSources f on tti.FundingSourceUID = f.FundingSourceUID
where tm.Action = 'TRANSFER'
and tm.PurchaseUID is null
--963
