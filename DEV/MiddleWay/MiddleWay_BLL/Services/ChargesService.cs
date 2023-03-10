using MiddleWay_DTO.Models.MiddleWay_BLL;
using MiddleWay_DTO.RepositoryInterfaces.TIPWeb;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL;
using MiddleWay_DTO.View_Models;
using System;
using System.Collections.Generic;

namespace MiddleWay_BLL.Services
{
    public class ChargesService : IChargesService
    {
        #region Private Variables and Properties

        private IConfigurationService _configurationService;
        private INotificationsService _notificationService;

        private IChargesRepository _chargesRepository;

        #endregion Private Variables and Properties

        #region Constructor

        public ChargesService(IChargesRepository chargesRepository, IConfigurationService configurationService, INotificationsService notificationService)
        {
            _chargesRepository = chargesRepository;
            _configurationService = configurationService;
            _notificationService = notificationService;
        }

        #endregion Constructor

        #region Get Functions

        #endregion Get Functions

        #region Add Functions


        #endregion Add Functions

        #region Change Functions

        public void ProcessCharges(string importFileName)
        {
            //var paymentsFileData = ft.serializeChargePaymentsFile(importFileName);

            //var paymentData = map.mapPaymentDetails(paymentsFileData);

            //var paymentsToProcess = paymentData.Where(u => !u.Void).ToList();
            //var chargesToVoid = paymentData.Where(u => u.Void).ToList();

            //_repo.voidCharges(chargesToVoid);
            //_repo.insertPaymentDetails(paymentsToProcess);

            //string bodyMovement = "<!DOCTYPE html>  <html> <body>     <div>         <h1>Hayes Software Systems</h1>         <h4 style=\"padding-bottom:20px;\">Automatic Notification from Hayes Software Systems</h4>     </div>     <div style=\"margin-left:5%;\">         <p>Data import successful!</p>         <ul style=\"list-style:none;\">               <li>Records Processed: {0}</li>       </ul>     </div>     <div style=\"margin-left:3%;\">  <p> Please do not reply to this email.If you have any questions or concerns, please contact Dan Cathcart at dcathcart@hayessoft.com </p>          <p> Have a wonderful day,</p>         <p> The Hayes Software Team </p> </div> </body> </html> ";

            //string body = string.Format(bodyMovement, paymentData.Count);

            //SqlDbMailService mailer = new SqlDbMailService(_repo);
            var notification = new EmailMessageModel
            {
                //Body = body,
                Recipients = _configurationService.NotificationSentTo, // ConfigurationManager.AppSettings["notificationSentTo"].Split(',').ToList(),
                Sender = _configurationService.NotificationFrom, // ConfigurationManager.AppSettings["notificationFrom"],
                Subject = "Automatic Notification from Hayes Software Systems",
                SentDate = DateTime.Now
            };

            _notificationService.Send(notification);

            //ft.archiveFile(importFileName);
            //_repo.completeIntegration();
        }

        #endregion Change Functions

        #region Remove Functions

        #endregion Remove Functions

        #region Export Functions

        public void createExportFile(List<ChargeExportFile> results, string fileName)
        {
            //using (StreamWriter writer = File.AppendText(fileName))
            //{
            //    var csv = new CsvWriter(writer);
            //    csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];
            //    //csv.Configuration.IgnoreQuotes = true;
            //    csv.Configuration.HasHeaderRecord = false;
            //    //csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
            //    //csv.Configuration.QuoteAllFields = true;

            //    csv.WriteRecords(results);
            //}
        }


        #endregion Export Functions
    }
}
