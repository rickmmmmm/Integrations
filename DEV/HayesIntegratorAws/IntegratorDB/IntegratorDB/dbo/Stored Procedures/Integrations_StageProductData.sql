CREATE PROCEDURE [dbo].[Integrations_StageProductData] (@client varchar(50))
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    INSERT INTO Products
        (ProductName, ProductDescription, ProductType, Manufacturer, Model, Client, SuggestedPrice)
    SELECT --DISTINCT 
        fd.PRODUCT_NAME, fd.PRODUCT_NAME AS PRODUCT_DESC, fd.PRODUCT_TYPE, fd.MANUFACTURER, fd.MODEL, @client AS Client, MAX(fd.PURCHASE_PRICE) PURCHASE_PRICE
    FROM dbo.PurchaseOrderIntegrationFlatData fd
    --JOIN DataIntegrations di 
    --    ON di.IntegrationsID = fd.IntegrationsID
    LEFT JOIN dbo.Products p 
        ON LOWER(p.ProductName) = LOWER(fd.PRODUCT_NAME)
    WHERE 
        p.ProductNumber IS NULL
        OR (p.ProductNumber IS NOT NULL AND p.Client <> @client)
    GROUP BY 
        fd.PRODUCT_NAME, fd.PRODUCT_TYPE, fd.MANUFACTURER, fd.MODEL--, fd.PURCHASE_PRICE
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Integrations_StageProductData] TO [intg-cps]
    WITH GRANT OPTION
    AS [dbo];

