using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay_Controller;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.Services
{
    public class EtlInventoryService : IEtlInventoryService
    {
        #region Private Variables and Properties

        private IEtlInventoryRepository _etlInventoryRepository;
        private IClientConfiguration _clientConfiguration;
        private IProcessTasksService _processTasksService;

        #endregion Private Variables and Properties

        #region Constructor

        public EtlInventoryService(IEtlInventoryRepository etlInventoryRepository, IClientConfiguration clientConfiguration,
                                   IProcessTasksService processTasksService)
        {
            _etlInventoryRepository = etlInventoryRepository;
            _clientConfiguration = clientConfiguration;
            _processTasksService = processTasksService;
        }

        #endregion Constructor

        #region Get Methods

        public EtlInventoryModel Get(int etlInventoryUid)
        {
            try
            {
                return _etlInventoryRepository.Select(etlInventoryUid);
            }
            catch
            {
                throw;
            }
        }

        public EtlInventoryModel GetByTag(int processTaskUid, string tag)
        {
            try
            {
                return _etlInventoryRepository.SelectByTag(processTaskUid, tag);
            }
            catch
            {
                throw;
            }
        }

        public EtlInventoryModel GetByAsset(int processTaskUid, string assetId)
        {
            try
            {
                return _etlInventoryRepository.SelectByAssetId(processTaskUid, assetId);
            }
            catch
            {
                throw;
            }
        }

        //public EtlInventoryModel GetBySerial(string client, string processName, string serial)
        //{
        //    try
        //    {
        //        return _etlInventoryRepository.SelectBySerial(client, processName, serial);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public EtlInventoryModel GetByInventoryId(int processTaskUid, int inventoryUid)
        {
            try
            {
                return _etlInventoryRepository.SelectByInventoryUid(processTaskUid, inventoryUid);
            }
            catch
            {
                throw;
            }
        }

        public List<EtlInventoryModel> Get(int processTaskUid, int offset, int limit)
        {
            try
            {
                return _etlInventoryRepository.Select(processTaskUid, offset, limit);
            }
            catch
            {
                throw;
            }
        }

        public List<EtlInventoryModel> GetLatest(int offset, int limit)
        {
            try
            {
                return _etlInventoryRepository.SelectLatest(_clientConfiguration.Client, _clientConfiguration.ProcessName, offset, limit);
            }
            catch
            {
                throw;
            }
        }

        public int GetTotal(int processTaskUid)
        {
            try
            {
                return _etlInventoryRepository.GetTotal(processTaskUid);
            }
            catch
            {
                throw;
            }
        }

        public int GetTotalLatest()
        {
            try
            {
                return _etlInventoryRepository.GetTotalLatest(_clientConfiguration.Client, _clientConfiguration.ProcessName);
            }
            catch
            {
                throw;
            }
        }

        #endregion Get Methods

        #region Add Methods

        public int Add(EtlInventoryModel item)
        {
            try
            {
                var processTaskUid = _processTasksService.GetProcessTaskUid;
                item.ProcessTaskUid = processTaskUid;
                return _etlInventoryRepository.Insert(item);
            }
            catch
            {
                throw;
            }
        }

        public bool AddRange(List<EtlInventoryModel> items)
        {
            try
            {
                var processTaskUid = _processTasksService.GetProcessTaskUid;
                items.ForEach(row => row.ProcessTaskUid = processTaskUid);
                return _etlInventoryRepository.InsertRange(items);
                //TODO: Log count of items saved?
            }
            catch
            {
                throw;
            }
        }

        #endregion Add Methods

        #region Change Methods

        public bool Edit(EtlInventoryModel item)
        {
            try
            {
                var processTaskUid = _processTasksService.GetProcessTaskUid;
                item.ProcessTaskUid = processTaskUid;
                return _etlInventoryRepository.Update(item);
            }
            catch
            {
                throw;
            }
        }

        public bool EditRange(List<EtlInventoryModel> items)
        {
            try
            {
                var processTaskUid = _processTasksService.GetProcessTaskUid;
                items.ForEach(x => x.ProcessTaskUid = processTaskUid);
                return _etlInventoryRepository.UpdateRange(items);
            }
            catch
            {
                throw;
            }
        }

        public bool ValidateEtlInventory(int processUid, int processTaskUid, ProcessSources sourceProcess)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Tags");
                if (!_etlInventoryRepository.ValidateTags(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Tags returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Items");
                if (!_etlInventoryRepository.ValidateItems(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Items returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Item Types");
                if (!_etlInventoryRepository.ValidateItemTypes(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Item Types returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Custom Fields");
                if (!_etlInventoryRepository.ValidateCustomFields(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Custom Fields returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Manufacturers");
                if (!_etlInventoryRepository.ValidateManufacturers(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Manufacturers returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Areas");
                if (!_etlInventoryRepository.ValidateAreas(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Areas returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Sites");
                if (!_etlInventoryRepository.ValidateSites(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Sites returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Departments");
                if (!_etlInventoryRepository.ValidateDepartments(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Departments returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Funding Sources");
                if (!_etlInventoryRepository.ValidateFundingSources(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Funding Sources returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Vendors");
                if (!_etlInventoryRepository.ValidateVendors(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Vendors returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Status");
                if (!_etlInventoryRepository.ValidateStatus(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Status returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Purchase Orders");
                if (!_etlInventoryRepository.ValidatePurchaseOrders(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validate Purchase Orders returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Validations Complete");
                //TODO: Log count of items saved?

                return true;
            }
            catch
            {
                throw;
            }
        }

        public bool SubmitEtlInventory(int processUid, int processTaskUid, ProcessSources sourceProcess)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Vendors");
                if (!_etlInventoryRepository.SubmitVendors(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Vendors returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Manufacturers");
                if (!_etlInventoryRepository.SubmitManufacturers(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Manufacturers returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Areas");
                if (!_etlInventoryRepository.SubmitAreas(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Areas returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Funding Sources");
                if (!_etlInventoryRepository.SubmitFundingSources(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Funding Sources returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Items");
                if (!_etlInventoryRepository.SubmitItems(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Items returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Purchase Details");
                if (!_etlInventoryRepository.SubmitPurchaseItemDetails(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Purchase Details returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Purchase Shipments");
                if (!_etlInventoryRepository.SubmitPurchaseItemShipments(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Purchase Shipments returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Inventory");
                if (!_etlInventoryRepository.SubmitInventory(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Inventory returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Purchase Inventory");
                if (!_etlInventoryRepository.SubmitPurchaseInventory(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Purchase Inventory returned false");
                    return false;
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit InventoryExt");
                if (!_etlInventoryRepository.SubmitInventoryExt(processUid, processTaskUid, (int)sourceProcess))
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit InventoryExt returned false");
                    return false;
                }
                //TODO: Log count of items saved?
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")} - Submit Complete");

                return true;
            }
            catch
            {
                throw;
            }
        }

        #endregion Change Methods

        #region Delete Methods

        public bool Remove(int etlInventoryUid)
        {
            try
            {
                return _etlInventoryRepository.Delete(etlInventoryUid);
            }
            catch
            {
                throw;
            }
        }

        public bool RemoveAll()
        {
            try
            {
                return _etlInventoryRepository.DeleteAll(_clientConfiguration.Client, _clientConfiguration.ProcessName);
            }
            catch
            {
                throw;
            }
        }

        public bool RemoveAll(int processTaskUid)
        {
            try
            {
                return _etlInventoryRepository.DeleteAll(processTaskUid);
            }
            catch
            {
                throw;
            }
        }

        #endregion Delete Methods
    }
}
