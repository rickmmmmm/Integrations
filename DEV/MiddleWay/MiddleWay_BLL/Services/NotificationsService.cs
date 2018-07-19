using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay_BLL;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
//using DataAccess;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL;
using MiddleWay_DTO.ServiceInterfaces.TIPWeb;
using System;

namespace MiddleWay_BLL.Services
{
    public class NotificationsService : INotificationsService
    {
        #region Private Variables and Properties

        private IConfigurationService _configurationService;
        private IElasticMailService _elasticMailService;

        private IEmailService _emailService;

        #endregion Private Variables and Properties

        #region Constructor

        public NotificationsService(IEmailService emailService, IConfigurationService configurationService, IElasticMailService elasticMailService)
        {
            _configurationService = configurationService;
            _elasticMailService = elasticMailService;
            _emailService = emailService;
        }

        #endregion Constructor

        #region Send Functions

        public void Send(MessageModel message)
        {
            //    _emailRepository.sendEmail(ConfigurationManager.AppSettings["SqlServerDbMailProfileName"], message.Receivers.First(), message.Subject, message.Body);
            NotificationType notificationType;

            if (Enum.TryParse(_configurationService.NotificationType, out notificationType))
            {
                switch (notificationType)
                {
                    case NotificationType.Email:

                        var email = (EmailMessageModel)message;

                        if (email != null)
                        {
                            email.Recipients = _configurationService.NotificationSentTo;

                            if (!string.IsNullOrEmpty(_configurationService.SqlServerDbMailProfileName))
                            {
                                var mailProfileName = _configurationService.SqlServerDbMailProfileName;

                                _emailService.Send(mailProfileName, email);

                            }
                            else if (!string.IsNullOrEmpty(_configurationService.ElasticEmailAPIUrl) && !string.IsNullOrEmpty(_configurationService.ElasticEmailApiKey))
                            {
                                var apiKey = _configurationService.ElasticEmailApiKey;
                                var address = _configurationService.ElasticEmailAPIUrl;
                                var fromName = _configurationService.FromName;
                                //var attachment = string.IsNullOrEmpty(email.FileAttachment) ? null : email.FileAttachment;

                                _elasticMailService.Send(email, apiKey, fromName, address);

                            }
                        }
                        break;
                    case NotificationType.Sms:
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public void SendAsync(MessageModel message)
        {
            throw new NotImplementedException();
        }

        #endregion Send Functions
    }
}
