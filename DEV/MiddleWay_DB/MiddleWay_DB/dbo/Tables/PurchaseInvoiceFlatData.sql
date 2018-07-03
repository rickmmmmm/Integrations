CREATE TABLE [dbo].[PurchaseInvoiceFlatData] (
    [PurchaseInvoiceFlatDataUID] INT            IDENTITY (1, 1) NOT NULL,
    [ProcessUid]                 INT            NOT NULL,
	[RowID]						 INT			NOT NULL,
    [OrderNumber]                VARCHAR (50)   NULL,
    [LineNumber]                 INT            NULL,
    [InvoiceNumber]              VARCHAR (100)  NULL,
    [InvoiceDate]                DATETIME       NULL,
    [InvoiceStatus]              VARCHAR (50)   NULL,
    [AuthorizationStatus]        VARCHAR (50)   NULL,
    [AccountingDate]             VARCHAR (50)   NULL,
    [LineDescription]            VARCHAR (1000) NULL,
    [AssetPrice]                 DECIMAL (18)   NULL,
    [InvoicePrice]               DECIMAL (18)   NULL,
    [Quantity]                   INT            NULL,
    [LineAmount]                 DECIMAL (18)   NULL,
 	[Rejected]                   BIT			NOT NULL CONSTRAINT [DF_PurchaseInvoiceFlatData_Rejected] DEFAULT 0,
	[RejectedNotes]			     TEXT			NULL,
    CONSTRAINT [PK_PurchaseInvoiceFlatData] PRIMARY KEY CLUSTERED ([PurchaseInvoiceFlatDataUID] ASC),
    CONSTRAINT [FK_PurchaseInvoiceFlatData_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);

