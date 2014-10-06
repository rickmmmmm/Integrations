IF (SELECT COUNT(*) FROM tblVendor WHERE VendorID = 0) = 0
	BEGIN
		SET IDENTITY_INSERT tblVendor ON 
		INSERT INTO tblVendor (VendorID, VendorName, Contact, Address, Address2, City, State, Zip, Phone, Fax,
		Email, AccountNumber, CampusID, Notes, Active, UserID, ModifiedDate, ApplicationUID)
		VALUES (0, 'NONE', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
		NULL, NULL, NULL, 'DATA IMPORT ' + CONVERT(NVARCHAR(10), GETDATE(), 120), 1, 0, GETDATE(), 0)
		SET IDENTITY_INSERT tblVendor OFF
	END

INSERT INTO tblVendor (VendorName, Contact, Address, Address2, City, State, Zip, Phone, Fax,
Email, AccountNumber, CampusID, Notes, Active, UserID, ModifiedDate, ApplicationUID)
SELECT Purchases.Vendor, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
NULL, MIN(Purchases.VendorAccountNumber), NULL, 'DATA IMPORT ' + CONVERT(NVARCHAR(10), GETDATE(), 120), 1, 0, GETDATE(), 2
FROM _ETL_Purchases AS Purchases
LEFT JOIN tblVendor ON UPPER(Purchases.Vendor) = UPPER(tblVendor.VendorName)
AND tblVendor.ApplicationUID = 2
WHERE Purchases.Vendor IS NOT NULL AND Purchases.Vendor <> ''
AND Purchases.Vendor <> 'N/A' AND Purchases.Vendor <> 'NONE' AND Purchases.Vendor <> 'UNKNOWN'
AND tblVendor.VendorID IS NULL
GROUP BY Purchases.Vendor

UPDATE _ETL_Purchases
SET VendorUID = tblVendor.VendorID
FROM _ETL_Purchases AS Purchases
JOIN tblVendor ON UPPER(Purchases.Vendor) = UPPER(tblVendor.VendorName)
AND tblVendor.ApplicationUID = 2
WHERE Purchases.Vendor IS NOT NULL AND Purchases.Vendor <> ''
AND Purchases.Vendor <> 'N/A' AND Purchases.Vendor <> 'NONE' AND Purchases.Vendor <> 'UNKNOWN'
AND (Purchases.VendorUID IS NULL
OR Purchases.VendorUID <> tblVendor.VendorID)

UPDATE _ETL_Purchases
SET VendorUID = 0
WHERE VendorUID IS NULL
