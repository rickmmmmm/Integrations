using MiddleWay_DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces
{
    public interface IElasticMailService
    {
        void send(EmailMessageModel message, string apiKey, string fromName, string address);

        void sendAsync(EmailMessageModel message, string apiKey, string fromName, string address);
    }
}
