using MiddleWay_DAL.DataProvider;
using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class FundingSourcesRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public FundingSourcesRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        public int getFundingSourceUIDFromName(string sourceName)
        {
            int fundingSource = -1;

            string returnQuery = "SELECT FundingSourceUID FROM tblFundingSources WHERE LOWER(FundingSource) = '" + sourceName.ToLower() + "'";

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                fundingSource = (int)reader[0];
            }

            reader.Close();
            _conn.Close();

            if (fundingSource == -1)
            {
                throw new Exception("The specified Funding Source Name was not found.");
            }
            else
            {
                return fundingSource;
            }
        }

        #endregion Select Functions

        #region Insert Functions

        public void addFundingSource(string source)
        {
            string query = "INSERT INTO tblFundingSources (FundingSource, Active, CreatedByUserID, ApplicationUID) VALUES ('" + source + "',1,0,2)";

            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }

            SqlCommand cmd = new SqlCommand(query, _conn);

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
