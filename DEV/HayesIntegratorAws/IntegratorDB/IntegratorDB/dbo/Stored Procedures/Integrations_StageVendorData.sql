CREATE PROCEDURE [dbo].[Integrations_StageVendorData] (@client varchar(50))
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Vendors
        (VendorID, VendorName, Address1, Address2, City, State, ZipCode, Phone, Email, Added, Updated, AddedDate, Client)
    SELECT
        fd.VENDOR_ID ,fd.VENDOR_NAME, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 0, GETUTCDATE(), @client AS Client
    FROM dbo.PurchaseOrderIntegrationFlatData fd
    LEFT JOIN dbo.Vendors v 
        ON LOWER(v.VendorName) = LOWER(fd.VENDOR_NAME)
    WHERE 
        v.VendorName IS NULL
        OR (v.VendorName IS NOT NULL AND v.Client <> @client)
    GROUP BY 
        fd.VENDOR_ID ,fd.VENDOR_NAME
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Integrations_StageVendorData] TO [intg-cps]
    WITH GRANT OPTION
    AS [dbo];
GO

