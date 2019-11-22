using System;
using System.Data;
using System.Data.SqlClient;

namespace Data_Integration_Service.DAL
{
    public class IntergrationMiddlway
    {
        public static int GetTimerCharges(string SQLServerConnectionString)
        {
            int Results = 300000;
            string SQLCommand = "SELECT SettingValue FROM app.RefApplicationSettings WHERE SettingName = 'IntegrationsChargesTimer'";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        Results = Convert.ToInt32(command.ExecuteScalar());
                        EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Setting Charges Timer to(" + Results.ToString() + ")", true);
                    }
                    catch (Exception e)
                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while setting Charges Timer - " + e.Message.ToString(), true);
                    }
                    
                }

                connection.Close();
            }
            
            return Results;
        }

        public static string GetChargesCustomerAPI(string SQLServerConnectionString,string CustomerCode)
        {
            string Results = "";
            string SQLCommand = "SELECT SettingValue FROM app.RefApplicationSettings WHERE SettingName = '" + CustomerCode  + "AddFeeAPICall'";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        Results = command.ExecuteScalar().ToString();
                    }
                    catch (Exception e)
                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while setting Charges API URL for " + CustomerCode  + " - " + e.Message.ToString(), true);
                    }

                    connection.Close();
                }
                                
            }

            return Results;
        }

        public static string GetChargesCustomerLastQueryDate(string SQLServerConnectionString, string CustomerCode)
        {
            string Results = "";
            string SQLCommand = @"
            SELECT SUBSTRING(ApplicationLogMessage,CHARINDEX('LastQueryDate: ',ApplicationLogMessage) + 15,24)
            FROM app.ApplicationLog  WITH (NOLOCK) WHERE ApplicationLogID =
            (SELECT MAX(ApplicationLogID)
            FROM app.ApplicationLog WITH (NOLOCK)
            WHERE EventTypeID = 1 
            AND ApplicationLogMessage LIKE 'Instegration Service - Successful Charges CustomerCode: " + CustomerCode + " LastQueryDate: %')";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        Results = command.ExecuteScalar().ToString();
                    }
                    catch (Exception e)
                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while setting Charges LastQueryDate for " + CustomerCode + " - " + e.Message.ToString(), true);
                    }

                    connection.Close();
                }

            }

            if (Results == "" || Results == null)
            {
                Results = "2019-11-01 00:00:00.000";
            }

            return Results;
        }

        public static string GetChargesCustomerTIPWebITDatabase(string SQLServerConnectionString, string CustomerCode)
        {
            string Results = "TipWebHostedChicagoPS";

            // for Furture Use
            //string SQLCommand = "SELECT SettingValue FROM app.RefApplicationSettings WHERE SettingName = '" + CustomerCode + "AddFeeAPICall'";

            //using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            //{

            //    using (SqlCommand command = new SqlCommand(SQLCommand, connection))
            //    {
            //        connection.Open();

            //        try
            //        {
            //            Results = command.ExecuteScalar().ToString();
            //        }
            //        catch (Exception e)
            //        {
            //            EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while setting Charges API URL for " + CustomerCode + " - " + e.Message.ToString(), true);
            //        }

            //        connection.Close();
            //    }

            //}

            return Results;
        }

        public static DataTable GetCharges(string SQLServerConnectionString,string CustomerCode, string FromDate)
        {
            DataTable Results = new DataTable();

            string SQLCommand = @"
            SELECT  tblUnvCharges.ChargeUID,v_IMAllEntityInfo.EntityID AS StudentID,tblCampuses.CampusID,
            CONVERT(VARCHAR(50), tblUnvCharges.CreatedDate, 101) AS CreateDate,
			CONVERT(VARCHAR(50), DATEADD(HH, DATEDIFF(HOUR, GETUTCDATE(), GETDATE()), tblUnvCharges.CreatedDate), 121) AS CreateDateUTC,
            'TIPWeb-IM: ' + tblUnvChargeTypes.Name AS ItemCode,
            tblUnvCharges.ChargeAmount, ISNULL(tblUnvCharges.Notes, '') + ', ' + ISNULL(F.Accession, '') + ' - ' + REPLACE(ISNULL(dbo.tblBookInventory.Title, ''), '""', '') AS TransactionComments,
            G.LoginName,GETDATE() AS QueryDateTime

            FROM dbo.tblBookInventory

            RIGHT OUTER JOIN tblUnvChargeTypes

            INNER JOIN tblUnvCharges
            ON dbo.tblUnvChargeTypes.ChargeTypeUID = dbo.tblUnvCharges.ChargeTypeUID

            LEFT OUTER JOIN dbo.tblCampuses

            INNER JOIN dbo.v_IMAllEntityInfo
            ON dbo.tblCampuses.CampusID = dbo.v_IMAllEntityInfo.CampusID ON dbo.tblUnvCharges.EntityTypeUID = dbo.v_IMAllEntityInfo.EntityTypeUID AND
            dbo.tblUnvCharges.EntityUID = dbo.v_IMAllEntityInfo.EntityUID ON dbo.tblBookInventory.BookInventoryUID = dbo.tblUnvCharges.ItemUID

            LEFT OUTER JOIN dbo.tblUnvChargePayments
            ON dbo.tblUnvCharges.ChargeUID = dbo.tblUnvChargePayments.ChargeUID

            LEFT JOIN tblStudentsDistribution AS F
            ON F.StudentsUID = v_IMAllEntityInfo.EntityID AND dbo.tblUnvCharges.UniversalID = F.Accession

            INNER JOIN tblUser AS G
            ON tblUnvCharges.CreatedByUserID = G.UserID

            WHERE(dbo.v_IMAllEntityInfo.EntityType = 'Student') AND(dbo.tblUnvCharges.ApplicationUID = 1)
            AND tblUnvCharges.DateSatisfied IS NULL
            AND tblUnvCharges.CreatedDate >= '" + FromDate + "'";

            using (SqlConnection connection = new SqlConnection(SQLServerConnectionString))
            {

                using (SqlCommand command = new SqlCommand(SQLCommand, connection))
                {
                    connection.Open();

                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {

                            da.Fill(Results);
                        }
                    }
                    catch (Exception e)
                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Error while getting charges data for " + CustomerCode + " - " + e.Message.ToString(), true);
                    }

                    connection.Close();
                }

            }

            return Results;
        }
    }
}
