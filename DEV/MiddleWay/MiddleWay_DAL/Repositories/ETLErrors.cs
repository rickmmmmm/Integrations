using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class ETLErrors
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ETLErrors(TIPWebContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        #endregion Select Functions

        #region Insert Functions
        public void logError(string message, string exceptionMessage)
        {
            string query = "INSERT INTO _ETL_Errors (InterfaceMessage, ExceptionMessage, ImportDataID) VALUES ('" + message + "','" + exceptionMessage + "','" + _importCode.ToString() + "')";

            SqlCommand cmd = new SqlCommand(query, _conn);

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
