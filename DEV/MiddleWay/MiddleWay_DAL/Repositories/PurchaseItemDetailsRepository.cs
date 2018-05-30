using MiddleWay_DTO.Models;
using MiddleWay_DAL.EF_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DAL.Repositories
{
    public class PurchaseItemDetailsRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public PurchaseItemDetailsRepository(TIPWebContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions


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

        #endregion Select Functions

        #region Insert Functions

        public void addOrderDetails(List<PurchaseItemDetailModel> details)
        {
            string detailQuery;

            foreach (var detail in details)
            {
                string message;

                if (checkOrderDetailExists(detail.ParentPurchase.PurchaseOrderNumber, detail.LineNumber))
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
                    detailQuery += "VALUES ('" + getPurchaseUIDFromOrderNumber(detail.ParentPurchase.PurchaseOrderNumber).ToString() + "','" + detail.ItemUID.ToString() + "','";
                    detailQuery += detail.FundingSourceUID.ToString() + "','" + detail.StatusUID + "','" + detail.SiteAddedSiteUID.ToString() + "','" + detail.QuantityOrdered.ToString();
                    detailQuery += "','" + detail.QuantityReceived.ToString() + "','" + detail.PurchasePrice + "','" + detail.AccountCode + "','" + detail.TechDepartmentUID.ToString();
                    detailQuery += "','" + detail.CreatedByUserID.ToString() + "','" + detail.CreatedDate.ToString() + "','" + detail.LastModifiedByUserID + "','" + detail.LastModifiedDate.ToString() + "','" + detail.LineNumber.ToString() + "')";

                    message = "Successfully added detail for order number " + detail.ParentPurchase.PurchaseOrderNumber + " and Line Number " + detail.LineNumber.ToString();
                }

                if (_conn.State == ConnectionState.Closed)
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

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
