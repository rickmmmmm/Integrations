using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleWay_DTO.Models;
using DataAccess;
using System.Configuration;
using MiddleWay_DAL.Repositories;
using MiddleWay_DTO.RepositoryInterfaces;
using MiddleWay_DTO.ServiceInterfaces;

namespace Services
{
    public class SqlDbMailService : ISender
    {
        #region Private Variables and Properties

        private IEmailRepository _emailRepository;

        #endregion Private Variables and Properties

        #region Constructor

        public SqlDbMailService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        #endregion Constructor

        #region Send Functions

        public void send(MessageModel message)
        {
            //    _emailRepository.sendEmail(ConfigurationManager.AppSettings["SqlServerDbMailProfileName"], message.Receivers.First(), message.Subject, message.Body);
            //}

            //public void send(EmailMessageModel message)
            //{
            var mailProfileName = ConfigurationManager.AppSettings["SqlServerDbMailProfileName"];

            if (message is EmailMessageModel)
            {
                var emailMessage = (EmailMessageModel)message;
                var attachment = string.IsNullOrEmpty(emailMessage.FileAttachment) ? null : emailMessage.FileAttachment;

                _emailRepository.sendEmail(mailProfileName, string.Join(";", emailMessage.Recipients), emailMessage.Subject, message.Body, attachment);
            }
        }

        public void sendAsync(MessageModel message)
        {
            throw new NotImplementedException();
        }

        #endregion Send Functions
    }
}
