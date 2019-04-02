

--INSERT INTO tblDistrictDistributions (ISBN, Code, Source, Amount, Copies, Notes, UserID, ModifiedDate)

 SELECT  ISBN, 'DIST', 'Vendor Order' as source, 0 as Amount, Received as copies, 'Hayes Move Items from TIPWeb-IT',  0 as userid, getdate() as modifieddate
 FROM tblBookInventory bi
 join tblVendorOrderDetails vod on bi.BookInventoryUID = vod.BookInventoryUID
where VendorOrderDetailsUID in (select VendorOrderDetailsUID from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM where [action] = 'transfer')
--294


--INSERT INTO tblDistrictDistributions_Tx (Distributionid, ISBN, Code, Source, Amount, Copies, Notes, UserID, ModifiedDate, ActionTaken)

select Distributionid, ISBN, Code, Source, Amount, Copies, Notes, UserID, ModifiedDate, 'INSERTED'
from tblDistrictDistributions
where notes = 'Hayes Move Items from TIPWeb-IT'
--294



select * 
--update tm set DistributionID = dd.DistributionID
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
join tblVendorOrderDetails vod on tm.VendorOrderDetailsUID = vod.VendorOrderDetailsUID
join tblBookInventory bi on tm.BookInventoryUID = bi.BookInventoryUID
join tblDistrictDistributions dd on bi.ISBN = dd.ISBN and vod.Received = dd.Copies
where [action] = 'transfer'
and dd.Notes = 'Hayes Move Items from TIPWeb-IT' 
order by 1




