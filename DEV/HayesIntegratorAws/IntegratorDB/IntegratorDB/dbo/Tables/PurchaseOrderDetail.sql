CREATE TABLE [dbo].[PurchaseOrderDetail] (
    [OrderNumber]        VARCHAR (50)  NOT NULL,
    [LineNumber]         INT           NOT NULL,
    [Status]             VARCHAR (50)  NULL,
    [SiteID]             VARCHAR (50)  NULL,
    [FundingSource]      VARCHAR (50)  NULL,
    [ProductName]        VARCHAR (100) NULL,
    [QuantityOrdered]    INT           NULL,
    [QuantityReceived]   INT           NULL,
    [PurchasePrice]      MONEY         NULL,
    [AccountCode]        VARCHAR (100) NULL,
    [DepartmentID]       VARCHAR (50)  NULL,
    [CFDA]               VARCHAR (50)  NULL,
    [DataIntegrationsID] VARCHAR (100) NOT NULL,
    [ShouldSubmit]       BIT           CONSTRAINT [DF_PurchaseOrderDetail_ShouldSubmit] DEFAULT ((1)) NULL,
    [Submitted]          BIT           CONSTRAINT [DF_PurchaseOrderDetail_Submitted] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_PurchaseOrderDetail] PRIMARY KEY CLUSTERED ([OrderNumber] ASC, [LineNumber] ASC, [DataIntegrationsID] ASC)
);





