CREATE PROCEDURE [dbo].[Integrations_RemoveExistingInserts](
    @intgid int,
    @headers bit,
    @details bit,
    @shipping bit,
    @inventory bit,
    @charges bit,
    @payments bit
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    DECLARE @intgdate date
    DECLARE @previntgdate date
    DECLARE @client varchar(50)
    SET @intgdate = (SELECT IntegrationDate FROM DataIntegrations WHERE IntegrationsID = @intgid)
    SET @client = (SELECT Client FROM DataIntegrations WHERE IntegrationsID = @intgid)
    SET @previntgdate = (SELECT max(IntegrationDate) FROM DataIntegrations WHERE IntegrationDate < @intgdate and Client = @client and DataProcessedSuccessfully = 1)

    -- Insert statements for procedure here
    IF @headers = 'True'
    BEGIN
        UPDATE PurchaseOrderHeader
        SET ShouldSubmit = 'False'
        FROM (SELECT * FROM PurchaseOrderHeader WHERE Dataintegrationsid = @intgid) h1
        JOIN (SELECT * FROM PurchaseOrderHeader WHERE Dataintegrationsid < @intgid) h2 on h1.OrderNumber = h2.OrderNumber
        WHERE h1.OrderNumber not in (SELECT DISTINCT ErrorNumber FROM DataIntegrationsErrors WHERE DataIntegrationsID in (SELECT IntegrationsID FROM DataIntegrations WHERE IntegrationDate < @previntgdate))
    END

    IF @details = 'True'
    BEGIN
        UPDATE PurchaseOrderDetail
        SET ShouldSubmit = 'False'
        FROM (SELECT * FROM PurchaseOrderDetail WHERE Dataintegrationsid = @intgid) h1
        JOIN (SELECT * FROM PurchaseOrderDetail WHERE Dataintegrationsid < @intgid) h2 on h1.OrderNumber = h2.OrderNumber
                                                                                        AND h1.LineNumber = h2.LineNumber
        WHERE h1.OrderNumber not in (SELECT DISTINCT ErrorNumber FROM DataIntegrationsErrors WHERE DataIntegrationsID in (SELECT IntegrationsID FROM DataIntegrations WHERE IntegrationDate < @previntgdate))
    END
    IF @shipping = 'True'
    BEGIN
        UPDATE Shipments
        SET ShouldSubmit = 'False'
        FROM (SELECT * FROM Shipments WHERE IntegrationsID = @intgid) s1
        JOIN (SELECT * FROM Shipments WHERE IntegrationsID < @intgid) s2 on s1.OrderNumber = s2.OrderNumber
                                                                                        AND s1.LineNumber = s2.LineNumber
                                                                                        AND s1.SiteID = s2.SiteID
        WHERE s1.OrderNumber not in (SELECT DISTINCT ErrorNumber FROM DataIntegrationsErrors WHERE DataIntegrationsID in (SELECT IntegrationsID FROM DataIntegrations WHERE IntegrationDate < @previntgdate))
    END

END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Integrations_RemoveExistingInserts] TO [intg-cps]
    WITH GRANT OPTION
    AS [dbo];

