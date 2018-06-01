using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Interfaces
{
    public interface IConfigurationService
    {
        string ApiKey { get; }
        string Delimiter { get; }
        string ElasticAPI { get; }
        string FromName { get; }
        string NotificationFrom { get; }
        string NotificationSentTo { get; }
        string SMSAPI { get; }
        string SqlServerDbMailProfileName { get; }
        string TextQualifier { get; }

        string TIPWebConnection { get; }

        string GetConfigurationByName(string name);
    }
}
