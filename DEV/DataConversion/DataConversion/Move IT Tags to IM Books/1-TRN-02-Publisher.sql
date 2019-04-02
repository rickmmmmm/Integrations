insert into tblVendor  (VendorName, CampusID, Notes, Active, UserID, ModifiedDate, ApplicationUID)

select distinct tm.PublisherName, 0, 'Hayes Move Items from TIPWeb-IT', 1, 0, getdate(), 1
from tblVendor v
right outer join Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm on v.VendorName = tm.PublisherName
where [action] = 'transfer'
and v.VendorID is null and tm.PublisherName is not null
order by 1


 select distinct tm.PublisherName, v.VendorName
 --update tm set PublisherID = v.VendorID
from tblVendor v
join Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm on v.VendorName = tm.PublisherName
  where [action] = 'transfer'
and tm.PublisherID is null

