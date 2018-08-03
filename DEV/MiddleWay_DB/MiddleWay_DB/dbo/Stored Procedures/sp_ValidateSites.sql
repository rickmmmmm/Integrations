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
                @AllowStackingErrors    AS BIT,
                @ErrorCode              AS INT;

        SET @TargetDatabase = [dbo].[fn_GetTargetDatabaseName](@ProcessUid);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to determine the Target Database for the Process', 1;
            END

        SET @SourceTable = [dbo].[fn_GetSourceTable](@SourceProcess);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to determine the Source Table for the Process', 1;
            END

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

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for AllowStackingErrors', 1;
            END

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

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Site by SiteID', 1;
            END

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

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Site by SiteName', 1;
            END

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

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Sites by SiteID in the SiteName', 1;
            END

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

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the SiteID and SiteName are Null or Empty', 1;
            END

        --Reject any records where the SiteUID = 0
        --SELECT TargetSite.SiteUID, TargetSite.SiteUID
        UPDATE TargetSite SET Rejected = 1, SiteUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: SiteID/SiteName; SiteID/SiteName could not be Matched'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        WHERE
            TargetSite.SiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the Site could not be matched', 1;
            END


        -- Match the PurchaseSite by ID
        --SELECT TargetSite.PurchaseSiteUID, TargetSite.PurchaseSiteID, TargetSite.PurchaseSiteName, SourceSite.SiteID, SourceSite.SiteUID
        UPDATE TargetSite SET TargetSite.PurchaseSiteUID = SourceSite.SiteUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        INNER JOIN 
            TipWebHostedChicagoPS.dbo.tblTechSites SourceSite
            ON UPPER(LTRIM(RTRIM(TargetSite.PurchaseSiteID))) = UPPER(LTRIM(RTRIM(SourceSite.SiteID)))
        WHERE
            TargetSite.PurchaseSiteID IS NOT NULL
        AND LTRIM(RTRIM(TargetSite.PurchaseSiteID)) <> ''
        AND TargetSite.PurchaseSiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match PurchaseSite by PurchaseSiteID', 1;
            END

        -- Match the PurchaseSite by Name
        --SELECT TargetSite.PurchaseSiteUID, TargetSite.PurchaseSiteID, TargetSite.PurchaseSiteName, SourceSite.SiteName, SourceSite.SiteUID
        UPDATE TargetSite SET TargetSite.PurchaseSiteUID = SourceSite.SiteUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechSites SourceSite
            ON UPPER(LTRIM(RTRIM(TargetSite.PurchaseSiteName))) = UPPER(LTRIM(RTRIM(SourceSite.SiteName)))
        WHERE
            TargetSite.PurchaseSiteName IS NOT NULL
        AND LTRIM(RTRIM(TargetSite.PurchaseSiteName)) <> ''
        AND TargetSite.PurchaseSiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Site by PurchaseSiteName', 1;
            END

        --Lookup where PurchaseSiteID in SiteName
        --SELECT SourceSite.PurchaseSiteUID, SourceSite.PurchaseSiteID, SourceSite.PurchaseSiteName, TargetSite.SiteUID, TargetSite.SiteID, TargetSite.SiteName
        UPDATE TargetSite SET TargetSite.PurchaseSiteUID = SourceSite.SiteUID
        FROM 
            TipWebHostedChicagoPS.dbo.tblTechSites SourceSite
        INNER JOIN
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
            ON UPPER(LTRIM(RTRIM(SourceSite.SiteName))) LIKE '%' + UPPER(LTRIM(RTRIM(TargetSite.PurchaseSiteID))) + '%'
        WHERE
            TargetSite.PurchaseSiteID IS NOT NULL
        AND LTRIM(RTRIM(TargetSite.PurchaseSiteID)) <> ''
        AND TargetSite.PurchaseSiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Sites by PurchaseSiteID in the SiteName', 1;
            END

        --Reject any records remaining where the PurchaseSiteUID = 0 and PurchaseSiteID and PurchaseSiteName are null or empty
        --SELECT TargetSite.PurchaseSiteUID, TargetSite.PurchaseSiteUID
        UPDATE TargetSite SET Rejected = 1, PurchaseSiteUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Properties: PurchaseSiteID, PurchaseSiteName; PurchaseSiteID and PurchaseSiteName are NULL or Empty'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        WHERE
            TargetSite.PurchaseSiteUID = 0
        AND (TargetSite.PurchaseSiteID IS NULL 
             OR LTRIM(RTRIM(TargetSite.PurchaseSiteID)) = '')
        AND (TargetSite.PurchaseSiteName IS NULL 
             OR LTRIM(RTRIM(TargetSite.PurchaseSiteName)) = '')
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the PurchaseSiteID and PurchaseSiteName are Null or Empty', 1;
            END

        --Reject any records where the PurchaseSiteUID = 0
        --SELECT TargetSite.PurchaseSiteUID, TargetSite.PurchaseSiteUID
        UPDATE TargetSite SET Rejected = 1, PurchaseSiteUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: PurchaseSiteID/PurchaseSiteName; PurchaseSiteID/PurchaseSiteName could not be Matched'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        WHERE
            TargetSite.PurchaseSiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the PurchaseSite could not be matched', 1;
            END

        -- Match the SiteAddedSite by ID
        --SELECT TargetSite.SiteAddedSiteUID, TargetSite.SiteAddedSiteID, TargetSite.SiteAddedSiteName, SourceSite.SiteID, SourceSite.SiteUID
        UPDATE TargetSite SET TargetSite.SiteAddedSiteUID = SourceSite.SiteUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        INNER JOIN 
            TipWebHostedChicagoPS.dbo.tblTechSites SourceSite
            ON UPPER(LTRIM(RTRIM(TargetSite.SiteAddedSiteID))) = UPPER(LTRIM(RTRIM(SourceSite.SiteID)))
        WHERE
            TargetSite.SiteAddedSiteID IS NOT NULL
        AND LTRIM(RTRIM(TargetSite.SiteAddedSiteID)) <> ''
        AND TargetSite.SiteAddedSiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match SiteAddedSite by SiteAddedSiteID', 1;
            END

        -- Match the SiteAddedSite by Name
        --SELECT TargetSite.SiteAddedSiteUID, TargetSite.SiteAddedSiteID, TargetSite.SiteAddedSiteName, SourceSite.SiteName, SourceSite.SiteUID
        UPDATE TargetSite SET TargetSite.SiteAddedSiteUID = SourceSite.SiteUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechSites SourceSite
            ON UPPER(LTRIM(RTRIM(TargetSite.SiteAddedSiteName))) = UPPER(LTRIM(RTRIM(SourceSite.SiteName)))
        WHERE
            TargetSite.SiteAddedSiteName IS NOT NULL
        AND LTRIM(RTRIM(TargetSite.SiteAddedSiteName)) <> ''
        AND TargetSite.SiteAddedSiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Site by SiteAddedSiteName', 1;
            END

        --Lookup where SiteAddedSiteID in SiteName
        --SELECT SourceSite.SiteAddedSiteUID, SourceSite.SiteAddedSiteID, SourceSite.SiteAddedSiteName, TargetSite.SiteUID, TargetSite.SiteID, TargetSite.SiteName
        UPDATE TargetSite SET TargetSite.SiteAddedSiteUID = SourceSite.SiteUID
        FROM 
            TipWebHostedChicagoPS.dbo.tblTechSites SourceSite
        INNER JOIN
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
            ON UPPER(LTRIM(RTRIM(SourceSite.SiteName))) LIKE '%' + UPPER(LTRIM(RTRIM(TargetSite.SiteAddedSiteID))) + '%'
        WHERE
            TargetSite.SiteAddedSiteID IS NOT NULL
        AND LTRIM(RTRIM(TargetSite.SiteAddedSiteID)) <> ''
        AND TargetSite.SiteAddedSiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Sites by SiteAddedSiteID in the SiteName', 1;
            END

        --Reject any records remaining where the SiteAddedSiteUID = 0 and SiteAddedSiteID and SiteAddedSiteName are null or empty
        --SELECT TargetSite.SiteAddedSiteUID, TargetSite.SiteAddedSiteUID
        UPDATE TargetSite SET Rejected = 1, SiteAddedSiteUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Properties: SiteAddedSiteID, SiteAddedSiteName; SiteAddedSiteID and SiteAddedSiteName are NULL or Empty'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        WHERE
            TargetSite.SiteAddedSiteUID = 0
        AND (TargetSite.SiteAddedSiteID IS NULL 
             OR LTRIM(RTRIM(TargetSite.SiteAddedSiteID)) = '')
        AND (TargetSite.SiteAddedSiteName IS NULL 
             OR LTRIM(RTRIM(TargetSite.SiteAddedSiteName)) = '')
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the SiteAddedSiteID and SiteAddedSiteName are Null or Empty', 1;
            END

        --Reject any records where the SiteAddedSiteUID = 0
        --SELECT TargetSite.SiteAddedSiteUID, TargetSite.SiteAddedSiteUID
        UPDATE TargetSite SET Rejected = 1, SiteAddedSiteUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: SiteAddedSiteID/SiteAddedSiteName; SiteAddedSiteID/SiteAddedSiteName could not be Matched'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        WHERE
            TargetSite.SiteAddedSiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the SiteAddedSite could not be matched', 1;
            END


        -- Match the ShippedToSite by ID
        --SELECT TargetSite.ShippedToSiteUID, TargetSite.ShippedToSiteID, TargetSite.ShippedToSiteName, SourceSite.SiteID, SourceSite.SiteUID
        UPDATE TargetSite SET TargetSite.ShippedToSiteUID = SourceSite.SiteUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        INNER JOIN 
            TipWebHostedChicagoPS.dbo.tblTechSites SourceSite
            ON UPPER(LTRIM(RTRIM(TargetSite.ShippedToSiteID))) = UPPER(LTRIM(RTRIM(SourceSite.SiteID)))
        WHERE
            TargetSite.ShippedToSiteID IS NOT NULL
        AND LTRIM(RTRIM(TargetSite.ShippedToSiteID)) <> ''
        AND TargetSite.ShippedToSiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match ShippedToSite by ShippedToSiteID', 1;
            END

        -- Match the ShippedToSite by Name
        --SELECT TargetSite.ShippedToSiteUID, TargetSite.ShippedToSiteID, TargetSite.ShippedToSiteName, SourceSite.SiteName, SourceSite.SiteUID
        UPDATE TargetSite SET TargetSite.ShippedToSiteUID = SourceSite.SiteUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechSites SourceSite
            ON UPPER(LTRIM(RTRIM(TargetSite.ShippedToSiteName))) = UPPER(LTRIM(RTRIM(SourceSite.SiteName)))
        WHERE
            TargetSite.ShippedToSiteName IS NOT NULL
        AND LTRIM(RTRIM(TargetSite.ShippedToSiteName)) <> ''
        AND TargetSite.ShippedToSiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Site by ShippedToSiteName', 1;
            END

        --Lookup where ShippedToSiteID in SiteName
        --SELECT SourceSite.ShippedToSiteUID, SourceSite.ShippedToSiteID, SourceSite.ShippedToSiteName, TargetSite.SiteUID, TargetSite.SiteID, TargetSite.SiteName
        UPDATE TargetSite SET TargetSite.ShippedToSiteUID = SourceSite.SiteUID
        FROM 
            TipWebHostedChicagoPS.dbo.tblTechSites SourceSite
        INNER JOIN
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
            ON UPPER(LTRIM(RTRIM(SourceSite.SiteName))) LIKE '%' + UPPER(LTRIM(RTRIM(TargetSite.ShippedToSiteID))) + '%'
        WHERE
            TargetSite.ShippedToSiteID IS NOT NULL
        AND LTRIM(RTRIM(TargetSite.ShippedToSiteID)) <> ''
        AND TargetSite.ShippedToSiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Sites by ShippedToSiteID in the SiteName', 1;
            END

        --Reject any records remaining where the ShippedToSiteUID = 0 and ShippedToSiteID and ShippedToSiteName are null or empty
        --SELECT TargetSite.ShippedToSiteUID, TargetSite.ShippedToSiteUID
        UPDATE TargetSite SET Rejected = 1, ShippedToSiteUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Properties: ShippedToSiteID, ShippedToSiteName; ShippedToSiteID and ShippedToSiteName are NULL or Empty'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        WHERE
            TargetSite.ShippedToSiteUID = 0
        AND (TargetSite.ShippedToSiteID IS NULL 
             OR LTRIM(RTRIM(TargetSite.ShippedToSiteID)) = '')
        AND (TargetSite.ShippedToSiteName IS NULL 
             OR LTRIM(RTRIM(TargetSite.ShippedToSiteName)) = '')
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the ShippedToSiteID and ShippedToSiteName are Null or Empty', 1;
            END

        --Reject any records where the ShippedToSiteUID = 0
        --SELECT TargetSite.ShippedToSiteUID, TargetSite.ShippedToSiteUID
        UPDATE TargetSite SET Rejected = 1, ShippedToSiteUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: ShippedToSiteID/ShippedToSiteName; ShippedToSiteID/ShippedToSiteName could not be Matched'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetSite
        WHERE
            TargetSite.ShippedToSiteUID = 0
        AND TargetSite.ProcessTaskUID = @ProcessTaskUid
        AND (TargetSite.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the ShippedToSite could not be matched', 1;
            END


        SET NOCOUNT OFF;

        RETURN 0;

    END --End Procedure