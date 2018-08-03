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

        public bool ValidateEtlInventory(int processUid, int processTaskUid, int processSource)
        {
            try
            {
                if (!_etlInventoryRepository.ValidateTags(processUid, processTaskUid, processSource))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateItems(processUid, processTaskUid, processSource))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateItemTypes(processUid, processTaskUid, processSource))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateCustomFields(processUid, processTaskUid, processSource))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateManufacturers(processUid, processTaskUid, processSource))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateAreas(processUid, processTaskUid, processSource))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateSites(processUid, processTaskUid, processSource))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateDepartments(processUid, processTaskUid, processSource))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateFundingSources(processUid, processTaskUid, processSource))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateVendors(processUid, processTaskUid, processSource))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidateStatus(processUid, processTaskUid, processSource))
                {
                    return false;
                }
                if (!_etlInventoryRepository.ValidatePurchaseOrders(processUid, processTaskUid, processSource))
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

        public bool SubmitEtlInventory(int processUid, int processTaskUid, int processSource)
        {
            try
            {
                //Submit Products

                //Submit Manufacturers

                //Submit Funding Sources

                //Submit Vendors

                //Submit Inventory

                //Submit Custom Fields

                //Submit Parent Data

                //Submit Purchase Orders

                //Submit Purchase Details

                //Submit Purchase Shipments

                //Submit Purchase Inventory

                //Submit Invoice Data

                //TODO: Log count of items saved?

                return false;
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
