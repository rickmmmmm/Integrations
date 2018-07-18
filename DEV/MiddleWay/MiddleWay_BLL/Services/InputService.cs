using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL;
using MiddleWay_EDS.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace MiddleWay_BLL.Services
{
    public class InputService : IInputService
    {
        #region Private Variables

        //private IClientConfiguration _clientConfiguration;
        private IConfigurationService _configurationService;
        private IMappingsService _mappingsService;
        private ITransformationsService _transformationService;

        private FlatFileService fileReader;
        private ExternalDataSourceService dataReader;

        private DataSourceTypes dataSourceType;
        private string filePath;
        private string delimiter = ","; // Default delimiter is comma
        private string textQualifier = "\""; // Default qualifier is the double quote
        private string connectionString;
        private string querySelect;
        private string queryBody;
        private string queryWhere;
        private string queryOffset;
        private int total = -1;
        private int offset;
        private int limit;
        private bool isConfigured = false;

        #endregion Private Variables

        #region Properties

        //public DataSourceTypes DataSourceType
        //{
        //    get
        //    {
        //        return dataSourceType;
        //    }
        //    set
        //    {
        //        dataSourceType = value;
        //    }
        //}

        //public string SourcePath
        //{
        //    get
        //    {
        //        return filePath;
        //    }
        //    set
        //    {
        //        filePath = value;
        //    }
        //}

        //public string Delimiter
        //{
        //    get
        //    {
        //        return delimiter;
        //    }
        //    set
        //    {
        //        delimiter = value;
        //    }
        //}

        //public string TextQualifier
        //{
        //    get
        //    {
        //        return textQualifier;
        //    }
        //    set
        //    {
        //        textQualifier = value;
        //    }
        //}

        //public string Connection
        //{
        //    get
        //    {
        //        return connectionString;
        //    }
        //    set
        //    {
        //        connectionString = value;
        //    }
        //}

        //public string QuerySelect
        //{
        //    get
        //    {
        //        return querySelect;
        //    }
        //    set
        //    {
        //        querySelect = value;
        //    }
        //}

        //public string QueryBody
        //{
        //    get
        //    {
        //        return queryBody;
        //    }
        //    set
        //    {
        //        queryBody = value;
        //    }
        //}

        //public string QueryWhere
        //{
        //    get
        //    {
        //        return queryWhere;
        //    }
        //    set
        //    {
        //        queryWhere = value;
        //    }
        //}

        //public string QueryOffset
        //{
        //    get
        //    {
        //        return queryOffset;
        //    }
        //    set
        //    {
        //        queryOffset = value;
        //    }
        //}

        //public int OffSet
        //{
        //    get
        //    {
        //        return offset;
        //    }
        //    set
        //    {
        //        offset = value;
        //    }
        //}

        //public int Limit
        //{
        //    get
        //    {
        //        return limit;
        //    }
        //    set
        //    {
        //        limit = value;
        //    }
        //}

        #endregion Properties

        #region Constructor

        public InputService(IConfigurationService configurationService, IMappingsService mappingsService, ITransformationsService transformationService) //IClientConfiguration clientConfiguration, IConfigurationService configurationService
        {
            //_clientConfiguration = clientConfiguration;
            _configurationService = configurationService;
            _mappingsService = mappingsService;
            _transformationService = transformationService;

            var sourceType = (DataSourceTypes)Enum.Parse(typeof(DataSourceTypes), _configurationService.DataSourceType);

            //Configure data source
            dataSourceType = sourceType;

            if (dataSourceType == DataSourceTypes.FlatFile)
            {
                filePath = _configurationService.DataSourcePath;
                delimiter = _configurationService.Delimiter;
                textQualifier = _configurationService.TextQualifier;
            }
            else if (dataSourceType == DataSourceTypes.SQL || dataSourceType == DataSourceTypes.MySQL)
            {
                connectionString = _configurationService.ExternalDataSourceConnection;
                querySelect = _configurationService.ExternalDataSourceQuerySelect;
                queryBody = _configurationService.ExternalDataSourceQueryBody;
                queryWhere = _configurationService.ExternalDataSourceQueryWhere;
                queryOffset = _configurationService.ExternalDataSourceQueryOffset;
                offset = _configurationService.ReadOffset;
                limit = _configurationService.ReadLimit;
            }
        }

        #endregion Constructor

        #region Functions

        private bool IsConfigured()
        {
            if (!isConfigured)
            {
                switch (dataSourceType)
                {
                    case DataSourceTypes.SQL:
                    case DataSourceTypes.MySQL:
                        if (!(string.IsNullOrEmpty(connectionString) ||
                                       string.IsNullOrEmpty(querySelect) ||
                                       string.IsNullOrEmpty(queryBody) ||
                                       string.IsNullOrEmpty(queryOffset)))
                        {
                            dataReader = new ExternalDataSourceService();
                            dataReader.SetConnection(connectionString);
                            dataReader.Select = querySelect;
                            dataReader.Body = queryBody;
                            dataReader.Where = queryWhere;
                            dataReader.Offset = queryOffset;
                            dataReader.ReadOffset = offset;
                            dataReader.ReadLimit = limit;
                        }
                        else
                        {
                            isConfigured = false;
                        }
                        break;
                    case DataSourceTypes.FlatFile:
                        if (!(string.IsNullOrEmpty(filePath) ||
                                       string.IsNullOrEmpty(delimiter) ||
                                       string.IsNullOrEmpty(textQualifier)))
                        {
                            fileReader = new FlatFileService();
                            fileReader.FilePath = filePath;
                            fileReader.Delimiter = delimiter;
                            fileReader.TextQualifier = textQualifier;
                            //TODO: Add properties to configure and use Flat File Reader
                        }
                        else
                        {
                            isConfigured = false;
                        }
                        break;
                    case DataSourceTypes.Other:
                    default:
                        isConfigured = false;
                        break;
                }
            }

            return isConfigured;
        }

        public bool HasNext()
        {
            if (!IsConfigured())
            {
                throw new InvalidOperationException("The Input Service has not been configured");
            }
            else
            {
                if (total < 0)
                {
                    total = GetInputCount();
                    if (total > 0)
                    {
                        return offset < total;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return offset < total;
                }
            }
        }

        public int GetInputCount()
        {
            if (!IsConfigured())
            {
                throw new InvalidOperationException("The Input Service has not been configured");
            }
            else
            {
                //Perform Action
                switch (dataSourceType)
                {
                    case DataSourceTypes.SQL:
                    case DataSourceTypes.MySQL:
                        total = dataReader.GetCount();
                        return total;
                    case DataSourceTypes.FlatFile:
                        total = fileReader.GetCount();
                        return total;
                    case DataSourceTypes.Other:
                    default:
                        return 0;
                }
            }
        }

        public List<T> ReadInput<T>() where T : new()
        {
            if (!IsConfigured())
            {
                throw new InvalidOperationException("The Input Service has not been configured");
            }
            else
            {
                //Perform Action
                switch (dataSourceType)
                {
                    case DataSourceTypes.SQL:
                    case DataSourceTypes.MySQL:
                        //Read from DB source directly into the expected type (via dapper)
                        var batch = dataReader.ReadInput<T>();
                        return batch;
                    ////Apply tranformations
                    //var transformedBatch = _transformationService.Transform<T>(batch); //TODO; Verify this....
                    ////Cast Dynamic list to type list
                    //var result = new List<T>();
                    //foreach (var row in transformedBatch)
                    //{
                    //    var cast = (T)row;
                    //    result.Add(cast);
                    //}
                    //return result;
                    //break;
                    case DataSourceTypes.FlatFile:
                        //var batch = fileReader.ReadNext<T>();
                        //var transformedBatch = _transformationService.Transform(batch);
                        //var mappedBatch = _mappingsService.Map(transformedBatch);
                        throw new NotImplementedException();
                    //break;
                    case DataSourceTypes.Other:
                    default:
                        //return null;
                        throw new NotImplementedException();
                }
            }
        }

        public List<T> ReadNext<T>() where T : new()
        {
            if (!IsConfigured())
            {
                throw new InvalidOperationException("The Input Service has not been configured");
            }
            else
            {
                List<T> batch;
                //Perform Action
                switch (dataSourceType)
                {
                    case DataSourceTypes.SQL:
                    case DataSourceTypes.MySQL:
                        batch = dataReader.ReadNext<T>();
                        //return batch;

                        //Apply transformations
                        if (_transformationService.HasTransformations(ProcessSteps.Ingest))
                        {
                            batch = _transformationService.Transform<T>(batch, ProcessSteps.Ingest); //TODO; Verify this....
                        }

                        //Apply Mappings
                        if (_mappingsService.HasMappings(ProcessSteps.Ingest))
                        {
                            batch = _mappingsService.Map<T, T>(batch, ProcessSteps.Ingest); //TODO; Verify this....
                        }

                        //Cast Dynamic list to type list?????
                        //var result = new List<T>();
                        //foreach (var row in mappedBatch)
                        //{
                        //    var cast = (T)row;
                        //    result.Add(cast);
                        //}
                        //return result;
                        //break;

                        return batch;
                    case DataSourceTypes.FlatFile:
                        //batch = fileReader.ReadNext<T>();
                        //var transformedBatch = _transformationService.TransformToDynamic(batch, ProcessSteps.Ingest);
                        //var mappedBatch = _mappingsService.Map<ExpandoObject, T>(transformedBatch, ProcessSteps.Ingest);
                        throw new NotImplementedException();
                    //break;
                    case DataSourceTypes.Other:
                    default:
                        //return null;
                        throw new NotImplementedException();
                }
            }
        }

        public List<T> Read<T>(int offset, int limit) where T : new()
        {
            if (!IsConfigured())
            {
                throw new InvalidOperationException("The Input Service has not been configured");
            }
            else
            {
                //Perform Action
                switch (dataSourceType)
                {
                    case DataSourceTypes.SQL:
                    case DataSourceTypes.MySQL:
                        return dataReader.Read<T>(offset, limit);
                    //break;
                    case DataSourceTypes.FlatFile:
                        throw new NotImplementedException();
                    //break;
                    case DataSourceTypes.Other:
                    default:
                        //return null;
                        throw new NotImplementedException();
                }
            }
        }

        #endregion Functions
    }
}
