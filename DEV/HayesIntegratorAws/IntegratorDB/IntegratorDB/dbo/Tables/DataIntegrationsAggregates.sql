CREATE TABLE [dbo].[DataIntegrationsAggregates] (
    [AggregatesID]         INT           IDENTITY (1, 1) NOT NULL,
    [Client]               VARCHAR (50)  NULL,
    [IntegrationType]      VARCHAR (50)  NULL,
    [DateRun]              DATE          NULL,
    [DataType]             VARCHAR (50)  NULL,
    [ReferenceVal]         VARCHAR (50)  NULL,
    [TotalCount]           BIGINT        NULL,
    [ReferenceDescription] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_DataIntegrationsAggregates] PRIMARY KEY CLUSTERED ([AggregatesID] ASC)
);



