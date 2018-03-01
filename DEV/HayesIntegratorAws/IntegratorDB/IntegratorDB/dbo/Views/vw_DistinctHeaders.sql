CREATE VIEW [dbo].[vw_DistinctHeaders]
AS 
SELECT DISTINCT OrderNumber
FROM PurchaseOrderHeader
WHERE Submitted = 1