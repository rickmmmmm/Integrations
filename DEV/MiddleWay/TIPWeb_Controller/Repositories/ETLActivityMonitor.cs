using TIPWeb_Controller.DataProvider;
using TIPWeb_Controller.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace TIPWeb_Controller.Repositories
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
        public void logAction(int importCode, string actionName, string actionDescription)
        {
            try
            {
                var activity = new EtlActivityMonitor
                {
                    ImportDataId = importCode,
                    ActivityStep = actionName,
                    ActivityMessage = actionDescription

                };

                _context.EtlActivityMonitor.Add(activity);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
            //string query = "INSERT INTO _ETL_ActivityMonitor (ActivityStep, ActivityMessage, ImportDataID) VALUES ('" + actionName + "','" + actionDescription + "','" + importCode.ToString() + "')";

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
