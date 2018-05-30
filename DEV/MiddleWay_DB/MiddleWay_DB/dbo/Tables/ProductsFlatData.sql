CREATE TABLE [dbo].[ProductsFlatData] (
    [ProductsFlatDataUID]    INT            IDENTITY (1, 1) NOT NULL,
    [ProcessUid]             INT            NOT NULL,
    [ProductName]            VARCHAR (100)  NULL,
    [ProductDescription]     VARCHAR (1000) NULL,
    [ProductTypeName]        VARCHAR (50)   NULL,
    [ProductTypeDescription] VARCHAR (1000) NULL,
    [ModelNumber]            VARCHAR (100)  NULL,
    [ManufacturerName]       VARCHAR (100)  NULL,
    [SuggestedPrice]         DECIMAL (18)   NULL,
    [AreaName]               VARCHAR (100)  NULL,
    [Notes]                  VARCHAR (8000) NULL,
    [SKU]                    VARCHAR (50)   NULL,
    [SerialRequired]         BIT            NULL,
    [ProjectedLife]          INT            NULL,
    [OtherField1]            VARCHAR (1000) NULL,
    [OtherField2]            VARCHAR (1000) NULL,
    [OtherField3]            VARCHAR (1000) NULL,
    [AllowUntagged]          BIT            NULL,
    CONSTRAINT [PK_ProductsFlatData] PRIMARY KEY CLUSTERED ([ProductsFlatDataUID] ASC),
    CONSTRAINT [FK_ProductsFlatData_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);

