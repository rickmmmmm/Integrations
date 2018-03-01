CREATE TABLE [dbo].[FundingSources] (
    [FundingSourceID] VARCHAR (500) NOT NULL,
    [Added]           BIT           DEFAULT ('True') NULL,
    [Updated]         BIT           DEFAULT ('False') NULL,
    [AddedDate]       DATETIME      DEFAULT (getdate()) NULL,
    [LastUpdatedDate] DATETIME      DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([FundingSourceID] ASC)
);

