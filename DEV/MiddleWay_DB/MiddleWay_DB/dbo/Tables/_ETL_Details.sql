CREATE TABLE [dbo].[_ETL_Details] (
    [_ETL_DetailUID]           INT            IDENTITY (1, 1) NOT NULL,
    [ProcessTaskUid]           INT            NOT NULL,
    [RowID]                    INT            NOT NULL,
    [PurchaseItemDetailUID]    INT            NOT NULL,
    [PurchaseUID]              INT            NOT NULL,
    [OrderNumber]              VARCHAR (50)   NOT NULL,
    [LineNumber]               INT            NOT NULL,
    [StatusID]                 INT            NOT NULL,
    [Status]                   VARCHAR (50)   NULL,
    [SiteAddedSiteUID]         INT            NOT NULL,
    [SiteAddedSiteID]          VARCHAR (100)  NULL,
    [SiteAddedSiteName]        VARCHAR (100)  NULL,
    [FundingSourceUID]         INT            NOT NULL,
    [FundingSource]            VARCHAR (500)  NULL,
    [FundingSourceDescription] VARCHAR (500)  NULL,
    [ItemUID]                  INT            NOT NULL,
    [ProductNumber]            VARCHAR (50)   NULL,
    [ProductName]              VARCHAR (100)  NULL,
    [ProductDescription]       VARCHAR (1000) NULL,
    [ItemTypeUID]              INT            NOT NULL,
    [ProductTypeName]          VARCHAR (50)   NULL,
    [ProductTypeDescription]   VARCHAR (1000) NULL,
    [QuantityOrdered]          INT            NOT NULL,
    [QuantityReceived]         INT            NOT NULL,
    [PurchasePrice]            DECIMAL (18)   NULL,
    [AccountCode]              VARCHAR (100)  NULL,
    [TechDepartmentUID]        INT            NULL,
    [DepartmentName]           VARCHAR (50)   NULL,
    [DepartmentID]             VARCHAR (50)   NULL,
    [CFDA]                     VARCHAR (50)   NULL,
    [IsAssociated]             BIT            NOT NULL,
    [Rejected]                 BIT            NOT NULL CONSTRAINT [DF__ETL_Details_Rejected] DEFAULT ((0)),
    [RejectedNotes]            TEXT           NULL,
    CONSTRAINT [PK__ETL_Details] PRIMARY KEY CLUSTERED ([_ETL_DetailUID] ASC),
    CONSTRAINT [FK__ETL_Details_ProcessTasks] FOREIGN KEY ([ProcessTaskUid]) REFERENCES [dbo].[ProcessTasks] ([ProcesstaskUid])
);



