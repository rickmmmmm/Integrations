using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Model;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//using DataAccess.Enums;

namespace DataAccess
{
    public class Repository : IRepository
    {

        //private SqlConnection _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdoConnectionString"].ConnectionString);
        private int _importCode;

        public Repository()
        {
            //_importCode = getNewImportCode();
        }

        #region Purchase Orders
      



        public event EventHandler<DbErrorEventArgs> Error;
        public event EventHandler<DbActivityEventArgs> Action;

        protected virtual void OnError(DbErrorEventArgs e)
        {
            EventHandler<DbErrorEventArgs> handler = Error;

            handler(this, e);
        }

        protected virtual void OnAction(DbActivityEventArgs e)
        {
            EventHandler<DbActivityEventArgs> handler = Action;

            handler(this, e);
        }


        #endregion

        #region Charges


        #endregion

        #region Fixed Asset
        #endregion
    }

}
