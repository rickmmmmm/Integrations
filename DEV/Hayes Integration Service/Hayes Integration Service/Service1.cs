using System.ServiceProcess;
using System.Configuration;
using System;
using System.Data;
using System.Timers;

namespace Hayes_Integration_Service
{
    public partial class Service1 : ServiceBase
    {

        string IntergrationConnectionString = ConfigurationManager.ConnectionStrings["Intergration"].ToString();
        string TIPWebITConnectionString = ConfigurationManager.ConnectionStrings["TIPWeb-IT"].ToString();

        private static Timer AspenChargesTimer;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            double DefaultTimer = 120000; // 1000 = 1 Second and 60,000 = 1 min

            // Aspen Charges timer
            AspenChargesTimer = new Timer(DefaultTimer);
            AspenChargesTimer.Enabled = true;
            AspenChargesTimer.Elapsed += AspenTIPWebCharges;

            // Aspen Payment timer

        }

        protected override void OnStop()
        {
        }

        public void AspenTIPWebCharges(object sender, System.Timers.ElapsedEventArgs args)
        {
            int Results = 0;
            string APIURL = "";
            string CustomerCode = "CPS";
            string AlertEmailAddresses = "";
            string TIPWebDatabase = "";
            string EmailBody = "";
            string QueryString = "";

            DataTable ChargesData = new DataTable();

            ChargesTimer.Enabled = false;
            EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Starting Charges Process", true);

            //Get API URL for Customer
            APIURL = IntergrationMiddlway.GetChargesCustomerAPI(IntergrationConnectionString, "CPS");

            //Set TIPWeb-IT Connection String
            TIPWebDatabase = IntergrationMiddlway.GetChargesCustomerTIPWebDatabase(IntergrationConnectionString, "CPS");
            TIPWebITConnectionString = TIPWebITConnectionString.Replace("TIPWebITDBNameHere", TIPWebDatabase);

            //Getting Charges Data
            if (APIURL != "" && TIPWebITConnectionString != "")
            {
                ChargesData = IntergrationMiddlway.GetCharges(TIPWebITConnectionString, CustomerCode);

                if (ChargesData.Rows.Count > 0)
                {
                    Results = Conversions.DataConversions.ConvertToChargesPostAPICall(APIURL, ChargesData, CustomerCode);

                    if (Results == 0)
                    {
                        EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Successful Charges CustomerCode: " + CustomerCode + " LastQueryDate: " + ChargesData.Rows[0]["QueryDateTime"].ToString() + " (Total Charges Proccessed: " + ChargesData.Rows.Count.ToString() + ")", true);
                    }
                    else
                    {
                        //Set AlertEmailAddresses
                        AlertEmailAddresses = ConfigurationManager.AppSettings["AlertEmailAddress"];

                        EmailBody = "There are " + Results.ToString() + " total errors for customer " + CustomerCode + " in thier Charges/Fees API Call(s)." +
                                    "The error messages might need to be reviewed by the customer to determine the correct action(s).";

                        QueryString = "SELECT DISTINCT A.UniqueID AS ChargeUID, A.EventText AS APIErrorMessage " +
                            "FROM app.IntergationServiceLog AS A WITH(NOLOCK) " +
                            "" +
                            "LEFT JOIN app.IntergationServiceLog AS B WITH(NOLOCK) " +
                            "ON A.UniqueID = B.UniqueID AND B.Successful = 1 AND A.ColumnName = B.ColumnName AND A.IntegrationType = B.IntegrationType AND A.CustomerCode = B.CustomerCode " +
                            " " +
                            "WHERE A.Successful = 0 " +
                            "AND A.IntegrationType = 'Charges' " +
                            "AND A.CustomerCode = '" + CustomerCode + "' " +
                            "AND B.UniqueID IS NULL ";

                        //Send alerts through DBMail
                        SendEmails.DBEmailsHighPriority(IntergrationConnectionString, AlertEmailAddresses, EmailBody, "Instegration Service - API Errors for " + CustomerCode, QueryString, "");

                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Instegration Service - Charges API Call failed for CustomerCode: " + CustomerCode + " with Total Errors: " + Results.ToString(), true);
                    }
                }
                else
                {
                    EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Successful Charges CustomerCode: " + CustomerCode + " LastQueryDate: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " (No Data to Send)", true);
                }

            }
            else
            {
                EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Instegration Service - Charges API Call is black for CustomerCode: " + CustomerCode, true);
            }


            //Turn Timer Back on
            //ChargesTimer.Enabled = true;
        }
    }
}
