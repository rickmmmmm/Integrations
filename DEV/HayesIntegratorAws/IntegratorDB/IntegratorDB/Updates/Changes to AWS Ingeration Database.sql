USE [IntgAppData]
GO

ALTER TABLE [dbo].[PurchaseOrderIntegrationFlatData] DROP CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_Chunk]
GO

ALTER TABLE [dbo].[PurchaseOrderIntegrationFlatData] DROP CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_Manufacturer]
GO

ALTER TABLE [dbo].[PurchaseOrderIntegrationFlatData] DROP CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_Model]
GO

ALTER TABLE [dbo].[PurchaseOrderIntegrationFlatData] DROP CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_ProductType]
GO

/****** Object:  Table [dbo].[PurchaseOrderIntegrationFlatData]    Script Date: 8/27/2018 10:20:19 AM ******/
DROP TABLE [dbo].[PurchaseOrderIntegrationFlatData]
GO

/****** Object:  Table [dbo].[PurchaseOrderIntegrationFlatData]    Script Date: 8/27/2018 10:20:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PurchaseOrderIntegrationFlatData](
	[PurchaseOrderIntegrationFlatDataID] [int] IDENTITY(1,1) NOT NULL,
	[PO_NUMBER] [varchar](50) NOT NULL,
	[PO_DATE] [datetime] NULL,
	[VENDOR_NAME] [varchar](100) NULL,
	[VENDOR_ID] [int] NULL,
	[LINE_NUMBER] [int] NOT NULL,
	[PRODUCT_NAME] [varchar](100) NULL,
	[PRODUCT_TYPE] [varchar](100) NULL,
	[MODEL] [varchar](100) NULL,
	[MANUFACTURER] [varchar](100) NULL,
	[FUNDING_SOURCE] [varchar](100) NULL,
	[DEPARTMENT] [varchar](100) NULL,
	[ACCOUNT_CODE] [varchar](100) NULL,
	[PURCHASE_PRICE] [money] NULL,
	[QUANTITYORDERED] [int] NULL,
	[NOTES] [varchar](max) NULL,
	[SHIPPEDTOSITE] [varchar](100) NULL,
	[QUANTITYSHIPPED] [int] NULL,
	[CFDA] [varchar](50) NULL,
	[STATEFUNDING] [varchar](5) NULL,
	[FEDERALFUNDING] [varchar](5) NULL,
	[IntegrationsID] [varchar](100) NOT NULL,
	[Chunk] [bit] NULL,
	[CreateDate] DATETIME NOT NULL,
 CONSTRAINT [PK_PurchaseOrderIntegrationFlatData] PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderIntegrationFlatDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[PurchaseOrderIntegrationFlatData] ADD  CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_ProductType]  DEFAULT ('Unassigned') FOR [PRODUCT_TYPE]
GO

ALTER TABLE [dbo].[PurchaseOrderIntegrationFlatData] ADD  CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_Model]  DEFAULT ('None') FOR [MODEL]
GO

ALTER TABLE [dbo].[PurchaseOrderIntegrationFlatData] ADD  CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_Manufacturer]  DEFAULT ('None') FOR [MANUFACTURER]
GO

ALTER TABLE [dbo].[PurchaseOrderIntegrationFlatData] ADD  CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_Chunk]  DEFAULT ((1)) FOR [Chunk]
GO

ALTER TABLE [dbo].[PurchaseOrderIntegrationFlatData] ADD  CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_CreateDate]  DEFAULT (GETDATE()) FOR [CreateDate]
GO

INSERT INTO DataIntegrationsMappings
(MappingsID,MappingsObject,Client)
VALUES
('po headers','{"fromVal": "STATEFUNDING", "toVal": "STATEFUNDING"}','CPS')

INSERT INTO DataIntegrationsMappings
(MappingsID,MappingsObject,Client)
VALUES
('purchases','{"fromVal": "STATEFUNDING", "toVal": "STATEFUNDING"}','CPS')

INSERT INTO DataIntegrationsMappings
(MappingsID,MappingsObject,Client)
VALUES
('po headers','{"fromVal": "FEDERALFUNDING", "toVal": "FEDERALFUNDING"}','CPS')

INSERT INTO DataIntegrationsMappings
(MappingsID,MappingsObject,Client)
VALUES
('purchases','{"fromVal": "FEDERALFUNDING", "toVal": "FEDERALFUNDING"}','CPS')

INSERT INTO DataIntegrationsLinkTable
(Client,SourceVal,DestVal,LinkType)
VALUES
('CPS','11870','11940','Departments')

INSERT INTO DataIntegrationsLinkTable
(Client,SourceVal,DestVal,LinkType)
VALUES
('CPS','11940','11940','Departments')

INSERT INTO DataIntegrationsLinkTable
(Client,SourceVal,DestVal,LinkType)
VALUES
('CPS','11610','11610','Departments')

INSERT INTO DataIntegrationsLinkTable
(Client,SourceVal,DestVal,LinkType)
VALUES
('CPS','11670','11610','Departments')

INSERT INTO DataIntegrationsLinkTable
(Client,SourceVal,DestVal,LinkType)
VALUES
('CPS','13725','13725','Departments')

INSERT INTO DataIntegrationsLinkTable
(Client,SourceVal,DestVal,LinkType)
VALUES
('CPS','13727','13725','Departments')

INSERT INTO DataIntegrationsLinkTable
(Client,SourceVal,DestVal,LinkType)
VALUES
('CPS','11860','11860-11880','Departments')

INSERT INTO DataIntegrationsLinkTable
(Client,SourceVal,DestVal,LinkType)
VALUES
('CPS','11880','11860-11880','Departments')


ALTER TABLE PurchaseOrderHeader
	ADD StateFunding VARCHAR(5),
	FederalFunding VARCHAR(5)

ALTER TABLE PurchaseOrderHeader
	ADD CreateDate DATETIME NOT NULL DEFAULT GETDATE()

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Gailey
-- Create date: 8/24/2018
-- Description:	Correct State and Federal Funding from empty string to null
-- and to check that both are not above 100%
-- =============================================
CREATE PROCEDURE dbo.BL_FundingPercentageUpdate

@DataintegrationsID VARCHAR(50)

AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @PONumber			VARCHAR(50)
	DECLARE @VendorID			INT
	DECLARE @Status				VARCHAR(50)
	DECLARE @StateFunding		DECIMAL(5, 2)
	DECLARE @FederalFunding		DECIMAL(5, 2) 
	DECLARE @Diff				DECIMAL(5, 2)
	DECLARE @Counter			INT = 1
	DECLARE @CounterMax			INT

	UPDATE PurchaseOrderHeader
	SET StateFunding=NULL
	WHERE LEN(StateFunding)  = 0
	AND DataIntegrationsID=@DataintegrationsID

	UPDATE PurchaseOrderHeader
	SET FederalFunding=NULL
	WHERE LEN(FederalFunding)  = 0
	AND DataIntegrationsID=@DataintegrationsID

	UPDATE PurchaseOrderHeader
	SET StateFunding=LTRIM(RTRIM(StateFunding)),
		FederalFunding=LTRIM(RTRIM(FederalFunding))
	WHERE DataIntegrationsID=@DataintegrationsID

	SELECT @CounterMax=MAX(RowID)
	FROM 
	(SELECT OrderNumber,
	CAST(StateFunding AS DECIMAL(5,2)) AS StateFunding,
	CAST(FederalFunding AS DECIMAL(5,2)) AS FederalFunding,
	ROW_NUMBER() OVER (ORDER BY  OrderNumber,Status,VendorID) AS RowID
	FROM PurchaseOrderHeader
	WHERE DataIntegrationsID=@DataintegrationsID
	AND StateFunding IS NOT NULL
	AND FederalFunding IS NOT NULL) AS TotalRows

	WHILE @Counter <= @CounterMax
	BEGIN 
		SELECT @StateFunding=CAST(StateFunding AS DECIMAL(5,2)),@FederalFunding=CAST(FederalFunding AS DECIMAL(5,2)),
		@PONumber=OrderNumber,@VendorID=VendorID,@Status=Status
		FROM
		(SELECT OrderNumber,VendorID,Status,
		CAST(StateFunding AS DECIMAL(5,2)) AS StateFunding,
		CAST(FederalFunding AS DECIMAL(5,2)) AS FederalFunding,
		ROW_NUMBER() OVER (ORDER BY  OrderNumber,Status,VendorID) AS RowID
		FROM PurchaseOrderHeader
		WHERE DataIntegrationsID=@DataintegrationsID
		AND StateFunding IS NOT NULL
		AND FederalFunding IS NOT NULL) AS TotalRows
		WHERE RowID=@Counter

		IF @StateFunding IS NOT NULL OR @FederalFunding IS NOT NULL
		BEGIN
			IF (ISNULL(@StateFunding,0) + ISNULL(@FederalFunding,0)) > 100
			BEGIN
				IF @StateFunding IS NULL AND @FederalFunding > 0
				BEGIN
					SET @FederalFunding=100
				END

				IF @StateFunding > 0 AND @FederalFunding IS NULL
				BEGIN
					SET @StateFunding=100
				END

				IF (@StateFunding + @FederalFunding) > 100
				BEGIN
					SELECT @Diff = ((@StateFunding + @FederalFunding) -100)/2
					SET @StateFunding=@StateFunding - @Diff
					SET @FederalFunding=@FederalFunding - @Diff
				END

				UPDATE PurchaseOrderHeader
				SET StateFunding=CAST(@StateFunding AS VARCHAR(5)),
					FederalFunding=CAST(@FederalFunding AS VARCHAR(5))
				WHERE OrderNumber=@PONumber AND VendorID=@VendorID AND Status=@Status
				AND DataIntegrationsID=@DataintegrationsID
			END
		END

		SET @Counter= @Counter+1
	END
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Gailey
-- Create date: 8/27/2018
-- Description:	To map deparment consolidations
-- =============================================
CREATE PROCEDURE dbo.BL_DeparmentMappingsUpdate 

@DataintegrationsID VARCHAR(50)

AS
BEGIN
	SET NOCOUNT ON

	UPDATE C
	SET DepartmentID=A.DestVal

	FROM DataIntegrationsLinkTable AS A

	INNER JOIN DataIntegrations AS B 
	ON A.Client=B.Client AND A.LinkType='Departments' AND B.IntegrationsID=@DataintegrationsID

	INNER JOIN PurchaseOrderDetail AS C
	ON B.IntegrationsID=C.DataIntegrationsID AND A.SourceVal=C.DepartmentID
END
GO

GRANT EXEC ON dbo.BL_FundingPercentageUpdate TO PUBLIC
GRANT EXEC ON dbo.BL_DeparmentMappingsUpdate TO PUBLIC
GO

/****** Object:  StoredProcedure [dbo].[Integrations_RemoveUnnecessaryUpdates]    Script Date: 8/24/2018 11:26:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Integrations_RemoveUnnecessaryUpdates](
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
    
	EXEC dbo.BL_FundingPercentageUpdate @DataintegrationsID=@intgid
	EXEC dbo.BL_DeparmentMappingsUpdate @DataintegrationsID=@intgid
END
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [INDX_PurchasOrderDetails_DataIntegrationsID]    Script Date: 8/27/2018 9:31:58 AM ******/
CREATE NONCLUSTERED INDEX [INDX_PurchasOrderDetails_DataIntegrationsID] ON [dbo].[PurchaseOrderDetail]
(
	[DataIntegrationsID] ASC,
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 70) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [INDX_DataIntegrationsLinkTable_Client_LinkType]    Script Date: 8/27/2018 9:35:24 AM ******/
CREATE NONCLUSTERED INDEX [INDX_DataIntegrationsLinkTable_Client_LinkType] ON [dbo].[DataIntegrationsLinkTable]
(
	[Client] ASC,
	[LinkType] ASC
)
INCLUDE ( 	[LinkID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
