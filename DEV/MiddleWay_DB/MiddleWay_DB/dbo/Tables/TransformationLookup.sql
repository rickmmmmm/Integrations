CREATE TABLE [dbo].[TransformationLookup] (
    [TransformationLookupUID] INT            IDENTITY (1, 1) NOT NULL,
    [ProcessUid]              INT            NOT NULL,
    [TransformationLookupKey] VARCHAR (50)   NOT NULL,
    [Key]                     VARCHAR (1000) NOT NULL,
    [Value]                   VARCHAR (1000) NOT NULL,
    [Enabled]                 BIT            CONSTRAINT [DF_TransformationLookup_Enabled] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TransformationLookup] PRIMARY KEY CLUSTERED ([TransformationLookupUID] ASC),
    CONSTRAINT [FK_TransformationLookup_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);

