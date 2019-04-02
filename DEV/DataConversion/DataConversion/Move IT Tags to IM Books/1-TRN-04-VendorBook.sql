

--insert into tblVendorBooks (VendorID, ISBN, UserID, ModifiedDate)

select distinct publisherID, tm.ISBN, 0, getdate()
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
left outer join tblVendorBooks vb on tm.ISBN = vb.ISBN and tm.PublisherID = vb.VendorID
where action = 'transfer' 
and vb.ISBN is null

--only 3 match / 256 to add

