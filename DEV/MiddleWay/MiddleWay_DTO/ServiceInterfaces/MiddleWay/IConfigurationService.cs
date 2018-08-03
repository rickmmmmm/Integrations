using MiddleWay_DTO.Models.MiddleWay_Controller;
using System.Collections.Generic;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
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
        string ExternalDataSourceQueryGroup { get; }
        string ExternalDataSourceQueryOrder { get; }
        string ExternalDataSourceQueryOffset { get; }
        int ReadOffset { get; }
        int ReadLimit { get; }

        ConfigurationsModel GetConfigurationByName(string name);
        string GetConfigurationValueByName(string name);
        //void ReadConfiguration();
        List<ConfigurationsModel> GetAllConfigurations();

    }
}
