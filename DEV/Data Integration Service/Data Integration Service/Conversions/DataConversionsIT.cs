using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Dts.Runtime;
using Data_Integration_Service.EventLogs;
using Data_Integration_Service.DAL;
using Data_Integration_Service.Reporting;
using Data_Integration_Service.Alerts;

namespace Data_Integration_Service.Conversions
{
    public class DataConversionsIT
    {
        public static string GetITDataConversionsToProcess(string DC_SQLServerConnectionString,string TIPWeb_SQLServerConnectionString)
        {
            DataTable ITDataConversions = new DataTable();

            string Results = "Succussful";
            int IPKey = 0;
            string FolderName = "";
            int ProcessID = 0;
            DateTime GoLiveDate;
            string ProcessEmails;
            string StagingDatabase;
            string ProducitonDatabase;
            string ReportName;
            int ConversionStatusID;
            bool ReleaseToProduction=false;
            string ProcessSQLServerConnectionString;

            string SQLCommand= @"
            SELECT ProcessID,iPkey,FolderName,Go_LiveDate,ProcessEmail,StagingDBName,ProductionDBName,ConversionStatusID
            FROM SSIS_Variables 
            WHERE Active = 1 AND  ConversionType = 2 
            AND ProcessID = 1 AND ConversionStatusID < 14";

            using (SqlConnection connection = new SqlConnection(DC_SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {

                            da.Fill(ITDataConversions);
                        }
                    }
                    catch (Exception e)
                    {
                        Results = "Failure";
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while getting ITDataConversions to process - " + e.Message.ToString(), true);
                    }

                    connection.Close();
                }

            }


            for (int i = 0; i < ITDataConversions.Rows.Count; i++)
            {
                IPKey = Convert.ToInt16(ITDataConversions.Rows[i]["IPKey"]);
                FolderName = ITDataConversions.Rows[i]["FolderName"].ToString();
                ProcessID = Convert.ToInt16(ITDataConversions.Rows[i]["ProcessID"]);
                ConversionStatusID = Convert.ToInt16(ITDataConversions.Rows[i]["ConversionStatusID"]);
                GoLiveDate = Convert.ToDateTime(ITDataConversions.Rows[i]["GoLiveDate"]);
                ProcessEmails = ITDataConversions.Rows[i]["ProcessEmails"].ToString();
                StagingDatabase = ITDataConversions.Rows[i]["StagingDatabase"].ToString();
                ProducitonDatabase = ITDataConversions.Rows[i]["ProducitonDatabase"].ToString();


                ProcessSQLServerConnectionString = TIPWeb_SQLServerConnectionString.Replace("", StagingDatabase);

                try
                {
                    EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Starting IT Data Conversion Process for " + FolderName, true); 

                    Results = ProcessRawData(ProcessSQLServerConnectionString, FolderName);

                    if (Results == "Successful")
                    {
                        Results = DataConversion.UpdateDataConversionStatus(DC_SQLServerConnectionString, IPKey, 2);
                    }

                    //Preliminary Report
                    ReportName = GenerateReport.PreliminaryReport(IPKey.ToString(),@"~\GeneratedReports\",FolderName);

                    if (ReportName.Contains("Successful"))
                    {
                        Results = "Successful";
                        ReportName = ReportName.Replace("Successful - ", "");
                    }
                    else
                    {
                        Results = "Failure";
                    }

                    if (Results == "Successful")
                    {
                        SendEmails.DBEmailsNormalPriority(DC_SQLServerConnectionString, ProcessEmails, "Preliminary Data Conversion Report", "Preliminary Data Conversion Report for " + FolderName, "", @"~\GeneratedReports\" + ReportName);
                    }
                    
                    //Clone Production Database from Producton
                    if (Results == "Successful")
                    {
                        Results =  DataConversionsIT.CloneProductionDatabase(StagingDatabase, ProducitonDatabase, FolderName);
                    }

                    //Populate Staging or Produciton
                    if (Results == "Successful")
                    {
                        Results = DataConversionsIT.PopulateTIPWebITDatabase(IPKey, ReleaseToProduction, FolderName);
                    }

                    //Conversion Report
                    GenerateReport.ConversionReport(IPKey.ToString(), @"~\GeneratedReports\", FolderName);

                    if (ReportName.Contains("Successful"))
                    {
                        Results = "Successful";
                        ReportName = ReportName.Replace("Successful - ", "");
                    }
                    else
                    {
                        Results = "Failure";
                    }

                    if (Results == "Successful")
                    {
                        SendEmails.DBEmailsNormalPriority(DC_SQLServerConnectionString, ProcessEmails, "Data Conversion Report", "Data Conversion Report for " + FolderName, "", @"~\GeneratedReports\" + ReportName);
                    }

                    EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Completed Successfully IT Data Conversion Process for " + FolderName, true);
                }
                catch
                {
                    Results = "Failure";
                    EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Instegration Service - Completed with Failure IT Data Conversion Process for " + FolderName, true);
                }
            }

            return Results;
        }
        public static string ProcessRawData(string TIPWebDatabase,string FolderName)
        {
            EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Starting IT Data Conversion RawData Import for " + FolderName, true);

            string Results = "Succussful"; ;

            Results = DataConversion.RebuildIndex(TIPWebDatabase);

            if (Results == "Successful")
            {
                Results = DataConversion.PoplateStagingTable(TIPWebDatabase);
            }

            if (Results == "Successful")
            {
                Results = DataConversion.FixMissingHeaders(TIPWebDatabase);
            }

            if (Results == "Successful")
            {
                Results = DataConversion.RemoveBlankLines(TIPWebDatabase);
            }

            if (Results == "Successful")
            {
                Results = DataConversion.PoplateClientsETL(TIPWebDatabase);
            }

            if (Results == "Successful")
            {
                Results = DataConversion.RenameStagingTable(TIPWebDatabase);
            }

            if (Results == "Successful")
            {
                Results = DataConversion.RemoveSpecialCharters(TIPWebDatabase);
            }

            if (Results == "Successful")
            {
                Results = DataConversion.RemoveEmptyStrings(TIPWebDatabase);
            }

            if (Results == "Successful")
            {
                Results = DataConversion.UpdateClientsETL(TIPWebDatabase);
            }

            if (Results == "Successful")
            {
                Results = DataConversion.UpdateClientsETL(TIPWebDatabase);
            }

            if (Results == "Successful")
            {
                EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Completed Successfully IT Data Conversion RawData Import for " + FolderName, true);
            }
            else
            {
                EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Instegration Service - Completed with Failure IT Data Conversion RawData Import for " + FolderName, true);
            }
            

            return Results;
        }
        public static string CloneProductionDatabase(string StagingDatabase, string ProductionDatabase, string FolderName)
        {
            EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Starting IT Data Conversion Cloning Producution Database for " + FolderName, true);

            string PackageName = "DatabaseClone.dtsx";
            string PackagePath = @"~\SSIS Packages\";

            Package pkg;
            Application app;
            DTSExecResult pkgResults;
            Variables vars;
            string Results = "Succussful"; ;

            app = new Application();
            pkg = app.LoadPackage(PackagePath + PackageName, null);

            vars = pkg.Variables;
            vars["SourceDatabase"].Value = ProductionDatabase;
            vars["TargetDatabase"].Value = StagingDatabase;

            pkgResults = pkg.Execute(null, vars, null, null, null);

            if (pkgResults == DTSExecResult.Failure)
            {
                Results = "Failure";
                EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Instegration Service - Completed with Failure IT Data Conversion Cloning Production Database for " + FolderName, true);
            }
            else
            {
                EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Completed Successfully IT Data Conversion Cloning Production Databasefor " + FolderName, true);
            }

            return Results;
        }
        public static string PopulateTIPWebITDatabase(int IPKey,bool ReleaseToProduction, string FolderName)
        {
            EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Starting IT Data Conversion Populating TIPWeb Database for " + FolderName, true);

            string PackageName = "IT_Conversion.dtsx";
            string PackagePath = @"~\SSIS Packages\";

            Package pkg;
            Application app;
            DTSExecResult pkgResults;
            Variables vars;
            string Results = "Succussful"; ;

            app = new Application();
            pkg = app.LoadPackage(PackagePath + PackageName, null);

            vars = pkg.Variables;
            vars["IPKey"].Value = IPKey;
            vars["ProductionReleased"].Value = ReleaseToProduction;

            pkgResults = pkg.Execute(null, vars, null, null, null);

            if (pkgResults == DTSExecResult.Failure)
            {
                Results = "Failure";
                EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Instegration Service - Completed with Failure IT Data ConversionPopulating TIPWeb Database for " + FolderName, true);
            }
            else
            {
                EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Completed Successfully IT Data Conversion Populating TIPWeb Databasefor " + FolderName, true);
            }

            return Results;
        }

    }
}
