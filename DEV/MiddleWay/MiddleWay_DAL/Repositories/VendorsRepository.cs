using MiddleWay_DAL.DataProvider;
using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class VendorsRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public VendorsRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        public int getVendorUIDFromName(string vendorName)
        {
            int vendorId = -1;

            string returnQuery = "SELECT VendorID FROM tblVendor WHERE LOWER(VendorName) = '" + vendorName.ToLower().Replace("'", "''").Trim() + "'";

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
            _conn.Open();
            SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    vendorId = (int)reader[0];
                }

                catch
                {
                    vendorId = -1;
                }
            }

            reader.Close();
            _conn.Close();

            if (vendorId == -1)
            {
                throw new Exception("The specified Item Type was not found.");
            }
            else
            {
                return vendorId;
            }
        }

        #endregion Select Functions

        #region Insert Functions

        public void addVendor(string vendorName)
        {
            string query = "INSERT INTO tblVendor (VendorName, Active, UserID, ApplicationUID, ModifiedDate) VALUES ('" + vendorName + "',1,0,2,getdate())";

            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }

            try
            {
                SqlCommand cmd = new SqlCommand(query, _conn);

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                DbErrorEventArgs args = new DbErrorEventArgs();
                args.InterfaceMessage = "ERROR adding new vendor information.";
                args.ExceptionMessage = e.Message;
                OnError(args);
            }
            _conn.Close();
        }

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
