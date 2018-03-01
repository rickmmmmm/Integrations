CREATE TABLE [dbo].[Invoices] (
    [OrderNumber]         VARCHAR (50)  NOT NULL,
    [InvoiceNumber]       VARCHAR (50)  NOT NULL,
    [InvoiceDate]         VARCHAR (50)  NULL,
    [InvoiceStatus]       VARCHAR (50)  NULL,
    [AuthorizationStatus] VARCHAR (50)  NULL,
    [ShouldSubmit]        BIT           DEFAULT ((1)) NULL,
    [DataIntegrationsID]  VARCHAR (100) NOT NULL,
    [LastModifiedDate]    VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([InvoiceNumber] ASC, [OrderNumber] ASC, [DataIntegrationsID] ASC)
);

