using TIPWeb_Controller.DataProvider;
using MiddleWay_DTO.Models;
using TIPWeb_Controller.EF_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using MiddleWay_DTO.Models.TIPWeb_Controller;
using MiddleWay_DTO.Enumerations;

namespace TIPWeb_Controller.Repositories
{
    public class PurchaseItemDetailsRepository
    {
        #region Private Variables and Properties

        private TIPWebContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public PurchaseItemDetailsRepository(IDataProviderFactory dataProvider)
        {
            _context = dataProvider.GetContext();
        }

        #endregion Constructor

        #region Select Functions

        public bool CheckOrderDetailExists(string orderNumber, int lineNumber)
        {
            try
            {
                var exists = (from purchases in _context.TblTechPurchases
                              join purchaseItemDetails in _context.TblTechPurchaseItemDetails
                              on purchases.PurchaseUid equals purchaseItemDetails.PurchaseUid
                              where purchases.OrderNumber.Trim().ToLower() == orderNumber.Trim().ToLower()
                                 && purchaseItemDetails.LineNumber == lineNumber
                              select true).FirstOrDefault();

                return exists;
            }
            catch
            {
                throw;
            }
            //string returnQuery = "SELECT p.* FROM tblTechPurchaseItemDetails p ";
            //returnQuery += "JOIN tblTechPurchases p2 ON p.PurchaseUid = p2.PurchaseUid WHERE p2.OrderNumber = '" + orderNumber + "' AND p.LineNumber = " + lineNumber.ToString();

            //return false;

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

        #endregion Select Functions

        #region Insert Functions

        public void AddOrderDetails(List<PurchaseItemDetailModel> details)
        {
            try
            {
                //string detailQuery;

                foreach (var detail in details)
                {
                    string message;

                    if (CheckOrderDetailExists(detail.ParentPurchase.PurchaseOrderNumber, detail.LineNumber))
                    {
                        var detailToUpdate = (from purchases in _context.TblTechPurchases
                                              join purchaseItemDetails in _context.TblTechPurchaseItemDetails
                                                on purchases.PurchaseUid equals purchaseItemDetails.PurchaseUid
                                              where purchases.OrderNumber == detail.ParentPurchase.PurchaseOrderNumber
                                                 && purchaseItemDetails.LineNumber == detail.LineNumber
                                              select purchaseItemDetails).FirstOrDefault();

                        detailToUpdate.QuantityOrdered = detail.QuantityOrdered;
                        detailToUpdate.QuantityReceived = detail.QuantityReceived;
                        detailToUpdate.PurchasePrice = detail.PurchasePrice;
                        detailToUpdate.AccountCode = detail.AccountCode;
                        detailToUpdate.StatusUid = (int)ReceivingStatus.Open;
                        detailToUpdate.SiteAddedSiteUid = detail.SiteAddedSiteUid;
                        detailToUpdate.LastModifiedByUserId = 0;
                        detailToUpdate.LastModifiedDate = DateTime.Now;

                        _context.TblTechPurchaseItemDetails.Update(detailToUpdate);

                        //detailQuery = "UPDATE tblTechPurchaseItemDetails ";
                        //detailQuery += "SET QuantityOrdered = " + detail.QuantityOrdered.ToString() + ", ";
                        //detailQuery += "QuantityReceived = " + detail.QuantityReceived + ", ";
                        //detailQuery += "PurchasePrice = " + detail.PurchasePrice + ", ";
                        //detailQuery += "AccountCode = '" + detail.AccountCode + "', ";
                        //detailQuery += "StatusUid = 32,";
                        //detailQuery += "SiteAddedSiteUid = '" + detail.SiteAddedSiteUid + "',";
                        //detailQuery += "LastModifiedByUserID = 0,";
                        //detailQuery += "LastModifiedDate = '" + detail.LastModifiedDate.ToString() + "'";
                        //detailQuery += "FROM tblTechPurchaseItemDetails p JOIN tblTechPurchases p2 ";
                        //detailQuery += "ON p.PurchaseUid = p2.PurchaseUid";
                        //detailQuery += " WHERE p2.OrderNumber = '" + detail.ParentPurchase.PurchaseOrderNumber + "' AND p.LineNumber = " + detail.LineNumber.ToString();

                        message = "Detail record already exists. Updated detail record for order number " + detail.ParentPurchase.PurchaseOrderNumber + " and Line Number " + detail.LineNumber.ToString();
                    }
                    else
                    {
                        var purchaseUid = (from purchases in _context.TblTechPurchases
                                           where purchases.OrderNumber == detail.ParentPurchase.PurchaseOrderNumber
                                           select purchases.PurchaseUid).FirstOrDefault();

                        var detailToInsert = new TblTechPurchaseItemDetails()
                        {
                            PurchaseUid = purchaseUid,
                            LineNumber = detail.LineNumber,
                            ItemUid = detail.ItemUid,
                            FundingSourceUid = detail.FundingSourceUid,
                            StatusUid = (int)ReceivingStatus.Open,
                            SiteAddedSiteUid = detail.SiteAddedSiteUid,
                            QuantityOrdered = detail.QuantityOrdered,
                            QuantityReceived = detail.QuantityReceived,
                            PurchasePrice = detail.PurchasePrice,
                            AccountCode = detail.AccountCode,
                            TechDepartmentUid = detail.TechDepartmentUid,
                            CreatedByUserId = 0,
                            CreatedDate = DateTime.Now,
                            LastModifiedByUserId = 0,
                            LastModifiedDate = DateTime.Now
                        };

                        _context.TblTechPurchaseItemDetails.Add(detailToInsert);

                        //detailQuery = "INSERT INTO [dbo].[tblTechPurchaseItemDetails](";
                        //detailQuery += "[PurchaseUid],[ItemUid],[FundingSourceUid],[StatusUid],[SiteAddedSiteUid],";
                        //detailQuery += "[QuantityOrdered],[QuantityReceived],[PurchasePrice],[AccountCode],";
                        //detailQuery += "[TechDepartmentUid],[CreatedByUserID],[CreatedDate],[LastModifiedByUserID],";
                        //detailQuery += "[LastModifiedDate],[LineNumber]) ";
                        ////detailQuery += "VALUES ('" + GetPurchaseUidFromOrderNumber(detail.ParentPurchase.PurchaseOrderNumber).ToString() + "','" + detail.ItemUid.ToString() + "','";
                        //detailQuery += detail.FundingSourceUid.ToString() + "','" + detail.StatusUid + "','" + detail.SiteAddedSiteUid.ToString() + "','" + detail.QuantityOrdered.ToString();
                        //detailQuery += "','" + detail.QuantityReceived.ToString() + "','" + detail.PurchasePrice + "','" + detail.AccountCode + "','" + detail.TechDepartmentUid.ToString();
                        //detailQuery += "','" + detail.CreatedByUserID.ToString() + "','" + detail.CreatedDate.ToString() + "','" + detail.LastModifiedByUserID + "','" + detail.LastModifiedDate.ToString() + "','" + detail.LineNumber.ToString() + "')";

                        message = "Successfully added detail for order number " + detail.ParentPurchase.PurchaseOrderNumber + " and Line Number " + detail.LineNumber.ToString();
                    }

                    _context.SaveChanges();

                    //if (_conn.State == ConnectionState.Closed)
                    //{
                    //    _conn.Open();
                    //}

                    //try
                    //{
                    //    SqlCommand cmd = new SqlCommand(detailQuery, _conn);
                    //    cmd.ExecuteNonQuery();

                    //    DbActivityEventArgs args = new DbActivityEventArgs();
                    //    args.ActivityStep = "Add Header and Detail Data";
                    //    args.ActivityMessage = message;
                    //    OnAction(args);
                    //}
                    //catch (Exception e)
                    //{
                    //    DbErrorEventArgs args = new DbErrorEventArgs();
                    //    args.InterfaceMessage = "ERROR adding detail for Order Number " + detail.ParentPurchase.PurchaseOrderNumber + " and Line Number " + detail.LineNumber.ToString();
                    //    args.ExceptionMessage = e.Message;
                    //    continue;
                    //}

                }
            }
            catch
            {
                throw;
            }
        }

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
