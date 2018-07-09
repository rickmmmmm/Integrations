CREATE TABLE [dbo].[_ETL_Headers] (
    [_ETL_HeaderUID]        INT            IDENTITY (1, 1) NOT NULL,
	[RowID]                 INT			   NOT NULL,
    [ProcessUid]            INT            NOT NULL,
    [PurchaseUID]           INT            NOT NULL,
    [OrderNumber]           VARCHAR (50)   NOT NULL,
    [StatusID]              INT            NOT NULL,
    [Status]                VARCHAR (50)   NULL,
    [VendorUID]             INT            NOT NULL,
    [VendorName]            VARCHAR (100)  NULL,
    [VendorAccountNumber]   VARCHAR (50)   NULL,
    [SiteUID]               INT            NOT NULL,
    [SiteID]                VARCHAR (100)  NULL,
    [PurchaseDate]          DATETIME       NULL,
    [EstimatedDeliveryDate] DATETIME       NULL,
    [Notes]                 VARCHAR (1000) NULL,
    [Other1]                VARCHAR (100)  NULL,
    [FRN]                   VARCHAR (50)   NULL,
	[Rejected]              BIT			   NOT NULL CONSTRAINT [DF__ETL_Headers_Rejected] DEFAULT 0,
	[RejectedNotes]			TEXT		   NULL,
    CONSTRAINT [PK__ETL_Headers] PRIMARY KEY CLUSTERED ([_ETL_HeaderUID] ASC),
    CONSTRAINT [FK__ETL_Headers_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);



