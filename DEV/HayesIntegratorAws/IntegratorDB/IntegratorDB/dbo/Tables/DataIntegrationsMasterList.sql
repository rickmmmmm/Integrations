CREATE TABLE [dbo].[DataIntegrationsMasterList] (
    [Client]          VARCHAR (50)  NOT NULL,
    [IntegrationType] VARCHAR (50)  NOT NULL,
    [DateAdded]       DATETIME      CONSTRAINT [DF_DataIntegrationsMasterList_DateAdded] DEFAULT (getdate()) NULL,
    [AddObj]          VARCHAR (MAX) NULL,
    [Active]          BIT           CONSTRAINT [DF_DataIntegrationsMasterList_Active] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_DataIntegrationsMasterList] PRIMARY KEY CLUSTERED ([Client] ASC, [IntegrationType] ASC)
);



