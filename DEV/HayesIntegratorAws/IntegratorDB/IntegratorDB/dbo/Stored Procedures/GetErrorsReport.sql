--USE IntgAppData

--DECLARE @Client AS VARCHAR(10)
--SET @Client = 'CPS'

--DECLARE @StartDate AS DATE,
--        @EndDate AS DATE
CREATE PROCEDURE [dbo].[GetErrorsReport] (
    @Client AS VARCHAR(10),
    @Offset AS INT = NULL,
    @Limit AS INT = NULL,
    @StartDate AS DATETIME = NULL,
    @EndDate AS DATETIME = NULL,
    @ErrorName AS VARCHAR(500) = NULL,
    @ErrorObjectSearch AS VARCHAR(500) = NULL)
AS
    BEGIN
        DECLARE @DataIntegrations AS TABLE (DataIntegrationsID VARCHAR(100) NOT NULL)

        IF(@Offset IS NULL)
            BEGIN
                SET @Offset = 0
            END
        IF(@Limit IS NULL)
            BEGIN
                SET @Limit = 1000
            END

        IF(@StartDate IS NOT NULL AND @EndDate IS NOT NULL)
            BEGIN
                IF(@EndDate < @StartDate)
                    BEGIN
                        SET @EndDate = DATEADD(HOUR, -24, @StartDate)
                    END
            END
        ELSE IF(@StartDate IS NOT NULL AND @EndDate IS NULL)
            SET @EndDate = GETDATE()
        ELSE
            BEGIN
                SELECT @StartDate = DATEADD(HOUR, -24, GETDATE()),
                       @EndDate = GETDATE()
            END

        INSERT INTO @DataIntegrations
        SELECT IntegrationsID
        FROM DataIntegrations
        WHERE 
            Client = @Client AND
            DateAdded BETWEEN @StartDate AND @EndDate

        SELECT
            [DataIntegrationsErrors].DataIntegrationsErrorsID,
            DataIntegrationsID,
            AddedDate,
            ErrorNumber,
            ErrorName,
            ErrorDescription,
            --ErrorFound,
            --ErrorStart,
            --ErrorLenght,
            SUBSTRING(ErrorObject, ErrorStart, ErrorLenght - ErrorStart) AS Error,
            ErrorObject
        FROM [dbo].[DataIntegrationsErrors]
        JOIN (
            SELECT
                [DataIntegrationsErrors].DataIntegrationsErrorsID,
                CASE
                    WHEN CHARINDEX('"errorDescription":["', ErrorObject) > 0 THEN 'errorDescription'
                    WHEN CHARINDEX('"error":"', ErrorObject) > 0 THEN 'errorData'
                    WHEN CHARINDEX('"error":["', ErrorObject) > 0 THEN 'error'
                    WHEN CHARINDEX('"message":["', ErrorObject) > 0 THEN 'message'
                    ELSE 'None'
                END AS ErrorFound,
                CASE
                    WHEN CHARINDEX('"errorDescription":["', ErrorObject) > 0 THEN (CHARINDEX('"errorDescription":["', ErrorObject) + 21)
                    WHEN CHARINDEX('"error":"', ErrorObject) > 0 THEN (CHARINDEX('"error":"', ErrorObject) + 9)
                    WHEN CHARINDEX('"error":["', ErrorObject) > 0 THEN (CHARINDEX('"error":["', ErrorObject) + 10)
                    WHEN CHARINDEX('"message":["', ErrorObject) > 0 THEN (CHARINDEX('"message":["', ErrorObject) + 12)
                    ELSE 0
                END AS ErrorStart,
                CASE
                    WHEN CHARINDEX('"errorDescription":["', ErrorObject) > 0 THEN (CHARINDEX('"]', ErrorObject, CHARINDEX('"errorDescription":["', ErrorObject) + 21) )
                    WHEN CHARINDEX('"error":"', ErrorObject) > 0 THEN (CHARINDEX('"}', ErrorObject, CHARINDEX('"error":"', ErrorObject) + 9) )
                    WHEN CHARINDEX('"error":["', ErrorObject) > 0 THEN (CHARINDEX('"]', ErrorObject, CHARINDEX('"error":["', ErrorObject) + 10) )
                    WHEN CHARINDEX('"message":["', ErrorObject) > 0 THEN (CHARINDEX('"]', ErrorObject, CHARINDEX('"message":["', ErrorObject) + 12) )
                    ELSE LEN(ErrorObject)
                END AS ErrorLenght
            FROM [dbo].[DataIntegrationsErrors]
            ) ErrorLenghts
            ON DataIntegrationsErrors.DataIntegrationsErrorsID = ErrorLenghts.DataIntegrationsErrorsID
        WHERE 
            DataIntegrationsID IN (SELECT DataIntegrationsID FROM @DataIntegrations)
            AND (
                (@ErrorName IS NOT NULL AND ErrorName = @ErrorName) OR
                (@ErrorObjectSearch IS NOT NULL AND ErrorObject LIKE '%' + @ErrorObjectSearch + '%') OR 
                (@ErrorName IS NULL AND @ErrorObjectSearch IS NULL )
            )
        ORDER BY AddedDate
        OFFSET @Offset ROWS
        FETCH NEXT @LIMIT ROWS ONLY;

        PRINT N''
        PRINT N'Showing Errors ' + CAST((@Offset + 1) AS VARCHAR) + ' to ' + CAST((@Limit + 1) AS VARCHAR) + ' recorded between ' + CONVERT(VARCHAR, @StartDate, 101) + ' ' + CONVERT(VARCHAR, @StartDate, 108) + ' and ' + CONVERT(VARCHAR, @EndDate, 101) + ' '+ CONVERT(VARCHAR, @EndDate, 108)
        PRINT N'With Search String: @ErrorName = ''' + ISNULL(@ErrorName, 'NULL') + ''', @ErrorObjectSearch = ''' + '%' + ISNULL(@ErrorObjectSearch, 'NULL') + '%' + '''.'
    END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[GetErrorsReport] TO [intg-cps]
    WITH GRANT OPTION
    AS [dbo];

