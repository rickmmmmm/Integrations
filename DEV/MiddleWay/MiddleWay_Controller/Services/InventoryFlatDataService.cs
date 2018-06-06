using MiddleWay_Controller.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class InventoryFlatDataService : IInventoryFlatDataService
    {
        #region Private Variables and Properties

        private IInventoryFlatDataRepository _inventoryFlatDataRepository;
        
        #endregion Private Variables and Properties

        #region Constructor

        public InventoryFlatDataService(IInventoryFlatDataRepository inventoryFlatDataRepository)
        {
            _inventoryFlatDataRepository = inventoryFlatDataRepository;
        }

        #endregion Constructor

        #region Get Methods

        #endregion Get Methods

        #region Add Methods

        #endregion Add Methods

        #region Update Methods

        #endregion Update Methods

        #region Delete Methods

        #endregion Delete Methods
    }
}
