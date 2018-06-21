using MiddleWay_DTO.Models.MiddleWay_BLL;
using MiddleWay_DTO.RepositoryInterfaces.TIPWeb;
using MiddleWay_DTO.ServiceInterfaces.TIPWeb;
using System;
using System.Collections.Generic;
using System.Text;

namespace TIPWeb_Controller.Services
{
    public class EmailService : IEmailService
    {
        #region Private Variables

        private IEmailRepository _emailRepository;

        #endregion Private Variables

        #region Properties

        #endregion Properties

        #region Constructor

        public EmailService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        #endregion Constructor

        #region Methods

        public void Send(string profileName, EmailMessageModel email)
        {
            try
            {
                var attachment = string.IsNullOrEmpty(email.FileAttachment) ? null : email.FileAttachment;

                var recipients = string.Join(";", email.Recipients);

                _emailRepository.Send(profileName, recipients, email.Subject, email.Body, attachment);
            }
            catch
            {
                //TODO: Log this issue
            }
        }

        #endregion Methods
    }
}
