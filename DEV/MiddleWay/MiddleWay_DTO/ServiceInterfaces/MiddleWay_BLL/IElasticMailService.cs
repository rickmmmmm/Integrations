using MiddleWay_DTO.Models.MiddleWay_BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL
{
    public interface IElasticMailService
    {
        void Send(EmailMessageModel message, string apiKey, string fromName, string address);

        void SendAsync(EmailMessageModel message, string apiKey, string fromName, string address);
    }
}
