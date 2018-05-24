using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace MiddleWay_EDS.Services
{
    public class ExternalDataSource
    {
        private string _connectionString;

        public ExternalDataSource(string connectionString) : base()
        {
            _connectionString = connectionString;

        }

        public void Exec()
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var data = db.Query<Object>("Select * From Author").ToList();

                
            }
        }
    }
}
