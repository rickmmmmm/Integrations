﻿/*
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
                @AllowStackingErrors    AS BIT;

        SET @DefaultDepartmentUID = 0;
        SET @TargetDatabase = [dbo].[fn_GetTargetDatabaseName](@ProcessUid);
        --SET @SourceTable = [dbo].[fn_GetSourceTable](@SourceProcess);
        
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

        SELECT @DefaultDepartmentUID = CAST(ConfigurationValue AS INT)
        FROM [Configurations] 
        WHERE ConfigurationName = 'DefaultDepartmentUID' AND ProcessUid = @ProcessUid
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


        -- Match the Department by Name
        --SELECT TargetDepartment.TechDepartmentUID, TargetDepartment.DepartmentID, TargetDepartment.DepartmentName, SourceDepartment.DepartmentName, SourceDepartment.TechDepartmentUID
        UPDATE TargetDepartment SET TargetDepartment.TechDepartmentUID = SourceDepartment.TechDepartmentUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetDepartment
        INNER JOIN
            TipWebHostedChicagoPS.dbo.tblTechDepartments SourceDepartment
            ON UPPER(LTRIM(RTRIM(TargetDepartment.DepartmentName))) = UPPER(LTRIM(RTRIM(SourceDepartment.DepartmentName)))
        WHERE
            TargetDepartment.DepartmentName IS NOT NULL
        AND LTRIM(RTRIM(TargetDepartment.DepartmentName)) <> ''
        AND TargetDepartment.TechDepartmentUID = 0
        AND TargetDepartment.ProcessTaskUID = @ProcessTaskUid
        AND (TargetDepartment.Rejected = 0 OR @AllowStackingErrors = 1);


        --Match the Department by ID
        --SELECT TargetDepartment.TechDepartmentUID, TargetDepartment.DepartmentID, TargetDepartment.DepartmentName, SourceDepartment.DepartmentID, SourceDepartment.TechDepartmentUID
        UPDATE TargetDepartment SET TargetDepartment.TechDepartmentUID = SourceDepartment.TechDepartmentUID
        FROM 
            IntegrationMiddleWay.dbo._ETL_Inventory TargetDepartment
        INNER JOIN 
            TipWebHostedChicagoPS.dbo.tblTechDepartments SourceDepartment
            ON UPPER(LTRIM(RTRIM(TargetDepartment.DepartmentID))) = UPPER(LTRIM(RTRIM(SourceDepartment.DepartmentID)))
        WHERE
            TargetDepartment.DepartmentID IS NOT NULL
        AND LTRIM(RTRIM(TargetDepartment.DepartmentID)) <> ''
        AND TargetDepartment.TechDepartmentUID = 0
        AND TargetDepartment.ProcessTaskUID = @ProcessTaskUid
        AND (TargetDepartment.Rejected = 0 OR @AllowStackingErrors = 1);


        --if the TIPWeb Database is departments and the Default Department is 0 reject all 
        DECLARE @DepartmentCount AS INT

        SELECT @DepartmentCount = COUNT(TechDepartmentUID)
        FROM
            TipWebHostedChicagoPS.dbo.tblTechDepartments SourceDepartment
        WHERE 
            SourceDepartment.TechDepartmentUID > 0

        IF @DepartmentCount > 0 AND @DefaultDepartmentUID = 0
            BEGIN
                --SELECT TargetDepartment.TechDepartmentUID, TargetDepartment.TechDepartmentUID
                UPDATE TargetDepartment SET Rejected = 1, TechDepartmentUID = -1, RejectedNotes = CASE WHEN RejectedNotes IS NULL THEN N'' ELSE CAST(RejectedNotes AS VARCHAR(MAX)) + CAST(CHAR(13) AS VARCHAR(MAX)) END + N'Source Properties: TechDepartmentUID; TechDepartmentUID is invalid'
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetDepartment
                WHERE
                    TargetDepartment.TechDepartmentUID = 0
            END
        ELSE
            BEGIN
                --Set all non-matches to Default
                --SELECT TargetDepartment.TechDepartmentUID, TargetDepartment.DepartmentID
                UPDATE TargetDepartment SET TechDepartmentUID = @DefaultDepartmentUID
                FROM
                    IntegrationMiddleWay.dbo._ETL_Inventory TargetDepartment
                WHERE
                    TargetDepartment.TechDepartmentUID = 0
            END

    END --End Procedure