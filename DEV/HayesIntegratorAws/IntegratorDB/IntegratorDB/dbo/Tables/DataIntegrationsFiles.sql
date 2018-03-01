CREATE TABLE [dbo].[DataIntegrationsFiles] (
    [DataIntegrationsFilesID] INT           IDENTITY (1, 1) NOT NULL,
    [FileNameAws]             VARCHAR (MAX) NULL,
    [AwsFileLink]             VARCHAR (MAX) NULL,
    [Client]                  VARCHAR (50)  NULL,
    [AddedDate]               DATETIME      DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([DataIntegrationsFilesID] ASC)
);

