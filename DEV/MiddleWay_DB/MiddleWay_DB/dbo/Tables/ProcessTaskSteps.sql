CREATE TABLE [dbo].[ProcessTaskSteps](
    [ProcessTaskStepsUid] [int] IDENTITY(1,1) NOT NULL,
    [ProcessTaskUid] [int] NOT NULL,
    [StepName] [varchar](200) NOT NULL,
    [StartDate] [datetime] NOT NULL,
    [EndDate] [datetime] NULL,
    [Successful] [bit] NOT NULL,
 CONSTRAINT [PK_ProcessTaskSteps] PRIMARY KEY CLUSTERED 
(
    [ProcessTaskStepsUid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProcessTaskSteps] ADD  DEFAULT (getutcdate()) FOR [StartDate]
GO

ALTER TABLE [dbo].[ProcessTaskSteps] ADD  DEFAULT ((0)) FOR [Successful]
GO

ALTER TABLE [dbo].[ProcessTaskSteps]  WITH CHECK ADD  CONSTRAINT [FK_ProcessTaskSteps_ProcessTasks] FOREIGN KEY([ProcessTaskUid])
REFERENCES [dbo].[ProcessTasks] ([ProcessTaskUid])
GO

ALTER TABLE [dbo].[ProcessTaskSteps] CHECK CONSTRAINT [FK_ProcessTaskSteps_ProcessTasks]
GO


