using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using Data_Integration_Service.EventLogs;

namespace Data_Integration_Service.Alerts
{
    class AlertMessaging
    {
        public static void SMTPEmailsHighPriority (string SMTPServer, int SMTPPort, string EmailTo, string EmailFrom, string EMailBody, string EmailSubject)
        {
                //string SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPHost"].ToString();
                //int SMTPPort = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]);
                //string EmailTo = System.Configuration.ConfigurationManager.AppSettings["EmailToAddressForSummary"].ToString();
                //string EmailFrom = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddressForSummary"].ToString();
                //string EMailBody = Environment.NewLine;

                //DataTable EmailBodyTB = ProcessDeclinedPayments.DeclinePaymentsEmail(connString);

                //for (int i = 0; i < EmailBodyTB.Rows.Count; i++)
                //{
                //    EMailBody = EMailBody + EmailBodyTB.Rows[i]["EmailBody"].ToString() + Environment.NewLine;
                //}

                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(SMTPServer);
                    SmtpServer.Port = SMTPPort;

                    mail.From = new MailAddress(EmailFrom);
                    mail.To.Add(EmailTo);
                    mail.Priority = MailPriority.High;
                    mail.Subject = EmailSubject;
                    mail.Body = EMailBody;

                    SmtpServer.Send(mail);
                    EventLogWrite.WriteApplicationLogErrorEvent("Sending Email - Email Message Sent", true);
                }
                catch (Exception e)
                {
                    EventLogWrite.WriteApplicationLogErrorEvent("Sending Email - Failed while sending Summary Email Error Message (" + e + ")", true);
                }
        }

        public static void SMTPEmailsNormalPriority(string SMTPServer, int SMTPPort, string EmailTo, string EmailFrom, string EMailBody, string EmailSubject)
        {
            //string SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPHost"].ToString();
            //int SMTPPort = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]);
            //string EmailTo = System.Configuration.ConfigurationManager.AppSettings["EmailToAddressForSummary"].ToString();
            //string EmailFrom = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddressForSummary"].ToString();
            //string EMailBody = Environment.NewLine;

            //DataTable EmailBodyTB = ProcessDeclinedPayments.DeclinePaymentsEmail(connString);

            //for (int i = 0; i < EmailBodyTB.Rows.Count; i++)
            //{
            //    EMailBody = EMailBody + EmailBodyTB.Rows[i]["EmailBody"].ToString() + Environment.NewLine;
            //}

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SMTPServer);
                SmtpServer.Port = SMTPPort;

                mail.From = new MailAddress(EmailFrom);
                mail.To.Add(EmailTo);
                mail.Priority = MailPriority.Normal;
                mail.Subject = EmailSubject;
                mail.Body = EMailBody;

                SmtpServer.Send(mail);
                EventLogWrite.WriteApplicationLogErrorEvent("Sending Email - Email Message Sent", true);
            }
            catch (Exception e)
            {
                EventLogWrite.WriteApplicationLogErrorEvent("Sending Email - Failed while sending Summary Email Error Message (" + e + ")", true);
            }
            
        }

        public static void SMTPEmailsLowPriority(string SMTPServer, int SMTPPort, string EmailTo, string EmailFrom, string EMailBody, string EmailSubject)
        {
            //string SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPHost"].ToString();
            //int SMTPPort = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]);
            //string EmailTo = System.Configuration.ConfigurationManager.AppSettings["EmailToAddressForSummary"].ToString();
            //string EmailFrom = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddressForSummary"].ToString();
            //string EMailBody = Environment.NewLine;

            //DataTable EmailBodyTB = ProcessDeclinedPayments.DeclinePaymentsEmail(connString);

            //for (int i = 0; i < EmailBodyTB.Rows.Count; i++)
            //{
            //    EMailBody = EMailBody + EmailBodyTB.Rows[i]["EmailBody"].ToString() + Environment.NewLine;
            //}

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SMTPServer);
                SmtpServer.Port = SMTPPort;

                mail.From = new MailAddress(EmailFrom);
                mail.To.Add(EmailTo);
                mail.Priority = MailPriority.Low;
                mail.Subject = EmailSubject;
                mail.Body = EMailBody;

                SmtpServer.Send(mail);
                EventLogWrite.WriteApplicationLogErrorEvent("Sending Email - Email Message Sent", true);
            }
            catch (Exception e)
            {
                EventLogWrite.WriteApplicationLogErrorEvent("Sending Email - Failed while sending Summary Email Error Message (" + e + ")", true);
            }

        }

        public static void DBEmailsHighPriority(string SQLConnectionString, string EmailTo, string EmailFrom, string EMailBody, string EmailSubject)
        {
            try
            {
                EventLogWrite.WriteApplicationLogErrorEvent("Sending Email - Email Message Sent", true);
            }
            catch (Exception e)
            {
                EventLogWrite.WriteApplicationLogErrorEvent("Sending Email - Failed while sending Summary Email Error Message (" + e + ")", true);
            }
        }

        public static void DBEmailsNormalPriority(string SQLConnectionString, string EmailTo, string EmailFrom, string EMailBody, string EmailSubject)
        {
            try
            {
                EventLogWrite.WriteApplicationLogErrorEvent("Sending Email - Email Message Sent", true);
            }
            catch (Exception e)
            {
                EventLogWrite.WriteApplicationLogErrorEvent("Sending Email - Failed while sending Summary Email Error Message (" + e + ")", true);
            }
        }

        public static void DBEmailsLowPriority(string SQLConnectionString, string EmailTo, string EmailFrom, string EMailBody, string EmailSubject)
        {
            try
            {
                EventLogWrite.WriteApplicationLogErrorEvent("Sending Email - Email Message Sent", true);
            }
            catch (Exception e)
            {
                EventLogWrite.WriteApplicationLogErrorEvent("Sending Email - Failed while sending Summary Email Error Message (" + e + ")", true);
            }
        }
    }
}
