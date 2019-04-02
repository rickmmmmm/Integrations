--insert into tblBookInventory (ISBN, slc, Title, Price, Publisher, UserID, ModifiedDate, notes, grade)

select distinct tm.ISBN, '', tm.Title, PurchasePrice, PublisherID, 0, getdate(), 'Hayes Move Items from TIPWeb-IT', ''
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
left outer join tblBookInventory bi on tm.ISBN = bi.ISBN
where [action] = 'transfer'
and bi.BookInventoryUID is null

--252 total / 242 to add

select *
--update tm set BookInventoryUID = bi.BookInventoryUID
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
left outer join tblBookInventory bi on tm.ISBN = bi.ISBN
where [action] = 'transfer'
and tm.BookInventoryUID is null