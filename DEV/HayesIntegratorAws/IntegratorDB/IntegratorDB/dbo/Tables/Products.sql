CREATE TABLE [dbo].[Products] (
    [ProductNumber]      INT           IDENTITY (1, 1) NOT NULL,
    [ProductName]        VARCHAR (100) NOT NULL,
    [ProductDescription] VARCHAR (500) NULL,
    [ProductType]        VARCHAR (50)  NULL,
    [Model]              VARCHAR (50)  NULL,
    [Manufacturer]       VARCHAR (100) NULL,
    [SuggestedPrice]     MONEY         NULL,
    [SKU]                VARCHAR (50)  NULL,
    [Serial]             VARCHAR (50)  NULL,
    [Added]              BIT           CONSTRAINT [DF_Products_Added] DEFAULT ('True') NULL,
    [Updated]            BIT           CONSTRAINT [DF_Products_Updated] DEFAULT ('False') NULL,
    [AddedDate]          DATETIME      CONSTRAINT [DF_Products_AddedDate] DEFAULT (getdate()) NULL,
    [LastUpdatedDate]    DATETIME      CONSTRAINT [DF_Products_LastUpdatedDate] DEFAULT (getdate()) NULL,
    [Client]             VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductNumber] ASC, [ProductName] ASC, [Client] ASC)
);



