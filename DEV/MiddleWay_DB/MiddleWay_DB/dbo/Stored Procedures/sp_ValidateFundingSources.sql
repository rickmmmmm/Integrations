/*
 *  sp_ValidateFundingSources
 *  Match the Funding Source by Name or Description
 *  If CreateFundingSources is set to false, set the remaining records 
 *      to default or reject them
 */
CREATE PROCEDURE [dbo].[sp_ValidateFundingSources]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateFundingSources       AS BIT,
                @DefaultFundingSourceUID    AS INT,
                @TargetDatabase             AS VARCHAR(100),
                @SourceTable                AS VARCHAR(100),
                @AllowStackingErrors        AS BIT;

        SET @CreateFundingSources = 0;
        --SET @DefaultFundingSource = 'None';
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
            @CreateFundingSources = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'CreateFundingSources'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        SELECT @DefaultFundingSourceUID = CAST(ConfigurationValue AS INT)
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultFundingSourceUID' AND ProcessUid = @ProcessUid
          AND Enabled = 1;

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

        -- Match the Funding Source by Name
        --SELECT TargetFundingSource.FundingSourceUID, TargetFundingSource.FundingSource, TargetFundingSource.FundingSourceDescription, SourceFundingSource.FundingSource, SourceFundingSource.FundingSourceUID
        UPDATE TargetFundingSource SET TargetFundingSource.FundingSourceUID = SourceFundingSource.FundingSourceUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetFundingSource
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblFundingSources SourceFundingSource
            ON UPPER(LTRIM(RTRIM(TargetFundingSource.FundingSource))) = UPPER(LTRIM(RTRIM(SourceFundingSource.FundingSource)))
        WHERE 
            SourceFundingSource.FundingSource IS NOT NULL
        AND TargetFundingSource.FundingSource IS NOT NULL
        AND TargetFundingSource.FundingSourceUID = 0
        AND TargetFundingSource.ProcessTaskUID = @ProcessTaskUid
        AND (TargetFundingSource.Rejected = 0 OR @AllowStackingErrors = 1);

        -- Match the Funding Source by Description
        --SELECT TargetFundingSource.FundingSourceUID, TargetFundingSource.FundingSource, TargetFundingSource.FundingSourceDescription, SourceFundingSource.FundingSource, SourceFundingSource.FundingSourceUID
        UPDATE TargetFundingSource SET TargetFundingSource.FundingSourceUID = SourceFundingSource.FundingSourceUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetFundingSource
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblFundingSources SourceFundingSource
            ON UPPER(LTRIM(RTRIM(TargetFundingSource.FundingSourceDescription))) = UPPER(LTRIM(RTRIM(SourceFundingSource.FundingDesc)))
        WHERE 
            SourceFundingSource.FundingDesc IS NOT NULL
        AND TargetFundingSource.FundingSourceDescription IS NOT NULL
        AND TargetFundingSource.FundingSourceUID = 0
        AND TargetFundingSource.ProcessTaskUID = @ProcessTaskUid
        AND (TargetFundingSource.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @CreateFundingSources = 0
            BEGIN
                IF @DefaultFundingSourceUID IS NOT NULL
                    BEGIN
                        --Set All to the DefaultFundingSource
                        UPDATE TargetFundingSource SET TargetFundingSource.FundingSourceUID = @DefaultFundingSourceUID
                        FROM 
                            IntegrationMiddleWay.dbo._ETL_Inventory TargetFundingSource
                        WHERE
                            TargetFundingSource.FundingSourceUID = 0
                        AND TargetFundingSource.ProcessTaskUID = @ProcessTaskUid
                        AND (TargetFundingSource.Rejected = 0 OR @AllowStackingErrors = 1);
                    END
                ELSE
                    BEGIN
                        --Reject the remaining rows
                        UPDATE TargetFundingSource SET Rejected = 1, FundingSourceUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: FundingSource; FundingSource was not found'
                        FROM 
                            IntegrationMiddleWay.dbo._ETL_Inventory TargetFundingSource
                        WHERE
                            TargetFundingSource.FundingSourceUID = 0
                        AND TargetFundingSource.ProcessTaskUID = @ProcessTaskUid
                        AND (TargetFundingSource.Rejected = 0 OR @AllowStackingErrors = 1);
                    END
            END

    END --End Procedure