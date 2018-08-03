/*
 *  sp_ValidateStatus
 *  Match the Status to statusDesc where the statustype is room
 *  MODIFICATIONS ARE NECESSARY TO SEPARATE TAG STATUS FROM PURCHASE STATUS VALIDATIONS
 *      AND ROOM STATUS FROM STAFF/STUDENT STATUS
 */
CREATE PROCEDURE [dbo].[sp_ValidateStatus]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @DefaulStatusID         AS INT,
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @AllowStackingErrors    AS BIT,
                @ErrorCode                  AS INT;

        SET NOCOUNT ON;

        --SET @CreateRooms = 0;
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

        SELECT @DefaulStatusID = CAST(ConfigurationValue AS INT)
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultStatusID' AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for DefaultStatusID', 1;
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

        -- Match the Status to StatusDesc
        --SELECT TargetStatus.StatusID, TargetStatus.Status, SourceStatus.statusID, SourceStatus.statusDesc, SourceStatus.StatusTypeUID
        UPDATE TargetStatus SET TargetStatus.EntityUID = SourceStatus.statusID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetStatus
        INNER JOIN 
            TipWebHostedChicagoPS.dbo.tblStatus SourceStatus
            ON UPPER(LTRIM(RTRIM(TargetStatus.Status))) = UPPER(LTRIM(RTRIM(SourceStatus.statusDesc)))
            AND SourceStatus.StatusTypeUID = 6 -- Room
        WHERE
            TargetStatus.Status IS NOT NULL
        AND LTRIM(RTRIM(TargetStatus.Status)) <> ''
        AND TargetStatus.StatusID = 0
        AND TargetStatus.ProcessTaskUID = @ProcessTaskUid
        AND (TargetStatus.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match status by description', 1;
            END

        --Set all remaining entries to default values
        --SELECT TargetStatus.StatusID, TargetStatus.Status, SourceStatus.statusID, SourceStatus.statusDesc, SourceStatus.StatusTypeUID
        UPDATE TargetStatus SET TargetStatus.StatusID = SourceStatus.statusID, TargetStatus.Status = SourceStatus.statusDesc
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetStatus
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblStatus SourceStatus
            ON @DefaulStatusID = UPPER(LTRIM(RTRIM(SourceStatus.statusID)))
        WHERE
            TargetStatus.StatusID = 0
        AND TargetStatus.ProcessTaskUID = @ProcessTaskUid
        AND (TargetStatus.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to set the default status', 1;
            END

        --Reject all remaining entries
        --SELECT TargetStatus.StatusID, TargetStatus.Status
        UPDATE TargetStatus SET Rejected = 1, StatusID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: Status; Status could not be matched'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetStatus
        WHERE
            TargetStatus.StatusID = 0
        AND TargetStatus.ProcessTaskUID = @ProcessTaskUid
        AND (TargetStatus.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the status could not be matched', 1;
            END

        SET NOCOUNT OFF;

        RETURN 0;

    END --End Procedure