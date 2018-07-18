using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.Models.MiddleWay_BLL;
using MiddleWay_DTO.RepositoryInterfaces;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL;
using MiddleWay_Utilities;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace MiddleWay_BLL.Services
{
    public class AssetsService : IAssetsService
    {
        #region Private Variables and Properties

        private INotificationsService _notificationService;
        private IConfigurationService _configurationService;
        private IInventoryFlatDataService _inventoryFlatService;
        private IProcessesService _processesService;
        private IProcessTasksService _processTasksService;
        private IProcessTaskStepsService _processTaskStepsService;
        private IProcessTaskErrorsService _processTaskErrorsService;
        private IMappingsService _mappingsService;
        private ITransformationsService _transformationService;
        private IInputService _inputService;
        private IEtlInventoryService _etlInventoryService;

        #endregion Private Variables and Properties

        #region Constructor

        public AssetsService(INotificationsService notificationService, IConfigurationService configurationService,
                             IInventoryFlatDataService inventoryFlatService, IProcessesService processesService,
                             IProcessTasksService processTasksService, IProcessTaskStepsService processTaskStepsService,
                             IProcessTaskErrorsService processTaskErrorsService, IInputService inputService,
                             ITransformationsService transformationService, IMappingsService mappingsService,
                             IEtlInventoryService etlInventoryService)
        {
            _notificationService = notificationService;
            _configurationService = configurationService;
            _inventoryFlatService = inventoryFlatService;
            _processesService = processesService;
            _processTasksService = processTasksService;
            _processTaskStepsService = processTaskStepsService;
            _processTaskErrorsService = processTaskErrorsService;
            _mappingsService = mappingsService;
            _transformationService = transformationService;
            _inputService = inputService;
            _etlInventoryService = etlInventoryService;
        }

        #endregion Constructor

        #region Get Functions

        public void ProcessAssets(List<string> commands, string parameters = null)
        {
            var taskStepUid = 0;
            try
            {
                // Start Process Task
                var processTaskUid = _processTasksService.StartProcessTask(parameters);
                if (processTaskUid > 0)
                { // Create record, keep uid

                    taskStepUid = _processTaskStepsService.BeginTaskStep(processTaskUid, ProcessSteps.CleanUp);

                    // Send start process email
                    var message = new EmailMessageModel();
                    message.Body = "Test";
                    message.Subject = "Test";
                    _notificationService.Send(message);

                    //Delete flat table records for process
                    _inventoryFlatService.ClearData();

                    // Read configuration for reading input
                    //var dataSourceType = (DataSourceTypes)Enum.Parse(typeof(DataSourceTypes), _configurationService.DataSourceType);

                    ////Configure data source
                    ////TODO: Move this logic higher up the code stack to allow all services to have it
                    //_inputService.DataSourceType = dataSourceType;

                    //if (dataSourceType == DataSourceTypes.FlatFile)
                    //{
                    //    _inputService.SourcePath = _configurationService.DataSourcePath;
                    //    _inputService.Delimiter = _configurationService.Delimiter;
                    //    _inputService.TextQualifier = _configurationService.TextQualifier;
                    //}
                    //else if (dataSourceType == DataSourceTypes.SQL || dataSourceType == DataSourceTypes.MySQL)
                    //{
                    //    _inputService.Connection = _configurationService.ExternalDataSourceConnection;
                    //    _inputService.QuerySelect = _configurationService.ExternalDataSourceQuerySelect;
                    //    _inputService.QueryBody = _configurationService.ExternalDataSourceQueryBody;
                    //    _inputService.QueryWhere = _configurationService.ExternalDataSourceQueryWhere;
                    //    _inputService.QueryOffset = _configurationService.ExternalDataSourceQueryOffset;
                    //    var offsetString = _configurationService.ReadOffset;
                    //    int offset = 0;
                    //    if (!string.IsNullOrEmpty(offsetString) && Int32.TryParse(offsetString, out offset))
                    //    {
                    //        _inputService.OffSet = offset;
                    //    }
                    //    var limitString = _configurationService.ReadLimit;
                    //    int limit = 5000;
                    //    if (!string.IsNullOrEmpty(limitString) && Int32.TryParse(limitString, out limit))
                    //    {
                    //        _inputService.Limit = limit;
                    //    }
                    //}

                    _processTaskStepsService.EndTaskStep(taskStepUid, true);
                    taskStepUid = _processTaskStepsService.BeginTaskStep(processTaskUid, ProcessSteps.Ingest);

                    //Read data from source (in batches) - Read Input (Loop)
                    var total = _inputService.GetInputCount();
                    if (total > 0)
                    {
                        var rowCount = 1;
                        //  PER BATCH
                        //      Apply mappings and move to flat tables, catch and store errors
                        //      Apply transformations and move to stage tables, catch and store errors
                        while (_inputService.HasNext())
                        {
                            var batch = _inputService.ReadNext<InventoryFlatDataModel>();

                            batch.ForEach(row => row.RowId = rowCount++);

                            //var mappedBatch = _mappingsService.Map(batch);
                            //var transformedBatch = _transformationService.Transform(mappedBatch);

                            _inventoryFlatService.AddRange(batch);
                        }

                        _processTaskStepsService.EndTaskStep(taskStepUid, true);
                        taskStepUid = _processTaskStepsService.BeginTaskStep(processTaskUid, ProcessSteps.Stage);

                        var flatCount = _inventoryFlatService.GetTotal();
                        var limit = _configurationService.ReadLimit;
                        var currentCount = 0;
                        //Perform flat to ETL table mappings, transformations then insert (in a loop)
                        while (currentCount < flatCount)
                        {
                            var flatData = _inventoryFlatService.Get(currentCount, limit);

                            //TODO: Log count of flatdata records returned

                            var transformedData = _transformationService.TransformToDynamic(flatData, ProcessSteps.Ingest);
                            //TODO: Log count of transformed records returned

                            var mappedData = _mappingsService.Map<ExpandoObject, EtlInventoryModel>(transformedData, ProcessSteps.Ingest);
                            //TODO: Log count of mapped records returned

                            //if (!_etlInventoryService.AddRange(transformedData))
                            if (!_etlInventoryService.AddRange(mappedData))
                            {
                                //TODO: Log Error
                            }
                            currentCount += limit;
                        }

                        _processTaskStepsService.EndTaskStep(taskStepUid, true);

                        return;

                        taskStepUid = _processTaskStepsService.BeginTaskStep(processTaskUid, ProcessSteps.ProcessCommands);

                        // Process options
                        var options = ProcessInput.ReadOptions(commands);

                        // if MergeProducts - find similar products and merge to single product
                        for (var i = 0; i < options.Count; i++)
                        {
                            switch (options[i])
                            {
                                case "MergeProducts":
                                    //Use transformation lookup to convert possibles to a single result?
                                    break;
                                default:
                                    break;
                            }
                        }

                        _processTaskStepsService.EndTaskStep(taskStepUid, true);
                        taskStepUid = _processTaskStepsService.BeginTaskStep(processTaskUid, ProcessSteps.Validate);

                        // Validate ETLInventory data on TIPWeb
                        var validationPassed = _etlInventoryService.ValidateEtlInventory();

                        _processTaskStepsService.EndTaskStep(taskStepUid, validationPassed);

                        if (validationPassed)
                        {
                            taskStepUid = _processTaskStepsService.BeginTaskStep(processTaskUid, ProcessSteps.Upload);

                            // Submit valid ETLInventory data to TIPWeb
                            //validationPassed = _etlInventoryService.SubmitEtlInventory();

                            _processTaskStepsService.EndTaskStep(taskStepUid, validationPassed);
                        }
                    }
                    else
                    {
                        //TODO: No data to read, log it
                        _processTaskStepsService.EndTaskStep(taskStepUid, false); //TODO: Is this a fail??
                    }

                    // End ProcessTask
                    _processTasksService.EndProcessTask(true);
                }
                else
                {
                    //TODO: Cannot create Process Task, log issue
                }
            }
            catch
            {
                if (taskStepUid > 0)
                {
                    _processTaskStepsService.EndTaskStep(taskStepUid, false);
                }
                //TODO: Log Error
                throw;
            }
        }

        #endregion Get Functions

        #region Add Functions

        #endregion Add Functions

        #region Change Functions

        public void updateFixedAssetIds()
        {
            //_inventoryRepository.updateFixedAssetIds();
        }

        #endregion Change Functions

        #region Remove Functions

        #endregion Remove Functions
    }
}
