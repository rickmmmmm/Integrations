CREATE TABLE [dbo].[DataIntegrations] (
    [IntegrationsID]            VARCHAR (100) NOT NULL,
    [IntegrationsObject]        VARCHAR (MAX) NULL,
    [DateAdded]                 DATETIME      CONSTRAINT [DF_DataIntegrations_DateAdded] DEFAULT (getdate()) NOT NULL,
    [DataProcessedSuccessfully] BIT           CONSTRAINT [DF_DataIntegrations_DataProcessedSuccessfully] DEFAULT ('False') NULL,
    [DataProcessing]            BIT           CONSTRAINT [DF_DataIntegrations_DataProcessing] DEFAULT ('False') NULL,
    [DataSentToTipweb]          BIT           CONSTRAINT [DF_DataIntegrations_DataSentToTipweb] DEFAULT ('False') NULL,
    [DataCleared]               BIT           CONSTRAINT [DF_DataIntegrations_DataCleared] DEFAULT ('False') NULL,
    [Client]                    VARCHAR (100) NULL,
    [IntegrationDate]           DATE          CONSTRAINT [DF_DataIntegrations_IntegrationDate] DEFAULT (getdate()) NULL,
    [IntegrationType]           VARCHAR (50)  NULL,
    [DataPostProcessing]        BIT           CONSTRAINT [DF_DataIntegrations_DataPostProcessing] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_DataIntegrations] PRIMARY KEY CLUSTERED ([IntegrationsID] ASC, [DateAdded] ASC)
);



