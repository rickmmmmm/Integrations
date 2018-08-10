/*
 *  sp_AddUpdate_PurchaseItemShipments
 *  
 *  
 */
CREATE PROCEDURE [dbo].[sp_AddUpdate_PurchaseItemShipments]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateNewPurchaseShipments AS BIT,
                @TargetDatabase             AS VARCHAR(100),
                @SourceTable                AS VARCHAR(100),
                @ErrorCode                  AS INT,
                @Statement                  AS VARCHAR(MAX);

        SET NOCOUNT ON;

        SET @CreateNewPurchaseShipments = 0;
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
            @CreateNewPurchaseShipments = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'CreateNewPurchaseShipments'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateNewPurchaseShipments', 1;
            END

        PRINT N'Reading Configurations complete';

        IF @CreateNewPurchaseShipments = 1
            BEGIN

                PRINT N'Create new Shipment records';
                SET @Statement = '
                INSERT INTO ' + @TargetDatabase + '.dbo.tblTechPurchaseItemShipments --SourcePurchaseShipments
                    (PurchaseItemDetailUID, ShippedToSiteUID, TicketNumber, QuantityShipped, TicketedByUserID, TicketedDate, 
                     StatusUID, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate, InvoiceNumber, InvoiceDate)
                SELECT
                    TargetPurchaseShipments.PurchaseItemDetailUID, TargetPurchaseShipments.ShippedToSiteUID, NULL,
                    0, NULL, NULL, 32, 0, GETDATE(), 0, GETDATE(), NULL, NULL --COUNT(TargetPurchaseShipments.InventoryUID)
                FROM
                    IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetPurchaseShipments
                WHERE
                    TargetPurchaseShipments.PurchaseItemShipmentUID = 0
                AND TargetPurchaseShipments.PurchaseItemDetailUID > 0
                AND TargetPurchaseShipments.PurchaseUID > 0
                AND TargetPurchaseShipments.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                AND TargetPurchaseShipments.Rejected = 0
                GROUP BY
                    TargetPurchaseShipments.PurchaseItemDetailUID,
                    TargetPurchaseShipments.ShippedToSiteUID';
                EXECUTE (@Statement);
                --PRINT @Statement;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to create New Purchase Shipments', 1;
                    END

                PRINT N'Matching New Purchase Shipments to their Origin Records';
                SET @Statement = '
                UPDATE TargetPurchaseShipments
                SET TargetPurchaseShipments.PurchaseItemShipmentUID = SourcePurchaseShipments.PurchaseItemShipmentUID
                FROM
                    IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetPurchaseShipments
                INNER JOIN
                    ' + @TargetDatabase + '.dbo.tblTechPurchaseItemShipments SourcePurchaseShipments
                    ON TargetPurchaseShipments.PurchaseItemDetailUID = SourcePurchaseShipments.PurchaseItemDetailUID AND
                       TargetPurchaseShipments.ShippedToSiteUID = SourcePurchaseShipments.ShippedToSiteUID
                WHERE
                    TargetPurchaseShipments.PurchaseItemShipmentUID = 0
                AND TargetPurchaseShipments.PurchaseItemDetailUID > 0
                AND TargetPurchaseShipments.PurchaseUID > 0
                AND TargetPurchaseShipments.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                AND TargetPurchaseShipments.Rejected = 0';
                EXECUTE (@Statement);
                --PRINT @Statement;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match New Purchase Shipments to their Origin records', 1;
                    END

            END

    END -- End Procedure