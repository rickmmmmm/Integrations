using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.Models.MiddleWay_BLL;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

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
            var successful = false;
            var recordCount = 0;
            var rejectCount = 0;
            try
            {
                // Start Process Task
                var processUid = _processesService.GetProcessUid();
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

                        recordCount = rowCount - 1;
                        //TODO: Log count of source data records returned

                        _processTaskStepsService.EndTaskStep(taskStepUid, true);

                        taskStepUid = _processTaskStepsService.BeginTaskStep(processTaskUid, ProcessSteps.Stage);

                        var flatCount = _inventoryFlatService.GetTotal();
                        var limit = _configurationService.ReadLimit;
                        var currentCount = 0;
                        recordCount = 0;
                        //Perform flat to ETL table mappings, transformations then insert (in a loop)
                        while (currentCount < flatCount)
                        {
                            var flatData = _inventoryFlatService.Get(currentCount, limit);

                            var transformedData = _transformationService.TransformToDynamic(flatData, ProcessSteps.Stage);

                            // Get the rejected records of the FlatData and update the Rejected Notes and Rejected values
                            var errorFlatData = (from data in flatData
                                                 where data.Rejected
                                                 select data).ToList();

                            _inventoryFlatService.EditRange(errorFlatData); //Console.WriteLine(Utilities.ToStringObject(errorFlatData));
                            rejectCount += errorFlatData.Count;

                            var mappedData = _mappingsService.Map<ExpandoObject, EtlInventoryModel>(transformedData, ProcessSteps.Stage);

                            // Get the rejected records of the FlatData and update the Rejected Notes and Rejected values
                            var transformList = (from data in transformedData
                                                 select (data as IDictionary<string, object>));

                            var errorData = (from data in transformList
                                             where data.ContainsKey("Rejected") && (data["Rejected"]).Equals("true")
                                             select new InventoryFlatDataModel
                                             {
                                                 InventoryFlatDataUid = data.ContainsKey("InventoryFlatDataUid") ? Int32.Parse(data["InventoryFlatDataUid"].ToString()) : 0,
                                                 ProcessUid = data.ContainsKey("ProcessUid") ? Int32.Parse(data["ProcessUid"].ToString()) : 0,
                                                 RowId = data.ContainsKey("RowId") ? Int32.Parse(data["RowId"].ToString()) : 0,
                                                 AssetId = data.ContainsKey("AssetId") ? data["AssetId"].ToString() : string.Empty,
                                                 Tag = data.ContainsKey("Tag") ? data["Tag"].ToString() : string.Empty,
                                                 Serial = data.ContainsKey("Serial") ? data["Serial"].ToString() : string.Empty,
                                                 SiteId = data.ContainsKey("SiteId") ? data["SiteId"].ToString() : string.Empty,
                                                 SiteName = data.ContainsKey("SiteName") ? data["SiteName"].ToString() : string.Empty,
                                                 Location = data.ContainsKey("Location") ? data["Location"].ToString() : string.Empty,
                                                 LocationType = data.ContainsKey("LocationType") ? data["LocationType"].ToString() : string.Empty,
                                                 Status = data.ContainsKey("Status") ? data["Status"].ToString() : string.Empty,
                                                 DepartmentName = data.ContainsKey("DepartmentName") ? data["DepartmentName"].ToString() : string.Empty,
                                                 DepartmentId = data.ContainsKey("DepartmentId") ? data["DepartmentId"].ToString() : string.Empty,
                                                 FundingSource = data.ContainsKey("FundingSource") ? data["FundingSource"].ToString() : string.Empty,
                                                 FundingSourceDescription = data.ContainsKey("FundingSourceDescription") ? data["FundingSourceDescription"].ToString() : string.Empty,
                                                 PurchasePrice = data.ContainsKey("PurchasePrice") ? data["PurchasePrice"].ToString() : string.Empty,
                                                 PurchaseDate = data.ContainsKey("PurchaseDate") ? data["PurchaseDate"].ToString() : string.Empty,
                                                 ExpirationDate = data.ContainsKey("ExpirationDate") ? data["ExpirationDate"].ToString() : string.Empty,
                                                 InventoryNotes = data.ContainsKey("InventoryNotes") ? data["InventoryNotes"].ToString() : string.Empty,
                                                 OrderNumber = data.ContainsKey("OrderNumber") ? data["OrderNumber"].ToString() : string.Empty,
                                                 LineNumber = data.ContainsKey("LineNumber") ? data["LineNumber"].ToString() : string.Empty,
                                                 VendorName = data.ContainsKey("VendorName") ? data["VendorName"].ToString() : string.Empty,
                                                 VendorAccountNumber = data.ContainsKey("VendorAccountNumber") ? data["VendorAccountNumber"].ToString() : string.Empty,
                                                 ParentTag = data.ContainsKey("ParentTag") ? data["ParentTag"].ToString() : string.Empty,
                                                 ProductName = data.ContainsKey("ProductName") ? data["ProductName"].ToString() : string.Empty,
                                                 ProductDescription = data.ContainsKey("ProductDescription") ? data["ProductDescription"].ToString() : string.Empty,
                                                 ProductByNumber = data.ContainsKey("ProductByNumber") ? data["ProductByNumber"].ToString() : string.Empty,
                                                 ProductTypeName = data.ContainsKey("ProductTypeName") ? data["ProductTypeName"].ToString() : string.Empty,
                                                 ProductTypeDescription = data.ContainsKey("ProductTypeDescription") ? data["ProductTypeDescription"].ToString() : string.Empty,
                                                 ModelNumber = data.ContainsKey("ModelNumber") ? data["ModelNumber"].ToString() : string.Empty,
                                                 ManufacturerName = data.ContainsKey("ManufacturerName") ? data["ManufacturerName"].ToString() : string.Empty,
                                                 AreaName = data.ContainsKey("AreaName") ? data["AreaName"].ToString() : string.Empty,
                                                 CustomField1Value = data.ContainsKey("CustomField1Value") ? data["CustomField1Value"].ToString() : string.Empty,
                                                 CustomField1Label = data.ContainsKey("CustomField1Label") ? data["CustomField1Label"].ToString() : string.Empty,
                                                 CustomField2Value = data.ContainsKey("CustomField2Value") ? data["CustomField2Value"].ToString() : string.Empty,
                                                 CustomField2Label = data.ContainsKey("CustomField2Label") ? data["CustomField2Label"].ToString() : string.Empty,
                                                 CustomField3Value = data.ContainsKey("CustomField3Value") ? data["CustomField3Value"].ToString() : string.Empty,
                                                 CustomField3Label = data.ContainsKey("CustomField3Label") ? data["CustomField3Label"].ToString() : string.Empty,
                                                 CustomField4Value = data.ContainsKey("CustomField4Value") ? data["CustomField4Value"].ToString() : string.Empty,
                                                 CustomField4Label = data.ContainsKey("CustomField4Label") ? data["CustomField4Label"].ToString() : string.Empty,
                                                 InvoiceNumber = data.ContainsKey("InvoiceNumber") ? data["InvoiceNumber"].ToString() : string.Empty,
                                                 InvoiceDate = data.ContainsKey("InvoiceDate") ? data["InvoiceDate"].ToString() : string.Empty,
                                                 Rejected = data.ContainsKey("Rejected") ? Boolean.Parse(data["Rejected"].ToString()) : false,
                                                 RejectedNotes = data.ContainsKey("RejectedNotes") ? data["RejectedNotes"].ToString() : string.Empty
                                             }).ToList();

                            _inventoryFlatService.EditRange(errorData); //Console.WriteLine(Utilities.ToStringObject(errorData));
                            rejectCount += errorData.Count;

                            //if (!_etlInventoryService.AddRange(transformedData))
                            if (!_etlInventoryService.AddRange(mappedData))
                            {
                                //TODO: Log Error
                            }
                            currentCount += limit;
                            recordCount += mappedData.Count;
                        }

                        //TODO: Log count of mapped and transformed records returned

                        _processTaskStepsService.EndTaskStep(taskStepUid, true);

                        //taskStepUid = _processTaskStepsService.BeginTaskStep(processTaskUid, ProcessSteps.ProcessCommands);

                        //// Process options
                        //var options = ProcessInput.ReadOptions(commands);

                        //// if MergeProducts - find similar products and merge to single product
                        //for (var i = 0; i < options.Count; i++)
                        //{
                        //    switch (options[i])
                        //    {
                        //        case "MergeProducts":
                        //            //Use transformation lookup to convert possibles to a single result?
                        //            break;
                        //        default:
                        //            break;
                        //    }
                        //}

                        //_processTaskStepsService.EndTaskStep(taskStepUid, true);

                        taskStepUid = _processTaskStepsService.BeginTaskStep(processTaskUid, ProcessSteps.Validate);

                        // Validate ETLInventory data on TIPWeb
                        var validationPassed = _etlInventoryService.ValidateEtlInventory();

                        _processTaskStepsService.EndTaskStep(taskStepUid, validationPassed);

                        if (validationPassed)
                        {
                            taskStepUid = _processTaskStepsService.BeginTaskStep(processTaskUid, ProcessSteps.Upload);

                            // Submit valid ETLInventory data to TIPWeb
                            validationPassed = _etlInventoryService.SubmitEtlInventory();

                            _processTaskStepsService.EndTaskStep(taskStepUid, validationPassed);
                        }
                    }
                    else
                    {
                        //TODO: No data to read, log it
                        _processTaskStepsService.EndTaskStep(taskStepUid, false); //TODO: Is this a fail??
                    }

                    // End ProcessTask
                    //_processTasksService.EndProcessTask(true);
                    successful = true;
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
            finally
            {
                _processTasksService.EndProcessTask(successful);
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
