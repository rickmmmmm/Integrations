using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.RepositoryInterfaces
{
    public interface IEmailRepository
    {
        void sendEmail(string profileName, string recipients, string subject, string body, string Aattachment = null);
    }
}
