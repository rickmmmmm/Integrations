/*
 *  sp_AddUpdate_Areas
 *  
 *  
 */
CREATE PROCEDURE [dbo].[sp_AddUpdate_Areas]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateAreas            AS BIT,
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @ErrorCode              AS INT;

        SET NOCOUNT ON;

        SET @CreateAreas = 0;
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
            @CreateAreas = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'CreateAreas'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateAreas', 1;
            END

        IF @CreateAreas = 1
            BEGIN

                INSERT INTO TipWebHostedChicagoPS.dbo.tblUnvAreas
                    (AreaName, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
                SELECT DISTINCT
                    TargetArea.AreaName, 0, GETDATE(), 0, GETDATE()
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetArea
                WHERE
                    TargetArea.AreaUID = 0
                AND TargetArea.AreaName <> 'None'
                AND TargetArea.ProcessTaskUID = @ProcessTaskUid
                AND TargetArea.Rejected = 0;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to create New Areas', 1;
                    END

                UPDATE TargetArea
                SET TargetArea.AreaUID = SourceArea.AreaUID
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetArea
                INNER JOIN
                    TipWebHostedChicagoPS.dbo.tblUnvAreas SourceArea
                    ON UPPER(LTRIM(RTRIM(TargetArea.AreaName))) = UPPER(LTRIM(RTRIM(SourceArea.AreaName)))
                WHERE
                    TargetArea.AreaUID = 0
                AND TargetArea.AreaName <> 'None'
                AND TargetArea.ProcessTaskUID = @ProcessTaskUid
                AND TargetArea.Rejected = 0;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match New Areas to their origin records', 1;
                    END

            END

    END -- End Procedure