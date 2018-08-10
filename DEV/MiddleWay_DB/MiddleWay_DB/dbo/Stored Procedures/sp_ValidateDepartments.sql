/*
 *  sp_ValidateDepartments
 *  Match Departments By DepartmentID and then By DepartmentName
 *  If the tipweb database is use the DefaultDepartmentUID for the rest
 *      unless the database is departments and the default is 0
 */
CREATE PROCEDURE [dbo].[sp_ValidateDepartments]
    (@ProcessUid AS INT, @ProcessTaskUid AS INT, @SourceProcess AS INT)
AS
    BEGIN
        DECLARE @DefaultDepartmentUID   AS INT,
                @TargetDatabase         AS VARCHAR(100),
                @SourceTable            AS VARCHAR(100),
                @AllowStackingErrors    AS BIT,
                @ErrorCode              AS INT,
                @Statement              AS NVARCHAR(MAX),
                @ParamDefinition        AS NVARCHAR(MAX);

        SET NOCOUNT ON;

        SET @DefaultDepartmentUID = 0;
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

        SELECT @DefaultDepartmentUID = CAST(ConfigurationValue AS INT)
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultDepartmentUID' AND ProcessUid = @ProcessUid
          AND Enabled = 1;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Configuration for DefaultDepartmentUID', 1;
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

        -- Match the Department by Name
        --SELECT TargetDepartment.TechDepartmentUID, TargetDepartment.DepartmentID, TargetDepartment.DepartmentName, SourceDepartment.DepartmentName, SourceDepartment.TechDepartmentUID
        SET @Statement = '
        UPDATE TargetDepartment SET TargetDepartment.TechDepartmentUID = SourceDepartment.TechDepartmentUID
        FROM 
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetDepartment
        INNER JOIN
            ' + @TargetDatabase + '.dbo.tblTechDepartments SourceDepartment
            ON UPPER(LTRIM(RTRIM(TargetDepartment.DepartmentName))) = UPPER(LTRIM(RTRIM(SourceDepartment.DepartmentName)))
        WHERE
            TargetDepartment.DepartmentName IS NOT NULL
        AND LTRIM(RTRIM(TargetDepartment.DepartmentName)) <> ''''
        AND TargetDepartment.TechDepartmentUID = 0
        AND TargetDepartment.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND (TargetDepartment.Rejected = 0 OR ' + CAST(@AllowStackingErrors AS VARCHAR(1)) + ' = 1)';
        EXECUTE (@Statement);
        --PRINT @Statement;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Departments by Name', 1;
            END

        --Match the Department by ID
        --SELECT TargetDepartment.TechDepartmentUID, TargetDepartment.DepartmentID, TargetDepartment.DepartmentName, SourceDepartment.DepartmentID, SourceDepartment.TechDepartmentUID
        SET @Statement = '
        UPDATE TargetDepartment SET TargetDepartment.TechDepartmentUID = SourceDepartment.TechDepartmentUID
        FROM 
            IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetDepartment
        INNER JOIN 
            ' + @TargetDatabase + '.dbo.tblTechDepartments SourceDepartment
            ON UPPER(LTRIM(RTRIM(TargetDepartment.DepartmentID))) = UPPER(LTRIM(RTRIM(SourceDepartment.DepartmentID)))
        WHERE
            TargetDepartment.DepartmentID IS NOT NULL
        AND LTRIM(RTRIM(TargetDepartment.DepartmentID)) <> ''''
        AND TargetDepartment.TechDepartmentUID = 0
        AND TargetDepartment.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
        AND (TargetDepartment.Rejected = 0 OR ' + CAST(@AllowStackingErrors AS VARCHAR(1)) + ' = 1)';
        EXECUTE (@Statement);
        --PRINT @Statement;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to match Departments by DepartmentID', 1;
            END

        --if the TIPWeb Database is departments and the Default Department is 0 reject all 
        DECLARE @DepartmentCount AS INT

        SET @ParamDefinition = N'@DepartmentCount INT OUTPUT';
        SET @Statement = '
        SELECT @DepartmentCount = COUNT(TechDepartmentUID)
        FROM
            ' + @TargetDatabase + '.dbo.tblTechDepartments SourceDepartment
        WHERE 
            SourceDepartment.TechDepartmentUID > 0';
        EXECUTE sp_executesql @Statement, @ParamDefinition, @DepartmentCount = @DepartmentCount OUTPUT;
        --PRINT @Statement;

        IF @@ERROR <> 0
            BEGIN
                SET @ErrorCode = @@ERROR + 100000;
                --SET @ErrorMessage = ;
                --RETURN @ErrorCode;
                THROW @ErrorCode, 'Failed to read the Department Settings of the Target Database', 1;
            END

        IF @DepartmentCount > 0 AND @DefaultDepartmentUID = 0
            BEGIN
                --SELECT TargetDepartment.TechDepartmentUID, TargetDepartment.TechDepartmentUID
                SET @Statement = '
                UPDATE TargetDepartment 
                SET Rejected = 1, TechDepartmentUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'''' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N''Source Properties: TechDepartmentUID; TechDepartmentUID is invalid''
                FROM
                    IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetDepartment
                WHERE
                    TargetDepartment.TechDepartmentUID = 0
                    AND TargetDepartment.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                    AND (TargetDepartment.Rejected = 0 OR ' + CAST(@AllowStackingErrors AS VARCHAR(1)) + ' = 1)';
                EXECUTE (@Statement);
                --PRINT @Statement;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to reject Invalid Departments', 1;
                    END
            END
        ELSE
            BEGIN
                --Set all non-matches to Default
                --SELECT TargetDepartment.TechDepartmentUID, TargetDepartment.DepartmentID
                SET @Statement = '
                UPDATE TargetDepartment SET TechDepartmentUID = ' + CAST(@DefaultDepartmentUID AS VARCHAR(10)) + '
                FROM
                    IntegrationMiddleWay.dbo.' + @SourceTable + ' TargetDepartment
                WHERE
                    TargetDepartment.TechDepartmentUID = 0
                    AND TargetDepartment.ProcessTaskUID = ' + CAST(@ProcessTaskUid AS VARCHAR(3)) + '
                    AND (TargetDepartment.Rejected = 0 OR ' + CAST(@AllowStackingErrors AS VARCHAR(1)) + ' = 1)';
                EXECUTE (@Statement);
                --PRINT @Statement;

                IF @@ERROR <> 0
                    BEGIN
                        SET @ErrorCode = @@ERROR + 100000;
                        --SET @ErrorMessage = ;
                        --RETURN @ErrorCode;
                        THROW @ErrorCode, 'Failed to set all non-matches to DefaultDepartmentUID', 1;
                    END
            END

        SET NOCOUNT OFF;

        RETURN 0;

    END --End Procedure