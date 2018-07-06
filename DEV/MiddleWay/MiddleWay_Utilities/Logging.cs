using System;
using System.IO;

namespace MiddleWay_Utilities
{
    public class Logging
    {
        public static bool WriteToLog(string ClientCode, string MessageType, string Message, int ProcessTaskID, string ProcessTaskName)
        {
            // MessageType Should be (Informational, Warning, Successful, Failure)

            // Set to defaults if not provided
            if (String.IsNullOrEmpty(ClientCode))
                ClientCode = "MiddleWay";

            if (String.IsNullOrEmpty(MessageType))
                MessageType = "Informational";

            if (String.IsNullOrEmpty(ProcessTaskID.ToString()))
                ProcessTaskID = 0;

            if (String.IsNullOrEmpty(ProcessTaskName))
                ProcessTaskName = "Default Process Name";

            if (String.IsNullOrEmpty(Message))
                Message = "Default Message - No Message Provided";

            //string LogFilePath = "C:\\Temp\\Logs";  // For Testing only
            string LogFilePath = Directory.GetCurrentDirectory() + "\\Logs\\";

            if (String.IsNullOrEmpty(LogFilePath))
                LogFilePath = "C:\\Temp\\Logs\\";

            if (LogFilePath.Substring(LogFilePath.Length - 1, 1) != "\\")
            {
                LogFilePath = LogFilePath + "\\";
            }

            string LogFileDate = System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Year.ToString("0000");
            string LogFileName = ClientCode + "_" + LogFileDate + ".log";

            string LogEntry = "";

            // Cleanup message here to remove crt returns, line feeds, and tabs

            if (!File.Exists(LogFilePath))
            {
                Directory.CreateDirectory(LogFilePath);
            }


            if (File.Exists(LogFilePath + LogFileName))
            {
                LogEntry = System.DateTime.Now.ToString() + "|" + ProcessTaskID.ToString() + "|" + ProcessTaskName + "|" + MessageType + "|" + Message;
            }
            else
            {
                LogEntry = System.DateTime.Now.ToString() + "|0|Starting|Informational|File Creation" + System.Environment.NewLine;
                LogEntry = LogEntry + System.DateTime.Now.ToString() + "|" + ProcessTaskID.ToString() + "|" + ProcessTaskName + "|" + MessageType + "|" + Message;
            }

            try
            {
                FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", LogFilePath, LogFileName), FileMode.Append, FileAccess.Write);

                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(LogEntry);

                objStreamWriter.Close();
                objFilestream.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
