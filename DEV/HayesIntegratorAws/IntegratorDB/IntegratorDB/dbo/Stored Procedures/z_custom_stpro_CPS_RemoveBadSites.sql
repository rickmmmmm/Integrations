CREATE PROCEDURE [dbo].[z_custom_stpro_CPS_RemoveBadSites](
    @client varchar(50),
    @intgid int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    
    UPDATE PurchaseOrderHeader
    SET ShouldSubmit = 0
    WHERE DataIntegrationsID = @intgid AND SiteID not in (SELECT DISTINCT DestVal FROM DataIntegrationsLinkTable WHERE client = @client)

    UPDATE PurchaseOrderDetail
    SET ShouldSubmit = 0
    WHERE DataIntegrationsID = @intgid AND SiteID not in (SELECT DISTINCT DestVal FROM DataIntegrationsLinkTable WHERE client = @client)

    UPDATE Shipments
    SET ShouldSubmit = 0
    WHERE IntegrationsID = @intgid AND SiteID not in (SELECT DISTINCT DestVal FROM DataIntegrationsLinkTable WHERE client = @client)

END