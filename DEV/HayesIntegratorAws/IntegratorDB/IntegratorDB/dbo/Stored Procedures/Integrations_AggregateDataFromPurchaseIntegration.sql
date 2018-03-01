CREATE PROCEDURE [dbo].[Integrations_AggregateDataFromPurchaseIntegration] (
    @Client varchar(50),
    @DateToAgg date
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    INSERT INTO DataIntegrationsAggregates (DateRun, DataType, ReferenceVal, TotalCount, Client, IntegrationType, ReferenceDescription)
    SELECT IntegrationDate, 'Error', err.ErrorName, count(*), di.client, 'Purchases', err.ErrorDescription
    FROM DataIntegrations di
    JOIN DataIntegrationsErrors err on err.DataIntegrationsID = di.IntegrationsID
    WHERE IntegrationDate = @DateToAgg AND Client = @Client and err.ErrorNumber <> '500'
    GROUP BY IntegrationDate, err.ErrorName, di.client, ErrorDescription
    UNION
    SELECT IntegrationDate, 'Non-Error','Successful Purchase Order Header Imports', count(*), di.client, 'Purchases', ''
    FROM DataIntegrations di 
    JOIN PurchaseOrderHeader da on di.IntegrationsID = da.DataIntegrationsID
    LEFT JOIN DataIntegrationsErrors err on err.ErrorNumber = da.OrderNumber AND err.DataIntegrationsID = da.DataIntegrationsID
    WHERE IntegrationDate = @DateToAgg AND Client = @Client AND err.ErrorNumber IS NULL and da.ShouldSubmit = 1
    GROUP BY IntegrationDate, Client
    UNION
    SELECT IntegrationDate, 'Non-Error','Successful Purchase Order Detail Imports', count(*), di.client, 'Purchases', ''
    FROM DataIntegrations di 
    JOIN PurchaseOrderDetail da on di.IntegrationsID = da.DataIntegrationsID
    LEFT JOIN DataIntegrationsErrors err on err.ErrorNumber = da.OrderNumber AND err.DataIntegrationsID = da.DataIntegrationsID
    WHERE IntegrationDate = @DateToAgg AND Client = @Client AND err.ErrorNumber IS NULL and da.ShouldSubmit = 1
    GROUP BY IntegrationDate, Client
END