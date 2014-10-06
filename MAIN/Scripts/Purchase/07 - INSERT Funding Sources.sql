IF (SELECT COUNT(*) FROM tblFundingSources WHERE FundingSourceUID = 0) = 0
	BEGIN
		SET IDENTITY_INSERT tblFundingSources ON 
		INSERT INTO tblFundingSources (FundingSourceUID, FundingSource, FundingDesc, Active, ApplicationUID)
		VALUES (0, 'NONE', NULL, 1, 0)
		SET IDENTITY_INSERT tblFundingSources OFF
	END

INSERT INTO tblFundingSources (FundingSource, FundingDesc, Active, ApplicationUID)
SELECT Purchases.FundingSource, MAX(ISNULL(Purchases.FundingSourceDescription, '')), 1, 2
FROM _ETL_Purchases AS Purchases
LEFT JOIN tblFundingSources ON UPPER(Purchases.FundingSource) = UPPER(tblFundingSources.FundingSource)
AND tblFundingSources.ApplicationUID = 2
WHERE Purchases.FundingSource IS NOT NULL AND Purchases.FundingSource <> ''
AND Purchases.FundingSource <> 'N/A' AND Purchases.FundingSource <> 'NONE' AND Purchases.FundingSource <> 'UNKNOWN'
AND tblFundingSources.FundingSourceUID IS NULL
GROUP BY Purchases.FundingSource

UPDATE _ETL_Purchases
SET FundingSourceUID = tblFundingSources.FundingSourceUID
FROM _ETL_Purchases AS Purchases
JOIN tblFundingSources ON UPPER(Purchases.FundingSource) = UPPER(tblFundingSources.FundingSource)
AND tblFundingSources.ApplicationUID = 2
WHERE Purchases.FundingSource IS NOT NULL AND Purchases.FundingSource <> ''
AND Purchases.FundingSource <> 'N/A' AND Purchases.FundingSource <> 'NONE' AND Purchases.FundingSource <> 'UNKNOWN'
AND (Purchases.FundingSourceUID IS NULL
OR Purchases.FundingSourceUID <> tblFundingSources.FundingSourceUID)

UPDATE _ETL_Purchases
SET FundingSourceUID = 0
WHERE FundingSourceUID IS NULL
