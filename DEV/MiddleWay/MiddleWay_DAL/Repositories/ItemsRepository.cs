using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class ItemsRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ItemsRepository(TIPWebContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        public int getItemTypeUIDFromName(string itemType)
        {
            int itemTypeId = -1;

            string returnQuery = "SELECT ItemTypeUID FROM tblTechItems WHERE LOWER(ItemName) = '" + itemType.ToLower() + "'";

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                itemTypeId = (int)reader[0];
            }

            reader.Close();
            _conn.Close();

            if (itemTypeId == -1)
            {
                throw new Exception("The specified Item Type was not found.");
            }
            else
            {
                return itemTypeId;
            }
        }

        public int getItemUIDFromName(string name)
        {
            int itemId = -1;

            string returnQuery = "SELECT ItemUID FROM tblTechItems WHERE LOWER(ItemName) = '" + name.ToLower() + "'";

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                itemId = (int)reader[0];
            }

            reader.Close();
            _conn.Close();

            if (itemId == -1)
            {
                throw new Exception("The specified Item was not found.");
            }
            else
            {
                return itemId;
            }
        }

        public string getModelNumberFromProductName(string productName)
        {
            string model = null;

            string returnQuery = "SELECT ModelNumber FROM tblTechItems WHERE LOWER(ItemName) = '" + productName.ToLower().Replace("'", "''") + "'";

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    model = (string)reader[0];
                }
            }

            reader.Close();
            _conn.Close();

            if (model == null)
            {
                throw new Exception("The specified Item Model Number was not found.");
            }
            else
            {
                return model;
            }
        }

        public Item getItemFromName(string productName)
        {
            Item newItem = new Item();

            string returnQuery = "SELECT [ItemNumber],[ItemName],[ItemDescription],[ItemTypeUID],[ModelNumber],[ManufacturerUID],[ItemSuggestedPrice],[AreaUID],[ItemNotes],[SKU],[SerialRequired],[ProjectedLife],[Active],[CreatedByUserID],[CreatedDate],[LastModifiedByUserID],[LastModifiedDate],[AllowUntagged] FROM tblTechItems WHERE LOWER(ItemName) = '" + productName.ToLower() + "'";

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                newItem.ItemNumber = (string)reader[0];
                newItem.ItemName = (string)reader[1];
                newItem.ItemDescription = (string)reader[2];
                newItem.ItemType = (int)reader[3];
                newItem.ModelNumber = (string)reader[4];
            }

            reader.Close();
            _conn.Close();

            return newItem;
        }

        public Int64 getUniqueItemNumber()
        {

            Int64 itemNumber = 0;
            var validNumber = false;
            var checking = true;
            do
            {
                // get next universal item number from tblUnvCounter
                itemNumber = getNextItemNumber();

                do
                {
                    // check if item number already has an item in the database
                    var itemNumberCount = getItemNumberCount(itemNumber);

                    if (itemNumberCount == 0)
                    {
                        validNumber = true;
                    }
                    else
                    {
                        itemNumber++;
                    }
                } while (validNumber == false);

                // update item number in tblUnvCounter for next item that is added
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdoConnectionString"].ConnectionString))
                {
                    string returnQuery = "UPDATE tblUnvCounter SET Value = " + (itemNumber + 1) + " WHERE CounterUID = " + (int)CounterTypesEnum.ProductNumber;

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    connection.Open();

                    SqlCommand returnCmd = new SqlCommand(returnQuery, connection);
                    SqlTransaction transaction;

                    transaction = connection.BeginTransaction("UpdateCounterTransaction");
                    returnCmd.Transaction = transaction;
                    try
                    {
                        returnCmd.ExecuteNonQuery();
                        transaction.Commit();
                        checking = false;
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            } while (checking);

            return itemNumber;
        }

        public Int64 getNextItemNumber()
        {
            Int64 result = 0;

            string returnQuery = "SELECT Value FROM tblUnvCounter WHERE CounterUID = " + (int)CounterTypesEnum.ProductNumber;

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
                    result = (Int64)reader[0];
                }
                catch
                {
                    result = -1;
                }
            }

            reader.Close();
            _conn.Close();

            if (result == -1)
            {
                throw new Exception("Next product number was not found.");
            }
            else
            {
                return result;
            }
        }

        public int getItemNumberCount(Int64 itemNumber)
        {
            int result = 0;

            string returnQuery = "SELECT COUNT(*) FROM tblTechItems WHERE ItemNumber = 'H" + itemNumber.ToString() + "'";

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
                    result = (int)reader[0];
                }
                catch
                {
                    result = -1;
                }
            }

            reader.Close();
            _conn.Close();

            if (result == -1)
            {
                throw new Exception("Item number count was not found.");
            }
            else
            {
                return result;
            }
        }

        #endregion Select Functions

        #region Insert Functions

        public void addItems(Item item)
        {

            string query = "INSERT INTO tblTechItems ([ItemNumber],[ItemName],[ItemDescription],[ItemTypeUID],[ModelNumber],[ManufacturerUID],[ItemSuggestedPrice],[AreaUID],[ItemNotes],[SKU] ";
            query += ",[Active],[CreatedByUserID],[CreatedDate],[LastModifiedByUserID],[LastModifiedDate],SerialRequired,AllowUntagged,ProjectedLife) ";
            query += "VALUES ('" + item.ItemNumber + "','" + item.ItemName + "','" + item.ItemDescription + "','" + item.ItemType + "','" + item.ModelNumber + "','";
            query += item.ManufacturerUID + "','" + item.ItemSuggestedPrice + "','" + item.AreaUID + "','" + item.ItemNotes + "','" + item.SKU + "','" + item.Active + "','";
            query += item.CreatedByUserId + "','" + item.CreatedDate.ToString() + "','" + item.LastModifiedByUserID + "','" + item.LastModifiedDate.ToString() + "','FALSE',0,0)";

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
                args.InterfaceMessage = "ERROR adding new item information.";
                args.ExceptionMessage = e.Message;
                OnError(args);
            }
        }

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
