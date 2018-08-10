/*
 *  sp_ValidateFundingSources
 *  Match the Funding Source by Name or Description
 *  If CreateFundingSources is set to false, set the remaining records 
 *      to default or reject them
 */
CREATE PROCEDURE [dbo].[sp_ValidateFundingSources]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)--, @ErrorMessage AS VARCHAR(1000) OUTPUT)
AS
    BEGIN
        DECLARE @CreateFundingSources       AS BIT,
                @DefaultFundingSourceUID    AS INT,
                @TargetDatabase             AS VARCHAR(100),
                @SourceTable                AS VARCHAR(100),
                @AllowStackingErrors        AS BIT,
                @ErrorCode                  AS INT,
                @Statement                  AS VARCHAR(MAX);

        SET NOCOUNT ON;

        SET @CreateFundingSources = 0;
        --SET @DefaultFundingSource = 'None';
        SET @TargetDatabase = [dbo].[fn_GetTargetDatabaseName](@ProcessUid);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Unable to determine the Target Database for the Process', 1;
            END

        SET @SourceTable = [dbo].[fn_GetSourceTable](@SourceProcess);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Unable to determine the Source Table for the Process', 1;
            END

        --Check that Target Database is not null or empty
        IF @TargetDatabase IS NULL OR LEN(@TargetDatabase) = 0
            BEGIN
                ;
                THROW 100000, 'Target Database Name is empty.', 1;
            END;

        --Check that Source Table is not null or empty
        IF @SourceTable IS NULL OR LEN(@SourceTable) = 0
            BEGIN
                ;
                THROW 100000, 'Source Table could not be verified.', 1;
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

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateFundingSources', 1;
            END

        SELECT @DefaultFundingSourceUID = CAST(ConfigurationValue AS INT)
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultFundingSourceUID' AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for DefaultFundingSourceUID', 1;
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
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for AllowStackingErrors', 1;
            END

        -- Match the Funding Source by Name
        --SELECT TargetFundingSource.FundingSourceUID, TargetFundingSource.FundingSource, TargetFundingSource.FundingSourceDescription, SourceFundingSource.FundingSource, SourceFundingSource.FundingSourceUID
        SET @Statement = '
        UPDATE TargetFundingSource SET TargetFundingSource.FundingSourceUID = SourceFundingSource.FundingSourceUID
        FROM 
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetFundingSource
        INNER JOIN
            ' + @TargetDatabase + '.dbo.tblFundingSources SourceFundingSource
            ON UPPER(LTRIM(RTRIM(TargetFundingSource.FundingSource))) = UPPER(LTRIM(RTRIM(SourceFundingSource.FundingSource)))
        WHERE 
            SourceFundingSource.FundingSource IS NOT NULL
        AND TargetFundingSource.FundingSource IS NOT NULL
        AND TargetFundingSource.FundingSourceUID = 0
        AND TargetFundingSource.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND (TargetFundingSource.Rejected = 0 OR ' + CAST(@AllowStackingErrors AS VARCHAR(1)) + ' = 1)';
        EXECUTE (@Statement);
        --PRINT @Statement;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Funding Sources by Name', 1;
            END

        -- Match the Funding Source by Description
        --SELECT TargetFundingSource.FundingSourceUID, TargetFundingSource.FundingSource, TargetFundingSource.FundingSourceDescription, SourceFundingSource.FundingSource, SourceFundingSource.FundingSourceUID
        SET @Statement = '
        UPDATE TargetFundingSource SET TargetFundingSource.FundingSourceUID = SourceFundingSource.FundingSourceUID
        FROM 
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetFundingSource
        INNER JOIN
            ' + @TargetDatabase + '.dbo.tblFundingSources SourceFundingSource
            ON UPPER(LTRIM(RTRIM(TargetFundingSource.FundingSourceDescription))) = UPPER(LTRIM(RTRIM(SourceFundingSource.FundingDesc)))
        WHERE 
            SourceFundingSource.FundingDesc IS NOT NULL
        AND TargetFundingSource.FundingSourceDescription IS NOT NULL
        AND TargetFundingSource.FundingSourceUID = 0
        AND TargetFundingSource.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND (TargetFundingSource.Rejected = 0 OR ' + CAST(@AllowStackingErrors AS VARCHAR(1)) + ' = 1)';
        EXECUTE (@Statement);
        --PRINT @Statement;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Funding Sources by Description', 1;
            END

        IF @CreateFundingSources = 0
            BEGIN
                IF @DefaultFundingSourceUID IS NOT NULL
                    BEGIN
                        --Set All to the DefaultFundingSource
                        SET @Statement = '
                        UPDATE TargetFundingSource SET TargetFundingSource.FundingSourceUID = ' + CAST(@DefaultFundingSourceUID AS VARCHAR(10)) + '
                        FROM 
                            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetFundingSource
                        WHERE
                            TargetFundingSource.FundingSourceUID = 0
                        AND TargetFundingSource.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                        AND (TargetFundingSource.Rejected = 0 OR ' + CAST(@AllowStackingErrors AS VARCHAR(1)) + ' = 1)';
                        EXECUTE (@Statement);
                        --PRINT @Statement;

                        IF @@ERROR <> 0
                            BEGIN
                                SET @ErrorCode = @@ERROR + 100000;
                                --SET @ErrorMessage = ;
                                --RETURN @ErrorCode;
                                THROW @ErrorCode, 'Failed set the Default Funding Source', 1;
                            END
                    END
                ELSE
                    BEGIN
                        --Reject the remaining rows
                        SET @Statement = '
                        UPDATE TargetFundingSource 
                        SET Rejected = 1, FundingSourceUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'''' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N''Source Property: FundingSource; FundingSource was not found''
                        FROM 
                            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetFundingSource
                        WHERE
                            TargetFundingSource.FundingSourceUID = 0
                        AND TargetFundingSource.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                        AND (TargetFundingSource.Rejected = 0 OR ' + CAST(@AllowStackingErrors AS VARCHAR(1)) + ' = 1)';
                        EXECUTE (@Statement);
                        --PRINT @Statement;

                        IF @@ERROR <> 0
                            BEGIN
                                SET @ErrorCode = @@ERROR + 100000;
                                --SET @ErrorMessage = ;
                                --RETURN @ErrorCode;
                                THROW @ErrorCode, 'Failed to reject unmatched Funding Source records', 1;
                            END
                    END
            END

        SET NOCOUNT OFF;

        RETURN 0;

    END --End Procedure