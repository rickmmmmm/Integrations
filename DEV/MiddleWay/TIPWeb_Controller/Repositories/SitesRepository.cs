using TIPWeb_Controller.DataProvider;
using TIPWeb_Controller.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace TIPWeb_Controller.Repositories
{
    public class SitesRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public SitesRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        public int GetSiteUidFromName(string siteName)
        {
            int siteId = -1;

            string returnQuery = "SELECT SiteUid FROM tblTechSites WHERE LOWER(SiteID) = '" + siteName.ToLower() + "'";

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}
            //_conn.Open();
            //SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            //SqlDataReader reader = returnCmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    try
            //    {
            //        siteId = (int)reader[0];
            //    }

            //    catch
            //    {
            //        siteId = -1;
            //    }
            //}

            //reader.Close();
            //_conn.Close();

            //if (siteId == -1)
            //{
            //    throw new Exception("The specified Item Type was not found.");
            //}
            //else
            //{
                return siteId;
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
