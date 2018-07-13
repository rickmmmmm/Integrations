using TIPWeb_Controller.DataProvider;
using TIPWeb_Controller.EF_DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TIPWeb_Controller.Repositories
{
    public class ItemTypesRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ItemTypesRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        public int GetItemTypeUidFromName(string itemTypeName)
        {
            try
            {
                var itemTypeId = (from itemTypes in _context.TblTechItemTypes
                                  where itemTypes.ItemTypeName.Trim().ToLower() == itemTypeName.Trim().ToLower()
                                  select itemTypes.ItemTypeUid).FirstOrDefault();

                return itemTypeId;
            }
            catch
            {
                throw;
            }
            //int itemTypeId = -1;

            //string returnQuery = "SELECT ItemTypeUid FROM tblTechItems WHERE LOWER(ItemName) = '" + itemType.ToLower() + "'";

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            //SqlDataReader reader = returnCmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    itemTypeId = (int)reader[0];
            //}

            //reader.Close();
            //_conn.Close();

            //if (itemTypeId == -1)
            //{
            //    throw new Exception("The specified Item Type was not found.");
            //}
            //else
            //{
            //    return itemTypeId;
            //}
        }

        #endregion Select Functions

        #region Insert Functions

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
