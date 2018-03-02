CREATE TABLE [dbo].[PurchaseOrderIntegrationFlatData] (
    [PO_NUMBER]       VARCHAR (50)  NOT NULL,
    [PO_DATE]         DATETIME      NULL,
    [VENDOR_NAME]     VARCHAR (100) NULL,
    [VENDOR_ID]       INT           NULL,
    [LINE_NUMBER]     INT           NOT NULL,
    [PRODUCT_NAME]    VARCHAR (100) NULL,
    [PRODUCT_TYPE]    VARCHAR (100) CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_ProductType] DEFAULT ('Unassigned') NULL,
    [MODEL]           VARCHAR (100) CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_Model] DEFAULT ('None') NULL,
    [MANUFACTURER]    VARCHAR (100) CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_Manufacturer] DEFAULT ('None') NULL,
    [FUNDING_SOURCE]  VARCHAR (100) NULL,
    [DEPARTMENT]      VARCHAR (100) NULL,
    [ACCOUNT_CODE]    VARCHAR (100) NULL,
    [PURCHASE_PRICE]  MONEY         NULL,
    [QUANTITYORDERED] INT           NULL,
    [NOTES]           VARCHAR (MAX) NULL,
    [SHIPPEDTOSITE]   VARCHAR (100) NULL,
    [QUANTITYSHIPPED] INT           NULL,
    [CFDA]            VARCHAR (50)  NULL,
    [IntegrationsID]  VARCHAR (100) NOT NULL,
    [Chunk]           BIT           CONSTRAINT [DF_PurchaseOrderIntegrationFlatData_Chunk] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_PurchaseOrderIntegrationFlatData] PRIMARY KEY CLUSTERED ([PO_NUMBER] ASC, [IntegrationsID] ASC, [LINE_NUMBER] ASC)
);



