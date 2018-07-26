/*
 *  sp_ValidateTags
 *  Perform Tag Validation on rows in ETLInventory. The Target database and
 *      UseTagInNotesValidation values are extracted from the Configuration 
 *      of the specified ProcessUid.
 *  Reject sets InventoryUID to -1 and Rejected to true, add Reject Notes
 */
CREATE PROCEDURE [dbo].[sp_ValidateTags]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @UseTagInNotesValidation    AS BIT,
                @TargetDatabase             AS VARCHAR(100),
                @SourceTable                AS VARCHAR(100),
                @AllowStackingErrors        AS BIT;

        --Set a default starting values
        SET @UseTagInNotesValidation = 0;
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
            @UseTagInNotesValidation = (
                CASE 
                    WHEN UPPER(LTRIM(RTRIM(ConfigurationValue))) = 'TRUE' OR LTRIM(RTRIM(ConfigurationValue)) = '1' THEN 1
                    ELSE 0
                END)
        FROM [Configurations]
        WHERE ConfigurationName = 'UseTagInNotesValidation' 
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

        --SELECT
        --    @TargetDatabase = UPPER(LTRIM(RTRIM(ConfigurationValue))) 
        --FROM [Configurations]
        --WHERE ConfigurationName = 'TIPWebConnection' 
        --  AND ProcessUid = @ProcessUid
        --  AND Enabled = 1;

        --SET @FirstIndex = CHARINDEX(';Database=', @TargetDatabase, 0);

        ----Check that FirstIndex is NOT less than or equal to zero
        --IF @FirstIndex <= 0
        --    BEGIN
        --        ;
        --        THROW 50000, 'Target Database Name cannot be retrieved.', 1;
        --    END
        --ELSE
        --    BEGIN
        --        --Increase the index by the lenght of the search string
        --        SET @FirstIndex = @FirstIndex + 10;
        --    END

        --SET @TargetDatabase = LTRIM(RTRIM(SUBSTRING(@TargetDatabase, @FirstIndex, CHARINDEX(';', @TargetDatabase, @FirstIndex) - @FirstIndex)));

        --Get Matches by Tag
        --SELECT TargetInventory.Tag, SourceInventory.Tag, SourceInventory.InventoryUID
        UPDATE TargetInventory SET TargetInventory.InventoryUID = SourceInventory.InventoryUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        LEFT JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
            ON UPPER(LTRIM(RTRIM(TargetInventory.Tag))) = UPPER(LTRIM(RTRIM(SourceInventory.Tag)))
        WHERE 
            SourceInventory.Tag IS NOT NULL
        AND TargetInventory.InventoryUID = 0
        AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
        AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

        --Get Matches by AssetUID (Single match only)
        --SELECT TargetInventory.Tag, TargetInventory.AssetID, SourceInventory.Tag, SourceInventory.AssetID, SourceInventory.InventoryUID
        UPDATE TargetInventory SET TargetInventory.InventoryUID = SourceInventory.InventoryUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        LEFT JOIN (
            SELECT
                SourceInventory.Tag, SourceInventory.AssetID, SourceInventory.InventoryUID
            FROM (
                SELECT
                    SourceInventory.AssetID
                FROM 
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
                LEFT JOIN
                    TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                    ON UPPER(LTRIM(RTRIM(TargetInventory.AssetID))) = UPPER(LTRIM(RTRIM(SourceInventory.AssetID)))
                WHERE 
                    SourceInventory.AssetID IS NOT NULL
                AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
                AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1)
                GROUP BY 
                    SourceInventory.AssetID 
                HAVING 
                    COUNT(SourceInventory.InventoryUID) = 1
                ) UniqueAssets
            LEFT JOIN
                TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                ON UniqueAssets.AssetID = SourceInventory.AssetID
            WHERE
                SourceInventory.AssetID IS NOT NULL
            ) AS SourceInventory
            ON UPPER(LTRIM(RTRIM(TargetInventory.AssetID))) = UPPER(LTRIM(RTRIM(SourceInventory.AssetID)))
        WHERE 
            SourceInventory.AssetID IS NOT NULL
        AND TargetInventory.InventoryUID = 0
        AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
        AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

        --Get Matches by Serial (Single match only)
        --SELECT TargetInventory.Tag, TargetInventory.AssetID, SourceInventory.Tag, SourceInventory.Serial, SourceInventory.InventoryUID
        UPDATE TargetInventory SET TargetInventory.InventoryUID = SourceInventory.InventoryUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        LEFT JOIN (
            SELECT
                SourceInventory.Tag, SourceInventory.Serial, SourceInventory.InventoryUID
            FROM (
                SELECT
                    SourceInventory.Serial--, COUNT(SourceInventory.InventoryUID)
                FROM 
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
                LEFT JOIN
                    TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                    ON UPPER(LTRIM(RTRIM(TargetInventory.Serial))) = UPPER(LTRIM(RTRIM(SourceInventory.Serial)))
                WHERE 
                    SourceInventory.Serial IS NOT NULL
                AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
                AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1)
                GROUP BY 
                    SourceInventory.Serial 
                HAVING 
                    COUNT(SourceInventory.InventoryUID) = 1
                ) UniqueSerials
            LEFT JOIN 
                TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                ON UniqueSerials.Serial = SourceInventory.Serial
            WHERE
                UniqueSerials.Serial IS NOT NULL
             AND SourceInventory.Serial IS NOT NULL
            ) AS SourceInventory
            ON UPPER(LTRIM(RTRIM(TargetInventory.Serial))) = UPPER(LTRIM(RTRIM(SourceInventory.Serial)))
        WHERE 
            SourceInventory.Serial IS NOT NULL
        AND TargetInventory.InventoryUID = 0
        AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
        AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

        --Get Matches by Tag In Notes (Single match only)
        IF @UseTagInNotesValidation = 1
            BEGIN

                IF EXISTS(SELECT 1 FROM tempdb.sys.tables WHERE name LIKE '#Tags%')
                    BEGIN
                        TRUNCATE TABLE #Tags
                    END
                ELSE
                    BEGIN
                        CREATE TABLE #Tags (RowNum INT, Tag VARCHAR(50), CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED ( RowNum ASC ));
                    END

                IF EXISTS(SELECT 1 FROM tempdb.sys.tables WHERE name LIKE '#ContainsTag%')
                    BEGIN
                        TRUNCATE TABLE #ContainsTag
                    END
                ELSE
                    BEGIN
                        CREATE TABLE #ContainsTag (InventoryUID INT , Tag VARCHAR(50), CONSTRAINT [PK_ContainsTag] PRIMARY KEY CLUSTERED ( InventoryUID ASC ));--, InventoryNotes VARCHAR(3000))
                    END

                DECLARE @RowCount AS INT,
                        @RowTotal AS INT,
                        @TagSearch AS VARCHAR(50);

                INSERT INTO #Tags
                SELECT
                    ROW_NUMBER() OVER(ORDER BY Tag), 
                    TargetInventory.Tag 
                FROM 
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
                WHERE
                    TargetInventory.InventoryUID = 0
                AND TargetInventory.ProcessTaskUid = @ProcessTaskUid
                AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

                SET @RowTotal = (SELECT COUNT(Tag) FROM #Tags);
                SET @RowCount = 1;

                --Load all Tag to InventoryNotes matches in the table
                WHILE @RowCount <= @RowTotal
                    BEGIN
                        SET @TagSearch = 'Tag: ' + (SELECT UPPER(LTRIM(RTRIM(Tag))) FROM #Tags WHERE RowNum = @RowCount) ;

                        INSERT INTO #ContainsTag
                        SELECT
                            SourceInventory.InventoryUID, @TagSearch
                        FROM
                            TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                        WHERE
                            SourceInventory.InventoryNotes IS NOT NULL
                        AND UPPER(LTRIM(RTRIM(SourceInventory.InventoryNotes))) LIKE '%' + @TagSearch + '%';
                        --AND CONTAINS(SourceInventory.InventoryNotes, @TagSearch);

                        SET @RowCount = @RowCount + 1;

                    END

                --Get the InventoryUID of tags that have only 1 match
                --SELECT TargetInventory.Tag, TargetInventory.AssetID, SourceInventory.Tag, SourceInventory.InventoryUID
                UPDATE TargetInventory SET TargetInventory.InventoryUID = SourceInventory.InventoryUID
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
                LEFT JOIN (
                    SELECT
                        UniqueTagInNotes.InventoryUID, UniqueTagInNotes.Tag
                    FROM (
                        SELECT
                            UniqueTagInNotes.Tag
                        FROM
                            #ContainsTag UniqueTagInNotes
                        GROUP BY 
                            Tag
                        HAVING 
                            COUNT(InventoryUID) = 1
                        ) UniqueTags
                    INNER JOIN
                        #ContainsTag UniqueTagInNotes
                        ON UniqueTags.Tag = UniqueTagInNotes.Tag
                    ) SourceInventory
                    ON UPPER(LTRIM(RTRIM(TargetInventory.Tag))) = SourceInventory.Tag
                WHERE 
                    TargetInventory.InventoryUID = 0
                AND SourceInventory.Tag IS NOT NULL
                AND TargetInventory.ProcessTaskUid = @ProcessTaskUid
                AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

                --DROP TABLE #Tags;
                --DROP TABLE #ContainsTag;

            END

        --Reject where Tag is NULL or Empty
        --SELECT RowID, Tag
        UPDATE TargetInventory SET Rejected = 1, InventoryUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: Tag; Tag is NULL or Empty'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        WHERE 
            TargetInventory.InventoryUID = 0
        AND (TargetInventory.Tag IS NULL OR
             LTRIM(RTRIM(TargetInventory.Tag)) = '')
        AND TargetInventory.ProcessTaskUid = @ProcessTaskUid
        AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

    END --End Procedure