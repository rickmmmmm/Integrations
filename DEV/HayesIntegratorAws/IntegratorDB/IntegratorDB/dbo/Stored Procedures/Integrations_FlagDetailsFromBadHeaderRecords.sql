CREATE PROCEDURE [dbo].[Integrations_FlagDetailsFromBadHeaderRecords] (
    @intgid varchar(50)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here

    --Remove details with no header available    
    UPDATE PurchaseOrderDetail
    SET ShouldSubmit = 0
    FROM PurchaseOrderDetail det
    LEFT JOIN vw_DistinctHeaders head on det.OrderNumber = head.OrderNumber
    WHERE det.DataIntegrationsID = @intgid and head.OrderNumber IS NULL

    --Remove details that have already been submitted
    UPDATE PurchaseOrderDetail
    SET ShouldSubmit = 0
    FROM PurchaseOrderDetail det
    JOIN vw_DistinctDetails dd on dd.ordernumber = det.OrderNumber and dd.LineNumber = det.LineNumber
    WHERE det.DataIntegrationsID = @intgid

END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Integrations_FlagDetailsFromBadHeaderRecords] TO [intg-cps]
    WITH GRANT OPTION
    AS [dbo];

