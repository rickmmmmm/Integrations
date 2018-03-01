CREATE TABLE [dbo].[DataIntegrationsLinkTable] (
    [LinkID]    INT           IDENTITY (1, 1) NOT NULL,
    [Client]    VARCHAR (50)  NULL,
    [SourceVal] VARCHAR (MAX) NULL,
    [DestVal]   VARCHAR (MAX) NULL,
    [LinkType]  VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([LinkID] ASC)
);

