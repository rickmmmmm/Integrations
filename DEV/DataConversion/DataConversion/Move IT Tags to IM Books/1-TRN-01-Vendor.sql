

insert into tblVendor  (VendorName, CampusID, Notes, Active, UserID, ModifiedDate, ApplicationUID)

select distinct tm.VendorName, 0, 'Hayes Move Items from TIPWeb-IT', 1, 0, getdate(), 1
from tblVendor v
right outer join Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm on v.VendorName = tm.VendorName
 where [action] = 'transfer'
and v.VendorID is null
order by 1


 select distinct tm.VendorName, v.VendorName
 --update tm set VendorID = v.VendorID
from tblVendor v
join Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm on v.VendorName = tm.VendorName
  where [action] = 'transfer'
and tm.VendorID is null

