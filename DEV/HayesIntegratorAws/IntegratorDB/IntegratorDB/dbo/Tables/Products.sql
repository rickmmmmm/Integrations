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
    [Added]              BIT           DEFAULT ('True') NULL,
    [Updated]            BIT           DEFAULT ('False') NULL,
    [AddedDate]          DATETIME      DEFAULT (getdate()) NULL,
    [LastUpdatedDate]    DATETIME      DEFAULT (getdate()) NULL,
    [Client]             VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductNumber] ASC, [ProductName] ASC, [Client] ASC)
);

