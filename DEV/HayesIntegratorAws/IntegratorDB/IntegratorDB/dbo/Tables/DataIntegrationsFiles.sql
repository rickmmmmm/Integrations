CREATE TABLE [dbo].[DataIntegrationsFiles] (
    [DataIntegrationsFilesID] INT           IDENTITY (1, 1) NOT NULL,
    [FileNameAws]             VARCHAR (MAX) NULL,
    [AwsFileLink]             VARCHAR (MAX) NULL,
    [Client]                  VARCHAR (50)  NULL,
    [AddedDate]               DATETIME      CONSTRAINT [DF_DataIntegrationsFiles_AddedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_DataIntegrationsFiles] PRIMARY KEY CLUSTERED ([DataIntegrationsFilesID] ASC)
);



