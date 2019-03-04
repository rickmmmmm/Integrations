using System;
using System.IO;
using System.Configuration;

namespace MiddleWay_Utilities
{
    public class Logging
    {
        public static bool WriteToLog(string message, string processTaskName, int processTaskID = 0, string clientCode = "", string messageType = "")
        {
            // MessageType Should be (Informational, Warning, Successful, Failure)

            // Set to defaults if not provided
            if (String.IsNullOrEmpty(clientCode))
                clientCode = "MiddleWay";

            if (String.IsNullOrEmpty(messageType))
                messageType = "Informational";

            //if (String.IsNullOrEmpty(processTaskID.ToString()))
            //    processTaskID = 0;

            if (String.IsNullOrEmpty(processTaskName))
                processTaskName = "Default Process Name";

            if (String.IsNullOrEmpty(message))
                message = "Default Message - No Message Provided";

            //string LogFilePath = "C:\\Temp\\Logs";  // For Testing only
            string LogFilePath = Directory.GetCurrentDirectory() + "\\Logs\\";

            if (String.IsNullOrEmpty(LogFilePath))
                LogFilePath = "C:\\Temp\\Logs\\";

            if (LogFilePath.Substring(LogFilePath.Length - 1, 1) != "\\")
            {
                LogFilePath = LogFilePath + "\\";
            }

            string LogFileDate = System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Year.ToString("0000");
            string LogFileName = clientCode + "_" + LogFileDate + ".log";

            string LogEntry = "";

            // Cleanup message here to remove crt returns, line feeds, and tabs
            message=Utilities.CleanupHiddenCharacters(message);

            if (!File.Exists(LogFilePath))
            {
                Directory.CreateDirectory(LogFilePath);
            }


            if (File.Exists(LogFilePath + LogFileName))
            {
                LogEntry = System.DateTime.Now.ToString() + "|" + processTaskID.ToString() + "|" + processTaskName + "|" + messageType + "|" + message;
            }
            else
            {
                LogEntry = System.DateTime.Now.ToString() + "|0|Starting|Informational|File Creation" + System.Environment.NewLine;
                LogEntry = LogEntry + System.DateTime.Now.ToString() + "|" + processTaskID.ToString() + "|" + processTaskName + "|" + messageType + "|" + message;
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

    public class LogFileCleanup
    {
        public static void RemoveOldFiles()
        {
            string LogFilePath = Directory.GetCurrentDirectory() + "\\Logs\\";

            Int32 LogRetention = 30 * -1;
            string[] files = Directory.GetFiles(LogFilePath);
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (File.GetCreationTime(fi.ToString()) < DateTime.Now.AddDays(LogRetention))
                    fi.Delete();
            }
        }
    }
}
