CREATE VIEW [dbo].[vw_DistinctShipments]
AS
SELECT DISTINCT OrderNumber, LineNumber, SiteID
FROM Shipments
WHERE Submitted = 1