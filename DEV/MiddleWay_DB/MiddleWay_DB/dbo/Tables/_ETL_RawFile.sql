CREATE TABLE [dbo].[_ETL_RawFile](
    [RowID]             [INT] IDENTITY(1,1) NOT NULL,
    [ProcessTaskUid]    [INT] NOT NULL,
    [RawData]           [VARCHAR](MAX) NULL,
    [RawDataModified]   [VARCHAR](MAX) NULL,
    CONSTRAINT [PK__ETL_RawFile] PRIMARY KEY CLUSTERED ([RowID] ASC),
    CONSTRAINT [FK__ETL_RawFile_ProcessTasks] FOREIGN KEY ([ProcessTaskUid]) REFERENCES [dbo].[ProcessTasks] ([ProcessTaskUid])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


