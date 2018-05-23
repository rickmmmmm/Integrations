CREATE TABLE [dbo].[ShipmentIntegrationFlatData]
(
    [ShipmentIntegrationFlatDataID] INT             NOT NULL IDENTITY,
    [PO_NUMBER]                     VARCHAR(50)     NOT NULL,
    [PO_CREATION_DATE]              VARCHAR(100)    NOT NULL,
    [LINE_NUM]                      INT             NOT NULL,
    [QUANTITY_RECEIVED]             INT             NOT NULL,
    [SHIP_TO_UNIT]                  VARCHAR(100)    NOT NULL,
    [SHIP_TO_LOCATION_CODE]         VARCHAR(100)    NULL,
    [SHIP_TO_ADDRESS_LINE1]         VARCHAR(100)    NULL,
    [SHIP_TO_CITY]                  VARCHAR(100)    NULL,
    [SHIP_TO_STATE]                 VARCHAR(100)    NULL,
    [SHIP_TO_ZIP]                   VARCHAR(100)    NULL,
    [IntegrationsID]            VARCHAR(100)    NOT NULL,
    [Chunk]                         BIT             CONSTRAINT [DF_ShipmentIntegrationFlatData_Chunk] DEFAULT ((1)) NULL, 
    CONSTRAINT [PK_ShipmentIntegrationFlatData] PRIMARY KEY ([ShipmentIntegrationFlatDataID])
);
GO

CREATE INDEX [IX_ShipmentFlatDataIntegrationsID] ON [dbo].[ShipmentIntegrationFlatData] (IntegrationsID);
GO

CREATE INDEX [IX_ShipmentFlatChunk] ON [dbo].[ShipmentIntegrationFlatData] (Chunk);
GO
