﻿CREATE TABLE [dbo].[FundingSources] (
    [FundingSourceID] VARCHAR (500) NOT NULL,
    [Added]           BIT           CONSTRAINT [DF_FundingSources_Added] DEFAULT ('True') NULL,
    [Updated]         BIT           CONSTRAINT [DF_FundingSources_Updated] DEFAULT ('False') NULL,
    [AddedDate]       DATETIME      CONSTRAINT [DF_FundingSources_AddedDate] DEFAULT (getdate()) NULL,
    [LastUpdatedDate] DATETIME      CONSTRAINT [DF_FundingSources_LastUpdatedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_FundingSources] PRIMARY KEY CLUSTERED ([FundingSourceID] ASC)
);



