

--insert into dbo.tblVendorOrders (VendorOrderID, DateCreated, OrderStatus, PurchaseOrder, VendorID, ArrivalDate, SpecialInstructions, DateSubmitted, UserID, CampusID, DistrictCreated, FundingSourceUID, DateModified)

select 'Hayes Move Items from TIPWeb-IT',  --VendorOrderID, 
GETDATE(), --DateCreated
2, --OrderStatus complete
NULL,  --PurchaseOrder 
0, --VendorID
GETDATE(),  -- Arrival Date
NULL, --Special Instructions
NULL, --DateSubmitted
0,--userID
0, --campusID 
'TRUE', --district Created 
3, ---FundingSourceUID
GETDATE() --Date Modified

---no from or where - looking for 1 row to insert


select * from tblVendorORders where VendorOrderID = 'Hayes Move Items from TIPWeb-IT'
---UID 2177

select *
--update tm set VendorOrderUID = (select VendorOrderUID from tblVendorORders where VendorOrderID = 'Hayes Move Items from TIPWeb-IT')
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm 
where [action] = 'transfer' 



