CREATE TABLE [dbo].[_ETL_Shipments] (
    [_ETL_ShipmentsUID]       INT           IDENTITY (1, 1) NOT NULL,
	[RowID]					  AS (_ETL_ShipmentsUID+1),
    [ProcessUid]              INT           NOT NULL,
    [PurchaseItemShipmentUID] INT           NOT NULL,
    [PurchaseItemDetailUID]   INT           NOT NULL,
    [OrderNumber]             VARCHAR (50)  NOT NULL,
    [LineNumber]              INT           NOT NULL,
    [ShippedToSiteUID]        INT           NOT NULL,
    [SiteID]                  VARCHAR (100) NULL,
    [TicketNumber]            INT           NULL,
    [QuantityShipped]         INT           NOT NULL,
    [TicketedByUserID]        INT           NULL,
    [TicketedBy]              VARCHAR (50)  NULL,
    [TicketedDate]            DATETIME      NULL,
    [StatusID]                INT           NOT NULL,
    [Status]                  VARCHAR (50)  NULL,
    [InvoiceNumber]           VARCHAR (25)  NULL,
    [InvoiceDate]             DATE          NULL,
 	[Rejected]                BIT			NOT NULL CONSTRAINT [DF__ETL_Shipments_Rejected] DEFAULT 0,
	[RejectedNotes]			  TEXT			NULL
    CONSTRAINT [PK__ETL_Shipments] PRIMARY KEY CLUSTERED ([_ETL_ShipmentsUID] ASC),
    CONSTRAINT [FK__ETL_Shipments_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);



