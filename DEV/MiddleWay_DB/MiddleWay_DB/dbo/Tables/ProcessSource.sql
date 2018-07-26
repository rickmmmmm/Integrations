CREATE TABLE [dbo].[ProcessSource] (
    [ProcessSourceUid]         INT           NOT NULL,
    [ProcessSourceName]        VARCHAR (100) NOT NULL,
    [ProcessSourceTable]       VARCHAR (100) NOT NULL,
    [ProcessSourceDescription] VARCHAR (500) NOT NULL,
    [Enabled]                  BIT           CONSTRAINT [DF_ProcessSource_Enabled] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ProcessSource] PRIMARY KEY CLUSTERED ([ProcessSourceUid] ASC)
);

