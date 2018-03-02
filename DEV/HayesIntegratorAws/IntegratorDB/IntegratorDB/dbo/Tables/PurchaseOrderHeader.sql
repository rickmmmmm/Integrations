CREATE TABLE [dbo].[PurchaseOrderHeader] (
    [OrderNumber]           VARCHAR (50)  NOT NULL,
    [Status]                VARCHAR (50)  NULL,
    [VendorID]              INT           NULL,
    [VendorName]            VARCHAR (100) NULL,
    [SiteID]                VARCHAR (50)  NULL,
    [PurchaseDate]          DATETIME      NULL,
    [EstimatedDeliveryDate] DATETIME      NULL,
    [Notes]                 VARCHAR (500) NULL,
    [Other1]                VARCHAR (100) NULL,
    [DataIntegrationsID]    VARCHAR (100) NOT NULL,
    [ShouldSubmit]          BIT           CONSTRAINT [DF_PurchaseOrderHeader_ShouldSubmit] DEFAULT ('True') NULL,
    [Submitted]             BIT           CONSTRAINT [DF_PurchaseOrderHeader_Submitted] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_PurchaseOrderHeader] PRIMARY KEY CLUSTERED ([OrderNumber] ASC, [DataIntegrationsID] ASC)
);



