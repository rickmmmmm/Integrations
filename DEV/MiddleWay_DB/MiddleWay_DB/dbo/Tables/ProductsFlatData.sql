CREATE TABLE [dbo].[ProductsFlatData] (
    [ProductsFlatDataUID]    INT            IDENTITY (1, 1) NOT NULL,
    [ProcessTaskUid]         INT            NOT NULL,
    [RowID]                  INT            NOT NULL,
    [ProductName]            VARCHAR (MAX)  NULL,
    [ProductDescription]     VARCHAR (MAX)  NULL,
    [ProductTypeName]        VARCHAR (MAX)  NULL,
    [ProductTypeDescription] VARCHAR (MAX)  NULL,
    [ModelNumber]            VARCHAR (MAX)  NULL,
    [ManufacturerName]       VARCHAR (MAX)  NULL,
    [SuggestedPrice]         VARCHAR (MAX)  NULL,
    [AreaName]               VARCHAR (MAX)  NULL,
    [Notes]                  VARCHAR (MAX)  NULL,
    [SKU]                    VARCHAR (MAX)  NULL,
    [SerialRequired]         VARCHAR (MAX)  NULL,
    [ProjectedLife]          VARCHAR (MAX)  NULL,
    [OtherField1]            VARCHAR (MAX)  NULL,
    [OtherField2]            VARCHAR (MAX)  NULL,
    [OtherField3]            VARCHAR (MAX)  NULL,
    [AllowUntagged]          VARCHAR (MAX)  NULL,
    [Rejected]               BIT            NOT NULL CONSTRAINT [DF_ProductsFlatData_Rejected] DEFAULT ((0)),
    [RejectedNotes]          TEXT           NULL,
    CONSTRAINT [PK_ProductsFlatData] PRIMARY KEY CLUSTERED ([ProductsFlatDataUID] ASC),
    CONSTRAINT [FK_ProductsFlatData_ProcessTasks] FOREIGN KEY ([ProcessTaskUid]) REFERENCES [dbo].[ProcessTasks] ([ProcessTaskUid])
);

