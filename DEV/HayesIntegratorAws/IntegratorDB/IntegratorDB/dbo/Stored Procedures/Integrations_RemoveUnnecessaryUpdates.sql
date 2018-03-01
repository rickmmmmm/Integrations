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
    IF @headers = 'True'
    BEGIN
        UPDATE PurchaseOrderHeader
        SET ShouldSubmit = 'False'
        FROM (SELECT * FROM PurchaseOrderHeader WHERE Dataintegrationsid = @intgid) h1
        INNER JOIN (SELECT * FROM PurchaseOrderHeader WHERE Dataintegrationsid <> @intgid AND Submitted = 1) h2 on h1.OrderNumber = h2.OrderNumber
    END

    IF @details = 'True'
    BEGIN
        UPDATE PurchaseOrderDetail
        SET ShouldSubmit = 'False'
        FROM (SELECT * FROM PurchaseOrderDetail WHERE Dataintegrationsid = @intgid) h1
        JOIN (SELECT * FROM PurchaseOrderDetail WHERE Dataintegrationsid <> @intgid AND Submitted = 1) h2 on h1.OrderNumber = h2.OrderNumber
                                                                                        AND h1.LineNumber = h2.LineNumber
    END
    IF @shipping = 'True'
    BEGIN
        UPDATE Shipments
        SET ShouldSubmit = 'False'
        FROM (SELECT * FROM Shipments WHERE DataIntegrationsID = @intgid) s1
        JOIN (SELECT * FROM Shipments WHERE DataIntegrationsID <> @intgid AND Submitted = 1) s2 on s1.OrderNumber = s2.OrderNumber
                                                                                        AND s1.LineNumber = s2.LineNumber
                                                                                        AND s1.SiteID = s2.SiteID
    END
    
END