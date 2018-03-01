CREATE VIEW [dbo].[vw_DistinctDetails]
AS
SELECT DISTINCT OrderNumber, LineNumber
FROM PurchaseOrderDetail
WHERE Submitted = 1