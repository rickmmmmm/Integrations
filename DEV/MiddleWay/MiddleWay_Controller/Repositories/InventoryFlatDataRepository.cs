using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MiddleWay_Controller.Repositories
{
    public class InventoryFlatDataRepository : IInventoryFlatDataRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public InventoryFlatDataRepository(IntegrationMiddleWayContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        #endregion Select Functions

        #region Insert Functions

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        public void ClearData(string client, string processName)
        {
            try
            {
                var clientVal = client.Trim().ToLower();
                var processNameVal = processName.Trim().ToLower();

                var inventoryData = (from inventoryFlatData in _context.InventoryFlatData
                                     join processes in _context.Processes
                                       on inventoryFlatData.ProcessUid equals processes.ProcessUid
                                     where processes.Client.Trim().ToLower() == clientVal
                                        && processes.ProcessName.Trim().ToLower() == processNameVal
                                     select inventoryFlatData);

                _context.InventoryFlatData.RemoveRange(inventoryData);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        #endregion Delete Functions
    }
}
