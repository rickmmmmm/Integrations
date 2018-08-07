/*
 *  sp_ValidatePurchaseOrders
 *  Validate Purchasees, Purchase Details and Purchase Shipments
 *  Check form Matches on details then shipments or check settings to ensure
 *      that Purchases and Details 
 */
CREATE PROCEDURE [dbo].[sp_ValidatePurchaseOrders]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @RequirePurchaseOrder       AS BIT,
                @MatchByOrderNumberOnly     AS BIT,
                @CreateNewPurchaseOrders    AS BIT,
                @CreateNewPurchaseDetails   AS BIT,
                @MatchByProductAndLineOnly  AS BIT,
                @DefaultLineNumber          AS INT,
                @CreateNewPurchaseShipments AS BIT,
                @TargetDatabase             AS VARCHAR(100),
                @SourceTable                AS VARCHAR(100),
                @AllowStackingErrors        AS BIT,
                @ErrorCode                  AS INT;

        SET NOCOUNT ON;

        SET @RequirePurchaseOrder = 0;
        SET @MatchByOrderNumberOnly = 0;
        SET @CreateNewPurchaseOrders = 0;
        SET @CreateNewPurchaseDetails = 0;
        SET @MatchByProductAndLineOnly = 0;
        SET @DefaultLineNumber = -1;
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
            @RequirePurchaseOrder = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'RequirePurchaseOrder'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for RequirePurchaseOrder', 1;
            END

        SELECT
            @MatchByOrderNumberOnly = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'MatchByOrderNumberOnly'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for MatchByOrderNumberOnly', 1;
            END

        SELECT
            @CreateNewPurchaseOrders = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'CreateNewPurchaseOrders'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for CreateNewPurchaseOrders', 1;
            END

        SELECT
            @MatchByProductAndLineOnly = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'MatchByProductAndLineOnly'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for MatchByProductAndLineOnly', 1;
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

        SELECT @DefaultLineNumber = CAST(ConfigurationValue AS INT)
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultLineNumber'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for DefaultLineNumber', 1;
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


        PRINT N'Match the Inventory to existing Purchase Inventory';
        --SELECT TargetPurchaseInventory.PurchaseItemShipmentUID, TargetPurchaseInventory.InventoryUID, TargetPurchaseInventory.Tag, TargetPurchaseInventory.AssetID, TargetPurchaseInventory.ShippedToSiteUID,
        --       SourcePurchaseInventory.PurchaseItemShipmentUID, SourcePurchaseInventory.InventoryUID, SourcePurchaseItemDetails.PurchaseItemDetailUID, SourcePurchases.PurchaseUID
        UPDATE TargetPurchaseInventory
        SET TargetPurchaseInventory.PurchaseInventoryUID = SourcePurchaseInventory.PurchaseInventoryUID, TargetPurchaseInventory.PurchaseItemShipmentUID = SourcePurchaseItemShipments.PurchaseItemShipmentUID,
            TargetPurchaseInventory.PurchaseItemDetailUID = SourcePurchaseItemDetails.PurchaseItemDetailUID, TargetPurchaseInventory.PurchaseUID = SourcePurchases.PurchaseUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchaseInventory
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechPurchaseInventory SourcePurchaseInventory
            ON TargetPurchaseInventory.InventoryUID = SourcePurchaseInventory.InventoryUID
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechPurchaseItemShipments SourcePurchaseItemShipments
            ON SourcePurchaseInventory.PurchaseItemShipmentUID = SourcePurchaseItemShipments.PurchaseItemShipmentUID
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechPurchaseItemDetails SourcePurchaseItemDetails
            ON SourcePurchaseItemShipments.PurchaseItemDetailUID = SourcePurchaseItemDetails.PurchaseItemDetailUID
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechPurchases SourcePurchases
            ON SourcePurchaseItemDetails.PurchaseUID = SourcePurchases.PurchaseUID
        WHERE
            TargetPurchaseInventory.InventoryUID > 0
        --AND TargetPurchaseInventory.PurchaseItemShipmentUID > 0
        --AND TargetPurchaseInventory.PurchaseUID > 0
        AND TargetPurchaseInventory.ProcessTaskUID = @ProcessTaskUid
        AND (TargetPurchaseInventory.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Purchase Inventory by InventoryUID', 1;
            END

        PRINT N'Check RequirePurchaseOrder';

        IF @RequirePurchaseOrder = 1
            BEGIN
                PRINT N'Reject rows where the OrderNumber is empty';
                --Reject any rows where the OrderNumber is empty
                --SELECT TargetPurchase.OrderNumber, TargetPurchase.PurchaseDate, TargetPurchase.VendorUID, TargetPurchase.VendorName
                UPDATE TargetPurchase SET Rejected = 1, PurchaseUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: OrderNumber; OrderNumber cannot be Null or Empty'
                FROM 
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchase
                WHERE
                    TargetPurchase.PurchaseUID = 0
                AND (TargetPurchase.OrderNumber IS NULL OR
                     LTRIM(RTRIM(TargetPurchase.OrderNumber)) = '')
                AND TargetPurchase.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchase.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where the OrderNumber is Null or Empty', 1;
                    END
            END

        IF @MatchByOrderNumberOnly = 1
            BEGIN
                PRINT N'Validate Purchase Orders by Order Number Only';
                --Match by OrderNumber
                --SELECT TargetPurchase.OrderNumber, TargetPurchase.PurchaseDate, TargetPurchase.VendorUID, VendorName, SourcePurchase.PurchaseUID, SourcePurchase.OrderNumber, SourcePurchase.PurchaseDate, SourcePurchase.VendorUID
                UPDATE TargetPurchase SET TargetPurchase.PurchaseUID = SourcePurchase.PurchaseUID
                FROM 
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchase
                INNER JOIN 
                    TipWebHostedChicagoPS.dbo.tblTechPurchases SourcePurchase
                    ON UPPER(LTRIM(RTRIM(TargetPurchase.OrderNumber))) = UPPER(LTRIM(RTRIM(SourcePurchase.OrderNumber)))
                WHERE
                    TargetPurchase.PurchaseUID = 0
                AND TargetPurchase.OrderNumber IS NOT NULL
                AND LTRIM(RTRIM(TargetPurchase.OrderNumber)) <> ''
                AND TargetPurchase.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchase.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match Purchase Orders by Order Number only', 1;
                    END
            END
        ELSE
            BEGIN
                --TODO: Match by OrderNumber, PurchaseDate, 
                PRINT N'Match Purchase Orders by OrderNumber, PurchaseDate and Vendor';
                SELECT 0;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match Purchase Orders', 1;
                    END
            END

        PRINT N'Check Create New Purchase Orders';

        IF @CreateNewPurchaseOrders = 0
            BEGIN

                PRINT N'Reject rows where the OrderNumber could not be matched';
                --SELECT TargetPurchase.PurchaseUID, TargetPurchase.OrderNumber
                UPDATE TargetPurchase SET Rejected = 1, PurchaseUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: OrderNumber; OrderNumber could not be matched'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchase
                WHERE
                    TargetPurchase.PurchaseUID = 0
                AND TargetPurchase.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchase.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where the OrderNumber could not be matched', 1;
                    END

            END
        ELSE
            BEGIN

                PRINT N'Reject rows where PurchaseDate is NULL';
                --SELECT TargetPurchase.PurchaseUID, TargetPurchase.OrderNumber, TargetPurchase.PurchaseDate
                UPDATE TargetPurchase SET Rejected = 1, PurchaseUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: PurchaseDate; PurchaseDate is Null'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchase
                WHERE
                    TargetPurchase.PurchaseUID = 0
                AND TargetPurchase.PurchaseDate IS NULL
                AND TargetPurchase.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchase.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where the PurchaseDate is Null', 1;
                    END

            END

        PRINT N'Validate Details';

        IF @DefaultLineNumber >= 0
            BEGIN
                --Set all LineNumbers to the default where the Line Number is less than zero
                --SELECT TargetPurchaseDetails.PurchaseItemDetailUID, TargetPurchaseDetails.ItemUID, TargetPurchaseDetails.ProductName, TargetPurchaseDetails.LineNumber
                UPDATE TargetPurchaseDetails SET TargetPurchaseDetails.LineNumber = @DefaultLineNumber
                FROM 
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchaseDetails
                WHERE
                    TargetPurchaseDetails.PurchaseItemDetailUID = 0
                AND TargetPurchaseDetails.LineNumber < 0
                AND TargetPurchaseDetails.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchaseDetails.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to set the Default Line Number for Details', 1;
                    END

            END

        --Validate Details
        IF @MatchByProductAndLineOnly = 1
            BEGIN

                PRINT N'Lookup Detail by ItemUID and LineNumber';
                --SELECT TargetPurchaseDetails.PurchaseItemDetailUID, TargetPurchaseDetails.ItemUID, TargetPurchaseDetails.ProductName, TargetPurchaseDetails.LineNumber, 
                --       SourcePurchaseDetails.PurchaseItemDetailUID, SourcePurchaseDetails.ItemUID, SourcePurchaseDetails.LineNumber
                UPDATE TargetPurchaseDetails SET TargetPurchaseDetails.PurchaseItemDetailUID = SourcePurchaseDetails.PurchaseItemDetailUID
                FROM 
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchaseDetails
                INNER JOIN 
                    TipWebHostedChicagoPS.dbo.tblTechPurchaseItemDetails SourcePurchaseDetails
                    ON TargetPurchaseDetails.PurchaseUID = SourcePurchaseDetails.PurchaseUID
                    AND TargetPurchaseDetails.ItemUID = SourcePurchaseDetails.ItemUID
                    AND TargetPurchaseDetails.LineNumber = SourcePurchaseDetails.LineNumber
                WHERE
                    TargetPurchaseDetails.PurchaseUID > 0
                AND TargetPurchaseDetails.PurchaseItemDetailUID = 0
                AND TargetPurchaseDetails.ItemUID > 0
                AND TargetPurchaseDetails.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchaseDetails.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match Details by ItemUID and LineNumber', 1;
                    END

            END
        ELSE
            BEGIN

                PRINT N'Lookup Details by ALL Fields';
                --Lookup line details by PurchaseUid, ItemUid, FundingSourceUid, SiteAddedSiteUid, 
                --  DepartmentUid, Price, AccountCode (convert null to empty on both sides) and LineNumber
                --SELECT TargetPurchaseDetails.PurchaseItemDetailUID, TargetPurchaseDetails.ItemUID, TargetPurchaseDetails.ProductName, TargetPurchaseDetails.FundingSourceUID, 
                --       TargetPurchaseDetails.SiteAddedSiteUID, TargetPurchaseDetails.TechDepartmentUID, TargetPurchaseDetails.PurchasePrice, TargetPurchaseDetails.AccountCode, TargetPurchaseDetails.LineNumber, 
                --       SourcePurchaseDetails.PurchaseItemDetailUID, SourcePurchaseDetails.ItemUID, SourcePurchaseDetails.FundingSourceUID, SourcePurchaseDetails.SiteAddedSiteUID, 
                --       SourcePurchaseDetails.TechDepartmentUID, SourcePurchaseDetails.PurchasePrice, SourcePurchaseDetails.AccountCode, SourcePurchaseDetails.LineNumber
                UPDATE TargetPurchaseDetails SET TargetPurchaseDetails.PurchaseItemDetailUID = SourcePurchaseDetails.PurchaseItemDetailUID
                FROM 
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchaseDetails
                INNER JOIN 
                    TipWebHostedChicagoPS.dbo.tblTechPurchaseItemDetails SourcePurchaseDetails
                    ON TargetPurchaseDetails.PurchaseUID = SourcePurchaseDetails.PurchaseUID
                    AND TargetPurchaseDetails.ItemUID = SourcePurchaseDetails.ItemUID
                    AND TargetPurchaseDetails.FundingSourceUID = SourcePurchaseDetails.FundingSourceUID
                    AND TargetPurchaseDetails.SiteAddedSiteUID = SourcePurchaseDetails.SiteAddedSiteUID
                    AND TargetPurchaseDetails.TechDepartmentUID = SourcePurchaseDetails.TechDepartmentUID
                    AND TargetPurchaseDetails.PurchasePrice = SourcePurchaseDetails.PurchasePrice
                    AND UPPER(LTRIM(RTRIM(ISNULL(TargetPurchaseDetails.AccountCode, '')))) = UPPER(LTRIM(RTRIM(ISNULL(SourcePurchaseDetails.AccountCode, ''))))
                    AND TargetPurchaseDetails.LineNumber = SourcePurchaseDetails.LineNumber
                WHERE
                    TargetPurchaseDetails.PurchaseUID > 0
                AND TargetPurchaseDetails.PurchaseItemDetailUID = 0
                AND TargetPurchaseDetails.ItemUID > 0
                AND TargetPurchaseDetails.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchaseDetails.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match Details by ItemUID, FundingSourceUID, SiteAddedSiteUID, TechDepartmentUID, PurchasePrice, AccountCode and LineNumber', 1;
                    END

            END

        PRINT N'Check Create New Purchase Details';

        IF @CreateNewPurchaseDetails = 1
            BEGIN

                PRINT N'Reject rows where PurchasePrice is NULL';
                --SELECT TargetPurchaseDetails.OrderNumber, TargetPurchaseDetails.PurchaseDate, TargetPurchaseDetails.ItemUID, TargetPurchaseDetails.ProductName, TargetPurchaseDetails.PurchasePrice, TargetPurchaseDetails.LineNumber
                UPDATE TargetPurchaseDetails SET Rejected = 1, PurchaseItemDetailUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: PurchasePrice; PurchasePrice is Null'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchaseDetails
                WHERE
                    TargetPurchaseDetails.PurchaseItemDetailUID = 0
                --AND TargetPurchaseDetails.PurchaseUID = 0
                AND TargetPurchaseDetails.PurchasePrice IS NULL
                AND TargetPurchaseDetails.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchaseDetails.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where PurchasePrice is Null', 1;
                    END

            END
        ELSE
            BEGIN

                PRINT N'Reject where PurchaseDetail could not be matched';
                --If ItemDetailUID = 0 reject
                --SELECT TargetPurchaseDetails.OrderNumber, TargetPurchaseDetails.PurchaseDate, TargetPurchaseDetails.ItemUID, TargetPurchaseDetails.ProductName, TargetPurchaseDetails.PurchasePrice, TargetPurchaseDetails.LineNumber
                UPDATE TargetPurchaseDetails SET Rejected = 1, PurchaseItemDetailUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: PurchaseDetail; PurchaseDetail could not be matched'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchaseDetails
                WHERE
                    TargetPurchaseDetails.PurchaseUID > 0
                AND TargetPurchaseDetails.PurchaseItemDetailUID = 0
                AND TargetPurchaseDetails.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchaseDetails.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where the Purchase Detail could not be matched', 1;
                    END

            END

        PRINT N'Validate Shipments';
        --Match to existing Shipments by ShipToSite
        --SELECT TargetPurchaseShipments.PurchaseItemShipmentUID, TargetPurchaseShipments.ItemUID, TargetPurchaseShipments.ProductName, TargetPurchaseShipments.LineNumber, TargetPurchaseShipments.ShippedToSiteUID,
        --       SourcePurchaseShipments.PurchaseItemDetailUID, SourcePurchaseShipments.ShippedToSiteUID, SourcePurchaseShipments.InvoiceDate, 
        --       SourcePurchaseShipments.InvoiceNumber, SourcePurchaseShipments.QuantityShipped
        UPDATE TargetPurchaseShipments SET TargetPurchaseShipments.PurchaseItemShipmentUID = SourcePurchaseShipments.PurchaseItemShipmentUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchaseShipments
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechPurchaseItemShipments SourcePurchaseShipments
            ON TargetPurchaseShipments.PurchaseItemDetailUID = SourcePurchaseShipments.PurchaseItemDetailUID AND
               TargetPurchaseShipments.ShippedToSiteUID = SourcePurchaseShipments.ShippedToSiteUID
        WHERE
            TargetPurchaseShipments.PurchaseItemShipmentUID = 0
        AND TargetPurchaseShipments.PurchaseItemDetailUID > 0
        AND TargetPurchaseShipments.PurchaseUID > 0
        AND TargetPurchaseShipments.ProcessTaskUID = @ProcessTaskUid
        AND (TargetPurchaseShipments.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Shipments by ShipToSite', 1;
            END

        PRINT N'Check Create New Purchase Shipments';

        IF @CreateNewPurchaseShipments = 0
            BEGIN
                --Reject any unmatched Shipments
                PRINT N'Reject PurchaseShipments that could not be matched';
                --SELECT TargetPurchaseDetails.OrderNumber, TargetPurchaseDetails.PurchaseDate, TargetPurchaseDetails.ItemUID, TargetPurchaseDetails.ProductName, TargetPurchaseDetails.PurchasePrice, TargetPurchaseDetails.LineNumber
                UPDATE TargetPurchaseShipments SET Rejected = 1, PurchaseItemShipmentUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: PurchaseItemShipmentUID; PurchaseItemShipmentUID could not be matched'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchaseShipments
                WHERE
                    TargetPurchaseShipments.PurchaseUID > 0
                AND TargetPurchaseShipments.PurchaseItemDetailUID > 0
                AND TargetPurchaseShipments.PurchaseItemShipmentUID = 0
                AND TargetPurchaseShipments.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchaseShipments.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject records where the Shipment could not be matched', 1;
                    END

            END
        --ELSE
        --    BEGIN
        --    END

        SET NOCOUNT OFF;

        RETURN 0;

    END --End Procedure