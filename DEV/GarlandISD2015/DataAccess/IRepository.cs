using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository
    {
        void addShipmentInfo();
        int getUserIdFromName(string userName);
        int getStatusUID(string status);
        int getVendorUIDFromName(string vendorName);
        int getSiteUIDFromName(string siteName);
        int getItemTypeUIDFromName(string itemType);
        int getManufacturerUIDFromName(string manufacturerName);
        int getAreaUIDFromName(string areaName);
        int getTechDepartmentUIDFromName(string name);
        int getFundingSourceUIDFromName(string sourceName);
        int getContainerUID(string name);
        int getInventorySourceUID(string name);
        Item getItemFromName(string productName);
        string getModelNumberFromProductName(string productName);
        int getItemUIDFromName(string name);
        void logError(string message, string exceptionMessage);
        void logAction(string actionName, string actionDescription);
        void logRejectRecord(RejectedRecord rejection);
        void logRejectRecord(List<RejectedRecord> rejections);
        int getNewImportCode();   
        void addOrderHeaders(List<PurchaseOrderHeader> orders);
        void addOrderDetails(List<PurchaseOrderDetail> details);
        void addItems(List<Item> items);
        bool checkOrderExists(string orderNumber);
        bool checkOrderDetailExists(string orderNumber, int lineNumber);
        List<RejectedRecord> getRejectionsFromLastImport();
        void completeIntegration();

        void sendEmail(string ProfileName, string Recipients, string Subject, string Body, string Attachment = null);

        void removeExistingBadDetailRecordsByPurchaseOrderNumber(List<PurchaseOrderHeader> orders);

        event EventHandler<DbErrorEventArgs> Error;

        event EventHandler<DbActivityEventArgs> Action;

        bool getDetailLinesWithZeroLineNumber(string orderNumber);
        string getItemIfHasTags(string orderNumber, int lineNumber);
    }
}
