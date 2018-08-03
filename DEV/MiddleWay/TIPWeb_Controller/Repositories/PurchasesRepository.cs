using TIPWeb_Controller.DataProvider;
using TIPWeb_Controller.EF_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using MiddleWay_DTO.Models.TIPWeb_Controller;
using MiddleWay_DTO.RepositoryInterfaces.TIPWeb;
using MiddleWay_DTO.Models.MiddleWay_BLL; //TODO: Modify?

namespace TIPWeb_Controller.Repositories
{
    public class PurchasesRepository : IPurchasesRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public PurchasesRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        public List<ReceivedTagsExportFile> ExportReceivedTags()
        {
            List<ReceivedTagsExportFile> export = new List<ReceivedTagsExportFile>();

            string returnQuery = "SELECT DISTINCT p.ordernumber, '0' as AmountAccepted, p.PurchaseDate, p.PurchaseDate as PDate, ItemNumber, det.QuantityOrdered, '0' as AmountDamaged, det.LineNumber, inv.AssetID, 'R' as TypeOfR ";
            returnQuery += "FROM tblTechInventory inv ";
            returnQuery += "JOIN tblTechItems item on item.ItemUid = inv.ItemUid ";
            returnQuery += "JOIN tblTechPurchaseInventory pinv on pinv.InventoryUid = inv.InventoryUid ";
            returnQuery += "JOIN tblTechPurchaseItemShipments ship on ship.PurchaseItemShipmentUid = pinv.PurchaseItemShipmentUid ";
            returnQuery += "JOIN tblTechPurchaseItemDetails det on det.PurchaseItemDetailUid = ship.PurchaseItemDetailUid ";
            returnQuery += "JOIN tblTechPurchases p on p.PurchaseUid = det.PurchaseUid";

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
            //        export.Add(new ReceivedTagsExportFile
            //        {
            //            POR_REF_NO = (string)reader[0], //OrderNumber
            //            POR_AMOUNT = (string)reader[1], //0
            //            POR_DT = (string)reader[2], //PurchaseDate
            //            POR_ENTRY_DT = (string)reader[3], //PurchaseDate
            //            POR_ITEM = (string)reader[4], //ItemCode
            //            POR_QTY = (string)reader[5], //Quantity
            //            POR_QTY_DAM = (string)reader[6], //0
            //            POR_SEQ = (string)reader[7], //LineNumber
            //            POR_TAG = (string)reader[8], //AssetID or blank
            //            POR_TYPE = (string)reader[9] //R
            //        });
            //    }

            //    catch (Exception e)
            //    {
            //        //DbErrorEventArgs args = new DbErrorEventArgs();
            //        //args.InterfaceMessage = "Unable to export received tags.";
            //        //args.ExceptionMessage = e.Message;
            //        //OnError(args);
            //        break;
            //    }
            //}

            //reader.Close();
            //_conn.Close();

            return export;

        }

        public bool CheckOrderExists(string orderNumber)
        {

            string returnQuery = "SELECT OrderNumber FROM tblTechPurchases WHERE OrderNumber = '" + orderNumber + "'";
            return false;

            //if (_conn.State == ConnectionState.Open)
            //{
            //    _conn.Close();
            //}

            //_conn.Open();
            //SqlCommand returnCmd = new SqlCommand(returnQuery, _conn);

            //SqlDataReader reader = returnCmd.ExecuteReader();

            //if (reader.HasRows)
            //{
            //    //reader.Close();
            //    //_conn.Close();
            //    return true;
            //}
            //else
            //{
            //    //reader.Close();
            //    //_conn.Close();
            //    return false;
            //}


        }


        private int GetPurchaseUidFromOrderNumber(string orderNumber)
        {
            int purchaseUid = -1;

            string returnQuery = "SELECT PurchaseUid FROM tblTechPurchases WHERE OrderNumber = '" + orderNumber + "'";

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
            //        purchaseUid = (int)reader[0];
            //    }

            //    catch
            //    {
            //        purchaseUid = -1;
            //    }
            //}

            //reader.Close();
            //_conn.Close();

            if (purchaseUid == -1)
            {
                throw new Exception("The specified Item Type was not found.");
            }
            else
            {
                return purchaseUid;
            }
        }
        
        #endregion Select Functions

        #region Insert Functions

        public void AddOrderHeaders(List<PurchaseModel> orders)
        {

            foreach (var order in orders)
            {
                try
                {
                    string headerQuery;
                    string message;

                    if (CheckOrderExists(order.PurchaseOrderNumber))
                    {
                        headerQuery = "UPDATE tblTechPurchases ";
                        headerQuery += "SET PurchaseDate = '" + order.PurchaseDate.ToString() + "', ";
                        headerQuery += "Notes = '" + order.Notes.Replace("'", "") + "', ";
                        headerQuery += "VendorUid = " + order.VendorUid + ", ";
                        headerQuery += "SiteUid = " + order.SiteId + ", ";
                        headerQuery += "StatusUid = 32,";
                        headerQuery += "LastModifiedByUserId = '0', ";
                        headerQuery += "LastModifiedDate = '" + DateTime.Now.ToString() + "' ";
                        headerQuery += "FROM tblTechPurchases WHERE OrderNumber = '" + order.PurchaseOrderNumber + "'";

                        message = "Order already exists. Updated header for order number " + order.PurchaseOrderNumber;
                    }

                    else
                    {
                        headerQuery = "INSERT INTO [dbo].[tblTechPurchases] ([StatusUid],[VendorUid],[SiteUid],[OrderNumber],[PurchaseDate],[Notes],[CreatedByUserID],[CreatedDate],[LastModifiedByUserID],[LastModifiedDate]) ";
                        headerQuery += "VALUES ('" + order.StatusUid.ToString() + "','" + order.VendorUid.ToString() + "','" + order.SiteId.ToString() + "','" + order.PurchaseOrderNumber + "','";
                        headerQuery += order.PurchaseDate.ToString() + "','" + order.Notes.Replace("'", "") + "','" + order.CreatedByUserId.ToString() + "','" + order.CreatedDate.ToString() + "','";
                        headerQuery += order.LastModifiedByUserId.ToString() + "','" + order.LastModifiedDate.ToString() + "')";

                        message = "Successfully added header for order number " + order.PurchaseOrderNumber;
                    }

                    //if (_conn.State == ConnectionState.Open)
                    //{
                    //    _conn.Close();
                    //}

                    //_conn.Open();
                    //SqlCommand cmd = new SqlCommand(headerQuery, _conn);

                    //cmd.ExecuteNonQuery();

                    //DbActivityEventArgs args = new DbActivityEventArgs();
                    //args.ActivityStep = "Add Header and Detail Data";
                    //args.ActivityMessage = message;
                    //OnAction(args);

                    //addOrderDetails(order.PurchaseOrderDetails.ToList());

                    //_conn.Close();
                }
                catch (Exception e)
                {
                    //DbErrorEventArgs args = new DbErrorEventArgs();
                    //args.InterfaceMessage = "Error adding product headers or details for order number " + order.PurchaseOrderNumber;
                    //args.ExceptionMessage = e.Message;
                    //OnError(args);
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
