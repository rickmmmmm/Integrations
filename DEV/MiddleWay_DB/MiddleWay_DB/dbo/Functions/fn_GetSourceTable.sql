/*
 *  fn_GetSourceTable
 *  Get the Source Table name for the specified ProcessSourceUid
 *      or NULL if the ProcessSourceUid is not valid
 */
CREATE FUNCTION [dbo].[fn_GetSourceTable]
    (@ProcessSourceUid AS INT)
RETURNS VARCHAR(100)
AS
    BEGIN
        DECLARE @SourceTableName AS VARCHAR(100)

        SELECT
            @SourceTableName = ProcessSourceTable
        FROM [dbo].[ProcessSource]
        WHERE ProcessSourceUid = @ProcessSourceUid
          AND Enabled = 1;
          
        RETURN @SourceTableName;
    END