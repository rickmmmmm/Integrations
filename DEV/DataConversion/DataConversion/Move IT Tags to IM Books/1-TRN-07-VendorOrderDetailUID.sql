--select * from tblVendorOrderDetails where vendororderuid = 2175


--insert into tblVendorOrderDetails (VendorOrderUID, BookInventoryUID, 
--FundingSourceUID, Ordered, DateOrdered, Received, DateReceived, StatusID,
--Price, UserID, DateModified, DateArriving)

select VendorOrderUID, BookInventoryUID, fundingSourceUID, count(ISBN), getdate(), Count(ISBN), getdate(), 2, isNull(PurchasePrice, 0), 0, getdate(), getdate()
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
where [action] = 'transfer'
group by VendorOrderUID, BookInventoryUID, fundingSourceUID, PurchasePrice

--294

select *
--update tm set VendorOrderDetailsUID = vod.VendorOrderDetailsUID
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
join tblVendorOrderDetails vod on tm.VendorOrderUID = vod.VendorOrderUID and tm.BookInventoryUID = vod.BookInventoryUID
	and tm.fundingSourceUID = vod.fundingSourceUID and isNull(tm.PurchasePrice, 0) = isNull(vod.Price, 0)
where [action] = 'transfer'
--22905


--select distinct price from tblBookInventory where BookInventoryUID in (
--select BookInventoryUID from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm where [action] = 'transfer')





