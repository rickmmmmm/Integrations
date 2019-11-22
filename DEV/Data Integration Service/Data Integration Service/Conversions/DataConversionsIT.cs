using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Dts.Runtime;
using Data_Integration_Service.EventLogs;
using Data_Integration_Service.DAL;

namespace Data_Integration_Service.Conversions
{
    public class DataConversionsIT
    {
        public static string GetITDataConversionsToProcess(string DC_SQLServerConnectionString,string TIPWeb_SQLServerConnectionString)
        {
            DataTable ITDataConversions = new DataTable();

            string Results = "Succussful";
            int IPKey = 0;
            int ProcessID = 0;
            DateTime GoLiveDate;
            string ProcessEmails;
            string StagingDatabase;
            string ProducitonDatabase;
            int ConversionStatusID;
            bool ReleaseToProduction=false;
            string ProcessSQLServerConnectionString;

            string SQLCommand= @"
            SELECT ProcessID,iPkey,Go_LiveDate,ProcessEmail,StagingDBName,ProductionDBName,ConversionStatusID
            FROM SSIS_ITVariables 
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
                ProcessID = Convert.ToInt16(ITDataConversions.Rows[i]["ProcessID"]);
                ConversionStatusID = Convert.ToInt16(ITDataConversions.Rows[i]["ConversionStatusID"]);
                GoLiveDate = Convert.ToDateTime(ITDataConversions.Rows[i]["GoLiveDate"]);
                ProcessEmails = ITDataConversions.Rows[i]["ProcessEmails"].ToString();
                StagingDatabase = ITDataConversions.Rows[i]["StagingDatabase"].ToString();
                ProducitonDatabase = ITDataConversions.Rows[i]["ProducitonDatabase"].ToString();


                ProcessSQLServerConnectionString = TIPWeb_SQLServerConnectionString.Replace("", StagingDatabase);

                try
                {
                    Results = ProcessRawData(ProcessSQLServerConnectionString);

                    if (Results == "Successful")
                    {
                        Results = DataConversion.UpdateDataConversionStatus(DC_SQLServerConnectionString, IPKey, 2);
                    }

                    Results = DataConversionsIT.PopulateTIPWebDatabase(IPKey, ReleaseToProduction);
                    //Log IDs that where sent
                    //EventLogWrite.WriteToIntegrationLog(Convert.ToInt32(ChargeUID), "ChargeUID", "Charges", CustomerCode);
                }
                catch
                {
                    Results = "Failure";
                }
            }
            return Results;
        }
        public static string ProcessRawData(string TIPWebDatabase)
        {
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

            return Results;
        }
        public static string PopulateTIPWebDatabase(int IPKey,bool ReleaseToProduction)
        {
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
            }
                       
            return Results;
        }

    }
}
