using MiddleWay_DTO.Models.MiddleWay;
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
        private IProcessesService _processesService;

        #endregion Private Variables and Properties

        #region Constructor

        public InventoryFlatDataService(IInventoryFlatDataRepository inventoryFlatDataRepository, IClientConfiguration clientConfiguration,
                                        IProcessesService processesService)
        {
            _inventoryFlatDataRepository = inventoryFlatDataRepository;
            _clientConfiguration = clientConfiguration;
            _processesService = processesService;
        }

        #endregion Constructor

        #region Get Methods

        public List<InventoryFlatDataModel> Get(int offset, int limit)
        {
            try
            {
                return _inventoryFlatDataRepository.Select(_clientConfiguration.Client, _clientConfiguration.ProcessName, offset, limit);
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

        public InventoryFlatDataModel GetByAssetId(string assetId)
        {
            try
            {
                return _inventoryFlatDataRepository.SelectByAssetId(_clientConfiguration.Client, _clientConfiguration.ProcessName, assetId);
            }
            catch
            {
                throw;
            }
        }

        public InventoryFlatDataModel GetByTag(string tag)
        {
            try
            {
                return _inventoryFlatDataRepository.SelectByTag(_clientConfiguration.Client, _clientConfiguration.ProcessName, tag);
            }
            catch
            {
                throw;
            }
        }

        public int GetTotal()
        {
            try
            {
                return _inventoryFlatDataRepository.GetTotal(_clientConfiguration.Client, _clientConfiguration.ProcessName);
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
                var processUid = _processesService.GetProcessUid();
                inventoryFlatData.ProcessUid = processUid;
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
                var processUid = _processesService.GetProcessUid();
                inventoryFlatDataBatch.ForEach(row => row.ProcessUid = processUid);
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
                var processUid = _processesService.GetProcessUid();
                inventoryFlatModel.ProcessUid = processUid;
                return _inventoryFlatDataRepository.Update(inventoryFlatModel);
            }
            catch
            {
                throw;
            }
        }

        public bool EditRange(List<InventoryFlatDataModel> inventoryFlatData)
        {
            var processUid = _processesService.GetProcessUid();
            inventoryFlatData.ForEach(x => x.ProcessUid = processUid);
            return _inventoryFlatDataRepository.UpdateRange(inventoryFlatData);
        }

        #endregion Change Methods

        #region Delete Methods

        public void ClearData()
        {
            try
            {
                _inventoryFlatDataRepository.Delete(_clientConfiguration.Client, _clientConfiguration.ProcessName);
            }
            catch
            {
                throw;
            }
        }

        #endregion Delete Methods
    }
}
