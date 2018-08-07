/*
 *  sp_AddUpdate_Manufacturers
 *  
 *  
 */
CREATE PROCEDURE [dbo].[sp_AddUpdate_Manufacturers]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateManufacturers    AS BIT,
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @ErrorCode              AS INT;

        SET NOCOUNT ON;

        SET @CreateManufacturers = 0;
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

        IF @CreateManufacturers = 1
            BEGIN

                INSERT INTO TipWebHostedChicagoPS.dbo.tblUnvManufacturers
                    (ManufacturerName, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
                SELECT DISTINCT
                    TargetManufacturer.ManufacturerName, 0, GETDATE(), 0, GETDATE()
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetManufacturer
                WHERE
                    TargetManufacturer.ManufacturerUID = 0
                AND TargetManufacturer.ManufacturerName <> 'None'
                AND TargetManufacturer.ProcessTaskUID = @ProcessTaskUid
                AND TargetManufacturer.Rejected = 0;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to create new Manufacturers', 1;
                    END

                --Match the manufacturer records to the new Manufacturers
                UPDATE TargetManufacturer
                SET TargetManufacturer.ManufacturerUID = SourceManufacturer.ManufacturerUID
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetManufacturer
                INNER JOIN
                    TipWebHostedChicagoPS.dbo.tblUnvManufacturers SourceManufacturer
                    ON UPPER(LTRIM(RTRIM(TargetManufacturer.ManufacturerName))) = UPPER(LTRIM(RTRIM(SourceManufacturer.ManufacturerName)))
                WHERE
                    TargetManufacturer.ManufacturerUID = 0
                AND TargetManufacturer.ManufacturerName <> 'None'
                AND TargetManufacturer.ProcessTaskUID = @ProcessTaskUid
                AND TargetManufacturer.Rejected = 0;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match new Manufacturers with the origin records', 1;
                    END

            END

    END -- End Procedure