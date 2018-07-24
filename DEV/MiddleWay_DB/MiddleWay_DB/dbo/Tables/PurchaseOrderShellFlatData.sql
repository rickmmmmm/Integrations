CREATE TABLE [dbo].[PurchaseOrderShellFlatData] (
    [PurchaseOrderShellFlatDataUID] INT             IDENTITY (1, 1) NOT NULL,
    [ProcessTaskUid]                INT             NOT NULL,
    [RowID]                         INT             NOT NULL,
    [OrderNumber]                   VARCHAR (MAX)   NULL,
    [PurchaseDate]                  VARCHAR (MAX)   NULL,
    [Status]                        VARCHAR (MAX)   NULL,
    [VendorName]                    VARCHAR (MAX)   NULL,
    [VendorAccountNumber]           VARCHAR (MAX)   NULL,
    [SiteID]                        VARCHAR (MAX)   NULL,
    [SiteName]                      VARCHAR (MAX)   NULL,
    [EstimatedDeliveryDate]         VARCHAR (MAX)   NULL,
    [Notes]                         VARCHAR (MAX)   NULL,
    [Other1]                        VARCHAR (MAX)   NULL,
    [FRN]                           VARCHAR (MAX)   NULL,
    [Rejected]                      BIT             NOT NULL CONSTRAINT [DF_PurchaseOrderShellFlatData_Rejected] DEFAULT ((0)),
    [RejectedNotes]                 TEXT            NULL,
    CONSTRAINT [PK_PurchaseOrderShellFlatData] PRIMARY KEY CLUSTERED ([PurchaseOrderShellFlatDataUID] ASC),
    CONSTRAINT [FK_PurchaseOrderShellFlatData_ProcessTasks] FOREIGN KEY ([ProcessTaskUid]) REFERENCES [dbo].[ProcessTasks] ([ProcessTaskUid])
);

