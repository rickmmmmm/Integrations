
DELETE FROM [Configurations] WHERE ProcessUid = 1
DELETE FROM [Transformations] WHERE ProcessUid = 1
DELETE FROM [Mappings] WHERE ProcessUid = 1


INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'NotificationType', 'Email', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ElasticEmailApiKey', '', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ElasticEmailAPIUrl', ' ', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ElasticSMSAPIUrl', '', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'SqlServerDbMailProfileName', 'TIPWEBIMEmail', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'FromName', 'Integration Test', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'NotificationFrom', 'tipwebmailnotifications@hayessoft.com', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'NotificationSentTo', 'gcollazo@hayessoft.com', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'TIPWebConnection', 'Server=.;Database=TIPWebHostedChicagoPS;Trusted_Connection=True;', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DataSourceType', 'SQL', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DataSourcePath', '', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'TextQualifier', '', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'Delimiter', '', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ExternalDataSourceConnection', 'Server=HayesConversion;Database=IntgCPSTechXL;User ID=integration-middleway;Password=p@ssw0rd;', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ReadBatchSize', '500', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ExternalDataSourceQuerySelect', 'SELECT DISTINCT
    AP.PO_XREF AS ORDERNUMBER,
    AP.DATE_PURCHASED AS PURCHASEDATE,
    ISNULL(AP.PURCHASE_PRICE, 0) AS PURCHASEPRICE,
    10000 AS LINENUMBER,
    ISNULL(A.OEM_MODEL,''UNKNOWN'') AS PRODUCTNAME,
    4 AS FUNDINGSOURCEUID,
    ''Unassigned'' AS FUNDINGSOURCE,
    CASE
        WHEN A.DEVICE_ROLE = ''DC'' THEN ''SV''
        WHEN A.DEVICE_ROLE IS NULL THEN ''UN''
        ELSE A.DEVICE_ROLE
    END AS PRODUCTTYPENAMEMAPPING,
       CASE 
       WHEN A.DEVICE_ROLE =''NB'' THEN ''COMP: TABLET/IPAD''
       WHEN A.DEVICE_ROLE =''DC'' THEN ''COMP: SERVER''
       WHEN A.DEVICE_ROLE =''WK'' THEN ''COMP: DESKTOP''
       WHEN A.DEVICE_ROLE =''LT'' THEN
              CASE 
              WHEN A.OEM_MODEL LIKE ''Mac%'' OR A.OEM_MAKE LIKE ''%Apple%'' THEN ''COMP: LAPTOP (MACBOOK)''
              WHEN A.OEM_MODEL LIKE ''Samsung%'' THEN ''COMP: LAPTOP (CHROMEBOOK)''
              ELSE ''COMP: LAPTOP (PC)''
              END
       ELSE ''UNKOWN'' END AS PRODUCTTYPENAME,
    ISNULL(A.OEM_MODEL,''UNKNOWN'') AS MODELNUMBER,
    ISNULL(A.OEM_MAKE,''UNKNOWN'') AS MANUFACTURERNAME,
    D.ORACLE_ID AS SITEID,
    A.ASSET_ID AS ASSETID,
    A.ASSET_TAG AS TAG,
    A.SERIAL_NUMBER AS SERIAL,
    ''UNVERIFIED'' AS LOCATIONNAME, --A.LOC_ROOM_NUM AS LOCATION,
    ''UNASSIGNED'' AS LOCATIONTYPENAME, --A.LOC_ROOM_TYPE AS LOCATIONTYPE,
    ''999'' AS DEPARTMENTID,
    A.[HOST_NAME] AS CUSTOMFIELD1VALUE,
    ''Host Name'' AS CUSTOMFIELD1LABEL,
    MAX(A.LAST_LOGON_USER) AS CUSTOMFIELD2VALUE,
    ''Last Logon'' AS CUSTOMFIELD2LABEL,
    MAX(ISNULL(ISNULL(ISNULL(ISNULL(ISNULL(A.DATE_COLLECTED_MSB,A.DATE_COLLECTED_SMS),DATE_COLLECTED_AU),DATE_COLLECTED_AV),DATE_COLLECTED_DHC), '''')) AS CUSTOMFIELD3VALUE,
    ''TechXL Last Scan Date'' AS CUSTOMFIELD3LABEL', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ExternalDataSourceQueryBody', 'FROM TECHXL_ASSETS A WITH(NOLOCK)
JOIN TECHXL_ASSET_PURCHASE_INFO AP WITH(NOLOCK)
    ON AP.ASSET_ID = A.ASSET_ID
LEFT OUTER JOIN TECHXL_DEPARTMENTS D WITH(NOLOCK)
    ON D.DEPARTMENT_ID = A.DEPARTMENT_ID
LEFT OUTER JOIN dbo.TEMP_JAMF TJ WITH(NOLOCK)
    ON A.SERIAL_NUMBER = TJ.serial_number', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ExternalDataSourceQueryWhere', 'WHERE 
    (CAST(DATE_COLLECTED_SMS AS date) > CASE WHEN DATEPART(dw,GETDATE())=1 THEN DATEADD(day, -15, CAST(getdate() AS date)) ELSE DATEADD(day, -2, CAST(getdate() AS date)) END
     OR CAST(DATE_COLLECTED_AU AS date) > CASE WHEN DATEPART(dw,GETDATE())=1 THEN DATEADD(day, -15, CAST(getdate() AS date)) ELSE DATEADD(day, -2, CAST(getdate() AS date)) END
     OR CAST(DATE_COLLECTED_AV AS date) > CASE WHEN DATEPART(dw,GETDATE())=1 THEN DATEADD(day, -15, CAST(getdate() AS date)) ELSE DATEADD(day, -1, CAST(getdate() AS date)) END
     OR CAST(DATE_COLLECTED_MSB AS date) > CASE WHEN DATEPART(dw,GETDATE())=1 THEN DATEADD(day, -15, CAST(getdate() AS date)) ELSE DATEADD(day, -1, CAST(getdate() AS date)) END
     OR CAST(DATE_COLLECTED_DHC AS date) > CASE WHEN DATEPART(dw,GETDATE())=1 THEN DATEADD(day, -15, CAST(getdate() AS date)) ELSE DATEADD(day, -1, CAST(getdate() AS date)) END)
    OR (CAST(LOST_ASSET_SVC_EXPIRE AS date) > CASE WHEN DATEPART(dw,GETDATE())=1 THEN DATEADD(day, 1025, CAST(getdate() AS date)) ELSE DATEADD(day, 1010, CAST(getdate() AS date)) END )
    AND TJ.serial_number IS NOT NULL', 0)
--INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ExternalDataSourceQueryWhere', 'WHERE 
--    (CAST(DATE_COLLECTED_SMS AS date) > CASE WHEN DATEPART(dw,GETDATE())=1 THEN DATEADD(day, -30, CAST(getdate() AS date)) ELSE DATEADD(day, -30, CAST(getdate() AS date)) END
--     OR CAST(DATE_COLLECTED_AU AS date) > CASE WHEN DATEPART(dw,GETDATE())=1 THEN DATEADD(day, -30, CAST(getdate() AS date)) ELSE DATEADD(day, -30, CAST(getdate() AS date)) END
--     OR CAST(DATE_COLLECTED_AV AS date) > CASE WHEN DATEPART(dw,GETDATE())=1 THEN DATEADD(day, -30, CAST(getdate() AS date)) ELSE DATEADD(day, -30, CAST(getdate() AS date)) END
--     OR CAST(DATE_COLLECTED_MSB AS date) > CASE WHEN DATEPART(dw,GETDATE())=1 THEN DATEADD(day, -30, CAST(getdate() AS date)) ELSE DATEADD(day, -30, CAST(getdate() AS date)) END
--     OR CAST(DATE_COLLECTED_DHC AS date) > CASE WHEN DATEPART(dw,GETDATE())=1 THEN DATEADD(day, -30, CAST(getdate() AS date)) ELSE DATEADD(day, -30, CAST(getdate() AS date)) END)
--    OR (CAST(LOST_ASSET_SVC_EXPIRE AS date) > CASE WHEN DATEPART(dw,GETDATE())=1 THEN DATEADD(day, 1025, CAST(getdate() AS date)) ELSE DATEADD(day, 1010, CAST(getdate() AS date)) END )
--    AND TJ.serial_number IS NOT NULL', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ExternalDataSourceQueryGroup', 'GROUP BY 
    AP.PO_XREF,
    AP.DATE_PURCHASED,
    ISNULL(AP.PURCHASE_PRICE, 0),
    --ISNULL(AP.PO_line_number, 10000),
    ISNULL(A.OEM_MODEL,''UNKNOWN''),
    CASE
        WHEN A.DEVICE_ROLE = ''DC'' THEN ''SV''
        WHEN A.DEVICE_ROLE IS NULL THEN ''UN''
        ELSE A.DEVICE_ROLE
    END,
       CASE 
       WHEN A.DEVICE_ROLE =''NB'' THEN ''COMP: TABLET/IPAD''
       WHEN A.DEVICE_ROLE =''DC'' THEN ''COMP: SERVER''
       WHEN A.DEVICE_ROLE =''WK'' THEN ''COMP: DESKTOP''
       WHEN A.DEVICE_ROLE =''LT'' THEN
              CASE 
              WHEN A.OEM_MODEL LIKE ''Mac%'' OR A.OEM_MAKE LIKE ''%Apple%'' THEN ''COMP: LAPTOP (MACBOOK)''
              WHEN A.OEM_MODEL LIKE ''Samsung%'' THEN ''COMP: LAPTOP (CHROMEBOOK)''
              ELSE ''COMP: LAPTOP (PC)''
              END
       ELSE ''UNKOWN'' END,
    ISNULL(A.OEM_MODEL,''UNKNOWN''),
    ISNULL(A.OEM_MAKE,''UNKNOWN''),
    D.ORACLE_ID,
    A.ASSET_ID,
    A.ASSET_TAG,
    A.SERIAL_NUMBER,
    --A.LOC_ROOM_NUM,
    --A.LOC_ROOM_TYPE,
    A.DEPARTMENT_ID,
    A.[HOST_NAME],
    A.LAST_LOGON_USER', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ExternalDataSourceQueryOrder', 'ORDER BY 
    ISNULL(A.OEM_MODEL,''UNKNOWN''),
    ISNULL(A.OEM_MAKE,''UNKNOWN''),
    D.ORACLE_ID,
    A.ASSET_ID,
    A.ASSET_TAG,
    A.SERIAL_NUMBER', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ExternalDataSourceQueryOffset', '--ORDER BY PO_NUMBER, LINE_NUMBER
OFFSET @OFFSET ROWS
FETCH NEXT @LIMIT ROWS ONLY;', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ReadOffset', '0', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'ReadLimit', '500', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'UseTagInNotesValidation', '1', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'CreateProducts', '1', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'UpdateProductName', '1', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'OverwriteProductManufacturerFromSource', '1', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'CreateProductTypes', '0', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DefaultProductType', 'Unknown', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'CreateCustomFields', '0', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'CreateManufacturers', 'False', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DefaultManufacturer', 'Unassigned', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DefaultModel', 'Unassigned', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'CreateFundingSources', '0', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DefaultFundingSourceUID', '4', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'CreateDefaultRoom', '1', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DefaultRoom', 'Unverified', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DefaultRoomType', 'Unassigned', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'CreateAreas', '0', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DefaultArea', 'None', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'CreateVendors', '0', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DefaultVendorUID', '0', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DefaultDepartmentUID', '9', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DefaultStatusID', '26', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'RequirePurchaseOrder', '1', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'MatchByOrderNumberOnly', '1', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'CreateNewPurchaseOrders', '0', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'MatchByProductAndLineOnly', '1', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'CreateNewPurchaseDetails', '1', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'DefaultLineNumber', '10000', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'CreateNewPurchaseShipments', '1', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'CreatePurchaseInvoice', '0', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'FailInvalidParentTagRelationships', '0', 1)   -- CHECK
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'AllowStackingErrors', '1', 1)   -- CHECK
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'UpdateSerial', '1', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'UpdateAssetUID', '1', 1)
INSERT INTO [Configurations] (ProcessUid, ConfigurationName, ConfigurationValue, Enabled) VALUES (1, 'InsertNote', 'Hayes TechXL Integration - {DATE}', 1)--'' + CONVERT(VARCHAR(8),GETDATE(),1)  AS Notes

INSERT INTO [dbo].[Transformations]
    ([ProcessUid], [StepName], [Function], [Parameters], [SourceColumn], [DestinationColumn], [Enabled], [Order])
VALUES
--    (0, '', '', '', '', '', NULL, 0)
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', 'InventoryFlatDataUid', '_ETL_InventoryUid', 1, 1),
    (1, 'Stage', '', '', 'ProcessUid', 'ProcessUid', 1, 1),
    (1, 'Stage', '', '', 'RowId', 'RowId', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'InventoryUid', 1, 1),
    (1, 'Stage', '', '', 'AssetId', 'AssetId', 1, 1),
    (1, 'Stage', 'default', '[]', 'Tag', 'Tag', 1, 1),
    (1, 'Stage', '', '', 'Serial', 'Serial', 1, 1),
    (1, 'Stage', 'default', '[1]', '', 'InventoryTypeUid', 1, 1),
    (1, 'Stage', 'default', '[Tagged]', 'InventoryTypeName', 'InventoryTypeName', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'ItemUid', 1, 1),
    (1, 'Stage', 'truncate', '[100]', 'ProductName', 'ProductName', 1, 1),                          --LOOKUP OR PATTERN MATCH??
    (1, 'Stage', 'truncate', '[1000]', 'ProductDescription', 'ProductDescription', 1, 1),
    (1, 'Stage', 'truncate', '[100]', 'ProductByNumber', 'ProductByNumber', 1, 1),
    (1, 'Stage', 'default', '[0]', 'ProductByNumber', 'ProductByNumber', 1, 2),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'ItemTypeUid', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'ProductTypeName', 'ProductTypeName', 1, 1),
    (1, 'Stage', 'truncate', '[1000]', 'ProductTypeDescription', 'ProductTypeDescription', 1, 1),
    (1, 'Stage', 'truncate', '[500]', 'ModelNumber', 'ModelNumber', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'ManufacturerUid', 1, 1),
    (1, 'Stage', 'truncate', '[100]', 'ManufacturerName', 'ManufacturerName', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'AreaUid', 1, 1),
    (1, 'Stage', 'truncate', '[100]', 'AreaName', 'AreaName', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'SiteUid', 1, 1),
    (1, 'Stage', 'truncate', '[100]', 'SiteId', 'SiteId', 1, 1),                                    --LOOKUP??
    (1, 'Stage', 'truncate', '[100]', 'SiteName', 'SiteName', 1, 1),                                --LOOKUP??
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'EntityUid', 1, 1),
    (1, 'Stage', 'default', '[]', '', 'EntityId', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'LocationName', 'EntityName', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'EntityTypeUid', 1, 1),
    (1, 'Stage', 'truncate', '[1000]', 'LocationTypeName', 'EntityTypeName', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'StatusId', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'Status', 'Status', 1, 1),                                     --LOOKUP??
    (1, 'Stage', 'default', '[9]', '', 'TechDepartmentUid', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'DepartmentName', 'DepartmentName', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'DepartmentId', 'DepartmentId', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'FundingSourceUid', 1, 1),
    (1, 'Stage', 'truncate', '[500]', 'FundingSource', 'FundingSource', 1, 1),
    (1, 'Stage', 'truncate', '[500]', 'FundingSourceDescription', 'FundingSourceDescription', 1, 1),
    (1, 'Stage', 'quickcast', '', 'PurchasePrice', 'PurchasePrice', 1, 1),
    (1, 'Stage', 'default', '', 'PurchasePrice', 'PurchasePrice', 1, 2),
    (1, 'Stage', 'quickcast', '', 'PurchaseDate', 'PurchaseDate', 1, 1),
    (1, 'Stage', 'default', '', 'PurchaseDate', 'PurchaseDate', 1, 2),
    (1, 'Stage', 'quickcast', '', 'ExpirationDate', 'ExpirationDate', 1, 1),
    (1, 'Stage', 'default', '', 'ExpirationDate', 'ExpirationDate', 1, 2),
    (1, 'Stage', '', '', 'InventoryNotes', 'InventoryNotes', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'ParentInventoryUid', 1, 1),
    (1, 'Stage', 'default', '', 'ParentTag', 'ParentTag', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'InventorySourceUid', 1, 1),
    (1, 'Stage', 'default', '', 'InventorySourceName', 'InventorySourceName', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'PurchaseUid', 1, 1),
    (1, 'Stage', 'default', '[]', 'OrderNumber', 'OrderNumber', 1, 1),
    (1, 'Stage', 'truncate', '[100]', 'SiteId', 'PurchaseSiteId', 1, 1),                                    --LOOKUP??
    (1, 'Stage', 'truncate', '[100]', 'SiteName', 'PurchaseSiteName', 1, 1),                                --LOOKUP??    
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'VendorUid', 1, 1),
    (1, 'Stage', 'truncate', '[100]', 'VendorName', 'VendorName', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'VendorAccountNumber', 'VendorAccountNumber', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'PurchaseItemDetailUid', 1, 1),
    (1, 'Stage', 'default', '', 'LineNumber', 'LineNumber', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'AccountCode', 1, 1),
    (1, 'Stage', 'truncate', '[100]', 'SiteId', 'SiteAddedSiteId', 1, 1),                                    --LOOKUP??
    (1, 'Stage', 'truncate', '[100]', 'SiteName', 'SiteAddedSiteName', 1, 1),                                --LOOKUP??
    (1, 'Stage', 'truncate', '[100]', 'SiteId', 'ShippedToSiteId', 1, 1),                                    --LOOKUP??
    (1, 'Stage', 'truncate', '[100]', 'SiteName', 'ShippedToSiteName', 1, 1),                                --LOOKUP??
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'PurchaseItemShipmentUid', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'InvoiceNumber', 'InvoiceNumber', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'InvoiceDate', 'InvoiceDate', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'InventoryExt1Uid', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'InventoryMeta1Uid', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'CustomField1Label', 'CustomField1Label', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'CustomField1Value', 'CustomField1Value', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'InventoryExt2Uid', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'InventoryMeta2Uid', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'CustomField2Label', 'CustomField2Label', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'CustomField2Value', 'CustomField2Value', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'InventoryExt3Uid', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'InventoryMeta3Uid', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'CustomField3Label', 'CustomField3Label', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'CustomField3Value', 'CustomField3Value', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'InventoryExt4Uid', 1, 1),
    --(1, 'Stage', '[FUNCTION]', '[PARAMETERS]', '', 'InventoryMeta4Uid', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'CustomField4Label', 'CustomField4Label', 1, 1),
    (1, 'Stage', 'truncate', '[50]', 'CustomField4Value', 'CustomField4Value', 1, 1)




INSERT INTO [Mappings]
    (ProcessUid, StepName, SourceColumn, DestinationColumn, Enabled)
VALUES
    --(1, 'Stage', 'InventoryFlatDataUid', '_ETL_InventoryUid', 1),
    (1, 'Stage', 'ProcessUid', 'ProcessUid', 1),
    (1, 'Stage', 'RowId', 'RowId', 1),
    --(1, 'Stage', '', 'InventoryUid', 1),
    (1, 'Stage', 'AssetId', 'AssetId', 1),
    (1, 'Stage', 'Tag', 'Tag', 1),
    (1, 'Stage', 'Serial', 'Serial', 1),
    (1, 'Stage', 'InventoryTypeUid', 'InventoryTypeUid', 1),
    (1, 'Stage', 'InventoryTypeName', 'InventoryTypeName', 1),
    (1, 'Stage', 'ProductNumber', 'ProductNumber', 1),
    (1, 'Stage', 'ProductName', 'ProductName', 1),
    (1, 'Stage', 'ProductDescription', 'ProductDescription', 1),
    (1, 'Stage', 'ProductByNumber', 'ProductByNumber', 1),
    --(1, 'Stage', '', 'ItemTypeUid', 1),
    (1, 'Stage', 'ProductTypeName', 'ProductTypeName', 1),
    (1, 'Stage', 'ProductTypeDescription', 'ProductTypeDescription', 1),
    (1, 'Stage', 'ModelNumber', 'ModelNumber', 1),
    --(1, 'Stage', '', 'ManufacturerUid', 1),
    (1, 'Stage', 'ManufacturerName', 'ManufacturerName', 1),
    --(1, 'Stage', '', 'AreaUid', 1),
    (1, 'Stage', 'AreaName', 'AreaName', 1),
    --(1, 'Stage', '', 'SiteUid', 1),
    (1, 'Stage', 'SiteId', 'SiteId', 1),
    (1, 'Stage', 'SiteName', 'SiteName', 1),
    --(1, 'Stage', '', 'EntityUid', 1),
    (1, 'Stage', 'EntityId', 'EntityId', 1),
    (1, 'Stage', 'EntityName', 'EntityName', 1),
    --(1, 'Stage', '', 'EntityTypeUid', 1),
    (1, 'Stage', 'EntityTypeName', 'EntityTypeName', 1),
    --(1, 'Stage', '', 'StatusId', 1),
    (1, 'Stage', 'Status', 'Status', 1),
    --(1, 'Stage', '', 'TechDepartmentUid', 1),
    (1, 'Stage', 'DepartmentName', 'DepartmentName', 1),
    (1, 'Stage', 'DepartmentId', 'DepartmentId', 1),
    --(1, 'Stage', '', 'FundingSourceUid', 1),
    (1, 'Stage', 'FundingSource', 'FundingSource', 1),
    (1, 'Stage', 'FundingSourceDescription', 'FundingSourceDescription', 1),
    (1, 'Stage', 'PurchasePrice', 'PurchasePrice', 1),
    (1, 'Stage', 'PurchaseDate', 'PurchaseDate', 1),
    (1, 'Stage', 'ExpirationDate', 'ExpirationDate', 1),
    (1, 'Stage', 'InventoryNotes', 'InventoryNotes', 1),
    --(1, 'Stage', '', 'ParentInventoryUid', 1),
    (1, 'Stage', 'ParentTag', 'ParentTag', 1),
    --(1, 'Stage', '', 'InventorySourceUid', 1),
    (1, 'Stage', 'InventorySourceName', 'InventorySourceName', 1),
    --(1, 'Stage', '', 'PurchaseUid', 1),
    (1, 'Stage', 'OrderNumber', 'OrderNumber', 1),
    (1, 'Stage', 'PurchaseSiteId', 'PurchaseSiteId', 1),
    (1, 'Stage', 'PurchaseSiteName', 'PurchaseSiteName', 1),
    --(1, 'Stage', '', 'VendorUid', 1),
    (1, 'Stage', 'VendorName', 'VendorName', 1),
    (1, 'Stage', 'VendorAccountNumber', 'VendorAccountNumber', 1),
    --(1, 'Stage', '', 'PurchaseItemDetailUid', 1),
    (1, 'Stage', 'LineNumber', 'LineNumber', 1),
    (1, 'Stage', 'AccountCode', 'AccountCode', 1),
    (1, 'Stage', 'SiteAddedSiteId', 'SiteAddedSiteId', 1),
    (1, 'Stage', 'SiteAddedSiteName', 'SiteAddedSiteName', 1),
    (1, 'Stage', 'ShippedToSiteId', 'ShippedToSiteId', 1),
    (1, 'Stage', 'ShippedToSiteName', 'ShippedToSiteName', 1),
    --(1, 'Stage', '', 'PurchaseItemShipmentUid', 1),
    (1, 'Stage', 'InvoiceNumber', 'InvoiceNumber', 1),
    (1, 'Stage', 'InvoiceDate', 'InvoiceDate', 1),
    --(1, 'Stage', '', 'InventoryExt1Uid', 1),
    --(1, 'Stage', '', 'InventoryMeta1Uid', 1),
    (1, 'Stage', 'CustomField1Label', 'CustomField1Label', 1),
    (1, 'Stage', 'CustomField1Value', 'CustomField1Value', 1),
    --(1, 'Stage', '', 'InventoryExt2Uid', 1),
    --(1, 'Stage', '', 'InventoryMeta2Uid', 1),
    (1, 'Stage', 'CustomField2Label', 'CustomField2Label', 1),
    (1, 'Stage', 'CustomField2Value', 'CustomField2Value', 1),
    --(1, 'Stage', '', 'InventoryExt3Uid', 1),
    --(1, 'Stage', '', 'InventoryMeta3Uid', 1),
    (1, 'Stage', 'CustomField3Label', 'CustomField3Label', 1),
    (1, 'Stage', 'CustomField3Value', 'CustomField3Value', 1),
    --(1, 'Stage', '', 'InventoryExt4Uid', 1),
    --(1, 'Stage', '', 'InventoryMeta4Uid', 1),
    (1, 'Stage', 'CustomField4Label', 'CustomField4Label', 1),
    (1, 'Stage', 'CustomField4Value', 'CustomField4Value', 1)