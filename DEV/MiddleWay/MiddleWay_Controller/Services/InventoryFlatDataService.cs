using MiddleWay_DTO.Models.MiddleWay_Controller;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWay_Controller.Services
{
    public class InventoryFlatDataService : IInventoryFlatDataService
    {
        #region Private Variables and Properties

        private IInventoryFlatDataRepository _inventoryFlatDataRepository;
        private IClientConfiguration _clientConfiguration;
        private IProcessTasksService _processTasksService;

        #endregion Private Variables and Properties

        #region Constructor

        public InventoryFlatDataService(IInventoryFlatDataRepository inventoryFlatDataRepository, IClientConfiguration clientConfiguration,
                                        IProcessTasksService processTasksService)
        {
            _inventoryFlatDataRepository = inventoryFlatDataRepository;
            _clientConfiguration = clientConfiguration;
            _processTasksService = processTasksService;
        }

        #endregion Constructor

        #region Get Methods

        public List<InventoryFlatDataModel> Get(int processTaskUid, int offset, int limit)
        {
            try
            {
                return _inventoryFlatDataRepository.Select(processTaskUid, offset, limit);
            }
            catch
            {
                throw;
            }
        }

        public List<InventoryFlatDataModel> GetLatest(int offset, int limit)
        {
            try
            {
                return _inventoryFlatDataRepository.SelectLatest(_clientConfiguration.Client, _clientConfiguration.ProcessName, offset, limit);
            }
            catch
            {
                throw;
            }
        }

        public InventoryFlatDataModel Get(int inventoryFlatDataUid)
        {
            try
            {
                return _inventoryFlatDataRepository.Select(inventoryFlatDataUid);
            }
            catch
            {
                throw;
            }
        }

        public InventoryFlatDataModel GetByAssetId(int processTaskUid, string assetId)
        {
            try
            {
                return _inventoryFlatDataRepository.SelectByAssetId(processTaskUid, assetId);
            }
            catch
            {
                throw;
            }
        }

        public InventoryFlatDataModel GetByTag(int processTaskUid, string tag)
        {
            try
            {
                return _inventoryFlatDataRepository.SelectByTag(processTaskUid, tag);
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
                return _inventoryFlatDataRepository.GetTotal(processTaskUid);
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
                return _inventoryFlatDataRepository.GetTotalLatest(_clientConfiguration.Client, _clientConfiguration.ProcessName);
            }
            catch
            {
                throw;
            }
        }

        #endregion Get Methods

        #region Add Methods

        public bool Add(InventoryFlatDataModel inventoryFlatData)
        {
            try
            {
                var processTaskUid = _processTasksService.GetProcessTaskUid;
                inventoryFlatData.ProcessTaskUid = processTaskUid;
                var inventoryFlatDataUid = _inventoryFlatDataRepository.Insert(inventoryFlatData);
                return (inventoryFlatDataUid > 0);
            }
            catch
            {
                throw;
            }
        }

        public bool AddRange(List<InventoryFlatDataModel> inventoryFlatDataBatch)
        {
            try
            {
                var processTaskUid = _processTasksService.GetProcessTaskUid;
                inventoryFlatDataBatch.ForEach(row => row.ProcessTaskUid = processTaskUid);
                return _inventoryFlatDataRepository.InsertRange(inventoryFlatDataBatch);
            }
            catch
            {
                throw;
            }
        }

        #endregion Add Methods

        #region Change Methods

        public bool Edit(InventoryFlatDataModel inventoryFlatModel)
        {
            try
            {
                var processTaskUid = _processTasksService.GetProcessTaskUid;
                inventoryFlatModel.ProcessTaskUid = processTaskUid;
                return _inventoryFlatDataRepository.Update(inventoryFlatModel);
            }
            catch
            {
                throw;
            }
        }

        public bool EditRange(List<InventoryFlatDataModel> inventoryFlatData)
        {
            var processTaskUid = _processTasksService.GetProcessTaskUid;
            inventoryFlatData.ForEach(x => x.ProcessTaskUid = processTaskUid);
            return _inventoryFlatDataRepository.UpdateRange(inventoryFlatData);
        }

        #endregion Change Methods

        #region Delete Methods

        public void Remove(int processTaskUid)
        {
            try
            {
                _inventoryFlatDataRepository.Delete(processTaskUid);
            }
            catch
            {
                throw;
            }
        }

        public void ClearData()
        {
            try
            {
                // Cleanup data based on configuration
                //_inventoryFlatDataRepository.DeleteAll(_clientConfiguration.Client, _clientConfiguration.ProcessName);
            }
            catch
            {
                throw;
            }
        }

        public void ClearData(int processTaskUid)
        {
            try
            {
                _inventoryFlatDataRepository.DeleteAll(processTaskUid);
            }
            catch
            {
                throw;
            }
        }

        #endregion Delete Methods
    }
}
