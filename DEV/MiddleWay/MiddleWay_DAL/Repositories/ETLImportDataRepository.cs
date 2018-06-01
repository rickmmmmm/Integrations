using MiddleWay_DAL.DataProvider;
using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class ETLImportDataRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ETLImportDataRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        #endregion Select Functions

        #region Insert Functions

        public int getNewImportCode()
        {

            int importCode = -1;

            string query = "INSERT INTO _ETL_ImportData(ImportUserId) ";
            query += " VALUES ('IntegrationTool')";
            SqlCommand cmd = new SqlCommand(query, _conn);

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            cmd.ExecuteNonQuery();

            string returnQuery = "SELECT MAX(ImportCode) FROM _ETL_ImportData";
            SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();
            while (reader.Read())
            {
                importCode = (int)reader[0];
            }

            reader.Close();
            _conn.Close();

            Console.WriteLine("Import Code is " + importCode.ToString());

            return importCode;
        }

        #endregion Insert Functions

        #region Update Functions

        public void completeIntegration()
        {
            string query = "UPDATE _ETL_ImportData SET ImportCompleted = 'True' WHERE ImportCode = " + _importCode.ToString();

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            SqlCommand cmd = new SqlCommand(query, _conn);

            cmd.ExecuteNonQuery();

            _conn.Close();
        }

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
