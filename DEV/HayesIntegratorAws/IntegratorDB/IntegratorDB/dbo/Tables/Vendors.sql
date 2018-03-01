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
    [Added]           BIT           DEFAULT ('True') NULL,
    [Updated]         BIT           DEFAULT ('False') NULL,
    [AddedDate]       DATETIME      DEFAULT (getdate()) NULL,
    [LastUpdatedDate] DATETIME      DEFAULT (getdate()) NULL,
    [Client]          VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([VendorID] ASC, [VendorName] ASC, [Client] ASC)
);

