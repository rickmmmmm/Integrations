CREATE TABLE [dbo].[Transformations] (
    [TransformationUid] INT           IDENTITY (1, 1) NOT NULL,
    [ProcessUid]        INT           NOT NULL,
    [Function]          VARCHAR (100) NOT NULL,
    [SourceColumn]      VARCHAR (100) NOT NULL,
    [DestinationColumn] VARCHAR (100) NULL,
    [Enabled]           BIT           CONSTRAINT [DF_Transformations_Enabled] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Transformations] PRIMARY KEY CLUSTERED ([TransformationUid] ASC),
    CONSTRAINT [FK_Transformations_Processes] FOREIGN KEY ([TransformationUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);

