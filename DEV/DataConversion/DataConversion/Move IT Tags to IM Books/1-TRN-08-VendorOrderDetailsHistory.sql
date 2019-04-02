

insert into tblVendorOrderDetailsHistory (VendorOrderDetailsUID, Received, DateReceived, UserID)

select distinct vod.VendorOrderDetailsUID, vod.Received, getdate(), 0
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
join tblVendorOrderDetails vod on tm.vendororderdetailsuid = vod.vendororderdetailsuid
where [action] = 'transfer'


--select *
----delete
--from tblVendorOrderDetailsHistory
--where dateReceived > '3/29/2019'