CREATE PROCEDURE [dbo].[Integrations_ClearData] (
    @dateStart date,
    @dateEnd date,
    @client varchar(50)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    DELETE FROM PurchaseOrderHeader
    WHERE DataIntegrationsID in (SELECT IntegrationsID FROM DataIntegrations WHERE @client = Client and IntegrationDate between @dateStart and @dateEnd)

    DELETE FROM PurchaseOrderDetail
    WHERE DataIntegrationsID in (SELECT IntegrationsID FROM DataIntegrations WHERE @client = Client and IntegrationDate between @dateStart and @dateEnd)

    DELETE FROM Shipments
    WHERE IntegrationsID in (SELECT IntegrationsID FROM DataIntegrations WHERE @client = Client and IntegrationDate between @dateStart and @dateEnd)

    DELETE FROM PurchaseOrderIntegrationFlatData
    WHERE IntegrationsID in (SELECT IntegrationsID FROM DataIntegrations WHERE @client = Client and IntegrationDate between @dateStart and @dateEnd)

    DELETE FROM DataIntegrationsErrors
    WHERE DataIntegrationsID in (SELECT IntegrationsID FROM DataIntegrations WHERE @client = Client and IntegrationDate between @dateStart and @dateEnd)

    UPDATE DataIntegrations
    SET DataCleared = 1
    WHERE Client = @client and IntegrationDate between @dateStart and @dateEnd

END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Integrations_ClearData] TO [intg-cps]
    WITH GRANT OPTION
    AS [dbo];

