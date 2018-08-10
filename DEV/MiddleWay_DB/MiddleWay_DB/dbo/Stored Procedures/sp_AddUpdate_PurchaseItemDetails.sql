/*
 *  sp_AddUpdate_PurchaseItemDetails
 *  
 *  
 */
CREATE PROCEDURE [dbo].[sp_AddUpdate_PurchaseItemDetails]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateNewPurchaseDetails   AS BIT,
                @TargetDatabase             AS VARCHAR(100),
                @SourceTable                AS VARCHAR(100),
                @ErrorCode                  AS INT,
                @Statement                  AS VARCHAR(MAX);

        SET NOCOUNT ON;

        SET @CreateNewPurchaseDetails = 0;
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
            @CreateNewPurchaseDetails = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'CreateNewPurchaseDetails'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateNewPurchaseDetails', 1;
            END

        PRINT N'Read All Configurations'

        IF @CreateNewPurchaseDetails = 1
            BEGIN
                PRINT N'Creating New Purchase Details'

                --Insert new Products
                SET @Statement = '
                INSERT INTO ' + @TargetDatabase + '.dbo.tblTechPurchaseItemDetails
                    (PurchaseUID, ItemUID, FundingSourceUID, StatusUID, SiteAddedSiteUID, QuantityOrdered, QuantityReceived, PurchasePrice, AccountCode, TechDepartmentUID, LineNumber,
                     CFDA, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate, IsAssociated)
                SELECT
                    TargetPurchaseDetails.PurchaseUID, TargetPurchaseDetails.ItemUID, TargetPurchaseDetails.FundingSourceUID, 32, TargetPurchaseDetails.SiteAddedSiteUID, 
                    0, 0, TargetPurchaseDetails.PurchasePrice, TargetPurchaseDetails.AccountCode, TargetPurchaseDetails.TechDepartmentUID, TargetPurchaseDetails.LineNumber,
                    NULL, 0, GETDATE(), 0, GETDATE(), 0
                FROM
                    IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetPurchaseDetails
                WHERE
                    TargetPurchaseDetails.PurchaseItemDetailUID = 0
                AND TargetPurchaseDetails.ItemUID > 0
                AND TargetPurchaseDetails.PurchaseUID > 0
                AND TargetPurchaseDetails.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                AND TargetPurchaseDetails.Rejected = 0
                GROUP BY
                    TargetPurchaseDetails.PurchaseUID,
                    TargetPurchaseDetails.ItemUID,
                    TargetPurchaseDetails.FundingSourceUID,
                    TargetPurchaseDetails.SiteAddedSiteUID,
                    TargetPurchaseDetails.PurchasePrice,
                    TargetPurchaseDetails.AccountCode,
                    TargetPurchaseDetails.TechDepartmentUID,
                    TargetPurchaseDetails.LineNumber';
                EXECUTE (@Statement);
                --PRINT @Statement;

                PRINT N'Created ' + CAST(@@ROWCOUNT AS VARCHAR) + ' Details'

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to create New Purchase Details', 1;
                    END

                PRINT N'Matching Created Details to original rows'

                --Match the created products to their origin records
                SET @Statement = '
                UPDATE TargetPurchaseDetails
                SET TargetPurchaseDetails.PurchaseItemDetailUID = SourcePurchaseDetails.PurchaseItemDetailUID
                FROM IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetPurchaseDetails
                INNER JOIN 
                    ' + @TargetDatabase + '.dbo.tblTechPurchaseItemDetails SourcePurchaseDetails
                    ON TargetPurchaseDetails.PurchaseUID  = SourcePurchaseDetails.PurchaseUID AND
                       TargetPurchaseDetails.ItemUID = SourcePurchaseDetails.ItemUID AND
                       TargetPurchaseDetails.FundingSourceUID = SourcePurchaseDetails.FundingSourceUID AND
                       TargetPurchaseDetails.SiteAddedSiteUID  = SourcePurchaseDetails.SiteAddedSiteUID AND
                       TargetPurchaseDetails.PurchasePrice   = SourcePurchaseDetails.PurchasePrice AND
                       ISNULL(TargetPurchaseDetails.AccountCode, '''') = ISNULL(SourcePurchaseDetails.AccountCode, '''') AND
                       TargetPurchaseDetails.TechDepartmentUID   = SourcePurchaseDetails.TechDepartmentUID AND
                       TargetPurchaseDetails.LineNumber = SourcePurchaseDetails.LineNumber
                WHERE
                    TargetPurchaseDetails.PurchaseItemDetailUID = 0
                AND TargetPurchaseDetails.ItemUID > 0
                AND TargetPurchaseDetails.PurchaseUID > 0
                AND TargetPurchaseDetails.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                AND TargetPurchaseDetails.Rejected = 0';
                EXECUTE (@Statement);
                --PRINT @Statement;

                PRINT N'Matched ' + CAST(@@ROWCOUNT AS VARCHAR) + ' Details'

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match New Purchase Details to their Origin record', 1;
                    END

            END

    END -- End Procedure