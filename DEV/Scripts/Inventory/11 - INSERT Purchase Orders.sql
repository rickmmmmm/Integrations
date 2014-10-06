IF (SELECT COUNT(*) FROM tblVendor WHERE VendorID = 0) = 0
	BEGIN
		SET IDENTITY_INSERT tblVendor ON 
		INSERT INTO tblVendor (VendorID, VendorName, Contact, Address, Address2, City, State, Zip, Phone, Fax,
		Email, AccountNumber, CampusID, Notes, Active, UserID, ModifiedDate, ApplicationUID)
		VALUES (0, 'NONE', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
		NULL, NULL, NULL, 'DATA IMPORT ' + CONVERT(NVARCHAR(10), GETDATE(), 120), 1, 0, GETDATE(), 0)
		SET IDENTITY_INSERT tblVendor OFF
	END

UPDATE _ETL_Inventory
SET PurchaseUID = _ETL_Purchases.PurchaseUID, InventoryTypeUID = 2,
FundingSourceUID = _ETL_Purchases.FundingSourceUID, PurchaseDate = _ETL_Purchases.PurchaseDate
FROM _ETL_Inventory AS Tags
JOIN _ETL_Purchases ON UPPER(Tags.PurchaseOrder) = UPPER(_ETL_Purchases.PurchaseOrder)
WHERE Tags.PurchaseOrder IS NOT NULL AND Tags.PurchaseOrder <> ''
AND Tags.PurchaseOrder <> 'N/A' AND Tags.PurchaseOrder <> 'NONE' AND Tags.PurchaseOrder <> 'UNKNOWN'
AND (Tags.PurchaseUID IS NULL
OR Tags.PurchaseUID <> _ETL_Purchases.PurchaseUID)

INSERT INTO tblTechPurchases (StatusUID, VendorUID, SiteUID, OrderNumber, PurchaseDate,
EstimatedDeliveryDate, Notes, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
SELECT 33, 0, MIN(Tags.SiteUID), Tags.PurchaseOrder,
MIN(ISNULL(Tags.PurchaseDate, GETDATE())), NULL,
'DATA IMPORT ' + CONVERT(NVARCHAR(10), GETDATE(), 120), 0, GETDATE(), 0, GETDATE()
FROM _ETL_Inventory AS Tags
LEFT JOIN tblTechPurchases ON UPPER(Tags.PurchaseOrder) = UPPER(tblTechPurchases.OrderNumber)
WHERE Tags.PurchaseOrder IS NOT NULL AND Tags.PurchaseOrder <> ''
AND Tags.PurchaseOrder <> 'N/A' AND Tags.PurchaseOrder <> 'NONE' AND Tags.PurchaseOrder <> 'UNKNOWN'
AND tblTechPurchases.PurchaseUID IS NULL
GROUP BY Tags.PurchaseOrder

UPDATE _ETL_Inventory
SET PurchaseUID = tblTechPurchases.PurchaseUID, InventoryTypeUID = 2
FROM _ETL_Inventory AS Tags
JOIN tblTechPurchases ON UPPER(Tags.PurchaseOrder) = UPPER(tblTechPurchases.OrderNumber)
WHERE Tags.PurchaseOrder IS NOT NULL AND Tags.PurchaseOrder <> ''
AND Tags.PurchaseOrder <> 'N/A' AND Tags.PurchaseOrder <> 'NONE' AND Tags.PurchaseOrder <> 'UNKNOWN'
AND (Tags.PurchaseUID IS NULL
OR Tags.PurchaseUID <> tblTechPurchases.PurchaseUID)

UPDATE _ETL_Inventory
SET PurchaseUID = 0, InventoryTypeUID = 1
WHERE PurchaseUID IS NULL
