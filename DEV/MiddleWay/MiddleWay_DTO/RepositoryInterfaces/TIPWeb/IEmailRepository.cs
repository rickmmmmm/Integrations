using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.RepositoryInterfaces.TIPWeb
{
    public interface IEmailRepository
    {
        void Send(string profileName, string recipients, string subject, string body, string Aattachment = null);
    }
}
