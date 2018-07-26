/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/

INSERT INTO [dbo].[ProcessSource]
    ([ProcessSourceUid], [ProcessSourceName], [ProcessSourceTable], [ProcessSourceDescription], [Enabled])
VALUES
    (0, 'PrintConfiguration', 'Configurations', 'Display all Configuratiosn stored for the Client and Process Name', 1),
    (1, 'Assets', '_ETL_Inventory', 'Import Assets from the Inventory Staging Table', 1),
    (2, 'PurchaseOrder', '_ETL_Headers', 'Import Purchase Order Header information from the Headers Table', 0),
    (3, 'PurchaseDetails', '_ETL_Details', 'Import Purchase Order Details information from the Details Table', 0),
    (4, 'PurchaseShipments', '_ETL_Shipments', 'Import Purchase Order Shipments information from the Shipments Table', 0),
    (5, 'Charges', '', '', 0),
    (6, 'ExportFile', '', '', 0),
    (7, 'MobileDeviceManagement', '', '', 0)