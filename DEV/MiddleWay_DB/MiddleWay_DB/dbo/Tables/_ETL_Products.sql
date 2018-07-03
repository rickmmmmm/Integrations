CREATE TABLE [dbo].[_ETL_Products] (
    [_ETL_ProductsUID]       INT            IDENTITY (1, 1) NOT NULL,
    [ProcessUid]             INT            NOT NULL,
    [ProductUID]             INT            NOT NULL,
    [ProductNumber]          VARCHAR (50)   NOT NULL,
    [ProductName]            VARCHAR (100)  NOT NULL,
    [ProductDescription]     VARCHAR (1000) NOT NULL,
    [ItemTypeUID]            INT            NOT NULL,
    [ProductTypeName]        VARCHAR (50)   NOT NULL,
    [ProductTypeDescription] VARCHAR (1000) NULL,
    [ModelNumber]            VARCHAR (100)  NULL,
    [ManufacturerUID]        INT            NOT NULL,
    [ManufacturerName]       VARCHAR (100)  NOT NULL,
    [SuggestedPrice]         DECIMAL (18)   NULL,
    [AreaUID]                INT            NOT NULL,
    [AreaName]               VARCHAR (100)  NULL,
    [ItemNotes]              VARCHAR (8000) NULL,
    [SKU]                    VARCHAR (50)   NULL,
    [SerialRequired]         BIT            NOT NULL,
    [ProjectedLife]          INT            NOT NULL,
    [CustomField1]           VARCHAR (1000) NULL,
    [CustomField2]           VARCHAR (1000) NULL,
    [CustomField3]           VARCHAR (1000) NULL,
    [Active]                 BIT            NOT NULL,
    [AllowUntagged]          BIT            NOT NULL,
 	[Rejected]               BIT			NOT NULL CONSTRAINT [DF__ETL_Products_Rejected] DEFAULT 0,
	[RejectedNotes]			 TEXT			NULL
    CONSTRAINT [PK__ETL_Products] PRIMARY KEY CLUSTERED ([_ETL_ProductsUID] ASC),
    CONSTRAINT [FK__ETL_Products_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);



