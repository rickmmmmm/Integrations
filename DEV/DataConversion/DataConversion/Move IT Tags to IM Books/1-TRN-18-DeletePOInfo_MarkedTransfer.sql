select tship.PurchaseItemShipmentUID, QuantityShipped, numRemoved
--update tShip set QuantityShipped = (QuantityShipped-numRemoved)
from tblTechPurchaseItemShipments tship 
join (
	select PurchaseItemShipmentUID, count(*) as numRemoved
	from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM
	where action = 'TRANSFER'
	and PurchaseItemShipmentUID is not null
	group by PurchaseItemShipmentUID
	) d on tship.PurchaseItemShipmentUID = d.PurchaseItemShipmentUID


select * 
--delete tship
from tblTechPurchaseItemShipments tship 
join Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m on tship.PurchaseItemShipmentUID = t2m.PurchaseItemShipmentUID
where action = 'TRANSFER'
and t2m.PUrchaseItemShipmentUID is not null
and QuantityShipped < 1


select tpid.PurchaseItemDetailUID, tpid.ItemUID, QuantityOrdered, QuantityReceived, numRemoved
--update tpid set QuantityOrdered = (QuantityOrdered-numRemoved), QuantityReceived = (QuantityReceived-numRemoved)
from tblTechPUrchaseItemDetails tpid
join (
	select PurchaseItemDetailUID, ItemUID, count(*) as numRemoved
	from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM
	where action = 'TRANSFER'
	and PurchaseItemDetailUID is not null
	group by PurchaseItemDetailUID, ItemUID
	) d on tpid.PurchaseItemDetailUID = d.PurchaseItemDetailUID and tpid.ItemUID = d.ItemUID


select * 
--delete tpid
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m
join tblTechPUrchaseItemDetails tpid on t2m.PurchaseItemDetailUID = tpid.PurchaseItemDetailUID
where action = 'TRANSFER'
and QuantityOrdered < 1 and QuantityReceived < 1

select distinct tpur.OrderNumber
--delete tpur
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m
join tblTechPurchases tpur on t2m.PUrchaseUID = tpur.PurchaseUID
where action = 'TRANSFER'
and tpur.PUrchaseUID not in (select PurchaseUID from tblTechPurchaseItemDetails)
