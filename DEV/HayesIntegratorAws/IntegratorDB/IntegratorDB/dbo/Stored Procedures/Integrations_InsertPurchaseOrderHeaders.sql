CREATE PROCEDURE [dbo].[Integrations_InsertPurchaseOrderHeaders] (
    @client varchar(50),
    @dataIntegrationsID varchar(100)
)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[PurchaseOrderHeader]
        ([OrderNumber], [Status], [VendorID], [VendorName], [SiteID], [PurchaseDate], [EstimatedDeliveryDate], [Notes], [Other1], [DataIntegrationsID])
    SELECT 
        [stagingData].[OrderNumber], [stagingData].[Status], [stagingData].[VendorID], [stagingData].[VendorName], [stagingData].[SiteID], [stagingData].[PurchaseDate], [stagingData].[EstimatedDeliveryDate], [stagingData].[Notes], [stagingData].[Other1], [stagingData].[DataIntegrationsID]
    FROM [dbo].[PurchaseOrderHeaderStaging] [stagingData]
    JOIN (
        SELECT 
            MIN([stagingData].[PurchaseOrderHeaderStagingUID]) [PurchaseOrderHeaderStagingUID]--, [stagingData].[OrderNumber], [stagingData].[PurchaseDate], [stagingData].[VendorID], [stagingData].[VendorName], [stagingData].[SiteID]
        FROM [dbo].[PurchaseOrderHeaderStaging] [stagingData]
        LEFT JOIN [dbo].[PurchaseOrderHeader] [headers]
            ON [stagingData].[OrderNumber] = [headers].[OrderNumber]
        WHERE
            --[headers].[OrderNumber] IS NULL
            [stagingData].[DataIntegrationsID] = @dataIntegrationsID
        GROUP BY 
            [stagingData].[OrderNumber], [stagingData].[PurchaseDate], [stagingData].[VendorID], [stagingData].[VendorName], [stagingData].[SiteID]
        ) [UniqueHeaders]
        ON [stagingData].[PurchaseOrderHeaderStagingUID] = [UniqueHeaders].[PurchaseOrderHeaderStagingUID]
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Integrations_InsertPurchaseOrderHeaders] TO [intg-cps]
    WITH GRANT OPTION
    AS [dbo];
GO