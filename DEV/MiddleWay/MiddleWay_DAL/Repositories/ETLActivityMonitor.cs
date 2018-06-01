using MiddleWay_DAL.DataProvider;
using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class ETLActivityMonitor
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ETLActivityMonitor(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        #endregion Select Functions

        #region Insert Functions
        public void logAction(string actionName, string actionDescription)
        {
            string query = "INSERT INTO _ETL_ActivityMonitor (ActivityStep, ActivityMessage, ImportDataID) VALUES ('" + actionName + "','" + actionDescription + "','" + _importCode.ToString() + "')";

            //SqlCommand cmd = new SqlCommand(query, _conn);

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //cmd.ExecuteNonQuery();
            //_conn.Close();
        }

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
