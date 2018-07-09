﻿CREATE TABLE [dbo].[InventoryFlatData] (
    [InventoryFlatDataUID]     INT            IDENTITY (1, 1) NOT NULL,
    [ProcessUid]               INT            NOT NULL,
    [RowID]                    INT            NOT NULL,
    [AssetID]                  VARCHAR (100)  NULL,
    [Tag]                      VARCHAR (50)   NULL,
    [Serial]                   VARCHAR (50)   NULL,
    [SiteID]                   VARCHAR (100)  NULL,
    [SiteName]                 VARCHAR (100)  NULL,
    [Location]                 VARCHAR (50)   NULL,
    [Status]                   VARCHAR (50)   NULL,
    [DepartmentName]           VARCHAR (50)   NULL,
    [DepartmentID]             VARCHAR (50)   NULL,
    [FundingSource]            VARCHAR (500)  NULL,
    [FundingSourceDescription] VARCHAR (500)  NULL,
    [PurchasePrice]            DECIMAL (18)   NULL,
    [PurchaseDate]             DATETIME       NULL,
    [ExpirationDate]           DATETIME       NULL,
    [InventoryNotes]           VARCHAR (3000) NULL,
    [OrderNumber]              VARCHAR (50)   NOT NULL,
    [VendorName]               VARCHAR (100)  NULL,
    [VendorAccountNumber]      VARCHAR (50)   NULL,
    [ParentTag]                VARCHAR (50)   NULL,
    [ProductName]              VARCHAR (100)  NULL,
    [ProductDescription]       VARCHAR (1000) NULL,
    [ProductByNumber]          VARCHAR (50)   NULL,
    [ProductTypeName]          VARCHAR (50)   NULL,
    [ProductTypeDescription]   VARCHAR (1000) NULL,
    [ModelNumber]              VARCHAR (100)  NULL,
    [ManufacturerName]         VARCHAR (100)  NULL,
    [AreaName]                 VARCHAR (100)  NULL,
    [CustomField1Value]        VARCHAR (50)   NULL,
    [CustomField1Label]        VARCHAR (50)   NULL,
    [CustomField2Value]        VARCHAR (50)   NULL,
    [CustomField2Label]        VARCHAR (50)   NULL,
    [CustomField3Value]        VARCHAR (50)   NULL,
    [CustomField3Label]        VARCHAR (50)   NULL,
    [CustomField4Value]        VARCHAR (50)   NULL,
    [CustomField4Label]        VARCHAR (50)   NULL,
    [InvoiceNumber]            VARCHAR (25)   NULL,
    [InvoiceDate]              DATE           NULL,
    [Rejected]                 BIT            NOT NULL CONSTRAINT [DF_InventoryFlatData_Rejected] DEFAULT 0,
    [RejectedNotes]            TEXT           NULL,
    CONSTRAINT [PK_InventoryFlatData] PRIMARY KEY CLUSTERED ([InventoryFlatDataUID] ASC),
    CONSTRAINT [FK_InventoryFlatData_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);



