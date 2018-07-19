using MiddleWay_DTO.Models.MiddleWay_BLL;
using MiddleWay_DTO.RepositoryInterfaces.TIPWeb;
using MiddleWay_DTO.ServiceInterfaces.TIPWeb;

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

                _emailRepository.Send(profileName, email.Recipients, email.Subject, email.Body, attachment);
            }
            catch
            {
                //TODO: Log this issue
                throw;
            }
        }

        #endregion Methods
    }
}
