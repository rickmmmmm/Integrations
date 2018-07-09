CREATE TABLE [dbo].[PurchaseOrderShellFlatData] (
    [PurchaseOrderShellFlatDataUID] INT            IDENTITY (1, 1) NOT NULL,
    [ProcessUid]                    INT            NOT NULL,
    [RowID]                         INT            NOT NULL,
    [OrderNumber]                   VARCHAR (50)   NULL,
    [PurchaseDate]                  DATETIME       NULL,
    [Status]                        VARCHAR (50)   NULL,
    [VendorName]                    VARCHAR (100)  NULL,
    [VendorAccountNumber]           VARCHAR (50)   NULL,
    [SiteID]                        VARCHAR (100)  NULL,
    [SiteName]                      VARCHAR (100)  NULL,
    [EstimatedDeliveryDate]         DATETIME       NULL,
    [Notes]                         VARCHAR (1000) NULL,
    [Other1]                        VARCHAR (100)  NULL,
    [FRN]                           VARCHAR (100)  NULL,
    [Rejected]                      BIT            NOT NULL CONSTRAINT [DF_PurchaseOrderShellFlatData_Rejected] DEFAULT 0,
    [RejectedNotes]                 TEXT           NULL,
    CONSTRAINT [PK_PurchaseOrderShellFlatData] PRIMARY KEY CLUSTERED ([PurchaseOrderShellFlatDataUID] ASC),
    CONSTRAINT [FK_PurchaseOrderShellFlatData_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);

