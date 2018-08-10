/*
 *  sp_AddUpdate_PurchaseInventory
 *  
 *  
 */
CREATE PROCEDURE [dbo].[sp_AddUpdate_PurchaseInventory]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @ErrorCode              AS INT,
                @Statement              AS VARCHAR(MAX);

        SET NOCOUNT ON;

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

        --Insert the Purchase Inventory Record
        SET @Statement = '
        INSERT INTO ' + @TargetDatabase + '.dbo.tblTechPurchaseInventory --SourcePurchaseInventory
            (InventoryUID, PurchaseItemShipmentUID, CreatedByUserID, CreatedDate, LastModifiedByUserID, LastModifiedDate)
        SELECT
            InventoryUID, PurchaseItemShipmentUID, 0, GETDATE(), 0, GETDATE()
        FROM
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetPurchaseInventory
        WHERE
            TargetPurchaseInventory.InventoryUID > 0
        AND TargetPurchaseInventory.PurchaseItemShipmentUID > 0
        AND TargetPurchaseInventory.PurchaseInventoryUID = 0
        AND TargetPurchaseInventory.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetPurchaseInventory.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to create New Purchase Inventory', 1;
            END

        --Match the created records to the Purchase Inventory
        SET @Statement = '
        UPDATE TargetPurchaseInventory
        SET TargetPurchaseInventory.PurchaseInventoryUID = SourcePurchaseInventory.PurchaseInventoryUID
        FROM
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetPurchaseInventory
        INNER JOIN
            ' + @TargetDatabase + '.dbo.tblTechPurchaseInventory SourcePurchaseInventory
            ON TargetPurchaseInventory.InventoryUID = SourcePurchaseInventory.InventoryUID AND
               TargetPurchaseInventory.PurchaseItemShipmentUID = SourcePurchaseInventory.PurchaseItemShipmentUID
        WHERE
            TargetPurchaseInventory.InventoryUID > 0
        AND TargetPurchaseInventory.PurchaseItemShipmentUID > 0
        AND TargetPurchaseInventory.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND TargetPurchaseInventory.Rejected = 0';
        EXECUTE (@Statement);
        --PRINT @Statement;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match created Purchase Inventory to their origin records', 1;
            END

        --Update the Shipment quantities for the Inventory Inserted
        SET @Statement = '
        UPDATE SourcePurchaseShipments
        SET SourcePurchaseShipments.QuantityShipped = SourcePurchaseShipments.QuantityShipped + TargetPurchaseShipments.InventoryCount
        FROM
            ' + @TargetDatabase + '.dbo.tblTechPurchaseItemShipments SourcePurchaseShipments
        INNER JOIN (
            SELECT
                TargetPurchaseShipments.PurchaseItemShipmentUID, COUNT(TargetPurchaseShipments.InventoryUID) AS InventoryCount
            FROM
                IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetPurchaseShipments
            WHERE
                TargetPurchaseShipments.PurchaseInventoryUID > 0
            AND TargetPurchaseShipments.PurchaseItemShipmentUID > 0
            AND TargetPurchaseShipments.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
            AND TargetPurchaseShipments.Rejected = 0
            GROUP BY
                TargetPurchaseShipments.PurchaseItemShipmentUID
            ) TargetPurchaseShipments
            ON SourcePurchaseShipments.PurchaseItemShipmentUID = TargetPurchaseShipments.PurchaseItemShipmentUID';
        EXECUTE (@Statement);
        --PRINT @Statement;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to update the Inventory Count of Purchase Inventory added to Shipments', 1;
            END

        --Update the Detail quantities for the Inventory Inserted
        SET @Statement = '
        UPDATE SourcePurchaseDetails
        SET SourcePurchaseDetails.QuantityOrdered = SourcePurchaseDetails.QuantityOrdered + TargetPurchaseDetails.InventoryCount, 
            SourcePurchaseDetails.QuantityReceived = SourcePurchaseDetails.QuantityReceived + TargetPurchaseDetails.InventoryCount
        FROM
            ' + @TargetDatabase + '.dbo.tblTechPurchaseItemDetails SourcePurchaseDetails
        INNER JOIN (
            SELECT
                TargetPurchaseDetails.PurchaseItemDetailUID, COUNT(TargetPurchaseDetails.InventoryUID) AS InventoryCount
            FROM
                IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetPurchaseDetails
            WHERE
                TargetPurchaseDetails.PurchaseInventoryUID > 0
            AND TargetPurchaseDetails.PurchaseItemDetailUID > 0
            AND TargetPurchaseDetails.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
            AND TargetPurchaseDetails.Rejected = 0
            GROUP BY
                TargetPurchaseDetails.PurchaseItemDetailUID
            ) TargetPurchaseDetails
            ON SourcePurchaseDetails.PurchaseItemDetailUID = TargetPurchaseDetails.PurchaseItemDetailUID';
        EXECUTE (@Statement);
        --PRINT @Statement;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to update the Inventory Count of Purchase Inventory added to Details', 1;
            END

    END -- End Procedure