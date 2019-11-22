using System.ServiceProcess;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Data_Integration_Service.DAL;

namespace Data_Integration_Service
{
    public partial class Service : ServiceBase
    {
        private static Timer ChargesTimer;
        private static Timer DataConversionITTimer;
        private static Timer DataConversionIMTimer;

        string DataConverionConnectionString = ConfigurationManager.ConnectionStrings["DC"].ToString();
        string IntergrationConnectionString = ConfigurationManager.ConnectionStrings["Intergration"].ToString();
        string TIPWebITConnectionString = ConfigurationManager.ConnectionStrings["TIPWeb-IT"].ToString();

        public Service()
        {
            InitializeComponent();

            ChargesTimer.Elapsed += new ElapsedEventHandler(GetCharges); 
        }

        protected override void OnStart(string[] args)
        {
            double DefaultTimer = 300000;
            EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Starting", true);

            // Charges timer
            ChargesTimer = new Timer(IntergrationMiddlway.GetTimerCharges(IntergrationConnectionString));
            ChargesTimer.Enabled = true;
            ChargesTimer.Elapsed += GetCharges;
            
            // TIPWeb-IT Data Conversions
            DataConversionITTimer = new Timer(DefaultTimer);
            ChargesTimer.Enabled = true;
            ChargesTimer.Elapsed += GetITDataConversion;

            // TIPWeb-IM Data Conversions
            DataConversionIMTimer = new Timer(DefaultTimer);
            ChargesTimer.Enabled = true;
            ChargesTimer.Elapsed += GetIMDataConversion;
        }

        protected override void OnStop()
        {
            EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Stopping",true);
        }

        public void GetCharges(object sender, System.Timers.ElapsedEventArgs args)
        {
            string Result = "";
            string APIURL = "";
            string CustomerCode = "CPS";
            string LastQueryDate = "";
            DataTable ChargesData = new DataTable();

            ChargesTimer.Enabled = false;
            EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Starting Charges Process", true);

            //Getting Last Date that Process Ran
            LastQueryDate = IntergrationMiddlway.GetChargesCustomerLastQueryDate(IntergrationConnectionString, "CPS");

            //Get API URL for Customer
            APIURL = IntergrationMiddlway.GetChargesCustomerAPI(IntergrationConnectionString, "CPS");

            //Set TIPWeb-IT Connection String
            TIPWebITConnectionString = TIPWebITConnectionString.Replace("TIPWebITDBNameHere", IntergrationMiddlway.GetChargesCustomerTIPWebITDatabase(IntergrationConnectionString, "CPS"));

            //Getting Charges Data
            if (APIURL != "" && TIPWebITConnectionString != "")
            {
                ChargesData = IntergrationMiddlway.GetCharges(TIPWebITConnectionString, CustomerCode, LastQueryDate);

                if (ChargesData.Rows.Count > 0)
                {
                    Result = Conversions.DataConversions.ConvertToChargesPostAPICall(APIURL, ChargesData, CustomerCode);

                    if (Result == "Succesful")
                    {
                        EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Successful Charges CustomerCode: " + CustomerCode + " LastQueryDate: " + ChargesData.Rows[0]["QueryDateTime"].ToString() + " (Total Charges Proccessed: " + ChargesData.Rows.Count.ToString() + ")", true);
                    }
                    else
                    {
                        EventLogs.EventLogWrite.WriteApplicationLogErrorEvent("Instegration Service - Charges API Call failed for CustomerCode: " + CustomerCode, true);
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
            ChargesTimer.Enabled = true;
        }

        public void GetITDataConversion(object sender, System.Timers.ElapsedEventArgs args)
        {
            EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Starting IT Data Conversion Process", true);
            Data_Integration_Service.Conversions.DataConversionsIT.GetITDataConversionsToProcess(DataConverionConnectionString, TIPWebITConnectionString);
        }

        public void GetIMDataConversion(object sender, System.Timers.ElapsedEventArgs args)
        {
            EventLogs.EventLogWrite.WriteApplicationLogInformatonEvent("Instegration Service - Starting IM Data Conversion Process", true);
        }
    }
}
