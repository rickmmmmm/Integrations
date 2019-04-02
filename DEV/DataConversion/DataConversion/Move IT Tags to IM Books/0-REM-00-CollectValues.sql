--update Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM set InventoryUID = null, ItemUID = null, VendorID = null, TechDepartmentUID = null, SiteUID = null, StatusID = null, 
--	EntityTypeUID = null, EntityUID = null, PurchaseUID = null, PurchaseItemDetailUID = null, PurchaseItemShipmentUID = null, PurchaseDate = null, TransferInventoryUID = null,
--  PublisherID = null, PublisherName = null, BookInventoryUID = null, FundingSource = null, FundingSourceUID = null, VendorOrderUID = null, VendorOrderDetailsUID = null,
--	DistributionID = null, CampusDistributionID = null, Title = null, ISBN = null, ItemName = null, ItemNumber = null, ItemType = null, PurchasePrice = null,
--	RequisitionUID = null, TeacherDistributionID = null, StudentDistributionID = null


--InventoryUID, ItemUID, TechDepartmentUID, SIteUID, StatusID, EntityTypeUID, EntityUID
select *
--update t2m set InventoryUID = tti.InventoryUID, ItemUID=tti.ItemUID, TechDepartmentUID= tti.TechDepartmentUID, 
--SiteUID=tti.SiteUID, StatusID = tti.StatusUID, EntityTypeUID = tti.EntityTypeUID, EntityUID = tti.EntityUID
from tblTechInventory tti
join Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m on tti.Tag = t2m.Tag
where action not in ('KEEP')

----Purchasing

select * 
--update t2m set PurchaseUID = tpid.PurchaseUID, PurchaseItemDetailUID = tpid.PurchaseItemDetailUID, PurchaseItemShipmentUID = tship.PurchaseItemShipmentUID, PurchaseDate = tpur.PurchaseDate, VendorID = tpur.VendorUID 
from tblTechInventory tti
join Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m on tti.Tag = t2m.Tag
join tblTechPurchaseInventory tpi on tti.InventoryUID = tpi.InventoryUID
join tblTechPurchaseItemShipments tship on tpi.PurchaseItemShipmentUID = tship.PurchaseItemShipmentUID
join tblTechPurchaseItemDetails tpid on tship.PurchaseItemDetailUID = tpid.PurchaseItemDetailUID
join tblTechPurchases tpur on tpid.PurchaseUID = tpur.PurchaseUID
where action not in ('KEEP')
and t2m.OrderNumber <> ''

--Chect
select *
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechPurchases tpur on t2m.PurchaseUID = tpur.PurchaseUID
where t2m.OrderNumber <> ''
and t2m.PurchaseUID is not null
and t2m.OrderNumber <> tpur.OrderNumber


select *
--update t2m set PurchaseUID = tpur.PurchaseUID, PurchaseDate = tpur.PurchaseDate, VendorID = tpur.VendorUID
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m 
join tblTechPurchases tpur on t2m.OrderNumber = tpur.OrderNumber
where t2m.OrderNumber <> ''
and t2m.PurchaseUID is null

--Check
select *
from Keller_IT2IM_Move.dbo.Keller_transfer_IT2IM t2m
where t2m.OrderNumber <> ''
and t2m.PurchaseUID is null
