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

        //TODO
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void logAction(string actionName, string actionDescription, DateTime actionDate)
        {
            throw new NotImplementedException();
        }

        public void logError(string message, string processStep, DateTime errorDate)
        {
            throw new NotImplementedException();
        }

        public int getStatusUID(string status)
        {
            throw new NotImplementedException();
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
            string query = "INSERT INTO _ETL_Rejects (ImportCode, Reference, RejectReason, RejectedValue, ExceptionMessage)";
            query += " VALUES (" + _importCode.ToString() + ",'" + rejection.orderNumber + "','" + rejection.rejectReason + "','" + rejection.rejectValue.Replace("'", "") + "','" + rejection.exceptionMessage.Replace("'","") + "')";
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
                string headerQuery;

                if (checkOrderExists(order.PurchaseOrderNumber))
                {
                    headerQuery = "UPDATE tblTechPurchases ";
                    headerQuery += "SET PurchaseDate = '" + order.PurchaseDate.ToString() +"', ";
                    headerQuery += "Notes = '" + order.Notes + "', ";
                    headerQuery += "LastModifiedByUserId = '0' ";
                    headerQuery += "LastModifiedDate = '" + DateTime.Now.ToString() + "' ";
                    headerQuery += "FROM tblTechPurchases WHERE OrderNumber = '" + order.PurchaseOrderNumber + "'";
                }

                else
                {
                    headerQuery = "INSERT INTO [dbo].[tblTechPurchases] ([StatusUID],[VendorUID],[SiteUID],[OrderNumber],[PurchaseDate],[Notes],[CreatedByUserID],[CreatedDate],[LastModifiedByUserID],[LastModifiedDate]) ";
                    headerQuery += "VALUES ('" + order.StatusUID.ToString() + "','" + order.VendorUID.ToString() + "','" + order.SiteID.ToString() + "','" + order.PurchaseOrderNumber + "','";
                    headerQuery += order.PurchaseDate.ToString() + "','" + order.Notes.Replace("'", "") + "','" + order.CreatedByUserId.ToString() + "','" + order.CreatedDate.ToString() + "','";
                    headerQuery += order.LastModifiedByUserId.ToString() + "','" + order.LastModifiedDate.ToString() + "')";
                }

                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }

                _conn.Open();
                SqlCommand cmd = new SqlCommand(headerQuery, _conn);

                cmd.ExecuteNonQuery();

                addOrderDetails(order.PurchaseOrderDetails.ToList());

                _conn.Close();
            }
        }

        public void addOrderDetails(List<PurchaseOrderDetail> details)
        {
            string detailQuery;

            foreach (var detail in details)
            {
                if (checkOrderDetailExists(detail.ParentPurchase.PurchaseOrderNumber,detail.LineNumber))
                {
                    detailQuery = "UPDATE tblTechPurchaseItemDetails ";
                    detailQuery += "SET QuantityOrdered = " + detail.QuantityOrdered.ToString() + ", ";
                    detailQuery += "PurchasePrice = " + detail.PurchasePrice + ", ";
                    detailQuery += "AccountCode = " + detail.AccountCode + ", ";
                    detailQuery += "FROM tblTechPurchaseItemDetails p JOIN tblTechPurchases p2 ";
                    detailQuery += "ON p.PurchaseUID = p2.PurchaseUID";
                    detailQuery += " WHERE p2.OrderNumber = '" + detail.ParentPurchase.PurchaseOrderNumber + "' AND p.LineNumber = " + detail.LineNumber.ToString();
                }
                else
                {
                    detailQuery = "INSERT INTO[dbo].[tblTechPurchaseItemDetails](";
                    detailQuery += "[PurchaseUID],[ItemUID],[FundingSourceUID],[StatusUID],[SiteAddedSiteUID],";
                    detailQuery += "[QuantityOrdered],[QuantityReceived],[PurchasePrice],[AccountCode],";
                    detailQuery += "[TechDepartmentUID],[CreatedByUserID],[CreatedDate],[LastModifiedByUserID],";
                    detailQuery += "[LastModifiedDate],[LineNumber]) ";
                    detailQuery += "VALUES ('"+ getPurchaseUIDFromOrderNumber(detail.ParentPurchase.PurchaseOrderNumber).ToString() + "','" + detail.ItemUID.ToString() + "','";
                    detailQuery += detail.FundingSourceUID.ToString() + "','" + detail.StatusUID + "','" + detail.SiteAddedSiteUID.ToString() + "','" + detail.QuantityOrdered.ToString();
                    detailQuery += "','" + detail.QuantityReceived.ToString() + "','" + detail.PurchasePrice + "','" + detail.AccountCode + "','" + detail.TechDepartmentUID.ToString();
                    detailQuery += "','" + detail.CreatedByUserID.ToString() + "','" + detail.CreatedDate.ToString() + "','" + detail.LastModifiedByUserID + "','" + detail.LastModifiedDate.ToString() + "')";
                }

                if(_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }

                SqlCommand cmd = new SqlCommand(detailQuery, _conn);
                cmd.ExecuteNonQuery();

            }
            
        }

        public void addItems(List<Item> items)
        {
            throw new NotImplementedException();
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
    }
}
