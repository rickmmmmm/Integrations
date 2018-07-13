CREATE TABLE [dbo].[PurchaseInvoiceFlatData] (
    [PurchaseInvoiceFlatDataUID] INT            IDENTITY (1, 1) NOT NULL,
    [ProcessUid]                 INT            NOT NULL,
    [RowID]                      INT            NOT NULL,
    [OrderNumber]                VARCHAR (MAX)  NULL,
    [LineNumber]                 VARCHAR (MAX)  NULL,
    [InvoiceNumber]              VARCHAR (MAX)  NULL,
    [InvoiceDate]                VARCHAR (MAX)  NULL,
    [InvoiceStatus]              VARCHAR (MAX)  NULL,
    [AuthorizationStatus]        VARCHAR (MAX)  NULL,
    [AccountingDate]             VARCHAR (MAX)  NULL,
    [LineDescription]            VARCHAR (MAX)  NULL,
    [AssetPrice]                 VARCHAR (MAX)  NULL,
    [InvoicePrice]               VARCHAR (MAX)  NULL,
    [Quantity]                   VARCHAR (MAX)  NULL,
    [LineAmount]                 VARCHAR (MAX)  NULL,
    [Rejected]                   BIT            NOT NULL CONSTRAINT [DF_PurchaseInvoiceFlatData_Rejected] DEFAULT 0,
    [RejectedNotes]              TEXT           NULL,
    CONSTRAINT [PK_PurchaseInvoiceFlatData] PRIMARY KEY CLUSTERED ([PurchaseInvoiceFlatDataUID] ASC),
    CONSTRAINT [FK_PurchaseInvoiceFlatData_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);

