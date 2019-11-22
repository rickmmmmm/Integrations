using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using Data_Integration_Service.APICalls;
using Data_Integration_Service.EventLogs;


namespace Data_Integration_Service.Conversions
{
    public class DataConversions
    {
        internal static DataTable ConvertJSONToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            //strip out bad characters
            string[] jsonParts = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");

            //hold column names
            List<string> dtColumns = new List<string>();

            //get columns
            foreach (string jp in jsonParts)
            {
                //only loop thru once to get column names
                string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
                foreach (string rowData in propData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string n = rowData.Substring(0, idx - 1);
                        string v = rowData.Substring(idx + 1);
                        if (!dtColumns.Contains(n))
                        {
                            dtColumns.Add(n.Replace("\"", ""));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}" + ex, rowData));
                    }

                }
                break; // TODO: might not be correct. Was : Exit For
            }

            //build dt
            foreach (string c in dtColumns)
            {
                dt.Columns.Add(c);
            }
            //get table data
            foreach (string jp in jsonParts)
            {
                string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in propData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string n = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string v = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[n] = v;
                    }
                    catch (Exception ex)
                    {
                        continue;
                        throw new Exception(string.Format("Error Parsing Column Name : {0}" + ex, rowData));
                    }

                }
                dt.Rows.Add(nr);
            }
            return dt;
        }

        public static string ConvertToChargesPostAPICall(string URL, DataTable ChargeData, string CustomerCode)
        {
            string Results = "Succesful";
            string Payload = "";
            string ChargeUID;
            string StudentID;
            string CampusID;
            string ChargeTypeName;
            string ChargeAmount;
            string ChargeDate;
            string ChargeComments;
            string LoginName;


            for (int i = 0; i < ChargeData.Rows.Count; i++)
            {
                ChargeUID = ChargeData.Rows[i]["ChargeUID"].ToString();
                StudentID = ChargeData.Rows[i]["StudentID"].ToString();
                CampusID = ChargeData.Rows[i]["CampusID"].ToString();
                ChargeTypeName = ChargeData.Rows[i]["CreateDate"].ToString();
                ChargeAmount = ChargeData.Rows[i]["ItemCode"].ToString();
                ChargeComments = ChargeData.Rows[i]["TransactionComments"].ToString();
                LoginName = ChargeData.Rows[i]["LoginName"].ToString();
                ChargeDate = ChargeData.Rows[i]["CreateDate"].ToString();

                if (CustomerCode == "CPS")
                {
                    ChargeDate = ChargeData.Rows[i]["CreateDateUTC"].ToString().Replace(" ","T") + "Z";

                    Payload = @"{
                        feeReference: " + ChargeUID + 
                        ",studentId: " + StudentID + 
                        ",schoolId: " + CampusID + 
                        ",itemCode: " + ChargeTypeName + 
                        ",transactionDate: " + ChargeDate + 
                        ",transactionQuantity: 1" +
                        ",transactionAmount: " + ChargeAmount + 
                        ", unitPrice: " + ChargeAmount + 
                        ",comment: " + ChargeComments + 
                        ",paymentDueDate: " + ChargeDate + 
                        ",feeEntryUser: " + LoginName + 
                        "} ";
                }

                try
                {
                    APICharges.PostAPICall(URL, Payload);

                    //Log IDs that where sent
                    EventLogWrite.WriteToIntegrationLog(Convert.ToInt32(ChargeUID),"ChargeUID","Charges", CustomerCode);
                }
                catch
                {
                    Results = "Failure";
                }                
            }

            return Results;
        }
    }
}
