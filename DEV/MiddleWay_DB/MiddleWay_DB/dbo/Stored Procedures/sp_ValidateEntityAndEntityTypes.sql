/*
 *  sp_ValidateEntityAndEntityTypes
 *  THIS PROCEDURE AND RELATED COLUMNS NEED TO BE UPDATED TO 
 *  ALLOW ASSETS TO BE ASSIGNED TO STAFF AND STUDENTS
 *  ALSO VALIDATE LOCATION TYPE (NOT ENTITYTYPE)
 */
CREATE PROCEDURE [dbo].[sp_ValidateEntityAndEntityTypes]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateRooms            AS BIT,
                @DefaultRoom            AS VARCHAR(50),
                @DefaultRoomType        AS VARCHAR(100),
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @AllowStackingErrors    AS BIT,
                @ErrorCode              AS INT;

        SET NOCOUNT ON;

        SET @CreateRooms = 0;
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
            @CreateRooms = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'CreateRooms'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateRooms', 1;
            END

        SELECT @DefaultRoom = UPPER(LTRIM(RTRIM(ConfigurationValue)))
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultRoom' AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for Default Room', 1;
            END

        SELECT @DefaultRoomType = UPPER(LTRIM(RTRIM(ConfigurationValue)))
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultRoomType' AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for DefaultRoomType', 1;
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

        -- Match the EntityID to RoomNumber
        --SELECT TargetEntity.EntityName, TargetEntity.EntityID, TargetEntity.EntityName, SourceRoom.RoomNumber, SourceRoom.RoomUID, SourceRoom.RoomTypeUID, SourceRoom.SiteUID
        UPDATE TargetEntity SET TargetEntity.EntityUID = SourceRoom.RoomUID, TargetEntity.EntityTypeUID = 2
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetEntity
        INNER JOIN 
            TipWebHostedChicagoPS.dbo.tblUnvRooms SourceRoom
            ON UPPER(LTRIM(RTRIM(TargetEntity.EntityID))) = UPPER(LTRIM(RTRIM(SourceRoom.RoomNumber)))
            AND TargetEntity.SiteUID = SourceRoom.SiteUID
        WHERE
            TargetEntity.EntityID IS NOT NULL
        AND LTRIM(RTRIM(TargetEntity.EntityID)) <> ''
        AND TargetEntity.EntityUID = 0
        AND TargetEntity.ProcessTaskUID = @ProcessTaskUid
        AND (TargetEntity.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match EntityID to Room Number', 1;
            END

        -- Match the EntityName to RoomNumber
        --SELECT TargetEntity.EntityName, TargetEntity.EntityID, TargetEntity.EntityName, SourceRoom.RoomNumber, SourceRoom.RoomUID, SourceRoom.RoomTypeUID, SourceRoom.SiteUID
        UPDATE TargetEntity SET TargetEntity.EntityUID = SourceRoom.RoomUID, TargetEntity.EntityTypeUID = 2
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetEntity
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblUnvRooms SourceRoom
            ON UPPER(LTRIM(RTRIM(TargetEntity.EntityName))) = UPPER(LTRIM(RTRIM(SourceRoom.RoomNumber)))
            AND TargetEntity.SiteUID = SourceRoom.SiteUID
        WHERE
            TargetEntity.EntityName IS NOT NULL
        AND LTRIM(RTRIM(TargetEntity.EntityName)) <> ''
        AND TargetEntity.EntityUID = 0
        AND TargetEntity.ProcessTaskUID = @ProcessTaskUid
        AND (TargetEntity.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match EntityName to RoomNumber', 1;
            END

        --IF CreateRoom is false set all remaining Entities to match defaults
        IF @CreateRooms = 0
            BEGIN
                --Set all remaining entities to default values
                --SELECT TargetEntity.EntityUID, TargetEntity.EntityID, TargetEntity.EntityName
                UPDATE TargetEntity SET TargetEntity.EntityUID = SourceRoom.RoomUID, TargetEntity.EntityID = @DefaultRoom, TargetEntity.EntityTypeUID = 2
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetEntity
                INNER JOIN
                    TipWebHostedChicagoPS.dbo.tblUnvRooms SourceRoom
                    ON @DefaultRoom = UPPER(LTRIM(RTRIM(SourceRoom.RoomNumber)))
                    AND TargetEntity.SiteUID = SourceRoom.SiteUID
                WHERE
                    TargetEntity.EntityUID = 0
                AND TargetEntity.ProcessTaskUID = @ProcessTaskUid
                AND (TargetEntity.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to set all remaining Entities to default values', 1;
                    END

                --Reject all remaining entites
                --SELECT TargetEntity.EntityUID, TargetEntity.EntityID, TargetEntity.EntityName
                UPDATE TargetEntity SET Rejected = 1, EntityUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: EntityID/EntityName; EntityID/EntityName could not be matched'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetEntity
                WHERE
                    TargetEntity.EntityUID = 0
                AND TargetEntity.ProcessTaskUID = @ProcessTaskUid
                AND (TargetEntity.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject unvalid entities', 1;
                    END
            END
        ELSE
            BEGIN
                --Reject all Entities where the EntityID and EntityName are null or empty
                --SELECT TargetEntity.EntityUID, TargetEntity.EntityID, TargetEntity.EntityName
                UPDATE TargetEntity SET Rejected = 1, EntityUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: EntityID/EntityName; EntityID/EntityName are Null or Empty'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetEntity
                WHERE
                    TargetEntity.EntityUID = 0
                AND (TargetEntity.EntityID IS NULL OR
                     LTRIM(RTRIM(TargetEntity.EntityID)) = '')
                AND (TargetEntity.EntityName IS NULL OR
                     LTRIM(RTRIM(TargetEntity.EntityName)) = '')
                AND TargetEntity.ProcessTaskUID = @ProcessTaskUid
                AND (TargetEntity.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject all Entities where the EntityID and EntityName are Empty or Null', 1;
                    END
            END

        SET NOCOUNT OFF;

        RETURN 0;

    END --End Procedure