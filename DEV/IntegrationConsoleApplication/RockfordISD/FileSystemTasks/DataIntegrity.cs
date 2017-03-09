using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using DataAccess;
using SystemTasks;

namespace SystemTasks
{
    public class DataIntegrity
    {
        private IRepository _rep;
        public DataIntegrity(IRepository rep)
        {
            _rep = rep;
        }
        public List<PurchaseOrderFile> removeBadElements(List<PurchaseOrderFile> payload)
        {

            int badItems = payload.Where(it => it.OrderNumber.Trim() == "" || it.OrderNumber == null).Count();

            ErrorEventArgs args = new ErrorEventArgs();
            args.message = badItems.ToString() + " elements missing Order Numbers were removed.";
            args.actionName = "Data Integrity";
            args.type = Logging.ChangeType.Activity;
            OnAction(args);

            return payload.Where(items => items.OrderNumber.Trim() != "" && items.OrderNumber != null).ToList();
        }

        //site not found
        public bool siteNotFound(PurchaseOrderFile item)
        {
            try
            {
                int testItem = _rep.getSiteUIDFromName(item.ShippedToSite);
                return true;
            }

            catch(Exception e)
            {
                ErrorEventArgs args = new ErrorEventArgs();
                args.message = "Record Rejected";
                args.actionName = "Data Integrity";
                args.type = Logging.ChangeType.RejectRecord;
                args.Data = new ErrorData
                {
                    Reference = item.OrderNumber,
                    Reason = "Site not found.",
                    ExceptionMessage = e.Message,
                    RejectedValue = item.ShippedToSite.ToString(),
                    LineNumber = item.LineNumber
                };
                OnRejectRecord(args);

                return false;
            }
        }

        public bool productNotFound(string productName)
        {
            try
            {
                int testItem = _rep.getItemUIDFromName(productName);
                return false;
            }
            catch
            {
                return true;
            }
        }

        //product not found in catalog
        public bool productNotFound(PurchaseOrderFile item)
        {
            try
            {
                int testItem = _rep.getItemUIDFromName(item.ProductName);
                return true;
            }

            catch (Exception e)
            {
                ErrorEventArgs args = new ErrorEventArgs();
                args.message = "Record Rejected";
                args.actionName = "Data Integrity";
                args.type = Logging.ChangeType.RejectRecord;
                args.Data = new ErrorData
                {
                    Reference = item.OrderNumber,
                    Reason = "Product not found in TIPWeb-IT catalog",
                    ExceptionMessage = e.Message,
                    RejectedValue = item.ProductName,
                    LineNumber = item.LineNumber
                };
                OnRejectRecord(args);
                return false;
            }
        }

        //model does not match existing product
        public bool modelNotFound(PurchaseOrderFile item)
        {
            try
            {
                string testItem = _rep.getModelNumberFromProductName(item.ProductName);

                if (testItem.ToLower() != item.Model.ToLower())
                {
                    throw new Exception();
                }

                return true;
            }

            catch (Exception e)
            {
                ErrorEventArgs args = new ErrorEventArgs();
                args.message = "Record Rejected";
                args.actionName = "Data Integrity";
                args.type = Logging.ChangeType.RejectRecord;
                args.Data = new ErrorData
                {
                    Reference = item.OrderNumber,
                    Reason = "Model does not match existing product in TIPWeb-IT catalog",
                    ExceptionMessage = e.Message,
                    RejectedValue = item.Model,
                    LineNumber = item.LineNumber
                };
                OnRejectRecord(args);
                return false;
            }
        }

        public bool vendorNotFound(string vendorName)
        {
            try
            {
                int testItem = _rep.getVendorUIDFromName(vendorName);
                return false;
            }
            catch
            {
                return true;
            }
        }

        //vendor not found
        public bool vendorNotFound(PurchaseOrderFile item)
        {
            try
            {
                int testItem = _rep.getVendorUIDFromName(item.VendorName);

                return true;
            }

            catch (Exception e)
            {
                ErrorEventArgs args = new ErrorEventArgs();
                args.message = "Record Rejected";
                args.actionName = "Data Integrity";
                args.type = Logging.ChangeType.RejectRecord;
                args.Data = new ErrorData
                {
                    Reference = item.OrderNumber,
                    Reason = "Vendor not found in TIPWeb-IT.",
                    ExceptionMessage = e.Message,
                    RejectedValue = item.VendorName,
                    LineNumber = item.LineNumber
                };
                OnRejectRecord(args);
                return false;
            }
        }

        //purchase date missing or invalid
        public bool purchaseDateMissingOrInvalid(PurchaseOrderFile item)
        {
            if (!item.OrderDate.IsValidDateFromString(true))
            {
                ErrorEventArgs args = new ErrorEventArgs();
                args.message = "Record Rejected";
                args.actionName = "Data Integrity";
                args.type = Logging.ChangeType.RejectRecord;
                args.Data = new ErrorData
                {
                    Reference = item.OrderNumber,
                    Reason = "Purchase Date missing or invalid.",
                    ExceptionMessage = "",
                    RejectedValue = item.OrderDate,
                    LineNumber = item.LineNumber
                };
                OnRejectRecord(args);

                return false;
            }
            
            else
            {
                return true;
            }         
        }

        //invalid quantity

        //invalid quantity shipped
        
        //invalid purchase price

        //invalid line number
        public bool invalidLineNumber(PurchaseOrderFile item)
        {
            if (item.LineNumber <= 0)
            {
                ErrorEventArgs args = new ErrorEventArgs();
                args.message = "Record Rejected";
                args.actionName = "Data Integrity";
                args.type = Logging.ChangeType.RejectRecord;
                args.Data = new ErrorData
                {
                    Reference = item.OrderNumber,
                    Reason = "Invalid Line Number",
                    ExceptionMessage = "",
                    RejectedValue = item.LineNumber.ToString(),
                    LineNumber = item.LineNumber
                };
                OnRejectRecord(args);

                return true;
            }
            else
            {
                return false;
            }
        }


        public bool missingFundingSource(string fundingSource)
        {
            try
            {
                _rep.getFundingSourceUIDFromName(fundingSource);
                return false;
            }
            catch
            {
                return true;
            }
        }
        //no funding source
        public bool missingFundingSource(PurchaseOrderFile item)
        {
            if (string.IsNullOrEmpty(item.FundingSource.Trim()))
            {
                ErrorEventArgs args = new ErrorEventArgs();
                args.message = "Record Rejected";
                args.actionName = "Data Integrity";
                args.type = Logging.ChangeType.RejectRecord;
                args.Data = new ErrorData
                {
                    Reference = item.OrderNumber,
                    Reason = "Missing Funding Source",
                    ExceptionMessage = "",
                    RejectedValue = item.FundingSource.ToString(),
                    LineNumber = item.LineNumber
                };
                OnRejectRecord(args);

                return true;
            }
            else
            {
                try
                {
                    _rep.getFundingSourceUIDFromName(item.FundingSource);
                    return false;
                }
                catch(Exception e)
                {
                    ErrorEventArgs args = new ErrorEventArgs();
                    args.message = "Record Rejected";
                    args.actionName = "Data Integrity";
                    args.type = Logging.ChangeType.RejectRecord;
                    args.Data = new ErrorData
                    {
                        Reference = item.OrderNumber,
                        Reason = "Missing Funding Source",
                        ExceptionMessage = "",
                        RejectedValue = item.FundingSource.ToString(),
                        LineNumber = item.LineNumber
                    };
                    OnRejectRecord(args);
                    return true;
                }
                
            }
        }
        //

        public bool checkMissingDataElement(string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public event EventHandler<ErrorEventArgs> Error;
        public event EventHandler<ErrorEventArgs> Action;
        public event EventHandler<ErrorEventArgs> Reject;

        protected virtual void OnRejectRecord(ErrorEventArgs e)
        {
            EventHandler<ErrorEventArgs> handler = Reject;

            handler(this, e);
        }

        protected virtual void OnError(ErrorEventArgs e)
        {
            EventHandler<ErrorEventArgs> handler = Error;

            handler(this, e);
        }

        public virtual void OnAction(ErrorEventArgs e)
        {
            EventHandler<ErrorEventArgs> handler = Action;

            handler(this, e);
        }

        //static void OnError(object sender, ErrorEventArgs e)
        //{
        //    _log.log(e.message, e.actionName, e.type);
        //}

        //static void OnAction(object sender, ErrorEventArgs e)
        //{
        //    _log.log(e.message, e.actionName, e.type);
        //}
    }

}
