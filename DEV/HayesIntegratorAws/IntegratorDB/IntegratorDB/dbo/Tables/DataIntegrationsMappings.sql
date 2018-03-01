CREATE TABLE [dbo].[DataIntegrationsMappings] (
    [MappingsID]     VARCHAR (50)  NOT NULL,
    [MappingsStep]   INT           IDENTITY (1, 1) NOT NULL,
    [MappingsObject] VARCHAR (MAX) NULL,
    [Client]         VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([MappingsID] ASC, [MappingsStep] ASC)
);

