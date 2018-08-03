﻿/*
 *  sp_ValidateVendors
 *  Match Vendors by Name or AccountNumber
 *  Set a default for the remaining vendors or reject
 *      as appropriate
 */
CREATE PROCEDURE [dbo].[sp_ValidateVendors]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateVendors          AS BIT,
                @DefaultVendorUID       AS INT,
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @AllowStackingErrors    AS BIT,
                @ErrorCode              AS INT;

        SET NOCOUNT ON;

        SET @CreateVendors = 0;
        SET @DefaultVendorUID = -1;
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
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateVendors', 1;
            END

        SELECT @DefaultVendorUID = CAST(ConfigurationValue AS INT)
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultVendorUID' AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for DefaultVendorUID', 1;
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

        --Match the Vendor by Name
        --SELECT TargetVendor.VendorUID, TargetVendor.VendorUID, TargetVendor.VendorName, SourceVendor.VendorID, SourceVendor.VendorID
        UPDATE TargetVendor SET TargetVendor.VendorUID = SourceVendor.VendorID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetVendor
        INNER JOIN 
            TipWebHostedChicagoPS.dbo.tblVendor SourceVendor
            ON UPPER(LTRIM(RTRIM(TargetVendor.VendorName))) = UPPER(LTRIM(RTRIM(SourceVendor.VendorName)))
        WHERE
            TargetVendor.VendorName IS NOT NULL
        AND LTRIM(RTRIM(TargetVendor.VendorName)) <> ''
        AND TargetVendor.VendorUID = 0
        AND TargetVendor.ProcessTaskUID = @ProcessTaskUid
        AND (TargetVendor.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Vendors by name', 1;
            END

        --Match the Vendor by Account Number
        --SELECT TargetVendor.VendorUID, TargetVendor.VendorName, TargetVendor.VendorAccountNumber, SourceVendor.VendorName, SourceVendor.AccountNumber, SourceVendor.VendorID
        UPDATE TargetVendor SET TargetVendor.VendorUID = SourceVendor.VendorID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetVendor
        INNER JOIN (
            SELECT
                SourceVendors.VendorID,
                SourceVendors.VendorName,
                SourceVendors.AccountNumber
            FROM (
                SELECT
                    UPPER(LTRIM(RTRIM(AccountNumber))) AccountNumber
                FROM
                    TipWebHostedChicagoPS.dbo.tblVendor SourceVendors
                GROUP BY
                    AccountNumber
                HAVING COUNT(VendorID) = 1
                ) UniqueVendorAccountNumbers
            INNER JOIN
                TipWebHostedChicagoPS.dbo.tblVendor SourceVendors
                ON UniqueVendorAccountNumbers.AccountNumber = UPPER(LTRIM(RTRIM(SourceVendors.AccountNumber)))
            ) SourceVendor
            ON UPPER(LTRIM(RTRIM(TargetVendor.VendorAccountNumber))) = SourceVendor.AccountNumber
        WHERE
            TargetVendor.VendorAccountNumber IS NOT NULL
        AND LTRIM(RTRIM(TargetVendor.VendorAccountNumber)) <> ''
        AND TargetVendor.VendorUID = 0
        AND TargetVendor.ProcessTaskUID = @ProcessTaskUid
        AND (TargetVendor.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Vendors by Account Number', 1;
            END

        IF @CreateVendors = 0
            BEGIN
                IF @DefaultVendorUID >= 0
                    BEGIN
                        --Set remaining Vendors to match the default
                        --SELECT TargetVendor.VendorUID, TargetVendor.VendorName, TargetVendor.VendorAccountNumber
                        UPDATE TargetVendor SET TargetVendor.VendorUID = @DefaultVendorUID, TargetVendor.VendorName = SourceVendors.VendorName
                        FROM
                            IntegrationMiddleWay.dbo._ETL_Inventory TargetVendor
                        INNER JOIN
                            TipWebHostedChicagoPS.dbo.tblVendor SourceVendors
                            ON @DefaultVendorUID = SourceVendors.VendorID
                        WHERE
                            TargetVendor.VendorUID = 0
                            AND TargetVendor.ProcessTaskUID = @ProcessTaskUid
                            AND (TargetVendor.Rejected = 0 OR @AllowStackingErrors = 1);

                        IF @@ERROR <> 0
                            BEGIN
                                SET @ErrorCode = @@ERROR;
                                --SET @ErrorMessage = ;
                                --RETURN @ErrorCode;
                                THROW @ErrorCode, 'Failed to set Vendors to Default', 1;
                            END

                    END
                ELSE
                    BEGIN
                        --No Default Vendor Set, reject remainders
                        --SELECT TargetVendor.VendorUID, TargetVendor.VendorUID
                        UPDATE TargetVendor SET Rejected = 1, VendorUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Properties: VendorName, VendorAccountNumber; VendorName and VendorAccountNumber are NULL or Empty'
                        FROM
                            IntegrationMiddleWay.dbo._ETL_Inventory TargetVendor
                        WHERE
                            (TargetVendor.VendorName IS NULL 
                             OR LTRIM(RTRIM(TargetVendor.VendorName)) = '')
                        AND (TargetVendor.VendorAccountNumber IS NULL 
                             OR LTRIM(RTRIM(TargetVendor.VendorAccountNumber)) = '')
                        AND TargetVendor.ProcessTaskUID = @ProcessTaskUid
                        AND (TargetVendor.Rejected = 0 OR @AllowStackingErrors = 1);

                        IF @@ERROR <> 0
                            BEGIN
                                SET @ErrorCode = @@ERROR;
                                --SET @ErrorMessage = ;
                                --RETURN @ErrorCode;
                                THROW @ErrorCode, 'Failed to reject unmatched Vendors', 1;
                            END

                    END
            END
        ELSE
            BEGIN
                --SELECT TargetVendor.VendorUID, TargetVendor.VendorUID
                UPDATE TargetVendor SET Rejected = 1, VendorUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Properties: VendorName, VendorAccountNumber; VendorName and VendorAccountNumber are NULL or Empty'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetVendor
                WHERE
                    (TargetVendor.VendorName IS NULL 
                     OR LTRIM(RTRIM(TargetVendor.VendorName)) = '')
                AND (TargetVendor.VendorAccountNumber IS NULL 
                     OR LTRIM(RTRIM(TargetVendor.VendorAccountNumber)) = '')
                AND TargetVendor.ProcessTaskUID = @ProcessTaskUid
                AND (TargetVendor.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where the Vendor Name and Account Number is Null or Empty', 1;
                    END

            END

        SET NOCOUNT OFF;

        RETURN 0;

    END --End Procedure