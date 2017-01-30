﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess
{
    class WebServiceRepository : IRepository
    {
        public event EventHandler<DbActivityEventArgs> Action;
        public event EventHandler<DbErrorEventArgs> Error;

        public void addItems(List<Item> items)
        {
            throw new NotImplementedException();
        }

        public void addOrderDetails(List<PurchaseOrderDetail> details)
        {
            throw new NotImplementedException();
        }

        public void addOrderHeaders(List<PurchaseOrderHeader> orders)
        {
            throw new NotImplementedException();
        }

        public bool checkOrderDetailExists(string orderNumber, int lineNumber)
        {
            throw new NotImplementedException();
        }

        public bool checkOrderExists(string orderNumber)
        {
            throw new NotImplementedException();
        }

        public void completeIntegration()
        {
            throw new NotImplementedException();
        }

        public int getAreaUIDFromName(string areaName)
        {
            throw new NotImplementedException();
        }

        public int getContainerUID(string name)
        {
            throw new NotImplementedException();
        }

        public int getFundingSourceUIDFromName(string sourceName)
        {
            throw new NotImplementedException();
        }

        public int getInventorySourceUID(string name)
        {
            throw new NotImplementedException();
        }

        public Item getItemFromName(string productName)
        {
            throw new NotImplementedException();
        }

        public int getItemTypeUIDFromName(string itemType)
        {
            throw new NotImplementedException();
        }

        public int getItemUIDFromName(string name)
        {
            throw new NotImplementedException();
        }

        public int getManufacturerUIDFromName(string manufacturerName)
        {
            throw new NotImplementedException();
        }

        public string getModelNumberFromProductName(string productName)
        {
            throw new NotImplementedException();
        }

        public int getNewImportCode()
        {
            throw new NotImplementedException();
        }

        public List<RejectedRecord> getRejectionsFromLastImport()
        {
            throw new NotImplementedException();
        }

        public List<RejectedRecord> getRejectionsFromLastImport(int importId)
        {
            throw new NotImplementedException();
        }

        public int getSiteUIDFromName(string siteName)
        {
            throw new NotImplementedException();
        }

        public int getStatusUID(string status)
        {
            throw new NotImplementedException();
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

        public void logAction(string actionName, string actionDescription)
        {
            throw new NotImplementedException();
        }

        public void logError(string message, string exceptionMessage)
        {
            throw new NotImplementedException();
        }

        public void logRejectRecord(List<RejectedRecord> rejections)
        {
            throw new NotImplementedException();
        }

        public void logRejectRecord(RejectedRecord rejection)
        {
            throw new NotImplementedException();
        }
    }
}
