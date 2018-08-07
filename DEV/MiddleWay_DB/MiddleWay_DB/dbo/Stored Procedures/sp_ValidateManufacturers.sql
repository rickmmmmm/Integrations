/*
 *  sp_ValidateManufacturers
 *  Perform matching by Manufacturer Name
 *  Reject set ManufacturerUID to -1 and Rejected to true, add Reject Notes
 */
CREATE PROCEDURE [dbo].[sp_ValidateManufacturers]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateManufacturers    AS BIT,
                @DefaultManufacturer    AS VARCHAR(100),
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @AllowStackingErrors    AS BIT,
                @ErrorCode              AS INT;

        SET NOCOUNT ON;

        SET @CreateManufacturers = 0;
        SET @DefaultManufacturer = 'None';
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
            @CreateManufacturers = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'CreateManufacturers'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateManufacturers', 1;
            END

        SELECT @DefaultManufacturer = UPPER(LTRIM(RTRIM(ConfigurationValue)))
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultManufacturer'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for DefaultManufacturer', 1;
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

        --If ManufacturerName IS Null or Empty set to DefaultManufacturer
        UPDATE TargetManufacturer SET TargetManufacturer.ManufacturerName = @DefaultManufacturer
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetManufacturer
        WHERE
            (TargetManufacturer.ManufacturerName IS NULL OR
             LTRIM(RTRIM(TargetManufacturer.ManufacturerName)) = '')
        AND TargetManufacturer.ManufacturerUID = 0
        AND TargetManufacturer.ProcessTaskUID = @ProcessTaskUid
        AND (TargetManufacturer.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to set the DefaultManufacturer for Manufacturers where the Name is Null or Empty', 1;
            END

        --Match Manufacturers By Name
        --SELECT TargetManufacturer.ManufacturerUID, TargetManufacturer.ManufacturerName, SourceManufacturer.ManufacturerName, SourceManufacturer.ManufacturerUID
        UPDATE TargetManufacturer SET TargetManufacturer.ManufacturerUID = SourceManufacturer.ManufacturerUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetManufacturer
        INNER JOIN (
            SELECT
                SourceManufacturer.ManufacturerName, MAX(SourceManufacturer.ManufacturerUID) ManufacturerUID
            FROM 
                IntegrationMiddleWay.dbo._ETL_Inventory TargetManufacturer
            LEFT JOIN
                TipWebHostedChicagoPS.dbo.tblUnvManufacturers SourceManufacturer
                ON UPPER(LTRIM(RTRIM(TargetManufacturer.ManufacturerName))) = UPPER(LTRIM(RTRIM(SourceManufacturer.ManufacturerName)))
            WHERE
                (SourceManufacturer.ManufacturerName IS NOT NULL AND
                 LTRIM(RTRIM(SourceManufacturer.ManufacturerName)) <> '')
            AND (TargetManufacturer.ManufacturerName IS NOT NULL AND
                 LTRIM(RTRIM(TargetManufacturer.ManufacturerName)) <> '')
            AND TargetManufacturer.ProcessTaskUID = @ProcessTaskUid
            AND (TargetManufacturer.Rejected = 0 OR @AllowStackingErrors = 1)
            GROUP BY
                SourceManufacturer.ManufacturerName
            ) SourceManufacturer
            ON UPPER(LTRIM(RTRIM(TargetManufacturer.ManufacturerName))) = UPPER(LTRIM(RTRIM(SourceManufacturer.ManufacturerName)))
        WHERE
            (SourceManufacturer.ManufacturerName IS NOT NULL AND
             LTRIM(RTRIM(SourceManufacturer.ManufacturerName)) <> '')
        AND (TargetManufacturer.ManufacturerName IS NOT NULL AND
             LTRIM(RTRIM(TargetManufacturer.ManufacturerName)) <> '')
        AND TargetManufacturer.ManufacturerUID = 0
        AND TargetManufacturer.ProcessTaskUID = @ProcessTaskUid
        AND (TargetManufacturer.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Manufacturers by Name', 1;
            END

        IF @CreateManufacturers = 1
            BEGIN
                --    If ManufacturerName IS Null or Empty then Reject
                --SELECT TargetManufacturer.ManufacturerUID, TargetManufacturer.ManufacturerUID
                UPDATE TargetManufacturer SET Rejected = 1, ManufacturerUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: ManufacturerName; ManufacturerName is NULL or Empty'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetManufacturer
                WHERE
                    (TargetManufacturer.ManufacturerName IS NULL OR
                     LTRIM(RTRIM(TargetManufacturer.ManufacturerName)) = '')
                AND TargetManufacturer.ProcessTaskUID = @ProcessTaskUid
                AND (TargetManufacturer.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where the ManufacturerName is Null or Empty', 1;
                    END

            END
        ELSE
            BEGIN
        --    If ManufacturerUID = 0 then Reject
                --SELECT TargetManufacturer.ManufacturerUID, TargetManufacturer.ManufacturerUID
                UPDATE TargetManufacturer SET Rejected = 1, ManufacturerUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: ManufacturerName; ManufacturerName could not be Matched'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetManufacturer
                WHERE
                    TargetManufacturer.ManufacturerUID = 0
                AND (TargetManufacturer.ManufacturerName IS NULL OR
                     LTRIM(RTRIM(TargetManufacturer.ManufacturerName)) = '')
                AND TargetManufacturer.ProcessTaskUID = @ProcessTaskUid
                AND (TargetManufacturer.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where the Manufacturer could not be matched', 1;
                    END

            END

        SET NOCOUNT OFF;

        RETURN 0;

    END --End Procedure