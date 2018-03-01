CREATE TABLE [dbo].[DataIntegrations] (
    [IntegrationsID]            VARCHAR (100) NOT NULL,
    [IntegrationsObject]        VARCHAR (MAX) NULL,
    [DateAdded]                 DATETIME      DEFAULT (getdate()) NOT NULL,
    [DataProcessedSuccessfully] BIT           DEFAULT ('False') NULL,
    [DataProcessing]            BIT           DEFAULT ('False') NULL,
    [DataSentToTipweb]          BIT           DEFAULT ('False') NULL,
    [DataCleared]               BIT           DEFAULT ('False') NULL,
    [Client]                    VARCHAR (100) NULL,
    [IntegrationDate]           DATE          DEFAULT (getdate()) NULL,
    [IntegrationType]           VARCHAR (50)  NULL,
    [DataPostProcessing]        BIT           DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([IntegrationsID] ASC, [DateAdded] ASC)
);

