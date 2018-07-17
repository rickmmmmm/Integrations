CREATE TABLE [dbo].[Mappings] (
    [MappingsUid]       INT           IDENTITY (1, 1) NOT NULL,
    [ProcessUid]        INT           NOT NULL,
    [StepName]          VARCHAR (100) NOT NULL,
    [SourceColumn]      VARCHAR (100) NOT NULL,
    [DestinationColumn] VARCHAR (100) NOT NULL,
    [Enabled]           BIT           CONSTRAINT [DF_Mappings_Enabled] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Mappings] PRIMARY KEY CLUSTERED ([MappingsUid] ASC),
    CONSTRAINT [FK_Mappings_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);

