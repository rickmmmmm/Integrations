CREATE TABLE [dbo].[Transformations] (
    [TransformationUid] INT           IDENTITY (1, 1) NOT NULL,
    [ProcessUid]        INT           NOT NULL,
    [StepName]          VARCHAR (100) NOT NULL,
    [Function]          VARCHAR (100) NOT NULL,
    [Parameters]        VARCHAR (500) NULL,
    [SourceColumn]      VARCHAR (100) NOT NULL,
    [DestinationColumn] VARCHAR (100) NULL,
    [Enabled]           BIT           CONSTRAINT [DF_Transformations_Enabled] DEFAULT ((0)) NOT NULL,
    [Order]             INT           NOT NULL,
    CONSTRAINT [PK_Transformations] PRIMARY KEY CLUSTERED ([TransformationUid] ASC),
    CONSTRAINT [FK_Transformations_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);



