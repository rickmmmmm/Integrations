using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class InventoryRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public InventoryRepository(TIPWebContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        #endregion Select Functions

        #region Insert Functions

        #endregion Insert Functions

        #region Update Functions

        public void updateFixedAssetIds()
        {
            string query = "UPDATE tblTechInventory ";
            query += "SET AssetID = 'FA' + Convert(varchar(50), InventoryUID) ";
            query += "FROM tblTechInventory inv ";
            query += "JOIN tblTechItems item on item.ItemUID = inv.ItemUID ";
            query += "WHERE item.ItemSuggestedPrice >= 5000 AND AssetID IS NULL";

            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }

            try
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception e)
            {
                DbErrorEventArgs args = new DbErrorEventArgs();
                args.InterfaceMessage = "ERROR updating fixed asset information.";
                args.ExceptionMessage = e.Message;
                OnError(args);
            }
        }

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
