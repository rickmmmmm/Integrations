using MiddleWay_DTO.Models.MiddleWay_Controller;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
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

        public bool ValidateEtlInventory(int processUid, int processTaskUid, int sourceProcess)
        {
            try
            {
                if (!_etlInventoryRepository.ValidateTags(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateItems(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateItemTypes(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateCustomFields(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateManufacturers(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateAreas(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateSites(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateDepartments(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateFundingSources(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateVendors(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateStatus(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidatePurchaseOrders(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }

                //TODO: Log count of items saved?

                return true;
            }
            catch
            {
                throw;
            }
        }

        public bool SubmitEtlInventory(int processUid, int processTaskUid, int sourceProcess)
        {
            try
            {
                //Submit Vendors
                if (!_etlInventoryRepository.SubmitVendors(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                //Submit Manufacturers
                if (!_etlInventoryRepository.SubmitManufacturers(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                //Submit Areas
                if (!_etlInventoryRepository.SubmitAreas(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                //Submit Funding Sources
                if (!_etlInventoryRepository.SubmitFundingSources(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                //Submit Items
                if (!_etlInventoryRepository.SubmitItems(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                //Submit Purchase Details
                if (!_etlInventoryRepository.SubmitPurchaseItemDetails(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                //Submit Purchase Shipments
                if (!_etlInventoryRepository.SubmitPurchaseItemShipments(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                //Submit Inventory
                if (!_etlInventoryRepository.SubmitInventory(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                //Submit Purchase Inventory
                if (!_etlInventoryRepository.SubmitPurchaseInventory(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                //Submit InventoryExt
                if (!_etlInventoryRepository.SubmitInventoryExt(processUid, processTaskUid, sourceProcess))
                {
                    return false;
                }
                //TODO: Log count of items saved?

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
