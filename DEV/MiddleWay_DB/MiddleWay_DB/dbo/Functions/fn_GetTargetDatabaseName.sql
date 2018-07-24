CREATE FUNCTION [dbo].[fn_GetTargetDatabaseName]
    (@ProcessUid AS INT)
RETURNS VARCHAR(100)
AS
BEGIN
    DECLARE @TargetDatabase AS VARCHAR(100),
            @FirstIndex     AS INT

        SELECT
            @TargetDatabase = LTRIM(RTRIM(ConfigurationValue))
        FROM [Configurations]
        WHERE ConfigurationName = 'TIPWebConnection' 
          AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        SET @FirstIndex = CHARINDEX(';Database=', @TargetDatabase, 0);

        --Check that FirstIndex is NOT less than or equal to zero
        IF @FirstIndex <= 0
            BEGIN
                RETURN NULL;
                --THROW 50000, 'Target Database Name cannot be retrieved.', 1;
            END
        ELSE
            BEGIN
                --Increase the index by the lenght of the search string
                SET @FirstIndex = @FirstIndex + 10;
            END

        SET @TargetDatabase = LTRIM(RTRIM(SUBSTRING(@TargetDatabase, @FirstIndex, CHARINDEX(';', @TargetDatabase, @FirstIndex) - @FirstIndex)));

        --Check that Target Database is not null or empty
        IF @TargetDatabase IS NULL OR LEN(@TargetDatabase) = 0
            BEGIN
                RETURN NULL;
                --THROW 50000, 'Target Database Name is empty.', 1;
            END

        RETURN @TargetDatabase;
END