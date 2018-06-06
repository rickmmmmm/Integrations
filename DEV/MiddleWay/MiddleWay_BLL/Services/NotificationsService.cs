using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleWay_DTO.Models;
//using DataAccess;
using System.Configuration;
using MiddleWay_DTO.RepositoryInterfaces;
using MiddleWay_DTO.ServiceInterfaces;
using MiddleWay_Controller.Interfaces;

namespace MiddleWay_BLL.Services
{
    public class NotificationsService : INotificationsService
    {
        #region Private Variables and Properties

        private IConfigurationService _configurationService;
        private IElasticMailService _elasticMailService;

        private IEmailRepository _emailRepository;

        #endregion Private Variables and Properties

        #region Constructor

        public NotificationsService(IEmailRepository emailRepository, IConfigurationService configurationService, IElasticMailService elasticMailService)
        {
            _configurationService = configurationService;
            _elasticMailService = elasticMailService;
            _emailRepository = emailRepository;
        }

        #endregion Constructor

        #region Send Functions

        public void send(EmailMessageModel message)
        {
            //    _emailRepository.sendEmail(ConfigurationManager.AppSettings["SqlServerDbMailProfileName"], message.Receivers.First(), message.Subject, message.Body);

            if (!string.IsNullOrEmpty(_configurationService.SqlServerDbMailProfileName))
            {
                var mailProfileName = _configurationService.SqlServerDbMailProfileName;

                var attachment = string.IsNullOrEmpty(message.FileAttachment) ? null : message.FileAttachment;

                _emailRepository.sendEmail(mailProfileName, string.Join(";", message.Recipients), message.Subject, message.Body, attachment);

            }
            else if (!string.IsNullOrEmpty(_configurationService.ElasticAPI) && !string.IsNullOrEmpty(_configurationService.ApiKey))
            {
                var apiKey = _configurationService.ApiKey;
                var address = _configurationService.ElasticAPI;
                var fromName = _configurationService.FromName;
                //var attachment = string.IsNullOrEmpty(message.FileAttachment) ? null : message.FileAttachment;

                _elasticMailService.send(message, apiKey, fromName, address);

            }
        }

        public void sendAsync(EmailMessageModel message)
        {
            throw new NotImplementedException();
        }

        #endregion Send Functions
    }
}
