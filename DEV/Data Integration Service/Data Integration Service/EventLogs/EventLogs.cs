using System;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;

namespace Data_Integration_Service.EventLogs
{
    class EventLogWrite
    {
        public static void WriteApplicationLogInformatonEvent(string EventMessage, Boolean WriteToDB)
        {
            if (!EventLog.SourceExists("Integration Service"))
                EventLog.CreateEventSource("Integration Service", "Application");

            EventLog.WriteEntry("Integration Service", EventMessage, EventLogEntryType.Information);

            if (WriteToDB == true)
            {
                EventLogWrite.WriteToDatabase("Integration Service", EventMessage, "Information", 1);
            }
        }

        public static void WriteApplicationLogWarningEvent(string EventMessage, Boolean WriteToDB)
        {
            if (!EventLog.SourceExists("Integration Service"))
                EventLog.CreateEventSource("Integration Service", "Application");

            EventLog.WriteEntry("Integration Service", EventMessage, EventLogEntryType.Warning);

            if (WriteToDB == true)
            {
                EventLogWrite.WriteToDatabase("Integration Service", EventMessage, "Warning", 2);
            }
        }

        public static void WriteApplicationLogErrorEvent(string EventMessage, Boolean WriteToDB)
        {
            if (!EventLog.SourceExists("Integration Service"))
                EventLog.CreateEventSource("Integration Service", "Application");

            EventLog.WriteEntry("Integration Service", EventMessage, EventLogEntryType.Error);

            if (WriteToDB == true)
            {
                EventLogWrite.WriteToDatabase("Integration Service", EventMessage, "Error", 3);
            }
        }

        public static void WriteToDatabase(string ApplicationName, string EventMessage, string EventTypeText, Int16 EventTypeID)
        {
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Intergration"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("app.ApplicationLogInsert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ApplicationName", ApplicationName));
                    command.Parameters.Add(new SqlParameter("@EventMessage", EventMessage));
                    command.Parameters.Add(new SqlParameter("@EventTypeID", EventTypeID));
                    command.Parameters.Add(new SqlParameter("@EventTypeText", EventTypeText));

                    connection.Open();

                    IDbDataAdapter da = new SqlDataAdapter();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        EventLogWrite.WriteApplicationLogErrorEvent("Integration Service - Error inserting data into databbase - (" + e + ")", true);
                    }

                    connection.Close();
                }
            }
        }

        public static void WriteToIntegrationLog(int UniqueID, string ColumnName, string IntegrationType, string CustomerCode, bool Successful, string EventText )
        {
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Intergration"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("app.IntegrationLogInsert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UniqueID", UniqueID));
                    command.Parameters.Add(new SqlParameter("@ColumnName", ColumnName));
                    command.Parameters.Add(new SqlParameter("@IntegrationType", IntegrationType));
                    command.Parameters.Add(new SqlParameter("@CustomCode", CustomerCode));
                    command.Parameters.Add(new SqlParameter("@Successful", Successful));

                    connection.Open();

                    IDbDataAdapter da = new SqlDataAdapter();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        EventLogWrite.WriteApplicationLogErrorEvent("Integration Service - Error inserting data into databbase - (" + e + ")", true);
                    }

                    connection.Close();
                }
            }
        }

    }
}
