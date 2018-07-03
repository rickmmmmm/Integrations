CREATE TABLE [dbo].[PurchaseShipmentFlatData] (
    [PurchaseShipmentFlatDataUID] INT           IDENTITY (1, 1) NOT NULL,
    [ProcessUid]                  INT           NOT NULL,
	[RowID]					      INT			NOT NULL,
    [OrderNumber]                 VARCHAR (50)  NULL,
    [LineNumber]                  INT           NULL,
    [ShippedToSiteID]             VARCHAR (100) NULL,
    [ShippedToSiteName]           VARCHAR (100) NULL,
    [ShippedToSiteAddress]        VARCHAR (100) NULL,
    [ShippedToSiteCity]           VARCHAR (50)  NULL,
    [ShippedToSiteState]          VARCHAR (50)  NULL,
    [ShippedToSiteZip]            VARCHAR (50)  NULL,
    [TicketNumber]                INT           NULL,
    [QuantityShipped]             INT           NULL,
    [TicketedBy]                  VARCHAR (50)  NULL,
    [TicketedDate]                DATETIME      NULL,
    [Status]                      VARCHAR (50)  NULL,
    [InvoiceNumber]               VARCHAR (25)  NULL,
    [InvoiceDate]                 DATETIME      NULL,
    CONSTRAINT [PK_PurchaseShipmentFlatData] PRIMARY KEY CLUSTERED ([PurchaseShipmentFlatDataUID] ASC),
    CONSTRAINT [FK_PurchaseShipmentFlatData_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);

