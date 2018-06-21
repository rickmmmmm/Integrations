using MiddleWay_DTO.Models.MiddleWay_BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.TIPWeb
{
    public interface IEmailService
    {
        void Send(string profileName, EmailMessageModel email);
    }
}
