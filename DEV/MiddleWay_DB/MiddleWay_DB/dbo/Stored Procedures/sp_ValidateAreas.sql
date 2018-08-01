﻿/*
 *  sp_ValidateAreas
 *  Perform matching by Area Name
 *  Reject set AreaUID to -1 and Rejected to true, add Reject Notes
 */
CREATE PROCEDURE [dbo].[sp_ValidateAreas]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateAreas    AS BIT,
                @DefaultArea    AS VARCHAR(100),
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @AllowStackingErrors    AS BIT;

        SET @CreateAreas = 0;
        SET @DefaultArea = 'None';
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
            @CreateAreas = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'CreateAreas'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        SELECT @DefaultArea = UPPER(LTRIM(RTRIM(ConfigurationValue)))
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultArea' AND ProcessUid = @ProcessUid
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


        --If AreaName IS Null or Empty set to DefaultArea
        UPDATE TargetArea SET TargetArea.AreaName = @DefaultArea
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetArea
        WHERE
            (TargetArea.AreaName IS NULL OR
             LTRIM(RTRIM(TargetArea.AreaName)) = '')
        AND TargetArea.AreaUID = 0
        AND TargetArea.ProcessTaskUID = @ProcessTaskUid
        AND (TargetArea.Rejected = 0 OR @AllowStackingErrors = 1);


        --Match Areas By Name
        --SELECT TargetArea.AreaUID, TargetArea.AreaName, SourceArea.AreaName, SourceArea.AreaUID
        UPDATE TargetArea SET TargetArea.AreaUID = SourceArea.AreaUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetArea
        INNER JOIN (
            SELECT
                SourceArea.AreaName, MAX(SourceArea.AreaUID) AreaUID
            FROM 
                IntegrationMiddleWay.dbo._ETL_Inventory TargetArea
            LEFT JOIN
                TipWebHostedChicagoPS.dbo.tblUnvAreas SourceArea
                ON UPPER(LTRIM(RTRIM(TargetArea.AreaName))) = UPPER(LTRIM(RTRIM(SourceArea.AreaName)))
            WHERE
                SourceArea.AreaName IS NOT NULL
            AND TargetArea.AreaName IS NOT NULL
            AND TargetArea.ProcessTaskUID = @ProcessTaskUid
            AND (TargetArea.Rejected = 0 OR @AllowStackingErrors = 1)
            GROUP BY
                SourceArea.AreaName
            ) SourceArea
            ON UPPER(LTRIM(RTRIM(TargetArea.AreaName))) = UPPER(LTRIM(RTRIM(SourceArea.AreaName)))
        WHERE 
            SourceArea.AreaName IS NOT NULL
        AND TargetArea.AreaName IS NOT NULL
        AND TargetArea.AreaUID = 0
        AND TargetArea.ProcessTaskUID = @ProcessTaskUid
        AND (TargetArea.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @CreateAreas = 1
            BEGIN
                --    If AreaName IS Null or Empty then Reject
                --SELECT TargetArea.AreaUID, TargetArea.AreaUID
                UPDATE TargetArea SET Rejected = 1, AreaUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: AreaName; AreaName is NULL or Empty'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetArea
                WHERE
                    TargetArea.AreaName IS NULL 
                 OR LTRIM(RTRIM(TargetArea.AreaName)) = ''
                AND TargetArea.ProcessTaskUID = @ProcessTaskUid
                AND (TargetArea.Rejected = 0 OR @AllowStackingErrors = 1);
            END
        ELSE
            BEGIN
        --    If AreaUID = 0 then Reject
                --SELECT TargetArea.AreaUID, TargetArea.AreaUID
                UPDATE TargetArea SET Rejected = 1, AreaUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: AreaName; AreaName could not be Matched'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetArea
                WHERE
                    TargetArea.AreaUID = 0
                AND TargetArea.ProcessTaskUID = @ProcessTaskUid
                AND (TargetArea.Rejected = 0 OR @AllowStackingErrors = 1);
            END

    END --End Procedure