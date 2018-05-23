CREATE TABLE [dbo].[Vendors] (
    [VendorID]        INT           NOT NULL,
    [VendorName]      VARCHAR (100) NOT NULL,
    [Address1]        VARCHAR (50)  NULL,
    [Address2]        VARCHAR (50)  NULL,
    [City]            VARCHAR (50)  NULL,
    [State]           VARCHAR (2)   NULL,
    [ZipCode]         VARCHAR (50)  NULL,
    [Phone]           VARCHAR (50)  NULL,
    [Email]           VARCHAR (100) NULL,
    [Added]           BIT           CONSTRAINT [DF_Vendors_Added] DEFAULT ((1)) NULL,
    [Updated]         BIT           CONSTRAINT [DF_Vendors_Updated] DEFAULT ((0)) NULL,
    [AddedDate]       DATETIME      CONSTRAINT [DF_Vendors_AddedDate] DEFAULT (getdate()) NULL,
    [LastUpdatedDate] DATETIME      CONSTRAINT [DF_Vendors_LastUpdatedDate] DEFAULT (getdate()) NULL,
    [Client]          VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_Vendors] PRIMARY KEY CLUSTERED ([VendorID] ASC, [VendorName] ASC, [Client] ASC)
);



