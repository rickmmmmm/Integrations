using MiddleWay_DAL.DataProvider;
using MiddleWay_DAL.EF_DAL;
using MiddleWay_DTO.Models;
using MiddleWay_DTO.TIPWeb_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MiddleWay_DTO.Enumerations;

namespace MiddleWay_DAL.Repositories
{
    public class ItemsRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ItemsRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        public int getItemUIDFromName(string itemName)
        {
            try
            {
                var itemID = (from items in _context.TblTechItems
                              where items.ItemName.Trim().ToLower() == itemName.Trim().ToLower()
                              select items.ItemUid).FirstOrDefault();

                return itemID;
            }
            catch
            {
                throw;
            }
            //int itemId = -1;

            //string returnQuery = "SELECT ItemUID FROM tblTechItems WHERE LOWER(ItemName) = '" + name.ToLower() + "'";

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            //SqlDataReader reader = returnCmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    itemId = (int)reader[0];
            //}

            //reader.Close();
            //_conn.Close();

            //if (itemId == -1)
            //{
            //    throw new Exception("The specified Item was not found.");
            //}
            //else
            //{
            //    return itemId;
            //}
        }

        public string GetModelNumberFromProductName(string itemName)
        {
            try
            {
                var modelNumber = (from items in _context.TblTechItems
                                   where items.ItemName.Trim().ToLower() == itemName.Trim().ToLower()
                                   select items.ModelNumber).FirstOrDefault();

                return modelNumber;
            }
            catch
            {
                throw;
            }
            //string model = null;

            //string returnQuery = "SELECT ModelNumber FROM tblTechItems WHERE LOWER(ItemName) = '" + productName.ToLower().Replace("'", "''") + "'";

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            //SqlDataReader reader = returnCmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    if (!reader.IsDBNull(0))
            //    {
            //        model = (string)reader[0];
            //    }
            //}

            //reader.Close();
            //_conn.Close();

            //if (model == null)
            //{
            //    throw new Exception("The specified Item Model Number was not found.");
            //}
            //else
            //{
            //    return model;
            //}
        }

        public ItemsModel GetItemFromName(string itemName)
        {
            try
            {
                var item = (from items in _context.TblTechItems
                            where items.ItemName.Trim().ToLower() == itemName.Trim().ToLower()
                            select new ItemsModel
                            {
                                ItemNumber = items.ItemNumber,
                                ItemName = items.ItemName,
                                ItemDescription = items.ItemDescription,
                                ItemType = items.ItemTypeUid,
                                ModelNumber = items.ModelNumber,
                                ManufacturerUID = items.ManufacturerUid,
                                ItemSuggestedPrice = items.ItemSuggestedPrice,
                                AreaUID = items.AreaUid,
                                ItemNotes = items.ItemNotes,
                                SKU = items.Sku,
                                SerialRequired = items.SerialRequired,
                                ProjectedLife = items.ProjectedLife,
                                Active = items.Active,
                                CreatedByUserId = items.CreatedByUserId,
                                CreatedDate = items.CreatedDate,
                                LastModifiedByUserID = items.LastModifiedByUserId,
                                LastModifiedDate = items.LastModifiedDate,
                                AllowUntagged = items.AllowUntagged
                            }).FirstOrDefault();

                return item;
            }
            catch
            {
                throw;
            }
            //ItemsModel newItem = new ItemsModel();

            //string returnQuery = "SELECT [ItemNumber],[ItemName],[ItemDescription],[ItemTypeUID],[ModelNumber],[ManufacturerUID],[ItemSuggestedPrice],[AreaUID],[ItemNotes],[SKU],[SerialRequired],[ProjectedLife],[Active],[CreatedByUserID],[CreatedDate],[LastModifiedByUserID],[LastModifiedDate],[AllowUntagged] FROM tblTechItems WHERE LOWER(ItemName) = '" + productName.ToLower() + "'";

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            //SqlDataReader reader = returnCmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    newItem.ItemNumber = (string)reader[0];
            //    newItem.ItemName = (string)reader[1];
            //    newItem.ItemDescription = (string)reader[2];
            //    newItem.ItemType = (int)reader[3];
            //    newItem.ModelNumber = (string)reader[4];
            //}

            //reader.Close();
            //_conn.Close();

            //return newItem;
        }

        public Int64 GetUniqueItemNumber()
        {
            try
            {

                Int64 itemNumber = 0;
                var validNumber = false;
                var checking = true;
                do
                {
                    // get next universal item number from tblUnvCounter
                    itemNumber = GetNextItemNumber();

                    do
                    {
                        // check if item number already has an item in the database
                        var itemNumberExists = ItemNumberExists(itemNumber);

                        if (!itemNumberExists)
                        {
                            validNumber = true;
                        }
                        else
                        {
                            itemNumber++;
                        }
                    } while (validNumber == false);

                    // update item number in tblUnvCounter for next item that is added
                    //using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdoConnectionString"].ConnectionString))
                    //{
                    //    string returnQuery = "UPDATE tblUnvCounter SET Value = " + (itemNumber + 1) + " WHERE CounterUID = " + (int)CounterTypesEnum.ProductNumber;

                    var counterValue = (from counter in _context.TblUnvCounter
                                        where counter.CounterUid == (int)CounterTypesEnum.ProductNumber
                                        select counter).FirstOrDefault();

                    counterValue.Value = (itemNumber + 1);

                    _context.TblUnvCounter.Update(counterValue);
                    _context.SaveChanges();

                    //    if (connection.State == ConnectionState.Open)
                    //    {
                    //        connection.Close();
                    //    }

                    //    connection.Open();

                    //    SqlCommand returnCmd = new SqlCommand(returnQuery, connection);
                    //    SqlTransaction transaction;

                    //    transaction = connection.BeginTransaction("UpdateCounterTransaction");
                    //    returnCmd.Transaction = transaction;
                    //    try
                    //    {
                    //        returnCmd.ExecuteNonQuery();
                    //        transaction.Commit();
                    //        checking = false;
                    //    }
                    //    catch
                    //    {
                    //        transaction.Rollback();
                    //    }
                    //}
                } while (checking);

                return itemNumber;

            }
            catch
            {
                throw;
            }
        }

        public Int64 GetNextItemNumber()
        {
            try
            {
                var counterValue = (from counter in _context.TblUnvCounter
                                    where counter.CounterUid == (int)CounterTypesEnum.ProductNumber
                                    select counter.Value).FirstOrDefault();

                return counterValue;
            }
            catch
            {
                throw;
            }
            //Int64 result = 0;

            //string returnQuery = "SELECT Value FROM tblUnvCounter WHERE CounterUID = " + (int)CounterTypesEnum.ProductNumber;

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
            //        result = (Int64)reader[0];
            //    }
            //    catch
            //    {
            //        result = -1;
            //    }
            //}

            //reader.Close();
            //_conn.Close();

            //if (result == -1)
            //{
            //    throw new Exception("Next product number was not found.");
            //}
            //else
            //{
            //    return result;
            //}
        }

        public bool ItemNumberExists(Int64 itemNumber)
        {
            try
            {
                var exists = (from items in _context.TblTechItems
                              where items.ItemNumber.Trim().ToLower() == itemNumber.ToString()
                              select true).FirstOrDefault();

                return exists;
            }
            catch
            {
                throw;
            }
            //int result = 0;

            //string returnQuery = "SELECT COUNT(*) FROM tblTechItems WHERE ItemNumber = 'H" + itemNumber.ToString() + "'";

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
            //        result = (int)reader[0];
            //    }
            //    catch
            //    {
            //        result = -1;
            //    }
            //}

            //reader.Close();
            //_conn.Close();

            //if (result == -1)
            //{
            //    throw new Exception("Item number count was not found.");
            //}
            //else
            //{
            //    return result;
            //}
        }

        #endregion Select Functions

        #region Insert Functions

        public void addItems(ItemsModel item)
        {
            try
            {
                var newItem = new TblTechItems()
                {
                    ItemNumber = item.ItemNumber,
                    ItemName = item.ItemName,
                    ItemDescription = item.ItemDescription,
                    ItemTypeUid = item.ItemType,
                    ModelNumber = item.ModelNumber,
                    ManufacturerUid = item.ManufacturerUID,
                    ItemSuggestedPrice = item.ItemSuggestedPrice,
                    AreaUid = item.AreaUID,
                    ItemNotes = item.ItemNotes,
                    Sku = item.SKU,
                    SerialRequired = item.SerialRequired,
                    ProjectedLife = item.ProjectedLife,
                    CustomField1 = item.CustomField1,
                    CustomField2 = item.CustomField2,
                    CustomField3 = item.CustomField3,
                    Active = true,
                    AllowUntagged = item.AllowUntagged,
                    CreatedByUserId = 0,
                    CreatedDate = DateTime.Now,
                    LastModifiedByUserId = 0,
                    LastModifiedDate = DateTime.Now
                };

                _context.TblTechItems.Add(newItem);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
            //string query = "INSERT INTO tblTechItems ([ItemNumber],[ItemName],[ItemDescription],[ItemTypeUID],[ModelNumber],[ManufacturerUID],[ItemSuggestedPrice],[AreaUID],[ItemNotes],[SKU] ";
            //query += ",[Active],[CreatedByUserID],[CreatedDate],[LastModifiedByUserID],[LastModifiedDate],SerialRequired,AllowUntagged,ProjectedLife) ";
            //query += "VALUES ('" + item.ItemNumber + "','" + item.ItemName + "','" + item.ItemDescription + "','" + item.ItemType + "','" + item.ModelNumber + "','";
            //query += item.ManufacturerUID + "','" + item.ItemSuggestedPrice + "','" + item.AreaUID + "','" + item.ItemNotes + "','" + item.SKU + "','" + item.Active + "','";
            //query += item.CreatedByUserId + "','" + item.CreatedDate.ToString() + "','" + item.LastModifiedByUserID + "','" + item.LastModifiedDate.ToString() + "','FALSE',0,0)";

            //if (_conn.State == ConnectionState.Closed)
            //{
            //    _conn.Open();
            //}

            //try
            //{
            //    SqlCommand cmd = new SqlCommand(query, _conn);
            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception e)
            //{
            //    DbErrorEventArgs args = new DbErrorEventArgs();
            //    args.InterfaceMessage = "ERROR adding new item information.";
            //    args.ExceptionMessage = e.Message;
            //    OnError(args);
            //}
        }

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
