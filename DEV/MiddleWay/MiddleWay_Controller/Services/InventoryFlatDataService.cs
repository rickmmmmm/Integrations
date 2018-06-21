using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class InventoryFlatDataService : IInventoryFlatDataService
    {
        #region Private Variables and Properties

        private IInventoryFlatDataRepository _inventoryFlatDataRepository;
        private IClientConfiguration _clientConfiguration;

        #endregion Private Variables and Properties

        #region Constructor

        public InventoryFlatDataService(IInventoryFlatDataRepository inventoryFlatDataRepository, IClientConfiguration clientConfiguration)
        {
            _inventoryFlatDataRepository = inventoryFlatDataRepository;
            _clientConfiguration = clientConfiguration;
        }

        #endregion Constructor

        #region Get Methods

        #endregion Get Methods

        #region Add Methods

        public bool Add(InventoryFlatDataModel inventoryFlatData)
        {
            throw new NotImplementedException();
        }

        public bool AddRange(List<InventoryFlatDataModel> inventoryFlatDataBatch)
        {
            throw new NotImplementedException();
        }

        #endregion Add Methods

        #region Update Methods

        public void ClearData()
        {
            _inventoryFlatDataRepository.ClearData(_clientConfiguration.Client, _clientConfiguration.ProcessName);
        }

        #endregion Update Methods

        #region Delete Methods

        #endregion Delete Methods
    }
}
