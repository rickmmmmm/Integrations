CREATE TABLE [dbo].[Shipments] (
    [OrderNumber]        VARCHAR (50)  NOT NULL,
    [LineNumber]         INT           NOT NULL,
    [SiteID]             VARCHAR (50)  NOT NULL,
    [TicketNumber]       INT           NULL,
    [QuantityShipped]    INT           NULL,
    [TicketedBy]         VARCHAR (50)  NULL,
    [TicketedDate]       DATETIME      NULL,
    [Status]             VARCHAR (50)  NULL,
    [InvoiceNumber]      VARCHAR (25)  NULL,
    [InvoiceDate]        DATETIME      NULL,
    [ShouldSubmit]       BIT           DEFAULT ('True') NULL,
    [DataIntegrationsID] VARCHAR (100) NOT NULL,
    [Submitted]          BIT           DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([OrderNumber] ASC, [LineNumber] ASC, [SiteID] ASC, [DataIntegrationsID] ASC)
);

