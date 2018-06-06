using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Interfaces
{
    public interface IConfigurationService
    {
        bool IsConfigurationLoaded { get; }
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
        string DataSource { get; }
        string DataSourcePath { get; }
        string ExternalDataSourceConnection { get; }
        string ReadBatchSize { get; }
        string ExternalDataSourceQuerySelect { get; }
        string ExternalDataSourceQueryBody { get; }
        string ExternalDataSourceQueryWhere { get; }

        string GetConfigurationByName(string name);

        void ReadConfiguration();
    }
}
