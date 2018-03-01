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
    [ShouldSubmit]          BIT           DEFAULT ('True') NULL,
    [Submitted]             BIT           DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([OrderNumber] ASC, [DataIntegrationsID] ASC)
);

