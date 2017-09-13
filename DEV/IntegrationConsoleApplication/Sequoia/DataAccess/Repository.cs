using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess
{
    public class Repository : IRepository
    {

        private SqlConnection _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdoConnectionString"].ConnectionString);
        private int _importCode;

        public Repository()
        {
            _importCode = getNewImportCode();
        }

        #region Purchase Orders
        public int getAreaUIDFromName(string areaName)
        {
            throw new NotImplementedException();
        }

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

        public int getContainerUID(string name)
        {
            throw new NotImplementedException();
        }

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

        public int getInventorySourceUID(string name)
        {
            throw new NotImplementedException();
        }


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
                itemTypeId = (int) reader[0];
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

        public int getManufacturerUIDFromName(string manufacturerName)
        {
            int manId = -1;

            string returnQuery = "SELECT ManufacturerUID FROM tblUnvManufacturers WHERE LOWER(ManufacturerName) = '" + manufacturerName.ToLower() + "'";

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
                    manId = (int) reader[0];
                }

                catch
                {
                    manId = -1;
                }
            }

            reader.Close();
            _conn.Close();

            if (manId == -1)
            {
                throw new Exception("The specified Manufacturer was not found.");
            }
            else
            {
                return manId;
            }
        }

        public int getSiteUIDFromName(string siteName)
        {
            int siteId = -1;

            string returnQuery = "SELECT SiteUID FROM tblTechSites WHERE LOWER(SiteID) = '" + siteName.ToLower() + "'";

            if(_conn.State == ConnectionState.Open)
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
                    siteId = (int) reader[0];
                }
                
                catch
                {
                    siteId = -1;
                }
            }

            reader.Close();
            _conn.Close();

            if (siteId == -1)
            {
                throw new Exception("The specified Item Type was not found.");
            }
            else
            {
                return siteId;
            }
        }

        public int getTechDepartmentUIDFromName(string name)
        {
            throw new NotImplementedException();
        }

        public int getUserIdFromName(string userName)
        {
            throw new NotImplementedException();
        }

        public int getVendorUIDFromName(string vendorName)
        {
            int vendorId = -1;

            string returnQuery = "SELECT VendorID FROM tblVendor WHERE LOWER(VendorName) = '" + vendorName.ToLower().Replace("'","''") + "'";

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
                    vendorId = (int) reader[0];
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

        public void logAction(string actionName, string actionDescription)
        {
            string query = "INSERT INTO _ETL_ActivityMonitor (ActivityStep, ActivityMessage, ImportDataID) VALUES ('" + actionName + "','" + actionDescription + "','" + _importCode.ToString() + "')";

            SqlCommand cmd = new SqlCommand(query, _conn);

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

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

        public int getStatusUID(string status)
        {
            return 32;
        }

        public int getItemUIDFromName(string name)
        {
            int itemId = -1;

            string returnQuery = "SELECT ItemUID FROM tblTechItems WHERE LOWER(ItemName) = '" + name.Replace("'","''").ToLower() + "' AND Active = 1";

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                itemId = (int) reader[0];
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

        public void logRejectRecord(RejectedRecord rejection)
        {
            string query = "INSERT INTO _ETL_Rejects (ImportCode, Reference, RejectReason, RejectedValue, ExceptionMessage, LineNumber)";
            query += " VALUES (" + _importCode.ToString() + ",'" + rejection.orderNumber + "','" + rejection.rejectReason + "','" + rejection.rejectValue.Replace("'", "") + "','" + rejection.exceptionMessage.Replace("'","") + "'," + rejection.LineNumber.ToString() + ")";
            SqlCommand cmd = new SqlCommand(query, _conn);

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public void logRejectRecord(List<RejectedRecord> rejections)
        {
            foreach (RejectedRecord rejection in rejections)
            {
                logRejectRecord(rejection);
            }
            
        }
        public void addOrderHeaders(List<PurchaseOrderHeader> orders)
        {

            foreach (var order in orders)
            {
                try
                { 
                    string headerQuery;
                    string message;

                    if (checkOrderExists(order.PurchaseOrderNumber))
                    {
                        headerQuery = "UPDATE tblTechPurchases ";
                        headerQuery += "SET PurchaseDate = '" + order.PurchaseDate.ToString() + "', ";
                        headerQuery += "Notes = '" + order.Notes.Replace("'","") + "', ";
                        headerQuery += "VendorUID = " + order.VendorUID + ", ";
                        headerQuery += "SiteUID = " + order.SiteID +", ";
                        headerQuery += "StatusUID = 32,";
                        headerQuery += "LastModifiedByUserId = '0', ";
                        headerQuery += "LastModifiedDate = '" + DateTime.Now.ToString() + "' ";
                        headerQuery += "FROM tblTechPurchases WHERE OrderNumber = '" + order.PurchaseOrderNumber + "'";

                        message = "Order already exists. Updated header for order number " + order.PurchaseOrderNumber;
                    }

                    else
                    {
                        headerQuery = "INSERT INTO [dbo].[tblTechPurchases] ([StatusUID],[VendorUID],[SiteUID],[OrderNumber],[PurchaseDate],[Notes],[CreatedByUserID],[CreatedDate],[LastModifiedByUserID],[LastModifiedDate]) ";
                        headerQuery += "VALUES ('" + order.StatusUID.ToString() + "','" + order.VendorUID.ToString() + "','" + order.SiteID.ToString() + "','" + order.PurchaseOrderNumber + "','";
                        headerQuery += order.PurchaseDate.ToString() + "','" + order.Notes.Replace("'", "") + "','" + order.CreatedByUserId.ToString() + "','" + order.CreatedDate.ToString() + "','";
                        headerQuery += order.LastModifiedByUserId.ToString() + "','" + order.LastModifiedDate.ToString() + "')";

                        message = "Successfully added header for order number " + order.PurchaseOrderNumber;
                    }

                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }

                    _conn.Open();
                    SqlCommand cmd = new SqlCommand(headerQuery, _conn);

                    cmd.ExecuteNonQuery();

                    DbActivityEventArgs args = new DbActivityEventArgs();
                    args.ActivityStep = "Add Header and Detail Data";
                    args.ActivityMessage = message;
                    OnAction(args);

                    addOrderDetails(order.PurchaseOrderDetails.ToList());

                    _conn.Close();
                }
                catch (Exception e)
                {
                    DbErrorEventArgs args = new DbErrorEventArgs();
                    args.InterfaceMessage = "Error adding product headers or details for order number " + order.PurchaseOrderNumber;
                    args.ExceptionMessage = e.Message;
                    OnError(args);
                    continue;
                }
            }
        }

        public void addOrderDetails(List<PurchaseOrderDetail> details)
        {
            string detailQuery;

            foreach (var detail in details)
            {
                string message;

                if (checkOrderDetailExists(detail.ParentPurchase.PurchaseOrderNumber,detail.LineNumber))
                {
                    detailQuery = "UPDATE tblTechPurchaseItemDetails ";
                    detailQuery += "SET QuantityOrdered = " + detail.QuantityOrdered.ToString() + ", ";
                    detailQuery += "QuantityReceived = " + detail.QuantityReceived + ", ";
                    detailQuery += "PurchasePrice = " + detail.PurchasePrice + ", ";
                    detailQuery += "AccountCode = '" + detail.AccountCode + "', ";
                    detailQuery += "StatusUID = 32,";
                    detailQuery += "SiteAddedSiteUID = '" + detail.SiteAddedSiteUID + "',";
                    detailQuery += "LastModifiedByUserID = 0,";
                    detailQuery += "LastModifiedDate = '" + detail.LastModifiedDate.ToString() + "'";
                    detailQuery += "FROM tblTechPurchaseItemDetails p JOIN tblTechPurchases p2 ";
                    detailQuery += "ON p.PurchaseUID = p2.PurchaseUID";
                    detailQuery += " WHERE p2.OrderNumber = '" + detail.ParentPurchase.PurchaseOrderNumber + "' AND p.LineNumber = " + detail.LineNumber.ToString();

                    message = "Detail record already exists. Updated detail record for order number " + detail.ParentPurchase.PurchaseOrderNumber + " and Line Number " + detail.LineNumber.ToString();
                }
                else
                {
                    detailQuery = "INSERT INTO [dbo].[tblTechPurchaseItemDetails](";
                    detailQuery += "[PurchaseUID],[ItemUID],[FundingSourceUID],[StatusUID],[SiteAddedSiteUID],";
                    detailQuery += "[QuantityOrdered],[QuantityReceived],[PurchasePrice],[AccountCode],";
                    detailQuery += "[TechDepartmentUID],[CreatedByUserID],[CreatedDate],[LastModifiedByUserID],";
                    detailQuery += "[LastModifiedDate],[LineNumber]) ";
                    detailQuery += "VALUES ('"+ getPurchaseUIDFromOrderNumber(detail.ParentPurchase.PurchaseOrderNumber).ToString() + "','" + detail.ItemUID.ToString() + "','";
                    detailQuery += detail.FundingSourceUID.ToString() + "','" + detail.StatusUID + "','" + detail.SiteAddedSiteUID.ToString() + "','" + detail.QuantityOrdered.ToString();
                    detailQuery += "','" + detail.QuantityReceived.ToString() + "','" + detail.PurchasePrice + "','" + detail.AccountCode + "','" + detail.TechDepartmentUID.ToString();
                    detailQuery += "','" + detail.CreatedByUserID.ToString() + "','" + detail.CreatedDate.ToString() + "','" + detail.LastModifiedByUserID + "','" + detail.LastModifiedDate.ToString() + "','" + detail.LineNumber.ToString() + "')";

                    message = "Successfully added detail for order number " + detail.ParentPurchase.PurchaseOrderNumber + " and Line Number " + detail.LineNumber.ToString();
                }

                if(_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }

                try
                {
                    SqlCommand cmd = new SqlCommand(detailQuery, _conn);
                    cmd.ExecuteNonQuery();

                    DbActivityEventArgs args = new DbActivityEventArgs();
                    args.ActivityStep = "Add Header and Detail Data";
                    args.ActivityMessage = message;
                    OnAction(args);
                }
                catch (Exception e)
                {
                    DbErrorEventArgs args = new DbErrorEventArgs();
                    args.InterfaceMessage = "ERROR adding detail for Order Number " + detail.ParentPurchase.PurchaseOrderNumber + " and Line Number " + detail.LineNumber.ToString();
                    args.ExceptionMessage = e.Message;
                    continue;
                }

            }
            
        }

        public void addShipmentInfo()
        {
            string query = "INSERT INTO [dbo].[tblTechPurchaseItemShipments]";
            query += "([PurchaseItemDetailUID] ";
            query += ",[ShippedToSiteUID] ";
            query += ",[TicketNumber] ";
            query += ",[QuantityShipped] ";
            query += ",[TicketedByUserID] ";
            query += ",[TicketedDate] ";
            query += ",[StatusUID] ";
            query += ",[CreatedByUserID] ";
            query += ",[CreatedDate] ";
            query += ",[LastModifiedByUserID] ";
            query += ",[LastModifiedDate]) ";
            query += "SELECT ttpid.[PurchaseItemDetailUID], ttpid.[SiteAddedSiteUID], NULL, ttpid.QuantityOrdered, NULL, NULL, ttpid.StatusUID, 0, getdate(), 0, getdate() ";
            query += "FROM tblTechPurchaseItemShipments ttpis ";
            query += "RIGHT JOIN tblTechPurchaseItemDetails ttpid on ttpis.[PurchaseItemDetailUID] = ttpid.[PurchaseItemDetailUID] ";
            query += "WHERE ttpis.PurchaseItemDetailUID is null";

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
                args.InterfaceMessage = "ERROR adding shipment information.";
                args.ExceptionMessage = e.Message;
            }


        }

        public List<ReceivedTagsExportFile> exportReceivedTags()
        {
            List<ReceivedTagsExportFile> export = new List<ReceivedTagsExportFile>();

            string returnQuery = "SELECT DISTINCT p.ordernumber, '0' as AmountAccepted, p.PurchaseDate, p.PurchaseDate as PDate, ItemNumber, det.QuantityOrdered, '0' as AmountDamaged, det.LineNumber, inv.AssetID, 'R' as TypeOfR ";
            returnQuery += "FROM tblTechInventory inv ";
            returnQuery += "JOIN tblTechItems item on item.ItemUID = inv.ItemUID ";
            returnQuery += "JOIN tblTechPurchaseInventory pinv on pinv.InventoryUID = inv.InventoryUID ";
            returnQuery += "JOIN tblTechPurchaseItemShipments ship on ship.PurchaseItemShipmentUID = pinv.PurchaseItemShipmentUID ";
            returnQuery += "JOIN tblTechPurchaseItemDetails det on det.PurchaseItemDetailUID = ship.PurchaseItemDetailUID ";
            returnQuery += "JOIN tblTechPurchases p on p.PurchaseUID = det.PurchaseUID";

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
                    export.Add(new ReceivedTagsExportFile
                    {
                        POR_REF_NO = (string) reader[0], //OrderNumber
                        POR_AMOUNT = (string) reader[1], //0
                        POR_DT = (string) reader[2], //PurchaseDate
                        POR_ENTRY_DT = (string) reader[3], //PurchaseDate
                        POR_ITEM = (string) reader[4], //ItemCode
                        POR_QTY = (string) reader[5], //Quantity
                        POR_QTY_DAM = (string) reader[6], //0
                        POR_SEQ = (string) reader[7], //LineNumber
                        POR_TAG = (string) reader[8], //AssetID or blank
                        POR_TYPE = (string) reader[9] //R
                    });
                }

                catch (Exception e)
                {
                    DbErrorEventArgs args = new DbErrorEventArgs();
                    args.InterfaceMessage = "Unable to export received tags.";
                    args.ExceptionMessage = e.Message;
                    OnError(args);
                    break;
                }
            }

            reader.Close();
            _conn.Close();

            return export;

        }
        public void updateFixedAssetIds()
        {
            string query = "UPDATE tblTechInventory ";
            query += "SET AssetID = 'FA' + Convert(varchar(50), InventoryUID) ";
            query += "FROM tblTechInventory inv ";
            query += "JOIN tblTechItems item on item.ItemUID = inv.ItemUID ";
            query += "WHERE item.ItemSuggestedPrice >= 5000 AND AssetID IS NULL";

            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }

            try
            {
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception e)
            {
                DbErrorEventArgs args = new DbErrorEventArgs();
                args.InterfaceMessage = "ERROR updating fixed asset information.";
                args.ExceptionMessage = e.Message;
                OnError(args);
            }
        }

        public void addItems(Item item)
        {

            string query = "INSERT INTO tblTechItems ([ItemNumber],[ItemName],[ItemDescription],[ItemTypeUID],[ModelNumber],[ManufacturerUID],[ItemSuggestedPrice],[AreaUID],[ItemNotes],[SKU] ";
            query += ",[Active],[CreatedByUserID],[CreatedDate],[LastModifiedByUserID],[LastModifiedDate],SerialRequired,AllowUntagged,ProjectedLife) ";
            query += "VALUES ('" + item.ItemNumber +"','" + item.ItemName + "','" + item.ItemDescription + "','" + item.ItemType + "','" + item.ModelNumber + "','";
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

        public string getModelNumberFromProductName(string productName)
        {
            string model = null;

            string returnQuery = "SELECT ModelNumber FROM tblTechItems WHERE LOWER(ItemName) = '" + productName.ToLower().Replace("'","''") + "'";

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
                    model = (string) reader[0];
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
                newItem.ItemName = (string) reader[1];
                newItem.ItemDescription = (string) reader[2];
                newItem.ItemType = (int) reader[3];
                newItem.ModelNumber = (string) reader[4];
            }

            reader.Close();
            _conn.Close();

            return newItem;
        }

        public bool checkOrderExists(string orderNumber)
        {

            string returnQuery = "SELECT OrderNumber FROM tblTechPurchases WHERE OrderNumber = '" + orderNumber + "'";

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Close();
                _conn.Close();
                return true;
            }
            else
            {
                reader.Close();
                _conn.Close();
                return false;
            }

            
        }

        public bool checkOrderDetailExists(string orderNumber, int lineNumber)
        {
            string returnQuery = "SELECT p.* FROM tblTechPurchaseItemDetails p ";
                returnQuery += "JOIN tblTechPurchases p2 ON p.PurchaseUID = p2.PurchaseUID WHERE p2.OrderNumber = '" + orderNumber + "' AND p.LineNumber = " + lineNumber.ToString();

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Close();
                _conn.Close();
                return true;
            }
            else
            {
                reader.Close();
                _conn.Close();
                return false;
            }
        }

        private int getPurchaseUIDFromOrderNumber(string orderNumber)
        {
            int purchaseUid = -1;

            string returnQuery = "SELECT PurchaseUID FROM tblTechPurchases WHERE OrderNumber = '" + orderNumber + "'";

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
                    purchaseUid = (int) reader[0];
                }

                catch
                {
                    purchaseUid = -1;
                }
            }

            reader.Close();
            _conn.Close();

            if (purchaseUid == -1)
            {
                throw new Exception("The specified Item Type was not found.");
            }
            else
            {
                return purchaseUid;
            }
        }

        public List<RejectedRecord> getRejectionsFromLastImport()
        {
            List<RejectedRecord> rejects = new List<RejectedRecord>();

            string returnQuery = "SELECT Reference, RejectReason, RejectedValue, ExceptionMessage, LineNumber FROM _ETL_Rejects WHERE ImportCode = " + _importCode.ToString();

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
                    rejects.Add(new RejectedRecord
                    {
                        orderNumber = (string) reader[0],
                        rejectReason = (string) reader[1],
                        rejectValue = (string) reader[2],
                        exceptionMessage = (string) reader[3],
                        LineNumber = (int) reader[4]
                    });
                }

                catch (Exception e)
                {
                    DbErrorEventArgs args = new DbErrorEventArgs();
                    args.InterfaceMessage = "Unable to get list of rejected records.";
                    args.ExceptionMessage = e.Message;
                    OnError(args);
                    break;
                }
            }

            reader.Close();
            _conn.Close();

            return rejects;
        }

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

        public void sendEmail(string ProfileName, string Recipients, string Subject, string Body, string Attachment = null)
        {
            string query = "EXEC msdb.dbo.sp_send_dbmail @profile_name = '" + ProfileName + "', @recipients='" + Recipients + "', @subject='" + Subject + "', @body='" + Body + "', @body_format='HTML'";

            if (!string.IsNullOrEmpty(Attachment))
            {
                query += " , @file_attachments = '" + Attachment + "'";
            }

            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }

            SqlCommand cmd = new SqlCommand(query, _conn);

            cmd.ExecuteNonQuery();

            _conn.Close();
        }

        #endregion

        #region Charges

        public List<ChargeExportFile> exportChargesToInTouch()
        {
            List<ChargeExportFile> charges = new List<ChargeExportFile>();

            string returnQuery = "SELECT chg.ChargeUID, sd.StudentID, items.ItemName, '' as ItemBarCode, chgt.Name  as ItemCollection, camp.CampusName as FineLocationCode, chg.[Notes], chg.CreatedDate, chg.ChargeAmount - ISNULL((SELECT SUM(ISNULL(pmt.ChargeAmount,0)) FROM tblUnvChargePayments pmt WHERE pmt.ChargeUID = chg.ChargeUID),0) as ChargeAmount ";
            returnQuery += "FROM tblUnvCharges chg ";
            returnQuery += "JOIN tblUnvChargeTypes chgt ON chgt.ChargeTypeUID = chg.[ChargeTypeUID] ";
            returnQuery += "JOIN tblStudents sd on chg.EntityUID = sd.StudentsUID and chg.entitytypeuid = 4 ";
            returnQuery += "JOIN tblCampuses camp on camp.CampusID = sd.CampusID ";
            returnQuery += "JOIN tblTechItems items on items.ItemUID = chg.ItemUID ";
            //returnQuery += "JOIN tblTechInventory inv on inv.ItemUID = items.ItemUID ";
            returnQuery += "WHERE chg.ChargeAmount - ISNULL((SELECT SUM(ISNULL(pmt.ChargeAmount,0)) FROM tblUnvChargePayments pmt WHERE pmt.ChargeUID = chg.ChargeUID),0) > 0 ";

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
                    //ChargeExportFile cef = new ChargeExportFile
                    //{
                    //    FineId = (int) reader[0],
                    //    StudentId = (int) reader[1],
                    //    ItemTitle = (string) reader[2],
                    //    ItemBarcode = (string) reader[3],
                    //    ItemCollection = (string) reader[4],
                    //    FineLocationCode = (string) reader[5],
                    //    FineDescription = (string) reader[6],
                    //    FineCreatedDate = Convert.ToDateTime((string) reader[7]),
                    //    FineAmount = Convert.ToDecimal((string) reader[7])
                    //};
                    ChargeExportFile cef = new ChargeExportFile();

                    cef.FineId = (int) reader[0];
                    cef.StudentId = (string) reader[1];
                    cef.ItemTitle = (string) reader[2];
                    cef.ItemBarcode = (string) reader[3];
                    cef.ItemCollection = (string) reader[4];
                    cef.FineLocationCode = (string) reader[5];
                    cef.FineDescription = (string) reader[6];
                    cef.FineCreatedDate = (DateTime) reader[7];
                    cef.FineAmount = (decimal) reader[8];

                    charges.Add(cef);
                       
                }

                catch (Exception e)
                {
                    DbErrorEventArgs args = new DbErrorEventArgs();
                    args.InterfaceMessage = "ERROR getting charge export data for fine " + (string) reader[0];
                    args.ExceptionMessage = e.Message;
                    continue;

                }
            }

            reader.Close();
            _conn.Close();

            return charges;
        }

        public void voidCharges(List<ChargePayments> voidedCharges)
        {
            string query = "UPDATE tblUnvCharges ";
            query += "SET Void = 1 ";
            query += "WHERE ChargeUID = {0}";

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
            _conn.Open();

            foreach (var charge in voidedCharges)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(string.Format(query, charge.ParentCharge.ChargeUID.ToString()), _conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    DbErrorEventArgs args = new DbErrorEventArgs();
                    args.InterfaceMessage = "ERROR updating data for fine " + charge.ParentCharge.ChargeUID.ToString();
                    args.ExceptionMessage = e.Message;
                    continue;
                }
            }
            _conn.Close();
        }

        public void insertPaymentDetails(List<ChargePayments> imports)
        {
            foreach (var import in imports)
            {
                insertPaymentDetail(import);
            }
        }

        public void insertPaymentDetail(ChargePayments import)
        {

            string query = "INSERT INTO tblUnvChargePayments (ApplicationUID, ChargeUID, ChargeAmount, CreatedDate, CreatedByUserID, LastModifiedDate, LastModifiedByUserID) ";
            query += "VALUES ({0}, {1}, {2}, '{3}', {4}, '{5}', {6})";

            SqlCommand cmd = new SqlCommand(string.Format(query, 2, import.ParentCharge.ChargeUID, import.ChargeAmount, DateTime.Now.ToString(), 0, DateTime.Now.ToString(), 0), _conn);

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public bool chargeExists(int chargeId)
        {
            int outputValue = 0;

            string query = "SELECT count(ChargeUID) FROM tblUnvCharges WHERE ChargeUID = " + chargeId.ToString();

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            SqlCommand returnCmd = new SqlCommand(query, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                    outputValue = (int) reader[0];   
            }

            if (outputValue == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public Charge getChargeAmountByChargeId(int chargeId)
        {
            Charge returnCharge = new Charge();

            string query = "SELECT chg.ChargeAmount, ISNULL((SELECT SUM(ISNULL(pmt.ChargeAmount,0)) FROM tblUnvChargePayments pmt WHERE pmt.ChargeUID = chg.ChargeUID),0) as PaidAmount FROM tblUnvCharges chg WHERE ChargeUID = " + chargeId.ToString();

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            SqlCommand returnCmd = new SqlCommand(query, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                returnCharge.ChargeUID = chargeId;
                returnCharge.ChargeAmount = (decimal) reader[0];
                returnCharge.Payments = getPaymentsByChargeId(chargeId);
            }

            return returnCharge;
        }

        private List<ChargePayments> getPaymentsByChargeId(int chargeId)
        {
            var payments = new List<ChargePayments>();

            string query = " WHERE ChargeUID = " + chargeId.ToString();

            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            _conn.Open();
            SqlCommand returnCmd = new SqlCommand(query, _conn);

            SqlDataReader reader = returnCmd.ExecuteReader();

            while (reader.Read())
            {
                var payment = new ChargePayments();

                payment.ParentCharge.ChargeUID = chargeId;
                payment.ChargeAmount = (decimal) reader[1];
                payment.PaymentDate = (DateTime) reader[2];
            }

            return payments;
        }

        #endregion

        #region Fixed Asset
        #endregion
    }

    public class DbErrorEventArgs : EventArgs
    {
        public string InterfaceMessage { get; set; }
        public string ExceptionMessage { get; set; }
    }

    public class DbActivityEventArgs : EventArgs
    {
        public string ActivityStep { get; set; }
        public string ActivityMessage { get; set; }
    }
}
