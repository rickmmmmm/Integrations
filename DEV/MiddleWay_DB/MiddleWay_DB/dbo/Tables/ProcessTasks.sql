CREATE TABLE [dbo].[ProcessTasks] (
    [ProcessTaskUid] INT           IDENTITY (1, 1) NOT NULL,
    [ProcessUid]     INT           NOT NULL,
    [StartTime]      DATETIME      NOT NULL,
    [EndTime]        DATETIME      NULL,
    [Parameters]     VARCHAR (500) NOT NULL,
    [Successful]     BIT           CONSTRAINT [DF_ProcessTasks_Successful] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ProcessTasks] PRIMARY KEY CLUSTERED ([ProcessTaskUid] ASC),
    CONSTRAINT [FK_ProcessTasks_Processes] FOREIGN KEY ([ProcessUid]) REFERENCES [dbo].[Processes] ([ProcessUid])
);



