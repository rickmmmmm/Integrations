CREATE TABLE [dbo].[ProcessErrors] (
    [ProcessErrorUid]  INT            IDENTITY (1, 1) NOT NULL,
    [ProcessUid]       INT            NOT NULL,
    [ErrorNumber]      INT            NULL,
    [ErrorDescription] VARCHAR (250)  NOT NULL,
    [ErrorField]       VARCHAR (100) NULL,
    [CreatedDate]      DATE           CONSTRAINT [DF_ProcessErrors_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_ProcessErrors] PRIMARY KEY CLUSTERED ([ProcessErrorUid] ASC),
    CONSTRAINT [FK_ProcessErrors_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);

