/*
 *  sp_AddUpdate_FundingSources
 *  
 *  
 */
CREATE PROCEDURE [dbo].[sp_AddUpdate_FundingSources]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateFundingSources   AS BIT,
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @ErrorCode              AS INT,
                @Statement              AS VARCHAR(MAX);

        SET NOCOUNT ON;

        SET @CreateFundingSources = 0;
        SET @TargetDatabase = [dbo].[fn_GetTargetDatabaseName](@ProcessUid);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to determine the Target Database for the Process', 1;
            END

        SET @SourceTable = [dbo].[fn_GetSourceTable](@SourceProcess);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to determine the Source Table for the Process', 1;
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

        IF @CreateFundingSources = 1
            BEGIN
                --Create new Funding Sources
                SET @Statement = '
                INSERT INTO ' + @TargetDatabase + '.dbo.tblFundingSources
                    (FundingSource, FundingDesc, Active, ApplicationUID, TransferNotificationEmail, StatusNotificationEmail, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
                SELECT DISTINCT
                    TargetFundingSource.FundingSource, TargetFundingSource.FundingSourceDescription, 1, 2, NULL, NULL, 0, GETDATE(), 0, GETDATE()
                FROM
                    IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetFundingSource
                WHERE
                    TargetFundingSource.FundingSourceUID = 0
                AND TargetFundingSource.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                AND TargetFundingSource.Rejected = 0';
                EXECUTE (@Statement);
                --PRINT @Statement;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to create New Funding Sources', 1;
                    END

                --Match the Created Funding Source to the Origin record
                SET @Statement = '
                UPDATE TargetFundingSource
                SET TargetFundingSource.FundingSourceUID = SourceFundingSource.FundingSourceUID
                FROM
                    IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetFundingSource
                INNER JOIN
                    ' + @TargetDatabase + '.dbo.tblFundingSources SourceFundingSource
                    ON UPPER(LTRIM(RTRIM(TargetFundingSource.FundingSource))) = UPPER(LTRIM(RTRIM(SourceFundingSource.FundingSource))) AND
                       UPPER(LTRIM(RTRIM(TargetFundingSource.FundingSourceDescription))) = UPPER(LTRIM(RTRIM(SourceFundingSource.FundingDesc)))
                WHERE
                    TargetFundingSource.FundingSourceUID = 0
                AND TargetFundingSource.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                AND TargetFundingSource.Rejected = 0';
                EXECUTE (@Statement);
                --PRINT @Statement;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match New Funding Sources to their source records', 1;
                    END

            END

    END -- End Procedure