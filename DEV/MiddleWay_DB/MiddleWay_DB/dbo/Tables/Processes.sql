CREATE TABLE [dbo].[Processes] (
    [ProcessUid]  INT           IDENTITY (1, 1) NOT NULL,
    [Client]      VARCHAR (100) NOT NULL,
    [ProcessName] VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (250) NOT NULL,
    [Enabled]     BIT           CONSTRAINT [DF_Processes_Enabled] DEFAULT ((0)) NOT NULL,
    [CreatedDate] DATE          CONSTRAINT [DF_Processes_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Processes] PRIMARY KEY CLUSTERED ([ProcessUid] ASC)
);

