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
                @TargetDatabase             AS VARCHAR(100),
                @SourceTable                AS VARCHAR(100),
                @AllowStackingErrors        AS BIT;

        SET @CreateNewPurchaseOrders = 0;
        SET @TargetDatabase = [dbo].[fn_GetTargetDatabaseName](@ProcessUid);
        SET @SourceTable = [dbo].[fn_GetSourceTable](@SourceProcess);
        
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
            @RequirePurchaseOrder = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'RequirePurchaseOrder'
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

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

        SELECT @DefaultLineNumber = CAST(ConfigurationValue AS INT)
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultLineNumber' AND ProcessUid = @ProcessUid
          AND Enabled = 1;

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

        IF @RequirePurchaseOrder = 1
            BEGIN
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
            END

        IF @MatchByOrderNumberOnly = 1
            BEGIN
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
            END
        ELSE
            BEGIN
                --TODO: Match by OrderNumber, PurchaseDate, 
                SELECT 0;
            END

        IF @CreateNewPurchaseOrders = 0
            BEGIN
                --Reject all unmatched Purchase Orders
                --SELECT TargetPurchase.PurchaseUID, TargetPurchase.OrderNumber
                UPDATE TargetPurchase SET Rejected = 1, PurchaseUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: OrderNumber; OrderNumber could not be matched'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchase
                WHERE
                    TargetPurchase.PurchaseUID = 0
                AND TargetPurchase.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchase.Rejected = 0 OR @AllowStackingErrors = 1);
            END
        ELSE
            BEGIN
                --If Purchase date is Null reject
                --SELECT TargetPurchase.PurchaseUID, TargetPurchase.OrderNumber, TargetPurchase.PurchaseDate
                UPDATE TargetPurchase SET Rejected = 1, PurchaseUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: PurchaseDate; PurchaseDate is Null'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchase
                WHERE
                    TargetPurchase.PurchaseDate IS NULL
                AND TargetPurchase.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchase.Rejected = 0 OR @AllowStackingErrors = 1);
            END

        --Validate Details
        IF @MatchByProductAndLineOnly = 1
            BEGIN
                --Match ItemDetails by ItemUID and LineNumber
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
                    TargetPurchaseDetails.PurchaseItemDetailUID = 0
                AND TargetPurchaseDetails.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchaseDetails.Rejected = 0 OR @AllowStackingErrors = 1);
            END
        ELSE
            BEGIN
                --Lookup line details by PurchaseUid, ItemUid, FundingSourceUid, SiteAddedSiteUid, 
                --  DepartmentUid, Price, AccountCode (convert null to empty on both sides) and LineNumber
                --SELECT TargetPurchaseDetails.PurchaseItemDetailUID, TargetPurchaseDetails.ItemUID, TargetPurchaseDetails.ProductName, TargetPurchaseDetails.FundingSourceUID, 
                --       TargetPurchaseDetails.SiteUID, TargetPurchaseDetails.TechDepartmentUID, TargetPurchaseDetails.PurchasePrice, TargetPurchaseDetails.AccountCode, TargetPurchaseDetails.LineNumber, 
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
                    AND TargetPurchaseDetails.SiteUID = SourcePurchaseDetails.SiteAddedSiteUID
                    AND TargetPurchaseDetails.TechDepartmentUID = SourcePurchaseDetails.TechDepartmentUID
                    AND TargetPurchaseDetails.PurchasePrice = SourcePurchaseDetails.PurchasePrice
                    AND UPPER(LTRIM(RTRIM(ISNULL(TargetPurchaseDetails.AccountCode, '')))) = UPPER(LTRIM(RTRIM(ISNULL(SourcePurchaseDetails.AccountCode, ''))))
                    AND TargetPurchaseDetails.LineNumber = SourcePurchaseDetails.LineNumber
                WHERE
                    TargetPurchaseDetails.PurchaseItemDetailUID = 0
                AND TargetPurchaseDetails.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchaseDetails.Rejected = 0 OR @AllowStackingErrors = 1);
            END

        If @CreateNewPurchaseDetails = 1
            BEGIN
                --If Purchase Price is NULL reject
                --SELECT TargetPurchaseDetails.OrderNumber, TargetPurchaseDetails.PurchaseDate, TargetPurchaseDetails.ItemUID, TargetPurchaseDetails.ProductName, TargetPurchaseDetails.PurchasePrice, TargetPurchaseDetails.LineNumber
                UPDATE TargetPurchaseDetails SET Rejected = 1, PurchaseItemDetailUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: PurchasePrice; PurchasePrice is Null'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchaseDetails
                WHERE
                    TargetPurchaseDetails.PurchaseUID = 0
                AND TargetPurchaseDetails.PurchaseItemDetailUID = 0
                AND TargetPurchaseDetails.PurchasePrice IS NULL
                AND TargetPurchaseDetails.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchaseDetails.Rejected = 0 OR @AllowStackingErrors = 1);
            END
        ELSE
            BEGIN
                --If ItemDetailUID = 0 reject
                --SELECT TargetPurchaseDetails.OrderNumber, TargetPurchaseDetails.PurchaseDate, TargetPurchaseDetails.ItemUID, TargetPurchaseDetails.ProductName, TargetPurchaseDetails.PurchasePrice, TargetPurchaseDetails.LineNumber
                UPDATE TargetPurchaseDetails SET Rejected = 1, PurchaseItemDetailUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: PurchaseDetail; PurchaseDetail could not be matched'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchaseDetails
                WHERE
                    TargetPurchaseDetails.PurchaseItemDetailUID = 0
                AND TargetPurchaseDetails.ProcessTaskUID = @ProcessTaskUid
                AND (TargetPurchaseDetails.Rejected = 0 OR @AllowStackingErrors = 1);
            END

        --Validate Shipments
        --Match to existing Shipments by ShipToSite
        --SELECT TargetPurchaseShipments.PurchaseItemShipmentUID, TargetPurchaseShipments.ItemUID, TargetPurchaseShipments.ProductName, TargetPurchaseShipments.LineNumber, TargetPurchaseShipments.SiteUID,
        --       SourcePurchaseShipments.PurchaseItemDetailUID, SourcePurchaseShipments.ShippedToSiteUID, SourcePurchaseShipments.InvoiceDate, 
        --       SourcePurchaseShipments.InvoiceNumber, SourcePurchaseShipments.QuantityShipped
        UPDATE TargetPurchaseShipments SET TargetPurchaseShipments.PurchaseItemShipmentUID = SourcePurchaseShipments.PurchaseItemShipmentUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetPurchaseShipments
        INNER JOIN 
            TipWebHostedChicagoPS.dbo.tblTechPurchaseItemShipments SourcePurchaseShipments
            ON TargetPurchaseShipments.SiteUID = SourcePurchaseShipments.ShippedToSiteUID
        WHERE
            TargetPurchaseShipments.PurchaseItemShipmentUID = 0
        AND TargetPurchaseShipments.PurchaseItemDetailUID <> 0
        AND TargetPurchaseShipments.PurchaseUID <> 0
        AND TargetPurchaseShipments.ProcessTaskUID = @ProcessTaskUid
        AND (TargetPurchaseShipments.Rejected = 0 OR @AllowStackingErrors = 1);

    END --End Procedure