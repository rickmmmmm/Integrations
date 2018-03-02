CREATE TABLE [dbo].[InvoiceDetails] (
    [OrderNumber]        VARCHAR (50)  NOT NULL,
    [InvoiceNumber]      VARCHAR (50)  NOT NULL,
    [LineNumber]         VARCHAR (50)  NOT NULL,
    [LineDescription]    VARCHAR (MAX) NULL,
    [AssetPrice]         VARCHAR (50)  NULL,
    [InvoicePrice]       VARCHAR (50)  NULL,
    [Quantity]           VARCHAR (50)  NULL,
    [LineAmount]         VARCHAR (50)  NULL,
    [ShouldSubmit]       BIT           CONSTRAINT [DF_InvoiceDetails_ShouldSubmit] DEFAULT ((1)) NULL,
    [DataIntegrationsID] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_InvoiceDetails] PRIMARY KEY CLUSTERED ([InvoiceNumber] ASC, [OrderNumber] ASC, [LineNumber] ASC, [DataIntegrationsID] ASC)
);



