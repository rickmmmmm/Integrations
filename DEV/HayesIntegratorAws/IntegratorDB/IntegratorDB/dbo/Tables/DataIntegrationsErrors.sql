CREATE TABLE [dbo].[DataIntegrationsErrors] (
    [DataIntegrationsErrorsID] INT           IDENTITY (1, 1) NOT NULL,
    [ErrorNumber]              VARCHAR (50)  NULL,
    [ErrorName]                VARCHAR (MAX) NULL,
    [ErrorDescription]         VARCHAR (MAX) NULL,
    [ErrorObject]              VARCHAR (MAX) NULL,
    [DataIntegrationsID]       VARCHAR (100) NULL,
    [AddedDate]                DATETIME      CONSTRAINT [DF_DataIntegrationsErrors_AddedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_DataIntegrationsErrors] PRIMARY KEY CLUSTERED ([DataIntegrationsErrorsID] ASC)
);



