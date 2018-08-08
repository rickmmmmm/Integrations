/*
 *  sp_AddUpdate_Vendors
 *  
 *  
 */
CREATE PROCEDURE [dbo].[sp_AddUpdate_Vendors]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateVendors            AS BIT,
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @Notes                  AS VARCHAR(100),
                @ErrorCode              AS INT;

        SET NOCOUNT ON;

        SET @CreateVendors = 0;
        SET @Notes = '';

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
            @CreateVendors = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'CreateVendors'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateVendors', 1;
            END

        SELECT
            @Notes = REPLACE(ISNULL(ConfigurationValue, 'MiddleWay Integration - {DATE}'), '{DATE}', CONVERT(VARCHAR(8), GETDATE(), 1))
        FROM [Configurations]
        WHERE ConfigurationName = 'InsertNotes'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for InsertNotes', 1;
            END

        IF @CreateVendors = 1
            BEGIN
                --Create new Vendors
                INSERT INTO TipWebHostedChicagoPS.dbo.tblVendor
                    (VendorName, Contact, Address, Address2, City, State, Zip, Phone, Fax, Email, AccountNumber, CampusID, Notes, Active, UserID, ModifiedDate, ApplicationUID)
                SELECT DISTINCT
                    TargetVendor.VendorName, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, TargetVendor.VendorAccountNumber, NULL, @Notes, 1, 0, GETDATE(), 2
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetVendor
                WHERE
                    TargetVendor.VendorUID = 0
                AND TargetVendor.VendorName <> 'NONE'
                AND TargetVendor.ProcessTaskUID = @ProcessTaskUid
                AND TargetVendor.Rejected = 0;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to Create New Vendors', 1;
                    END

                --Match created vendors with their origin records
                UPDATE TargetVendor
                SET TargetVendor.VendorUID = SourceVendor.VendorID
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetVendor
                INNER JOIN TipWebHostedChicagoPS.dbo.tblVendor SourceVendor
                    ON TargetVendor.VendorName = SourceVendor.VendorName AND
                       TargetVendor.VendorAccountNumber = SourceVendor.AccountNumber
                WHERE
                    TargetVendor.VendorUID = 0
                AND TargetVendor.VendorName <> 'NONE'
                AND TargetVendor.ProcessTaskUID = @ProcessTaskUid
                AND TargetVendor.Rejected = 0;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match Vendors to their origin records', 1;
                    END

            END

    END -- End Procedure