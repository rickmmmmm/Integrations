CREATE PROCEDURE [dbo].[Integrations_RemoveUnnecessaryUpdates](
    @intgid varchar(50),
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

    -- Insert statements for procedure here
    IF @headers = 1
    BEGIN
        UPDATE h1
        SET ShouldSubmit = 0
        FROM (SELECT * FROM PurchaseOrderHeader WHERE Dataintegrationsid = @intgid) h1
        INNER JOIN (SELECT * FROM PurchaseOrderHeader WHERE Dataintegrationsid <> @intgid AND Submitted = 1) h2 ON h1.OrderNumber = h2.OrderNumber
    END

    IF @details = 1
    BEGIN
        UPDATE h1
        SET ShouldSubmit = 0
        FROM (SELECT * FROM PurchaseOrderDetail WHERE Dataintegrationsid = @intgid) h1
        JOIN (SELECT * FROM PurchaseOrderDetail WHERE Dataintegrationsid <> @intgid AND Submitted = 1) h2 ON h1.OrderNumber = h2.OrderNumber
                                                                                        AND h1.LineNumber = h2.LineNumber
    END
    IF @shipping = 1
    BEGIN
        UPDATE s1
        SET ShouldSubmit = 0
        FROM (SELECT * FROM Shipments WHERE IntegrationsID = @intgid) s1
        JOIN (SELECT * FROM Shipments WHERE IntegrationsID <> @intgid AND Submitted = 1) s2 ON s1.OrderNumber = s2.OrderNumber
                                                                                        AND s1.LineNumber = s2.LineNumber
                                                                                        AND s1.SiteID = s2.SiteID
    END
    
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Integrations_RemoveUnnecessaryUpdates] TO [intg-cps]
    WITH GRANT OPTION
    AS [dbo];

