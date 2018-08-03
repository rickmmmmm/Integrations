CREATE TABLE [dbo].[_ETL_Shipments] (
    [_ETL_ShipmentsUID]       INT           IDENTITY (1, 1) NOT NULL,
    [ProcessTaskUid]          INT           NOT NULL,
    [RowID]                   INT           NOT NULL,
    [PurchaseItemShipmentUID] INT           NOT NULL,
    [PurchaseItemDetailUID]   INT           NOT NULL,
    [OrderNumber]             VARCHAR (50)  NOT NULL,
    [LineNumber]              INT           NOT NULL,
    [ShippedToSiteUID]        INT           NOT NULL,
    [ShippedToSiteID]         VARCHAR (100) NULL,
    [ShippedToSiteName]       VARCHAR (100) NULL,
    [TicketNumber]            INT           NULL,
    [QuantityShipped]         INT           NOT NULL,
    [TicketedByUserID]        INT           NULL,
    [TicketedBy]              VARCHAR (50)  NULL,
    [TicketedDate]            DATETIME      NULL,
    [StatusID]                INT           NOT NULL,
    [Status]                  VARCHAR (50)  NULL,
    [InvoiceNumber]           VARCHAR (25)  NULL,
    [InvoiceDate]             DATE          NULL,
    [Rejected]                BIT           NOT NULL CONSTRAINT [DF__ETL_Shipments_Rejected] DEFAULT ((0)),
    [RejectedNotes]           TEXT          NULL
    CONSTRAINT [PK__ETL_Shipments] PRIMARY KEY CLUSTERED ([_ETL_ShipmentsUID] ASC),
    CONSTRAINT [FK__ETL_Shipments_ProcessTasks] FOREIGN KEY ([ProcessTaskUid]) REFERENCES [dbo].[ProcessTasks] ([ProcessTaskUid])
);



