/*
 *  sp_ValidateSites
 *  Perform Site matching by SiteID, SiteName or SiteID in SiteName
 *  Reject any remainders where the SiteID and SiteName are null or empty and where SiteUID = 0
 *  SHIP TO SITE AND SITEADDED NEED TO BE ADDED TO DISTINGUISH TAG SITE FROM PURCHASE DETAIL AND SHIPMENT SITES
 */
CREATE PROCEDURE [dbo].[sp_ValidateSites]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @AllowStackingErrors    AS BIT;

        SET @TargetDatabase = [dbo].[fn_GetTargetDatabaseName](@ProcessUid);
        SET @SourceTable = [dbo].[fn_GetSourceTable](@SourceProcess);

        --Check that Target Database is not null or empty
        IF @TargetDatabase IS NULL OR LEN(@TargetDatabase) = 0
            BEGIN
                ;
                THROW 50000, 'Target Database Name is empty.', 1;
            END;

        --Check that Source Table is not null or empty
        IF @SourceTable IS NULL OR LEN(@SourceTable) = 0
            BEGIN
                ;
                THROW 50000, 'Source Table could not be verified.', 1;
            END

        SELECT
            @AllowStackingErrors = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'AllowStackingErrors' 
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        -- Match the Site by ID
        --SELECT TargetSite.SiteUID, TargetSite.SiteID, TargetSite.SiteName, SourceSite.SiteID, SourceSite.SiteUID
        UPDATE TargetSite SET TargetSite.SiteUID = SourceSite.SiteUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        INNER JOIN 
            TipWebHostedChicagoPS.dbo.tblTechSites SourceSite
            ON UPPER(LTRIM(RTRIM(TargetSite.SiteID))) = UPPER(LTRIM(RTRIM(SourceSite.SiteID)))
        WHERE
            TargetSite.SiteID IS NOT NULL
        AND LTRIM(RTRIM(TargetSite.SiteID)) <> ''
        AND TargetSite.SiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        -- Match the Site by Name
        --SELECT TargetSite.SiteUID, TargetSite.SiteID, TargetSite.SiteName, SourceSite.SiteName, SourceSite.SiteUID
        UPDATE TargetSite SET TargetSite.SiteUID = SourceSite.SiteUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechSites SourceSite
            ON UPPER(LTRIM(RTRIM(TargetSite.SiteName))) = UPPER(LTRIM(RTRIM(SourceSite.SiteName)))
        WHERE
            TargetSite.SiteName IS NOT NULL
        AND LTRIM(RTRIM(TargetSite.SiteName)) <> ''
        AND TargetSite.SiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        --Lookup where SiteID in SiteName
        --SELECT SourceSite.SiteUID, SourceSite.SiteID, SourceSite.SiteName, TargetSite.SiteUID, TargetSite.SiteID, TargetSite.SiteName
        UPDATE TargetSite SET TargetSite.SiteUID = SourceSite.SiteUID
        FROM 
            TipWebHostedChicagoPS.dbo.tblTechSites SourceSite
        INNER JOIN
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
            ON UPPER(LTRIM(RTRIM(SourceSite.SiteName))) LIKE '%' + UPPER(LTRIM(RTRIM(TargetSite.SiteID))) + '%'
        WHERE
            TargetSite.SiteID IS NOT NULL
        AND LTRIM(RTRIM(TargetSite.SiteID)) <> ''
        AND TargetSite.SiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);


        --Reject any records remaining where the SiteUID = 0 and SiteID and SiteName are null or empty
        --SELECT TargetSite.SiteUID, TargetSite.SiteUID
        UPDATE TargetSite SET Rejected = 1, SiteUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Properties: SiteID, SiteName; SiteID and SiteName are NULL or Empty'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        WHERE
            TargetSite.SiteUID = 0
        AND (TargetSite.SiteID IS NULL 
             OR LTRIM(RTRIM(TargetSite.SiteID)) = '')
        AND (TargetSite.SiteName IS NULL 
             OR LTRIM(RTRIM(TargetSite.SiteName)) = '')
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);


        --Reject any records where the SiteUID = 0
        --SELECT TargetSite.SiteUID, TargetSite.SiteUID
        UPDATE TargetSite SET Rejected = 1, SiteUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: SiteID/SiteName; SiteID/SiteName could not be Matched'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        WHERE
            TargetSite.SiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

    END --End Procedure