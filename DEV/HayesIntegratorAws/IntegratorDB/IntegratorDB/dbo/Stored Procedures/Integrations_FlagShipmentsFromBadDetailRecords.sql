﻿CREATE PROCEDURE [dbo].[Integrations_FlagShipmentsFromBadDetailRecords] (
    @intgid int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here

UPDATE Shipments
SET ShouldSubmit = 0
FROM Shipments s
LEFT JOIN vw_DistinctDetails d on s.OrderNumber = d.orderNumber and s.LineNumber = d.LineNumber
WHERE s.DataIntegrationsID = @intgid and d.ordernumber is null

UPDATE Shipments
SET ShouldSubmit = 0
FROM Shipments s
JOIN vw_DistinctShipments d on s.OrderNumber = d.orderNumber and s.LineNumber = d.LineNumber and s.SiteID = d.siteID
WHERE s.DataIntegrationsID = @intgid

END