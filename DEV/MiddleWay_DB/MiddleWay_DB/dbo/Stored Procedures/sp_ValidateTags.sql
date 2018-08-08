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
                @AllowStackingErrors        AS BIT,
                @ErrorCode                  AS INT;

        SET NOCOUNT ON;

        --Set a default starting values
        SET @UseTagInNotesValidation = 0;
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
            END

        --Check that Source Table is not null or empty
        IF @SourceTable IS NULL OR LEN(@SourceTable) = 0
            BEGIN
                ;
                THROW 100000, 'Source Table could not be verified.', 1;
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

          IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for UseTagInNotesValidation', 1;
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

        PRINT N'Reject where Tag is NULL or Empty';
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

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the Tag is Null or Empty', 1;
            END

        PRINT N'Reject Duplicate Tags in the Data';
        --SELECT TargetInventory.Tag, TargetInventory.AssetID, DuplicateTags.Repeats
        UPDATE TargetInventory
        SET Rejected = 1, InventoryUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN '' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: Tag; Tag is repeated ' + CAST(DuplicateTags.Repeats AS VARCHAR) + N' times in the source data'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        INNER JOIN (
            SELECT
                UPPER(LTRIM(RTRIM(TargetInventory.Tag))) Tag,
                COUNT(RowID) AS Repeats
            FROM
                IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
            WHERE
                TargetInventory.Tag IS NOT NULL
            AND UPPER(LTRIM(RTRIM(TargetInventory.Tag))) <> ''
            AND TargetInventory.InventoryUID = 0
            AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
            AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1)
            GROUP BY
                UPPER(LTRIM(RTRIM(TargetInventory.Tag)))
            HAVING
                COUNT(UPPER(LTRIM(RTRIM(TargetInventory.Tag)))) > 1
            ) DuplicateTags
            ON UPPER(LTRIM(RTRIM(TargetInventory.Tag))) = DuplicateTags.Tag
        WHERE
            TargetInventory.InventoryUID = 0
        AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
        AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the Tag is duplicated', 1;
            END

        PRINT N'Get Matches by AssetUID (Single match only)';
        --SELECT TargetInventory.Tag, TargetInventory.AssetID, SourceInventory.Tag, SourceInventory.AssetID, SourceInventory.InventoryUID
        UPDATE TargetInventory SET TargetInventory.InventoryUID = SourceInventory.InventoryUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        INNER JOIN (
            SELECT
                SourceInventory.Tag, SourceInventory.AssetID, SourceInventory.InventoryUID
            FROM (
                SELECT
                    SourceInventory.AssetID
                FROM 
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
                INNER JOIN
                    TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                    ON UPPER(LTRIM(RTRIM(TargetInventory.AssetID))) = UPPER(LTRIM(RTRIM(SourceInventory.AssetID)))
                WHERE
                    TargetInventory.InventoryUID = 0
                AND TargetInventory.AssetID IS NOT NULL
                AND LTRIM(RTRIM(TargetInventory.AssetID)) <> ''
                AND SourceInventory.AssetID IS NOT NULL
                AND LTRIM(RTRIM(SourceInventory.AssetID)) <> ''
                AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
                AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1)
                GROUP BY
                    SourceInventory.AssetID
                HAVING
                    COUNT(SourceInventory.InventoryUID) = 1
                ) UniqueAssets
            INNER JOIN
                TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                ON UniqueAssets.AssetID = SourceInventory.AssetID
            WHERE
                SourceInventory.AssetID IS NOT NULL
            ) AS SourceInventory
            ON UPPER(LTRIM(RTRIM(TargetInventory.AssetID))) = UPPER(LTRIM(RTRIM(SourceInventory.AssetID)))
        WHERE
            TargetInventory.AssetID IS NOT NULL
        AND LTRIM(RTRIM(TargetInventory.AssetID)) <> ''
        AND TargetInventory.InventoryUID = 0
        AND SourceInventory.AssetID IS NOT NULL
        AND LTRIM(RTRIM(SourceInventory.AssetID)) <> ''
        AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
        AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Inventory by AssetUID', 1;
            END

        PRINT N'Get Matches by Tag';
        --SELECT TargetInventory.Tag, SourceInventory.Tag, SourceInventory.InventoryUID
        UPDATE TargetInventory SET TargetInventory.InventoryUID = SourceInventory.InventoryUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
            ON UPPER(LTRIM(RTRIM(TargetInventory.Tag))) = UPPER(LTRIM(RTRIM(SourceInventory.Tag)))
        WHERE
            TargetInventory.InventoryUID = 0
        AND TargetInventory.Tag IS NOT NULL
        AND LTRIM(RTRIM(TargetInventory.Tag)) <> ''
        AND (SourceInventory.AssetID IS NULL OR
             LTRIM(RTRIM(SourceInventory.AssetID)) = '')
        AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
        AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Inventory by Tag', 1;
            END

        PRINT N'Get Matches by Serial (Single match only)';
        --SELECT TargetInventory.Tag, TargetInventory.AssetID, SourceInventory.Tag, SourceInventory.Serial, SourceInventory.InventoryUID
        UPDATE TargetInventory SET TargetInventory.InventoryUID = SourceInventory.InventoryUID
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        INNER JOIN (
            SELECT
                UPPER(LTRIM(RTRIM(TargetInventory.Serial))) Serial
            FROM
                IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
            WHERE
                TargetInventory.InventoryUID = 0
            AND TargetInventory.Serial IS NOT NULL
            AND LTRIM(RTRIM(TargetInventory.Serial)) <> ''
            AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
            AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1)
            GROUP BY
                UPPER(LTRIM(RTRIM(TargetInventory.Serial)))
            HAVING
                COUNT(UPPER(LTRIM(RTRIM(TargetInventory.Serial)))) = 1
            ) UniqueTagsBySerial
            ON UPPER(LTRIM(RTRIM(TargetInventory.Serial))) = UniqueTagsBySerial.Serial
        INNER JOIN (
            SELECT
                SourceInventory.InventoryUID, UPPER(LTRIM(RTRIM(SourceInventory.Tag))) Tag, UPPER(LTRIM(RTRIM(SourceInventory.Serial))) Serial
            FROM (
                SELECT
                    UPPER(LTRIM(RTRIM(SourceInventory.Serial))) Serial
                FROM
                    TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                WHERE 
                    SourceInventory.Serial IS NOT NULL
                AND LTRIM(RTRIM(SourceInventory.Serial)) <> ''
                AND (SourceInventory.AssetID IS NULL OR
                     LTRIM(RTRIM(SourceInventory.AssetID)) = '')
                GROUP BY
                    SourceInventory.Serial
                HAVING
                    COUNT(SourceInventory.InventoryUID) = 1
                ) UniqueSerials
            INNER JOIN
                TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                ON UniqueSerials.Serial = UPPER(LTRIM(RTRIM(SourceInventory.Serial)))
            WHERE
                SourceInventory.Serial IS NOT NULL
            AND LTRIM(RTRIM(SourceInventory.Serial)) <> ''
            AND (SourceInventory.AssetID IS NULL OR
                 LTRIM(RTRIM(SourceInventory.AssetID)) = '')
            ) AS SourceInventory
            ON UPPER(LTRIM(RTRIM(TargetInventory.Serial))) = SourceInventory.Serial
        WHERE
            SourceInventory.Serial IS NOT NULL
        AND LTRIM(RTRIM(SourceInventory.Serial)) <> ''
        AND TargetInventory.InventoryUID = 0
        AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
        AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Inventory by Serial', 1;
            END

        PRINT N'Check Use Tag In Notes';
        --Get Matches by Tag In Notes (Single match only)
        IF @UseTagInNotesValidation = 1
            BEGIN
                PRINT N'Checking for the Tag in the Tag notes';

                IF EXISTS(SELECT 1 FROM tempdb.sys.tables WHERE name LIKE '#Tags%')
                    BEGIN
                        TRUNCATE TABLE #Tags;
                    END
                ELSE
                    BEGIN
                        CREATE TABLE #Tags (RowNum INT, Tag VARCHAR(50), CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED ( RowNum ASC ));
                    END

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to create the temporary table #Tags', 1;
                    END

                IF EXISTS(SELECT 1 FROM tempdb.sys.tables WHERE name LIKE '#ContainsTag%')
                    BEGIN
                        TRUNCATE TABLE #ContainsTag;
                    END
                ELSE
                    BEGIN
                        --CREATE TABLE #ContainsTag (InventoryUID INT , Tag VARCHAR(50), CONSTRAINT [PK_ContainsTag] PRIMARY KEY CLUSTERED ( InventoryUID ASC ));--, InventoryNotes VARCHAR(3000));
                        CREATE TABLE #ContainsTag (InventoryUID INT , Tag VARCHAR(50), LookupTag VARCHAR(50), CONSTRAINT [PK_ContainsTag] PRIMARY KEY CLUSTERED ( LookupTag ASC ));--, InventoryNotes VARCHAR(3000));
                    END

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to create the temporary table #ContainsTag', 1;
                    END

                DECLARE @RowCount AS INT,
                        @RowTotal AS INT,
                        @TagSearch AS VARCHAR(50);

                INSERT INTO #Tags
                SELECT --TOP 100
                    ROW_NUMBER() OVER(ORDER BY Tag),
                    UPPER(LTRIM(RTRIM(TargetInventory.Tag))) Tag
                FROM 
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
                WHERE
                    TargetInventory.InventoryUID = 0
                AND TargetInventory.Tag IS NOT NULL
                AND LTRIM(RTRIM(TargetInventory.Tag)) <> ''
                AND TargetInventory.ProcessTaskUid = @ProcessTaskUid
                AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1)
                GROUP BY
                    TargetInventory.Tag;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to add Tags to match in TagNotes to the #Tags table', 1;
                    END

                SET @RowTotal = (SELECT COUNT(Tag) FROM #Tags);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to get the Total Tags to match in TagNotes', 1;
                    END

                SET @RowCount = 1;

                --Load all Tag to InventoryNotes matches in the table
                WHILE @RowCount <= @RowTotal
                    BEGIN
                        SET @TagSearch = (SELECT UPPER(LTRIM(RTRIM(Tag))) FROM #Tags WHERE RowNum = @RowCount);

                        IF @@ERROR <> 0
                            BEGIN
                                SET @ErrorCode = @@ERROR + 100000;
                                --SET @ErrorMessage = ;
                                --RETURN @ErrorCode;
                                THROW @ErrorCode, 'Failed to get the next Tag to search', 1;
                            END

                        INSERT INTO #ContainsTag
                        SELECT
                            SourceInventory.InventoryUID, UPPER(LTRIM(RTRIM(SourceInventory.Tag))), @TagSearch
                        FROM
                            TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                        WHERE
                            SourceInventory.InventoryNotes IS NOT NULL
                        AND UPPER(LTRIM(RTRIM(SourceInventory.InventoryNotes))) LIKE '%TAG: ' + @TagSearch + '%'
                        AND (SourceInventory.AssetID IS NULL OR
                             LTRIM(RTRIM(SourceInventory.AssetID)) = '');
                        --AND CONTAINS(SourceInventory.InventoryNotes, @TagSearch);

                        IF @@ERROR <> 0
                            BEGIN
                                SET @ErrorCode = @@ERROR + 100000;
                                --SET @ErrorMessage = ;
                                --RETURN @ErrorCode;
                                THROW @ErrorCode, 'Failed to find Tags in the TagNotes', 1;
                            END

                        SET @RowCount = @RowCount + 1;

                    END

                PRINT N'Get the InventoryUID of tags that have only 1 match';
                --SELECT TargetInventory.Tag, TargetInventory.AssetID, SourceInventory.Tag, SourceInventory.InventoryUID
                UPDATE TargetInventory SET TargetInventory.InventoryUID = SourceInventory.InventoryUID
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
                LEFT JOIN (
                    SELECT
                        UniqueTagInNotes.InventoryUID, UniqueTagInNotes.Tag, UniqueTagInNotes.LookupTag
                    FROM (
                        SELECT
                            UniqueTagInNotes.InventoryUID
                        FROM (
                            SELECT
                                UniqueTagInNotes.LookupTag
                            FROM
                                #ContainsTag UniqueTagInNotes
                            GROUP BY
                                UniqueTagInNotes.LookupTag
                            HAVING 
                                COUNT(InventoryUID) = 1 --Return Only lookup tags that have a single inventory match
                            ) UniqueTags
                        INNER JOIN
                            #ContainsTag UniqueTagInNotes
                            ON UniqueTags.LookupTag = UniqueTagInNotes.LookupTag
                        GROUP BY
                            UniqueTagInNotes.InventoryUID
                        HAVING
                            COUNT(UniqueTagInNotes.Tag) = 1 --Return only inventory that has a single LookupTag
                        ) UniqueInventory
                    INNER JOIN
                        #ContainsTag UniqueTagInNotes
                        ON UniqueInventory.InventoryUID = UniqueTagInNotes.InventoryUID
                    ) SourceInventory
                    ON UPPER(LTRIM(RTRIM(TargetInventory.Tag))) = SourceInventory.Tag
                WHERE 
                    TargetInventory.InventoryUID = 0
                AND SourceInventory.InventoryUID IS NOT NULL
                AND SourceInventory.Tag IS NOT NULL
                AND LTRIM(RTRIM(SourceInventory.Tag)) <> ''
                AND TargetInventory.ProcessTaskUid = @ProcessTaskUid
                AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to match Inventory that has a single match in TagNotes', 1;
                    END

                DROP TABLE #Tags;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to Drop the #Tags table', 1;
                    END

                DROP TABLE #ContainsTag;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to Drop the #ContainsTag table', 1;
                    END

            END

        PRINT N'Reject Tags that have a duplicate InventoryUID match';
        --SELECT RowID, Tag
        UPDATE TargetInventory SET Rejected = 1, InventoryUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: Asset/Tag; The Asset/Tag search found multiple records match the same Inventory'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        INNER JOIN (
            SELECT
                DuplicateInventory.InventoryUID
            FROM
                IntegrationMiddleWay.dbo._ETL_Inventory DuplicateInventory
            WHERE
                DuplicateInventory.InventoryUID > 0
                AND DuplicateInventory.ProcessTaskUid = @ProcessTaskUid
                AND (DuplicateInventory.Rejected = 0 OR @AllowStackingErrors = 1)
            GROUP BY
                DuplicateInventory.InventoryUID
            HAVING
                COUNT(DuplicateInventory.Tag) > 1
            ) DuplicateInventory
            ON TargetInventory.InventoryUID = DuplicateInventory.InventoryUID
        WHERE
            TargetInventory.InventoryUID > 0
        AND TargetInventory.ProcessTaskUid = @ProcessTaskUid
        AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records that have multiple matches to the same Inventory', 1;
            END

        PRINT N'Reject where Tag matches but AssetUID does not';
        --SELECT RowID, Tag
        UPDATE TargetInventory SET Rejected = 1, InventoryUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: Tag/AssetUID; Tag matches but AssetUID does not.'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
            ON UPPER(LTRIM(RTRIM(TargetInventory.Tag))) = UPPER(LTRIM(RTRIM(SourceInventory.Tag)))
        WHERE
            TargetInventory.InventoryUID = 0
        AND UPPER(LTRIM(RTRIM(TargetInventory.AssetID))) <> UPPER(LTRIM(RTRIM(SourceInventory.AssetID)))
        AND TargetInventory.ProcessTaskUid = @ProcessTaskUid
        AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the Tag is Null or Empty', 1;
            END

        PRINT N'Reject where AssetUID matches more than one Inventory';
        --SELECT TargetInventory.Tag, TargetInventory.AssetID--, SourceInventory.Tag, SourceInventory.AssetID, SourceInventory.InventoryUID
        UPDATE TargetInventory SET Rejected = 1, InventoryUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Property: Asset; The Asset search found multiple records match different Inventory'
        FROM
            IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
        INNER JOIN (
            SELECT
                SourceInventory.AssetID
            FROM 
                IntegrationMiddleWay.dbo._ETL_Inventory TargetInventory
            INNER JOIN
                TipWebHostedChicagoPS.dbo.tblTechInventory SourceInventory
                ON UPPER(LTRIM(RTRIM(TargetInventory.AssetID))) = UPPER(LTRIM(RTRIM(SourceInventory.AssetID)))
            WHERE
                TargetInventory.InventoryUID = 0
            AND TargetInventory.AssetID IS NOT NULL
            AND LTRIM(RTRIM(TargetInventory.AssetID)) <> ''
            AND SourceInventory.AssetID IS NOT NULL
            AND LTRIM(RTRIM(SourceInventory.AssetID)) <> ''
            AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
            AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1)
            GROUP BY
                SourceInventory.AssetID
            HAVING
                COUNT(SourceInventory.InventoryUID) > 1
            ) RepeatedAssets
            ON UPPER(LTRIM(RTRIM(TargetInventory.AssetID))) = UPPER(LTRIM(RTRIM(RepeatedAssets.AssetID)))
        WHERE
            TargetInventory.AssetID IS NOT NULL
        AND LTRIM(RTRIM(TargetInventory.AssetID)) <> ''
        AND TargetInventory.InventoryUID = 0
        AND TargetInventory.ProcessTaskUID = @ProcessTaskUid
        AND (TargetInventory.Rejected = 0 OR @AllowStackingErrors = 1);


        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to reject records where the AssetUID is duplicated in the Source Data', 1;
            END


        SET NOCOUNT OFF;

        RETURN 0;

    END --End Procedure