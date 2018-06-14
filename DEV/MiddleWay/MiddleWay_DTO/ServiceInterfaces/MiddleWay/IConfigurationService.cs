using MiddleWay_DTO.MiddleWay_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.ServiceInterfaces
{
    public interface IConfigurationService
    {
        bool HasConfiguration { get; }

        string NotificationType { get; }
        string ElasticEmailApiKey { get; }
        string ElasticEmailAPIUrl { get; }
        string ElasticSMSAPIUrl { get; }

        
        string Delimiter { get; }
        string TextQualifier { get; }
        string FromName { get; }
        string NotificationFrom { get; }
        string NotificationSentTo { get; }
        string SqlServerDbMailProfileName { get; }
        string TIPWebConnection { get; }
        string DataSourceType { get; }
        string DataSourcePath { get; }
        string ExternalDataSourceConnection { get; }
        string ReadBatchSize { get; }
        string ExternalDataSourceQuerySelect { get; }
        string ExternalDataSourceQueryBody { get; }
        string ExternalDataSourceQueryWhere { get; }

        ConfigurationsModel GetConfigurationByName(string name);
        string GetConfigurationValueByName(string name);

        //void ReadConfiguration();

    }
}
