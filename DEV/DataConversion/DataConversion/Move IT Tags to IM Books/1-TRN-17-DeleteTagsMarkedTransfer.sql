
select t.*
--delete t
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechTagHistory t on t2m.InventoryUID = t.InventoryUID
where action = 'TRANSFER'


select t.*
--delete t
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechInventoryDetails t on t2m.InventoryUID = t.InventoryUID
where action = 'TRANSFER'

select t.*
--delete t
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechPurchaseInventory t on t2m.InventoryUID = t.InventoryUID
where action = 'TRANSFER'

select t.*
--delete t
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechAuditDetailInventoryCounts t on t2m.InventoryUID = t.InventoryUID
where action = 'TRANSFER'

select t.*
--delete t
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechInventoryExt t on t2m.InventoryUID = t.InventoryUID
where action = 'TRANSFER'

select tri.*
--update t2m set TransferInventoryUID = tri.TransferInventoryUID
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechTransferInventoryTagged t on t2m.InventoryUID = t.InventoryUID
join tblTechTransferInventory tri on t.TransferInventoryUID = tri.TransferInventoryUID
where action = 'TRANSFER'

select t.*
--delete t
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechTransferInventoryTagged t on t2m.InventoryUID = t.InventoryUID
where action = 'TRANSFER'

select tri.*
--delete tri
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechTransferInventory tri on t2m.TransferInventoryUID = tri.TransferInventoryUID
where action = 'TRANSFER'

select t.*
--delete t
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechStudentInventory t on t2m.InventoryUID = t.InventoryUID
where action = 'TRANSFER'

select t.*
--delete t
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechInventoryDueDates t on t2m.InventoryUID = t.InventoryUID
where action = 'TRANSFER'

select t.*
--delete t
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechInventoryHistory h on t2m.InventoryUID = h.InventoryUID
join tblTechInventoryAccessories t on h.InventoryHistoryUID = t.InventoryHistoryUID
where action = 'TRANSFER'

select t.*
--delete t
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechInventoryHistory t on t2m.InventoryUID = t.InventoryUID
where action = 'TRANSFER'

select t.*
--delete t
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechInventory t on t2m.InventoryUID = t.InventoryUID
where action = 'TRANSFER'