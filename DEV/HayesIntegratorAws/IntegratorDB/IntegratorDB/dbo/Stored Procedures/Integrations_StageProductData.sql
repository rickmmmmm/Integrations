CREATE PROCEDURE [dbo].[Integrations_StageProductData] (
    @client varchar(50)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    INSERT INTO Products
        (ProductName, ProductDescription, ProductType, Manufacturer, Model, Client, SuggestedPrice)
    SELECT DISTINCT 
        PRODUCT_NAME, PRODUCT_NAME, PRODUCT_TYPE, fd.MANUFACTURER, fd.MODEL, @client, PURCHASE_PRICE
    FROM PurchaseOrderIntegrationFlatData fd
    JOIN DataIntegrations di 
        ON di.IntegrationsID = fd.IntegrationsID
    LEFT JOIN Products p 
        ON LOWER(p.ProductName) = LOWER(fd.PRODUCT_NAME)
    WHERE di.Client = @client and p.ProductNumber IS NULL
END