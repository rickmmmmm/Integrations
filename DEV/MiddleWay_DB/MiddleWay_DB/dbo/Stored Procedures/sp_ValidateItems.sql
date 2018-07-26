/*
 *  sp_ValidateItems
 *  Perform Product searches by Product Name, Product Description and Product By Number
 *  Get the highest ItemUID for the matching product if multiples
 *  If the CreateProducts configuration is not set Reject 
 *  Reject sets ItemUID to -1 and Rejected to true, add Reject Notes
 */
CREATE PROCEDURE [dbo].[sp_ValidateItems]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @CreateProducts         AS BIT,
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @AllowStackingErrors    AS BIT;

        --Set a default starting value
        SET @CreateProducts = 0;
        SET @TargetDatabase = [dbo].[fn_GetTargetDatabaseName](@ProcessUid);
        SET @SourceTable = [dbo].[fn_GetSourceTable](@SourceProcess);

        --Check that Target Database is not null or empty
        IF @TargetDatabase IS NULL OR LEN(@TargetDatabase) = 0
            BEGIN
                ;
                THROW 50000, 'Target Database Name is empty.', 1;
            END

        --Check that Source Table is not null or empty
        IF @SourceTable IS NULL OR LEN(@SourceTable) = 0
            BEGIN
                ;
                THROW 50000, 'Source Table could not be verified.', 1;
            END

        SELECT
            @CreateProducts = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations] 
        WHERE ConfigurationName = 'CreateProducts'
          AND ProcessUid = @ProcessUid
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

        -- Match the Item by Name
        --SELECT TargetItem.ItemUID, TargetItem.ProductName, SourceItem.ItemName, SourceItem.ItemUID
        UPDATE TargetItem SET TargetItem.ItemUID = SourceItem.ItemUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
        INNER JOIN (
            SELECT
                SourceItem.ItemName, MAX(SourceItem.ItemUID) ItemUID
            FROM 
                IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
            LEFT JOIN
                TipWebHostedChicagoPS.dbo.tblTechItems SourceItem
                ON UPPER(LTRIM(RTRIM(TargetItem.ProductName))) = UPPER(LTRIM(RTRIM(SourceItem.ItemName)))
            WHERE
                SourceItem.ItemName IS NOT NULL
            AND TargetItem.ProductName IS NOT NULL
            AND TargetItem.ProcessTaskUID = @ProcessTaskUid
            AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1)
            GROUP BY
                SourceItem.ItemName
            ) SourceItem
            ON UPPER(LTRIM(RTRIM(TargetItem.ProductName))) = UPPER(LTRIM(RTRIM(SourceItem.ItemName)))
        WHERE 
            SourceItem.ItemName IS NOT NULL
        AND TargetItem.ProductName IS NOT NULL
        AND TargetItem.ItemUID = 0
        AND TargetItem.ProcessTaskUID = @ProcessTaskUid
        AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1);

        -- Match the Item by Description
        --SELECT TargetItem.ProductName, TargetItem.ProductDescription, SourceItem.ItemDescription, SourceItem.ItemUID
        UPDATE TargetItem SET TargetItem.ItemUID = SourceItem.ItemUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
        INNER JOIN (
            SELECT
                SourceItem.ItemDescription, MAX(SourceItem.ItemUID) ItemUID
            FROM 
                IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
            LEFT JOIN
                TipWebHostedChicagoPS.dbo.tblTechItems SourceItem
                ON UPPER(LTRIM(RTRIM(TargetItem.ProductDescription))) = UPPER(LTRIM(RTRIM(SourceItem.ItemDescription)))
            WHERE
                SourceItem.ItemDescription IS NOT NULL
            AND TargetItem.ProductDescription IS NOT NULL
            AND TargetItem.ProcessTaskUID = @ProcessTaskUid
            AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1)
            GROUP BY
                SourceItem.ItemDescription
            ) SourceItem
            ON UPPER(LTRIM(RTRIM(TargetItem.ProductDescription))) = UPPER(LTRIM(RTRIM(SourceItem.ItemDescription)))
        WHERE
            SourceItem.ItemDescription IS NOT NULL
        AND TargetItem.ProductDescription IS NOT NULL
        AND TargetItem.ItemUID = 0
        AND TargetItem.ProcessTaskUID = @ProcessTaskUid
        AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1);

        -- Match the Item by ItemNumber
        --SELECT TargetItem.ProductName, TargetItem.ProductByNumber, SourceItem.ItemNumber, SourceItem.ItemUID
        UPDATE TargetItem SET TargetItem.ItemUID = SourceItem.ItemUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
        INNER JOIN (
            SELECT
                SourceItem.ItemNumber, MAX(SourceItem.ItemUID) ItemUID
            FROM 
                IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
            LEFT JOIN
                TipWebHostedChicagoPS.dbo.tblTechItems SourceItem
                ON UPPER(LTRIM(RTRIM(TargetItem.ProductByNumber))) = UPPER(LTRIM(RTRIM(SourceItem.ItemNumber)))
            WHERE
                SourceItem.ItemNumber IS NOT NULL
            AND TargetItem.ProductByNumber IS NOT NULL
            AND TargetItem.ProcessTaskUID = @ProcessTaskUid
            AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1)
            GROUP BY
                SourceItem.ItemNumber
            ) SourceItem
            ON UPPER(LTRIM(RTRIM(TargetItem.ProductByNumber))) = UPPER(LTRIM(RTRIM(SourceItem.ItemNumber)))
        WHERE
            SourceItem.ItemNumber IS NOT NULL
        AND TargetItem.ProductByNumber IS NOT NULL
        AND TargetItem.ItemUID = 0
        AND TargetItem.ProcessTaskUID = @ProcessTaskUid
        AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1);

        --Reject All Products Not matched where Product Name is NULL
        UPDATE TargetItem SET Rejected = 1, ItemUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: ProductName; ProductName is NULL or Empty'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
        WHERE 
            TargetItem.ItemUID = 0
        AND (TargetItem.ProductName IS NULL OR
                LTRIM(RTRIM(TargetItem.ProductName)) = '')
        AND TargetItem.ProcessTaskUID = @ProcessTaskUid
        AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @CreateProducts = 0
            BEGIN
                --Reject All Products Not matched
                UPDATE TargetItem SET Rejected = 1, ItemUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: ProductName; ProductName could be matched'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetItem
                WHERE 
                    TargetItem.ItemUID = 0
                AND TargetItem.ProcessTaskUID = @ProcessTaskUid
                AND (TargetItem.Rejected = 0 OR @AllowStackingErrors = 1);
            END

    END --End Procedure