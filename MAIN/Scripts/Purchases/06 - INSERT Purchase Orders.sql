IF (SELECT COUNT(*) FROM _ETL_Settings WHERE ETLSettingUID = 3 AND ETLSettingValue = 'SiteID') = 0
	BEGIN
		UPDATE _ETL_Purchases
		SET SiteUID = tblTechSites.SiteUID
		FROM _ETL_Purchases AS Purchases
		JOIN tblTechSites ON UPPER(Purchases.Site) = UPPER(tblTechSites.SiteName)
		WHERE (Purchases.SiteUID IS NULL
		OR Purchases.SiteUID <> tblTechSites.SiteUID)

		UPDATE _ETL_Purchases
		SET SiteAddedSiteUID = tblTechSites.SiteUID
		FROM _ETL_Purchases AS Purchases
		JOIN tblTechSites ON UPPER(Purchases.SiteAdded) = UPPER(tblTechSites.SiteName)
		WHERE (Purchases.SiteAddedSiteUID IS NULL
		OR Purchases.SiteAddedSiteUID <> tblTechSites.SiteUID)

		UPDATE _ETL_Purchases
		SET ShippedToSiteUID = tblTechSites.SiteUID
		FROM _ETL_Purchases AS Purchases
		JOIN tblTechSites ON UPPER(Purchases.ShippedToSite) = UPPER(tblTechSites.SiteName)
		WHERE (Purchases.ShippedToSiteUID IS NULL
		OR Purchases.ShippedToSiteUID <> tblTechSites.SiteUID)
	END
ELSE
	BEGIN
		UPDATE _ETL_Purchases
		SET SiteUID = tblTechSites.SiteUID
		FROM _ETL_Purchases AS Purchases
		JOIN tblTechSites ON UPPER(Purchases.Site) = UPPER(tblTechSites.SiteID)
		WHERE (Purchases.SiteUID IS NULL
		OR Purchases.SiteUID <> tblTechSites.SiteUID)

		UPDATE _ETL_Purchases
		SET SiteAddedSiteUID = tblTechSites.SiteUID
		FROM _ETL_Purchases AS Purchases
		JOIN tblTechSites ON UPPER(Purchases.SiteAdded) = UPPER(tblTechSites.SiteID)
		WHERE (Purchases.SiteAddedSiteUID IS NULL
		OR Purchases.SiteAddedSiteUID <> tblTechSites.SiteUID)

		UPDATE _ETL_Purchases
		SET ShippedToSiteUID = tblTechSites.SiteUID
		FROM _ETL_Purchases AS Purchases
		JOIN tblTechSites ON UPPER(Purchases.ShippedToSite) = UPPER(tblTechSites.SiteID)
		WHERE (Purchases.ShippedToSiteUID IS NULL
		OR Purchases.ShippedToSiteUID <> tblTechSites.SiteUID)
	END

UPDATE _ETL_Purchases
SET SiteUID = 1
WHERE SiteUID IS NULL

UPDATE _ETL_Purchases
SET SiteAddedSiteUID = 1
WHERE SiteAddedSiteUID IS NULL

UPDATE _ETL_Purchases
SET ShippedToSiteUID = 0
WHERE ShippedToSiteUID IS NULL

INSERT INTO tblTechPurchases (StatusUID, VendorUID, SiteUID, OrderNumber, PurchaseDate,
EstimatedDeliveryDate, Notes, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
SELECT 32, MAX(Purchases.VendorUID), MIN(Purchases.SiteUID), Purchases.PurchaseOrder,
MIN(ISNULL(Purchases.PurchaseDate, GETDATE())), MIN(ISNULL(Purchases.DeliveryDate, GETDATE())),
LTRIM(ISNULL(MAX(Purchases.Notes), '') + ' ' + 'DATA IMPORT ' + CONVERT(NVARCHAR(10), GETDATE(), 120)),
0, GETDATE(), 0, GETDATE()
FROM _ETL_Purchases AS Purchases
LEFT JOIN tblTechPurchases ON UPPER(Purchases.PurchaseOrder) = UPPER(tblTechPurchases.OrderNumber)
WHERE Purchases.PurchaseOrder IS NOT NULL AND Purchases.PurchaseOrder <> ''
AND Purchases.PurchaseOrder <> 'N/A' AND Purchases.PurchaseOrder <> 'NONE' AND Purchases.PurchaseOrder <> 'UNKNOWN'
AND tblTechPurchases.PurchaseUID IS NULL
GROUP BY Purchases.PurchaseOrder

UPDATE _ETL_Purchases
SET PurchaseUID = tblTechPurchases.PurchaseUID
FROM _ETL_Purchases AS Purchases
JOIN tblTechPurchases ON UPPER(Purchases.PurchaseOrder) = UPPER(tblTechPurchases.OrderNumber)
WHERE Purchases.PurchaseOrder IS NOT NULL AND Purchases.PurchaseOrder <> ''
AND Purchases.PurchaseOrder <> 'N/A' AND Purchases.PurchaseOrder <> 'NONE' AND Purchases.PurchaseOrder <> 'UNKNOWN'
AND (Purchases.PurchaseUID IS NULL
OR Purchases.PurchaseUID <> tblTechPurchases.PurchaseUID)

UPDATE _ETL_Purchases
SET PurchaseUID = 0
WHERE PurchaseUID IS NULL
