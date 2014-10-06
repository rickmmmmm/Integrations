IF (SELECT COUNT(*) FROM tblFundingSources WHERE FundingSourceUID = 0) = 0
	BEGIN
		SET IDENTITY_INSERT tblFundingSources ON 
		INSERT INTO tblFundingSources (FundingSourceUID, FundingSource, FundingDesc, Active, ApplicationUID)
		VALUES (0, 'NONE', NULL, 1, 0)
		SET IDENTITY_INSERT tblFundingSources OFF
	END

INSERT INTO tblFundingSources (FundingSource, FundingDesc, Active, ApplicationUID)
SELECT Tags.FundingSource, MAX(ISNULL(Tags.FundingSourceDescription, '')), 1, 2
FROM _ETL_Inventory AS Tags
LEFT JOIN tblFundingSources ON UPPER(Tags.FundingSource) = UPPER(tblFundingSources.FundingSource)
AND tblFundingSources.ApplicationUID = 2
WHERE Tags.FundingSource IS NOT NULL AND Tags.FundingSource <> ''
AND Tags.FundingSource <> 'N/A' AND Tags.FundingSource <> 'NONE' AND Tags.FundingSource <> 'UNKNOWN'
AND tblFundingSources.FundingSourceUID IS NULL
GROUP BY Tags.FundingSource

UPDATE _ETL_Inventory
SET FundingSourceUID = tblFundingSources.FundingSourceUID
FROM _ETL_Inventory AS Tags
JOIN tblFundingSources ON UPPER(Tags.FundingSource) = UPPER(tblFundingSources.FundingSource)
AND tblFundingSources.ApplicationUID = 2
WHERE Tags.FundingSource IS NOT NULL AND Tags.FundingSource <> ''
AND Tags.FundingSource <> 'N/A' AND Tags.FundingSource <> 'NONE' AND Tags.FundingSource <> 'UNKNOWN'
AND (Tags.FundingSourceUID IS NULL
OR Tags.FundingSourceUID <> tblFundingSources.FundingSourceUID)

UPDATE _ETL_Inventory
SET FundingSourceUID = 0
WHERE FundingSourceUID IS NULL
