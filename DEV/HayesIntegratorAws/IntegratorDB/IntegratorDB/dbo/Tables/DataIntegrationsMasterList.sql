CREATE TABLE [dbo].[DataIntegrationsMasterList] (
    [Client]          VARCHAR (50)  NOT NULL,
    [IntegrationType] VARCHAR (50)  NOT NULL,
    [DateAdded]       DATETIME      DEFAULT (getdate()) NULL,
    [AddObj]          VARCHAR (MAX) NULL,
    [Active]          BIT           DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([Client] ASC, [IntegrationType] ASC)
);

