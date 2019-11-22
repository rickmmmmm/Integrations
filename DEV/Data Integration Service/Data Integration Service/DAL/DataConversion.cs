using System;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Data_Integration_Service.DAL
{
    public class DataConversion
    {
        public static string RebuildIndex(string SQLServerConnectionString)
        {
            string Results = "Successful";
            string SQLCommand = "SELECT SettingValue FROM app.RefApplicationSettings WHERE SettingName = 'IntegrationsChargesTimer'";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteScalar();
                         }
                    catch (Exception e)
                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while Reindexing Primary Key - " + e.Message.ToString(), true);
                    }

                }

                connection.Close();
            }

            return Results;
        }

        public static string PoplateStagingTable(string SQLServerConnectionString)
        {
            string Results = "Successful";
            string SQLCommand = "EXEC [ETL].[MainETL] @BatchCount = 1";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while Poplating Clients_ETL - " + e.Message.ToString(), true);
                    }

                }

                connection.Close();
            }

            return Results;
        }

        public static string FixMissingHeaders(string SQLServerConnectionString)
        {
            string Results = "Successful";
            string SQLCommand = @"
                DECLARE @Counter		INT = 1
                DECLARE @CouterMax		INT
                DECLARE @MissingColumn	VARCHAR(100)
                DECLARE @SQL			VARCHAR(MAX)

                SET NOCOUNT ON

                DECLARE @ColumnNames TABLE
                (ColumnName VARCHAR(100) NOT NULL, Missing BIT NOT NULL)

                INSERT INTO @ColumnNames (ColumnName,Missing) VALUES
                ('ProductName',0),
                ('ManufacturerName',0),
                ('ModelNumber',0),
                ('ProductType',0),
                ('SerialRequired',0),
                ('Serial',0),
                ('ProjectedLife',0),
                ('ItemSuggestedPrice',0),
                ('SiteID',0),
                ('SiteName',0),
                ('RoomNumber',0),
                ('RoomDesc',0),
                ('RoomType',0),
                ('Tag',0),
                ('ParentTag',0),
                ('TagNotes',0),
                ('StudentorStaffID',0),
                ('StudentorStaffFirstName',0),
                ('StudentorStaffLastName',0),
                ('StudentorStaff',0),
                ('PONumber',0),
                ('FundingSource',0),
                ('PurchaseDate',0),
                ('PurchasePrice',0),
                ('PONotes',0),
                ('AccountCode',0),
                ('VendorName',0),
                ('VendorID',0),
                ('CustomField1Data',0),
                ('CustomField2Data',0),
                ('CustomField3Data',0),
                ('CustomField4Data',0),
                ('Accessory1Included',0),
                ('Accessory2Included',0),
                ('Accessory3Included',0),
                ('DepartmentName',0),
                ('DepartmentID',0),
                ('AssetCondition',0),
                ('AssetConditionID',0),
                ('FRN',0),
                ('DueDate',0),
                ('AssetID',0)

                UPDATE @ColumnNames
                SET Missing = 1
                WHERE ColumnName NOT IN
                (
                SELECT B.name
                FROM SYS.OBJECTS AS A
                INNER JOIN SYS.COLUMNS AS B
                ON A.object_id=B.object_id

                WHERE A.Name='RawImport'
                AND B.Name NOT IN ('RowID','ImportFIleName','PreRowID','ImportFileRowID','CustomerCode',
                'IntegrationName','CreationDate','CreationTime','Exception','ExceptionReason')
                )


                SELECT @CouterMax=MAX(RowID)
                FROM (
                SELECT ROW_NUMBER() OVER(ORDER BY ColumnName ASC) AS RowID,ColumnName
                FROM @ColumnNames
                WHERE Missing = 1
                ) AS TB

                WHILE @Counter <= @CouterMax
                BEGIN
	                SELECT @MissingColumn = ColumnName
	                FROM 
	                (SELECT ROW_NUMBER() OVER(ORDER BY ColumnName ASC) AS RowID,ColumnName
	                FROM @ColumnNames
	                WHERE Missing = 1) AS TBMissingCol
	                WHERE RowID = @Counter

	                SET @SQL = 'ALTER TABLE ETL.RawImport ADD [' + @MissingColumn +'] VARCHAR(200)'
	                EXEC (@SQL)

	                SET @Counter = @Counter + 1
                END";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while Fixing Missing Headers Clients_ETL - " + e.Message.ToString(), true);
                    }

                }

                connection.Close();
            }

            return Results;
        }

        public static string RemoveBlankLines(string SQLServerConnectionString)
        {
            string Results = "Successful";
            string SQLCommand = @"
                DELETE 
                FROM ETL.RawImport
                WHERE ProductName = '' AND ManufacturerName = ''
                AND ModelNumber = '' AND ProductType = ''
                AND SerialRequired = '' AND Serial = ''
                AND ProjectedLife = '' AND ItemSuggestedPrice = ''
                AND SiteID = '' AND SiteName = ''
                AND DepartmentName = '' AND RoomNumber = ''
                AND RoomDesc = '' AND RoomType = ''
                AND Tag = '' AND ParentTag = ''
                AND PONumber = '' AND FundingSource = ''
                AND PurchaseDate = '' AND PurchasePrice = ''
                AND AccountCode = '' AND VendorName = ''";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while removing blank lines - " + e.Message.ToString(), true);
                    }

                }

                connection.Close();
            }

            return Results;
        }

        public static string PoplateClientsETL(string SQLServerConnectionString)
        {
            string Results = "Successful";
            string SQLCommand = @"
            DECLARE @Counter		INT = 1
            DECLARE @CounterMax		INT
            DECLARE @IssueID		INT = 1
            DECLARE @ErrorRowCount	INT
            DECLARE @ColumnName		VARCHAR(100)
            DECLARE @ColumnLength	INT
            DECLARE @NumberOfRows	INT
            DECLARE @SQL			VARCHAR(MAX)

            SET NOCOUNT ON
            TRUNCATE TABLE ETL.Issues
            TRUNCATE TABLE ETL.IssuesRawData
            TRUNCATE TABLE Clients_ETL_Exceptions

            DECLARE @TableCoumns TABLE
            (RowID INT NOT NULL IDENTITY(1,1),ColumnName VARCHAR(100),ColumnLength INT) 

            INSERT INTO @TableCoumns
            (ColumnName,ColumnLength)

            SELECT	'ProductName' ,1000
            UNION ALL SELECT 'ManufacturerName' ,100
            UNION ALL SELECT 'ModelNumber' ,100
            UNION ALL SELECT 'ProductType' ,50
            UNION ALL SELECT 'ItemSuggestedPrice' ,50
            UNION ALL SELECT 'DepartmentName' ,100
            UNION ALL SELECT 'serialrequired',10
            UNION ALL SELECT 'Serial' ,250
            UNION ALL SELECT 'ProjectedLife' ,50
            UNION ALL SELECT 'SiteID' ,100
            UNION ALL SELECT 'SiteName' ,200
            UNION ALL SELECT 'RoomNumber' ,50
            UNION ALL SELECT 'RoomDesc' ,50
            UNION ALL SELECT 'RoomType' ,50
            UNION ALL SELECT 'Tag' ,50
            UNION ALL SELECT 'ParentTag' ,50
            UNION ALL SELECT 'TagNotes' ,1000
            UNION ALL SELECT 'StudentOrStaffID ',50
            UNION ALL SELECT 'StudentOrStaff',50
            UNION ALL SELECT 'PONumber' ,50
            UNION ALL SELECT 'Fundingsource' ,500
            UNION ALL SELECT 'PurchaseDate' ,50
            UNION ALL SELECT 'PurchasePrice' ,50
            UNION ALL SELECT 'AccountCode' ,100
            UNION ALL SELECT 'VendorName' ,300
            UNION ALL SELECT 'AssetCondition' ,300
            UNION ALL SELECT 'DueDate',50

            SET @CounterMax = @@ROWCOUNT

            WHILE @Counter <= @CounterMax
            BEGIN
	            SELECT @ColumnName = ColumnName, @ColumnLength = ColumnLength 
	            FROM @TableCoumns WHERE RowID = @Counter

	            SET @SQL = 'SELECT * FROM ETL.RawImport WHERE LEN('+ @ColumnName + ') > ' + CAST(@ColumnLength AS VARCHAR(50)) + '' 

	            EXEC(@SQL)

	            SET @NumberOfRows = @@ROWCOUNT

	            IF @NumberOfRows > 0
	            BEGIN
		            SET @SQL = 'INSERT INTO ETL.Issues (NumberOfRows,DataColumn,DescriptionOfIssue) SELECT ' +  CAST(@NumberOfRows AS varchar(50)) + ' AS NumberOfRows, '''  + @ColumnName +''' AS DataColumn, ''Data has exceed ' + CAST(@ColumnLength AS VARCHAR(50))  + ' in length. The extra data has been removed''' 

		            EXEC(@SQL)

		            SET @IssueID = @@IDENTITY

		            SET @SQL = 
		            'INSERT INTO ETL.IssuesRawData
		            (IssueID,RowID,Tag,ParentTag,TagNotes,Serial,SerialRequired,ProductName,ManufacturerName,ModelNumber,ProductType,ProjectedLife,ItemSuggestedPrice,SiteID,
		            SiteName,DepartmentName,RoomNumber,RoomDesc,RoomType,StudentorStaffID,StudentorStaff,PONumber,FundingSource,PurchaseDate,PurchasePrice,
		            PONotes,AccountCode,VendorName,StudentorStaffFirstName,StudentorStaffLastName,DueDate,
		            CustomField1Data,CustomField2Data,CustomField3Data,CustomField4Data,Accessory1Included,Accessory2Included,Accessory3Included,
		            ImportFIleName,ImportFileRowID,CreationDate,CreationTime)

		            SELECT ' + CAST(@IssueID AS varchar(50)) + ' AS IssueID,RowID,Tag,ParentTag,TagNotes,Serial,SerialRequired,ProductName,ManufacturerName,ModelNumber,ProductType,ProjectedLife,ItemSuggestedPrice,SiteID,
		            SiteName,DepartmentName,RoomNumber,RoomDesc,RoomType,StudentorStaffID,StudentorStaff,PONumber,FundingSource,PurchaseDate,PurchasePrice,
		            PONotes,AccountCode,VendorName,StudentorStaffFirstName,StudentorStaffLastName,DueDate,
		            CustomField1Data,CustomField2Data,CustomField3Data,CustomField4Data,Accessory1Included,Accessory2Included,Accessory3Included,
		            ImportFIleName,ImportFileRowID,CreationDate,CreationTime
		            FROM ETL.RawImport WHERE LEN('+ @ColumnName + ') > ' + CAST(@ColumnLength AS VARCHAR(50)) + '' 

		            EXEC(@SQL)

	            END

	            SET @Counter = @Counter + 1
            END

            INSERT INTO [dbo].[Clients_ETL]
            (ProductName,ManufacturerName,ModelNumber,ItemTypeName,Serial_Required,
            Serial,ProjectedLife,ItemSuggestedPrice,SiteID,SiteName,DepartmentName,
            RoomNumber,RoomDesc,RoomType,Tag,ParentTag,TagNotes,
            EntityID,EntityFirstName,EntityLastName,EntityType,DueDate,
            PONumber,FundingSource,PurchaseDate,PurchasePrice,PO_Notes,AccountCode,VendorName,
            CustomUDEF1_Value,CustomUDEF2_Value,CustomUDEF3_Value,CustomUDEF4_Value,
            Accessory1Included,Accessory2Included,Accessory3Included,FRN,AssetCondition)

            SELECT SUBSTRING(ProductName,1,1000),SUBSTRING(ManufacturerName,1,100),SUBSTRING(ModelNumber,1,100),SUBSTRING(ProductType,1,50),SUBSTRING(SerialRequired,1,50),
            SUBSTRING(Serial,1,250),SUBSTRING(ProjectedLife,1,50),SUBSTRING(ItemSuggestedPrice,1,50),SUBSTRING(SiteID,1,50),SUBSTRING(SiteName,1,200),SUBSTRING(DepartmentName,1,100),
            SUBSTRING(RoomNumber,1,50),SUBSTRING(RoomDesc,1,50),SUBSTRING(RoomType,1,50),SUBSTRING(Tag,1,50),SUBSTRING(ParentTag,1,50),SUBSTRING(TagNotes,1,1000),
            SUBSTRING(StudentorStaffID,1,50),SUBSTRING(StudentorStaffFirstName,1,50),SUBSTRING(StudentorStaffLastName,1,50),SUBSTRING(StudentorStaff,1,50),DueDate,
            SUBSTRING(PONumber,1,50),SUBSTRING(FundingSource,1,500),SUBSTRING(PurchaseDate,1,50),SUBSTRING(PurchasePrice,1,50),SUBSTRING(PONotes,1,50),SUBSTRING(AccountCode,1,50),SUBSTRING(VendorName,1,300),
            SUBSTRING(CustomField1Data,1,50),SUBSTRING(CustomField2Data,1,50),SUBSTRING(CustomField3Data,1,50),SUBSTRING(CustomField4Data,1,50),
            SUBSTRING(Accessory1Included,1,50),SUBSTRING(Accessory2Included,1,50),SUBSTRING(Accessory3Included,1,50),SUBSTRING(FRN,1,50),SUBSTRING(AssetCondition,1,50)

            FROM ETL.RawImport


            INSERT INTO ETL.RawImportStats
            (RawDataImportDate,TotalRowsInFile,TotalRowsImported,TotalRowsInETL,FileName)

            SELECT GETDATE() AS RawDataImportDate,MAX(ImportFileRowID)-1 AS TotalRowsInFile,COUNT(ImportFileRowID) AS TotalRowsImported,
            (SELECT COUNT(*) AS TotalRowsInETL FROM [dbo].[Clients_ETL]) AS TotalRowsInETL,
            (SELECT DISTINCT ImportFIleName FROM [ETL].[RawImport]) AS ImportFileName
            FROM [ETL].[RawImport]


            DELETE [dbo].[Clients_ETL]
            WHERE ProductName = '' AND ManufacturerName='' AND ItemTypeName=''";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while Poplating Clients_ETL table - " + e.Message.ToString(), true);
                    }

                }

                connection.Close();
            }

            return Results;
        }

        public static string RenameStagingTable(string SQLServerConnectionString)
        {
            string Results = "Successful";
            string SQLCommand = @"
                DECLARE @NewTableName VARCHAR(200) = ? + '_'+ FORMAT(DATEPART(MM,GETDATE()),'0#') + FORMAT(DATEPART(DD,GETDATE()),'0#') + FORMAT(DATEPART(YYYY,GETDATE()),'0000#') + '_' + FORMAT(DATEPART(HH,GETDATE()),'0#') + FORMAT(DATEPART(MI,GETDATE()),'0#')
                DECLARE @NewIndexName VARCHAR(200) = 'PK_' + @NewTableName

                EXEC sp_rename  'ETL.RawImport.PK_Stage_RawImport', @NewIndexName

                EXEC sp_rename 'ETL.RawImport', @NewTableName";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteScalar();
                    }
                    catch (Exception e)

                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while Poplating Clients_ETL table - " + e.Message.ToString(), true);
                    }

                }

                connection.Close();
            }

            return Results;
        }

        public static string RemoveSpecialCharters(string SQLServerConnectionString)
        {
            string Results = "Successful";
            string SQLCommand = @"
                DECLARE @SQL		VARCHAR(MAX) = 'UPDATE Clients_ETL '
                DECLARE @Counter	INT = 1
                DECLARE @CounterMax	INT

                SET NOCOUNT ON

                DECLARE @TableColumns TABLE
                (RowID INT NOT NULL IDENTITY(1,1), ColumnName VARCHAR(100) NOT NULL)

                INSERT INTO @TableColumns
                (ColumnName)

                SELECT B.name 
                FROM sys.objects AS A
                INNER JOIN sys.columns AS B
                ON A.object_id=B.object_id

                WHERE A.name = 'Clients_ETL'
                AND B.name NOT IN  ('IKey','Date_Loaded','DataSource','ReviewFlag','ReviewNotes','import','RejectNotes','CreateDate','ModfiedDate','ModfiedBy')

                SET @CounterMax = @@ROWCOUNT

                WHILE @Counter <= @CounterMax
                BEGIN
	
	                SELECT @SQL = @SQL + CASE WHEN RowID = 1 THEN ' SET ' ELSE ', ' END + ColumnName + ' = Cleanup.RemoveSpecialCharacters (' + ColumnName + ')'
	                FROM @TableColumns
	                WHERE RowID = @Counter

	                SET @Counter = @Counter + 1
                END


                EXEC(@SQL)";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteScalar();
                    }
                    catch (Exception e)

                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while Poplating Clients_ETL table - " + e.Message.ToString(), true);
                    }

                }

                connection.Close();
            }

            return Results;
        }

        public static string RemoveEmptyStrings(string SQLServerConnectionString)
        {
            string Results = "Successful";
            string SQLCommand = @"
                DECLARE @SQL		VARCHAR(MAX) 
                DECLARE @Counter	INT = 1
                DECLARE @CounterMax	INT

                SET NOCOUNT ON

                DECLARE @TableColumns TABLE
                (RowID INT NOT NULL IDENTITY(1,1), ColumnName VARCHAR(100) NOT NULL)

                INSERT INTO @TableColumns
                (ColumnName)

                SELECT B.name 
                FROM sys.objects AS A
                INNER JOIN sys.columns AS B
                ON A.object_id=B.object_id

                WHERE A.name = 'Clients_ETL'
                AND B.name NOT IN  ('IKey','Date_Loaded','DataSource','ReviewFlag','ReviewNotes','import','RejectNotes','CreateDate','ModfiedDate','ModfiedBy')

                SET @CounterMax = @@ROWCOUNT

                WHILE @Counter <= @CounterMax
                BEGIN
	
	                SELECT @SQL = 'UPDATE Clients_ETL  SET ' + ColumnName + ' = NULL WHERE '  + ColumnName + ' = '''' '
	                FROM @TableColumns
	                WHERE RowID = @Counter

	                EXEC(@SQL)

	                SET @Counter = @Counter + 1
                END";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteScalar();
                    }
                    catch (Exception e)

                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while Poplating Clients_ETL table - " + e.Message.ToString(), true);
                    }

                }

                connection.Close();
            }

            return Results;
        }

        public static string UpdateClientsETL(string SQLServerConnectionString)
        {
            // Custom Scripts to fix or correct data.
            string Results = "Successful";
            string SQLCommand = @"
                EXEC [ETL].[ClientsETLUpdate] @Debug = 0";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteScalar();
                    }
                    catch (Exception e)

                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while Poplating Clients_ETL table - " + e.Message.ToString(), true);
                    }

                }

                connection.Close();
            }

            return Results;
        }

        public static string UpdateDataConversionStatus(string SQLServerConnectionString,int IPKey,int StatusID)
        {
             string Results = "Successful";
            string SQLCommand = @"
                EXEC DataConversionStatusUpdate @IPKey=" + IPKey.ToString() +",@StatusID=" + StatusID.ToString();

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteScalar();
                    }
                    catch (Exception e)

                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while Updating Data Conversion Status - " + e.Message.ToString(), true);
                    }

                }

                connection.Close();
            }

            return Results;
        }

        public static string UpdateDataConversionProcessStatus(string SQLServerConnectionString, int IPKey, int ProcessID)
        {
            string Results = "Successful";
            string SQLCommand = @"
                UPDATE SSIS_ITVariables
                SET ProcessID = " + ProcessID.ToString() + ", " +
                "ProcessDate = GETDATE() " +
                "WHERE iPkey = " + IPKey.ToString();

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteScalar();
                    }
                    catch (Exception e)

                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while updating Data Conversion Process Status - " + e.Message.ToString(), true);
                    }

                }

                connection.Close();
            }

            return Results;
        }

    }
}
