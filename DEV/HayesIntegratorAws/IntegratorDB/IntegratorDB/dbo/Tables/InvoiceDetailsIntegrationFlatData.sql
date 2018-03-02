CREATE TABLE [dbo].[InvoiceDetailsIntegrationFlatData] (
    [OrderNumber]         VARCHAR (50)  NOT NULL,
    [InvoiceNumber]       VARCHAR (50)  NOT NULL,
    [InvoiceDate]         VARCHAR (50)  NULL,
    [InvoiceStatus]       VARCHAR (50)  NULL,
    [AuthorizationStatus] VARCHAR (50)  NULL,
    [LineNumber]          VARCHAR (50)  NOT NULL,
    [LineDescription]     VARCHAR (MAX) NULL,
    [AssetPrice]          VARCHAR (50)  NULL,
    [InvoicePrice]        VARCHAR (50)  NULL,
    [Quantity]            VARCHAR (50)  NULL,
    [LineAmount]          VARCHAR (50)  NULL,
    [DataIntegrationsID]  VARCHAR (100) NOT NULL,
    [Chunk]               BIT           CONSTRAINT [DF_InvoiceDetailsIntegrationFlatData_Chunk] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_InvoiceDetailsIntegrationFlatData] PRIMARY KEY CLUSTERED ([InvoiceNumber] ASC, [OrderNumber] ASC, [LineNumber] ASC, [DataIntegrationsID] ASC)
);



