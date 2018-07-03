CREATE TABLE [dbo].[ProcessTasksErrors] (
    [ProcessTaskErrorUid]	INT           IDENTITY (1, 1) NOT NULL,
    [ProcessTaskUid]		INT           NOT NULL,
    [ErrorNumber]			INT           NULL,
    [ErrorDescription]		VARCHAR (250) NOT NULL,
    [ErrorField]			VARCHAR (100) NULL,
    [CreatedDate]			DATE          NOT NULL CONSTRAINT [DF_ProcessTasksErrors_CreatedDate] DEFAULT (getutcdate()),
	[Validation]			BIT			  NOT NULL CONSTRAINT [DF_ProcessTasksErrors_Validation] DEFAULT 0,
    CONSTRAINT [PK_ProcessTasksErrors] PRIMARY KEY CLUSTERED ([ProcessTaskErrorUid] ASC),
    CONSTRAINT [FK_ProcessTasksErrors_Processes] FOREIGN KEY ([ProcessTaskUid]) REFERENCES [dbo].[ProcessTasks] ([ProcessTaskUid])
);



