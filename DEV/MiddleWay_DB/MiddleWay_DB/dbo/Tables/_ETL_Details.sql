﻿CREATE TABLE [dbo].[_ETL_Details] (
    [_ETL_DetailUID]           INT            IDENTITY (1, 1) NOT NULL,
    [ProcessUid]               INT            NOT NULL,
    [PurchaseItemDetailUID]    INT            NOT NULL,
    [PurchaseUID]              INT            NOT NULL,
    [OrderNumber]              VARCHAR (50)   NOT NULL,
    [LineNumber]               INT            NOT NULL,
    [StatusID]                 INT            NOT NULL,
    [Status]                   VARCHAR (50)   NULL,
    [SiteAddedSiteUID]         INT            NOT NULL,
    [SiteID]                   VARCHAR (100)  NULL,
    [FundingSourceUID]         INT            NOT NULL,
    [FundingSource]            VARCHAR (500)  NULL,
    [FundingSourceDescription] VARCHAR (500)  NULL,
    [ItemUID]                  INT            NOT NULL,
    [ProductName]              VARCHAR (100)  NULL,
    [ProductDescription]       VARCHAR (1000) NULL,
    [ItemTypeUID]              INT            NOT NULL,
    [ProductTypeName]          VARCHAR (50)   NULL,
    [ProductTypeDescription]   VARCHAR (1000) NULL,
    [QuantityOrdered]          INT            NOT NULL,
    [QuantityReceived]         INT            NOT NULL,
    [PurchasePrice]            DECIMAL (18)   NOT NULL,
    [AccountCode]              VARCHAR (100)  NULL,
    [TechDepartmentUID]        INT            NULL,
    [DepartmentName]           VARCHAR (50)   NULL,
    [DepartmentID]             VARCHAR (50)   NULL,
    [CFDA]                     VARCHAR (50)   NULL,
    [IsAssociated]             BIT            NOT NULL,
    CONSTRAINT [PK__ETL_Details] PRIMARY KEY CLUSTERED ([_ETL_DetailUID] ASC),
    CONSTRAINT [FK__ETL_Details_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);



