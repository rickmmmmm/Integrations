CREATE TABLE [dbo].[Shipments] (
    [OrderNumber]     VARCHAR (50)  NOT NULL,
    [LineNumber]      INT           NOT NULL,
    [SiteID]          VARCHAR (50)  NOT NULL,
    [TicketNumber]    INT           NULL,
    [QuantityShipped] INT           NULL,
    [TicketedBy]      VARCHAR (50)  NULL,
    [TicketedDate]    DATETIME      NULL,
    [Status]          VARCHAR (50)  NULL,
    [InvoiceNumber]   VARCHAR (25)  NULL,
    [InvoiceDate]     DATETIME      NULL,
    [ShouldSubmit]    BIT           CONSTRAINT [DF_Shipments_ShouldSubmit] DEFAULT ('True') NULL,
    [IntegrationsID]  VARCHAR (100) NOT NULL,
    [Submitted]       BIT           CONSTRAINT [DF_Shipments_Submitted] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Shipments] PRIMARY KEY CLUSTERED ([OrderNumber] ASC, [LineNumber] ASC, [SiteID] ASC, [IntegrationsID] ASC)
);



