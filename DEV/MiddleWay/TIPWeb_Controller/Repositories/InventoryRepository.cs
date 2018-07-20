using MiddleWay_DTO.RepositoryInterfaces.TIPWeb;
using TIPWeb_Controller.DataProvider;
using TIPWeb_Controller.EF_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TIPWeb_Controller.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public InventoryRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        #endregion Select Functions

        #region Insert Functions

        #endregion Insert Functions

        #region Update Functions

        public void updateFixedAssetIds()
        {
            try
            {
                //string query = "UPDATE tblTechInventory ";
                //query += "SET AssetID = 'FA' + Convert(varchar(50), InventoryUid) ";
                //query += "FROM tblTechInventory inv ";
                //query += "JOIN tblTechItems item on item.ItemUid = inv.ItemUid ";
                //query += "WHERE item.ItemSuggestedPrice >= 5000 AND AssetID IS NULL";

                var assetsToUpdate = (from inventory in _context.TblTechInventory
                                      join items in _context.TblTechItems
                                        on inventory.ItemUid equals items.ItemUid
                                      where items.ItemSuggestedPrice >= 5000 &&
                                      inventory.AssetId == null
                                      select inventory).ToList();

                assetsToUpdate.ForEach(inv => inv.AssetId = "FA" + inv.InventoryUid);
                
                _context.TblTechInventory.UpdateRange(assetsToUpdate);

                _context.SaveChanges();

                //if (_conn.State == ConnectionState.Closed)
                //{
                //    _conn.Open();
                //}

                //SqlCommand cmd = new SqlCommand(query, _conn);
                //cmd.ExecuteNonQuery();
                //_conn.Close();
            }
            catch// (Exception e)
            {
                //DbErrorEventArgs args = new DbErrorEventArgs();
                //args.InterfaceMessage = "ERROR updating fixed asset information.";
                //args.ExceptionMessage = e.Message;
                //OnError(args);
                throw;
            }
        }

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
