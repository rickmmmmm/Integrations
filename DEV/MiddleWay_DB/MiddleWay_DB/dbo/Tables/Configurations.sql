CREATE TABLE [dbo].[Configurations] (
    [ConfigurationUid]   INT           IDENTITY (1, 1) NOT NULL,
    [ProcessUid]         INT           NOT NULL,
    [ConfigurationName]  VARCHAR (100) NOT NULL,
    [ConfigurationValue] VARCHAR (250) NOT NULL,
    [Enabled]            BIT           NOT NULL,
    CONSTRAINT [PK_Configurations] PRIMARY KEY CLUSTERED ([ConfigurationUid] ASC),
    CONSTRAINT [FK_Configurations_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);



