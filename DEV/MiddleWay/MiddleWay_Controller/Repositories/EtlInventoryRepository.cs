using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWay_Controller.Repositories
{
    public class EtlInventoryRepository : IEtlInventoryRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public EtlInventoryRepository(IntegrationMiddleWayContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Methods

        public EtlInventoryModel Select(int etlInventoryUid)
        {
            try
            {
                return null;
                //var inventory = (from etlInventory in _context.EtlInventory
                //                 where etlInventory.EtlInventoryUid == etlInventoryUid
                //                 select new EtlInventoryModel
                //                 {
                //                     _ETL_InventoryUID = etlInventory.EtlInventoryUid,
                //                     ProcessUid = etlInventory.ProcessUid,
                //                     InventoryUID = etlInventory.InventoryUid,
                //                     AssetID = etlInventory.AssetId,
                //                     Tag = etlInventory.Tag,
                //                     Serial = etlInventory.Serial,
                //                     InventoryTypeUID = etlInventory.InventoryTypeUid,
                //                     InventoryTypeName = etlInventory.InventoryTypeName,
                //                     ItemUID = etlInventory.ItemUid,
                //                     ProductName = etlInventory.ProductName,
                //                     ProductDescription = etlInventory.ProductDescription,
                //                     ProductByNumber = etlInventory.ProductByNumber,
                //                     ItemTypeUID = etlInventory.ItemTypeUid,
                //                     ProductTypeName = etlInventory.ProductTypeName,
                //                     ProductTypeDescription = etlInventory.ProductTypeDescription,
                //                     ModelNumber = etlInventory.ModelNumber,
                //                     ManufacturerUID = etlInventory.ManufacturerUid,
                //                     ManufacturerName = etlInventory.ManufacturerName,
                //                     AreaUID = etlInventory.AreaUid,
                //                     AreaName = etlInventory.AreaName,
                //                     SiteUID = etlInventory.SiteUid,
                //                     SiteID = etlInventory.SiteId,
                //                     SiteName = etlInventory.SiteName,
                //                     EntityUID = etlInventory.EntityUid,
                //                     EntityID = etlInventory.EntityId,
                //                     EntityName = etlInventory.EntityName,
                //                     EntityTypeUID = etlInventory.EntityTypeUid,
                //                     EntityTypeName = etlInventory.EntityTypeName,
                //                     StatusID = etlInventory.StatusId,
                //                     Status = etlInventory.Status,
                //                     TechDepartmentUID = etlInventory.TechDepartmentUid,
                //                     DepartmentName = etlInventory.DepartmentName,
                //                     DepartmentID = etlInventory.DepartmentId,
                //                     FundingSourceUID = etlInventory.FundingSourceUid,
                //                     FundingSource = etlInventory.FundingSource,
                //                     FundingSourceDescription = etlInventory.FundingSourceDescription,
                //                     PurchasePrice = etlInventory.PurchasePrice,
                //                     PurchaseDate = etlInventory.PurchaseDate,
                //                     ExpirationDate = etlInventory.ExpirationDate,
                //                     InventoryNotes = etlInventory.InventoryNotes,
                //                     ParentInventoryUID = etlInventory.ParentInventoryUid,
                //                     ParentTag = etlInventory.ParentTag,
                //                     InventorySourceUID = etlInventory.InventorySourceUid,
                //                     InventorySourceName = etlInventory.InventorySourceName,
                //                     PurchaseUID = etlInventory.PurchaseUid,
                //                     OrderNumber = etlInventory.OrderNumber,
                //                     PurchaseItemDetailUID = etlInventory.PurchaseItemDetailUid,
                //                     LineNumber = etlInventory.LineNumber,
                //                     AccountCode = etlInventory.AccountCode,
                //                     VendorUID = etlInventory.VendorUid,
                //                     VendorName = etlInventory.VendorName,
                //                     VendorAccountNumber = etlInventory.VendorAccountNumber,
                //                     PurchaseItemShipmentUID = etlInventory.PurchaseItemShipmentUid,
                //                     InvoiceNumber = etlInventory.InvoiceNumber,
                //                     InvoiceDate = etlInventory.InvoiceDate,
                //                     InventoryExt1UID = etlInventory.InventoryExt1Uid,
                //                     InventoryMeta1UID = etlInventory.InventoryMeta1Uid,
                //                     CustomField1Label = etlInventory.CustomField1Label,
                //                     CustomField1Value = etlInventory.CustomField1Value,
                //                     InventoryExt2UID = etlInventory.InventoryExt2Uid,
                //                     InventoryMeta2UID = etlInventory.InventoryMeta2Uid,
                //                     CustomField2Label = etlInventory.CustomField2Label,
                //                     CustomField2Value = etlInventory.CustomField2Value,
                //                     InventoryExt3UID = etlInventory.InventoryExt3Uid,
                //                     InventoryMeta3UID = etlInventory.InventoryMeta3Uid,
                //                     CustomField3Label = etlInventory.CustomField3Label,
                //                     CustomField3Value = etlInventory.CustomField3Value,
                //                     InventoryExt4UID = etlInventory.InventoryExt4Uid,
                //                     InventoryMeta4UID = etlInventory.InventoryMeta4Uid,
                //                     CustomField4Label = etlInventory.CustomField4Label,
                //                     CustomField4Value = etlInventory.CustomField4Value
                //                 }).FirstOrDefault();
                //return inventory;
            }
            catch
            {
                throw;
            }
        }

        public EtlInventoryModel SelectByAssetID(string client, string processName, string assetId)
        {
            try
            {
                var clientVal = client.Trim().ToLower();
                var processNameVal = processName.Trim().ToLower();
                var assetIdVal = assetId.Trim().ToLower();
                return null;
                //var inventory = (from etlInventory in _context.EtlInventory
                //                 join processes in _context.Processes
                //                    on etlInventory.ProcessUid equals processes.ProcessUid
                //                 where processes.Client.Trim().ToLower() == clientVal
                //                    && processes.ProcessName.Trim().ToLower() == processNameVal
                //                    && etlInventory.AssetId.Trim().ToLower() == assetIdVal
                //                 select new EtlInventoryModel
                //                 {
                //                     _ETL_InventoryUID = etlInventory.EtlInventoryUid,
                //                     ProcessUid = etlInventory.ProcessUid,
                //                     InventoryUID = etlInventory.InventoryUid,
                //                     AssetID = etlInventory.AssetId,
                //                     Tag = etlInventory.Tag,
                //                     Serial = etlInventory.Serial,
                //                     InventoryTypeUID = etlInventory.InventoryTypeUid,
                //                     InventoryTypeName = etlInventory.InventoryTypeName,
                //                     ItemUID = etlInventory.ItemUid,
                //                     ProductName = etlInventory.ProductName,
                //                     ProductDescription = etlInventory.ProductDescription,
                //                     ProductByNumber = etlInventory.ProductByNumber,
                //                     ItemTypeUID = etlInventory.ItemTypeUid,
                //                     ProductTypeName = etlInventory.ProductTypeName,
                //                     ProductTypeDescription = etlInventory.ProductTypeDescription,
                //                     ModelNumber = etlInventory.ModelNumber,
                //                     ManufacturerUID = etlInventory.ManufacturerUid,
                //                     ManufacturerName = etlInventory.ManufacturerName,
                //                     AreaUID = etlInventory.AreaUid,
                //                     AreaName = etlInventory.AreaName,
                //                     SiteUID = etlInventory.SiteUid,
                //                     SiteID = etlInventory.SiteId,
                //                     SiteName = etlInventory.SiteName,
                //                     EntityUID = etlInventory.EntityUid,
                //                     EntityID = etlInventory.EntityId,
                //                     EntityName = etlInventory.EntityName,
                //                     EntityTypeUID = etlInventory.EntityTypeUid,
                //                     EntityTypeName = etlInventory.EntityTypeName,
                //                     StatusID = etlInventory.StatusId,
                //                     Status = etlInventory.Status,
                //                     TechDepartmentUID = etlInventory.TechDepartmentUid,
                //                     DepartmentName = etlInventory.DepartmentName,
                //                     DepartmentID = etlInventory.DepartmentId,
                //                     FundingSourceUID = etlInventory.FundingSourceUid,
                //                     FundingSource = etlInventory.FundingSource,
                //                     FundingSourceDescription = etlInventory.FundingSourceDescription,
                //                     PurchasePrice = etlInventory.PurchasePrice,
                //                     PurchaseDate = etlInventory.PurchaseDate,
                //                     ExpirationDate = etlInventory.ExpirationDate,
                //                     InventoryNotes = etlInventory.InventoryNotes,
                //                     ParentInventoryUID = etlInventory.ParentInventoryUid,
                //                     ParentTag = etlInventory.ParentTag,
                //                     InventorySourceUID = etlInventory.InventorySourceUid,
                //                     InventorySourceName = etlInventory.InventorySourceName,
                //                     PurchaseUID = etlInventory.PurchaseUid,
                //                     OrderNumber = etlInventory.OrderNumber,
                //                     PurchaseItemDetailUID = etlInventory.PurchaseItemDetailUid,
                //                     LineNumber = etlInventory.LineNumber,
                //                     AccountCode = etlInventory.AccountCode,
                //                     VendorUID = etlInventory.VendorUid,
                //                     VendorName = etlInventory.VendorName,
                //                     VendorAccountNumber = etlInventory.VendorAccountNumber,
                //                     PurchaseItemShipmentUID = etlInventory.PurchaseItemShipmentUid,
                //                     InvoiceNumber = etlInventory.InvoiceNumber,
                //                     InvoiceDate = etlInventory.InvoiceDate,
                //                     InventoryExt1UID = etlInventory.InventoryExt1Uid,
                //                     InventoryMeta1UID = etlInventory.InventoryMeta1Uid,
                //                     CustomField1Label = etlInventory.CustomField1Label,
                //                     CustomField1Value = etlInventory.CustomField1Value,
                //                     InventoryExt2UID = etlInventory.InventoryExt2Uid,
                //                     InventoryMeta2UID = etlInventory.InventoryMeta2Uid,
                //                     CustomField2Label = etlInventory.CustomField2Label,
                //                     CustomField2Value = etlInventory.CustomField2Value,
                //                     InventoryExt3UID = etlInventory.InventoryExt3Uid,
                //                     InventoryMeta3UID = etlInventory.InventoryMeta3Uid,
                //                     CustomField3Label = etlInventory.CustomField3Label,
                //                     CustomField3Value = etlInventory.CustomField3Value,
                //                     InventoryExt4UID = etlInventory.InventoryExt4Uid,
                //                     InventoryMeta4UID = etlInventory.InventoryMeta4Uid,
                //                     CustomField4Label = etlInventory.CustomField4Label,
                //                     CustomField4Value = etlInventory.CustomField4Value
                //                 }).FirstOrDefault();
                //return inventory;
            }
            catch
            {
                throw;
            }
        }

        public EtlInventoryModel SelectByInventoryUid(string client, string processName, int inventoryUid)
        {
            try
            {
                var clientVal = client.Trim().ToLower();
                var processNameVal = processName.Trim().ToLower();

                var inventory = (from etlInventory in _context.EtlInventory
                                 join processes in _context.Processes
                                    on etlInventory.ProcessUid equals processes.ProcessUid
                                 where processes.Client.Trim().ToLower() == clientVal
                                    && processes.ProcessName.Trim().ToLower() == processNameVal
                                    && etlInventory.InventoryUid == inventoryUid
                                 select new EtlInventoryModel
                                 {
                                     _ETL_InventoryUID = etlInventory.EtlInventoryUid,
                                     ProcessUid = etlInventory.ProcessUid,
                                     InventoryUID = etlInventory.InventoryUid,
                                     AssetID = etlInventory.AssetId,
                                     Tag = etlInventory.Tag,
                                     Serial = etlInventory.Serial,
                                     InventoryTypeUID = etlInventory.InventoryTypeUid,
                                     InventoryTypeName = etlInventory.InventoryTypeName,
                                     ItemUID = etlInventory.ItemUid,
                                     ProductName = etlInventory.ProductName,
                                     ProductDescription = etlInventory.ProductDescription,
                                     ProductByNumber = etlInventory.ProductByNumber,
                                     ItemTypeUID = etlInventory.ItemTypeUid,
                                     ProductTypeName = etlInventory.ProductTypeName,
                                     ProductTypeDescription = etlInventory.ProductTypeDescription,
                                     ModelNumber = etlInventory.ModelNumber,
                                     ManufacturerUID = etlInventory.ManufacturerUid,
                                     ManufacturerName = etlInventory.ManufacturerName,
                                     AreaUID = etlInventory.AreaUid,
                                     AreaName = etlInventory.AreaName,
                                     SiteUID = etlInventory.SiteUid,
                                     SiteID = etlInventory.SiteId,
                                     SiteName = etlInventory.SiteName,
                                     EntityUID = etlInventory.EntityUid,
                                     EntityID = etlInventory.EntityId,
                                     EntityName = etlInventory.EntityName,
                                     EntityTypeUID = etlInventory.EntityTypeUid,
                                     EntityTypeName = etlInventory.EntityTypeName,
                                     StatusID = etlInventory.StatusId,
                                     Status = etlInventory.Status,
                                     TechDepartmentUID = etlInventory.TechDepartmentUid,
                                     DepartmentName = etlInventory.DepartmentName,
                                     DepartmentID = etlInventory.DepartmentId,
                                     FundingSourceUID = etlInventory.FundingSourceUid,
                                     FundingSource = etlInventory.FundingSource,
                                     FundingSourceDescription = etlInventory.FundingSourceDescription,
                                     PurchasePrice = etlInventory.PurchasePrice,
                                     PurchaseDate = etlInventory.PurchaseDate,
                                     ExpirationDate = etlInventory.ExpirationDate,
                                     InventoryNotes = etlInventory.InventoryNotes,
                                     ParentInventoryUID = etlInventory.ParentInventoryUid,
                                     ParentTag = etlInventory.ParentTag,
                                     InventorySourceUID = etlInventory.InventorySourceUid,
                                     InventorySourceName = etlInventory.InventorySourceName,
                                     PurchaseUID = etlInventory.PurchaseUid,
                                     OrderNumber = etlInventory.OrderNumber,
                                     PurchaseItemDetailUID = etlInventory.PurchaseItemDetailUid,
                                     LineNumber = etlInventory.LineNumber,
                                     AccountCode = etlInventory.AccountCode,
                                     VendorUID = etlInventory.VendorUid,
                                     VendorName = etlInventory.VendorName,
                                     VendorAccountNumber = etlInventory.VendorAccountNumber,
                                     PurchaseItemShipmentUID = etlInventory.PurchaseItemShipmentUid,
                                     InvoiceNumber = etlInventory.InvoiceNumber,
                                     InvoiceDate = etlInventory.InvoiceDate,
                                     InventoryExt1UID = etlInventory.InventoryExt1Uid,
                                     InventoryMeta1UID = etlInventory.InventoryMeta1Uid,
                                     CustomField1Label = etlInventory.CustomField1Label,
                                     CustomField1Value = etlInventory.CustomField1Value,
                                     InventoryExt2UID = etlInventory.InventoryExt2Uid,
                                     InventoryMeta2UID = etlInventory.InventoryMeta2Uid,
                                     CustomField2Label = etlInventory.CustomField2Label,
                                     CustomField2Value = etlInventory.CustomField2Value,
                                     InventoryExt3UID = etlInventory.InventoryExt3Uid,
                                     InventoryMeta3UID = etlInventory.InventoryMeta3Uid,
                                     CustomField3Label = etlInventory.CustomField3Label,
                                     CustomField3Value = etlInventory.CustomField3Value,
                                     InventoryExt4UID = etlInventory.InventoryExt4Uid,
                                     InventoryMeta4UID = etlInventory.InventoryMeta4Uid,
                                     CustomField4Label = etlInventory.CustomField4Label,
                                     CustomField4Value = etlInventory.CustomField4Value
                                 }).FirstOrDefault();
                return inventory;
            }
            catch
            {
                throw;
            }
        }

        public EtlInventoryModel SelectByTag(string client, string processName, string tag)
        {
            try
            {
                var clientVal = client.Trim().ToLower();
                var processNameVal = processName.Trim().ToLower();
                var tagVal = tag.Trim().ToLower();

                var inventory = (from etlInventory in _context.EtlInventory
                                 join processes in _context.Processes
                                    on etlInventory.ProcessUid equals processes.ProcessUid
                                 where processes.Client.Trim().ToLower() == clientVal
                                    && processes.ProcessName.Trim().ToLower() == processNameVal
                                    && etlInventory.Tag.Trim().ToLower() == tagVal
                                 select new EtlInventoryModel
                                 {
                                     _ETL_InventoryUID = etlInventory.EtlInventoryUid,
                                     ProcessUid = etlInventory.ProcessUid,
                                     InventoryUID = etlInventory.InventoryUid,
                                     AssetID = etlInventory.AssetId,
                                     Tag = etlInventory.Tag,
                                     Serial = etlInventory.Serial,
                                     InventoryTypeUID = etlInventory.InventoryTypeUid,
                                     InventoryTypeName = etlInventory.InventoryTypeName,
                                     ItemUID = etlInventory.ItemUid,
                                     ProductName = etlInventory.ProductName,
                                     ProductDescription = etlInventory.ProductDescription,
                                     ProductByNumber = etlInventory.ProductByNumber,
                                     ItemTypeUID = etlInventory.ItemTypeUid,
                                     ProductTypeName = etlInventory.ProductTypeName,
                                     ProductTypeDescription = etlInventory.ProductTypeDescription,
                                     ModelNumber = etlInventory.ModelNumber,
                                     ManufacturerUID = etlInventory.ManufacturerUid,
                                     ManufacturerName = etlInventory.ManufacturerName,
                                     AreaUID = etlInventory.AreaUid,
                                     AreaName = etlInventory.AreaName,
                                     SiteUID = etlInventory.SiteUid,
                                     SiteID = etlInventory.SiteId,
                                     SiteName = etlInventory.SiteName,
                                     EntityUID = etlInventory.EntityUid,
                                     EntityID = etlInventory.EntityId,
                                     EntityName = etlInventory.EntityName,
                                     EntityTypeUID = etlInventory.EntityTypeUid,
                                     EntityTypeName = etlInventory.EntityTypeName,
                                     StatusID = etlInventory.StatusId,
                                     Status = etlInventory.Status,
                                     TechDepartmentUID = etlInventory.TechDepartmentUid,
                                     DepartmentName = etlInventory.DepartmentName,
                                     DepartmentID = etlInventory.DepartmentId,
                                     FundingSourceUID = etlInventory.FundingSourceUid,
                                     FundingSource = etlInventory.FundingSource,
                                     FundingSourceDescription = etlInventory.FundingSourceDescription,
                                     PurchasePrice = etlInventory.PurchasePrice,
                                     PurchaseDate = etlInventory.PurchaseDate,
                                     ExpirationDate = etlInventory.ExpirationDate,
                                     InventoryNotes = etlInventory.InventoryNotes,
                                     ParentInventoryUID = etlInventory.ParentInventoryUid,
                                     ParentTag = etlInventory.ParentTag,
                                     InventorySourceUID = etlInventory.InventorySourceUid,
                                     InventorySourceName = etlInventory.InventorySourceName,
                                     PurchaseUID = etlInventory.PurchaseUid,
                                     OrderNumber = etlInventory.OrderNumber,
                                     PurchaseItemDetailUID = etlInventory.PurchaseItemDetailUid,
                                     LineNumber = etlInventory.LineNumber,
                                     AccountCode = etlInventory.AccountCode,
                                     VendorUID = etlInventory.VendorUid,
                                     VendorName = etlInventory.VendorName,
                                     VendorAccountNumber = etlInventory.VendorAccountNumber,
                                     PurchaseItemShipmentUID = etlInventory.PurchaseItemShipmentUid,
                                     InvoiceNumber = etlInventory.InvoiceNumber,
                                     InvoiceDate = etlInventory.InvoiceDate,
                                     InventoryExt1UID = etlInventory.InventoryExt1Uid,
                                     InventoryMeta1UID = etlInventory.InventoryMeta1Uid,
                                     CustomField1Label = etlInventory.CustomField1Label,
                                     CustomField1Value = etlInventory.CustomField1Value,
                                     InventoryExt2UID = etlInventory.InventoryExt2Uid,
                                     InventoryMeta2UID = etlInventory.InventoryMeta2Uid,
                                     CustomField2Label = etlInventory.CustomField2Label,
                                     CustomField2Value = etlInventory.CustomField2Value,
                                     InventoryExt3UID = etlInventory.InventoryExt3Uid,
                                     InventoryMeta3UID = etlInventory.InventoryMeta3Uid,
                                     CustomField3Label = etlInventory.CustomField3Label,
                                     CustomField3Value = etlInventory.CustomField3Value,
                                     InventoryExt4UID = etlInventory.InventoryExt4Uid,
                                     InventoryMeta4UID = etlInventory.InventoryMeta4Uid,
                                     CustomField4Label = etlInventory.CustomField4Label,
                                     CustomField4Value = etlInventory.CustomField4Value
                                 }).FirstOrDefault();
                return inventory;
            }
            catch
            {
                throw;
            }
        }

        public List<EtlInventoryModel> Select(int processUid, int offset, int limit)
        {
            try
            {
                if (offset < 0)
                {
                    offset = 0;
                }
                if (limit <= 0)
                {
                    limit = 500;
                }

                var inventoryList = (from etlInventory in _context.EtlInventory
                                     where etlInventory.ProcessUid == processUid
                                     select new EtlInventoryModel
                                     {
                                         _ETL_InventoryUID = etlInventory.EtlInventoryUid,
                                         ProcessUid = etlInventory.ProcessUid,
                                         InventoryUID = etlInventory.InventoryUid,
                                         AssetID = etlInventory.AssetId,
                                         Tag = etlInventory.Tag,
                                         Serial = etlInventory.Serial,
                                         InventoryTypeUID = etlInventory.InventoryTypeUid,
                                         InventoryTypeName = etlInventory.InventoryTypeName,
                                         ItemUID = etlInventory.ItemUid,
                                         ProductName = etlInventory.ProductName,
                                         ProductDescription = etlInventory.ProductDescription,
                                         ProductByNumber = etlInventory.ProductByNumber,
                                         ItemTypeUID = etlInventory.ItemTypeUid,
                                         ProductTypeName = etlInventory.ProductTypeName,
                                         ProductTypeDescription = etlInventory.ProductTypeDescription,
                                         ModelNumber = etlInventory.ModelNumber,
                                         ManufacturerUID = etlInventory.ManufacturerUid,
                                         ManufacturerName = etlInventory.ManufacturerName,
                                         AreaUID = etlInventory.AreaUid,
                                         AreaName = etlInventory.AreaName,
                                         SiteUID = etlInventory.SiteUid,
                                         SiteID = etlInventory.SiteId,
                                         SiteName = etlInventory.SiteName,
                                         EntityUID = etlInventory.EntityUid,
                                         EntityID = etlInventory.EntityId,
                                         EntityName = etlInventory.EntityName,
                                         EntityTypeUID = etlInventory.EntityTypeUid,
                                         EntityTypeName = etlInventory.EntityTypeName,
                                         StatusID = etlInventory.StatusId,
                                         Status = etlInventory.Status,
                                         TechDepartmentUID = etlInventory.TechDepartmentUid,
                                         DepartmentName = etlInventory.DepartmentName,
                                         DepartmentID = etlInventory.DepartmentId,
                                         FundingSourceUID = etlInventory.FundingSourceUid,
                                         FundingSource = etlInventory.FundingSource,
                                         FundingSourceDescription = etlInventory.FundingSourceDescription,
                                         PurchasePrice = etlInventory.PurchasePrice,
                                         PurchaseDate = etlInventory.PurchaseDate,
                                         ExpirationDate = etlInventory.ExpirationDate,
                                         InventoryNotes = etlInventory.InventoryNotes,
                                         ParentInventoryUID = etlInventory.ParentInventoryUid,
                                         ParentTag = etlInventory.ParentTag,
                                         InventorySourceUID = etlInventory.InventorySourceUid,
                                         InventorySourceName = etlInventory.InventorySourceName,
                                         PurchaseUID = etlInventory.PurchaseUid,
                                         OrderNumber = etlInventory.OrderNumber,
                                         PurchaseItemDetailUID = etlInventory.PurchaseItemDetailUid,
                                         LineNumber = etlInventory.LineNumber,
                                         AccountCode = etlInventory.AccountCode,
                                         VendorUID = etlInventory.VendorUid,
                                         VendorName = etlInventory.VendorName,
                                         VendorAccountNumber = etlInventory.VendorAccountNumber,
                                         PurchaseItemShipmentUID = etlInventory.PurchaseItemShipmentUid,
                                         InvoiceNumber = etlInventory.InvoiceNumber,
                                         InvoiceDate = etlInventory.InvoiceDate,
                                         InventoryExt1UID = etlInventory.InventoryExt1Uid,
                                         InventoryMeta1UID = etlInventory.InventoryMeta1Uid,
                                         CustomField1Label = etlInventory.CustomField1Label,
                                         CustomField1Value = etlInventory.CustomField1Value,
                                         InventoryExt2UID = etlInventory.InventoryExt2Uid,
                                         InventoryMeta2UID = etlInventory.InventoryMeta2Uid,
                                         CustomField2Label = etlInventory.CustomField2Label,
                                         CustomField2Value = etlInventory.CustomField2Value,
                                         InventoryExt3UID = etlInventory.InventoryExt3Uid,
                                         InventoryMeta3UID = etlInventory.InventoryMeta3Uid,
                                         CustomField3Label = etlInventory.CustomField3Label,
                                         CustomField3Value = etlInventory.CustomField3Value,
                                         InventoryExt4UID = etlInventory.InventoryExt4Uid,
                                         InventoryMeta4UID = etlInventory.InventoryMeta4Uid,
                                         CustomField4Label = etlInventory.CustomField4Label,
                                         CustomField4Value = etlInventory.CustomField4Value
                                     });

                return inventoryList.Skip(offset).Take(limit).ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<EtlInventoryModel> Select(string client, string processName, int offset, int limit)
        {
            try
            {
                var clientVal = client.Trim().ToLower();
                var processNameVal = processName.Trim().ToLower();
                if (offset < 0)
                {
                    offset = 0;
                }
                if (limit <= 0)
                {
                    limit = 500;
                }

                var inventoryList = (from etlInventory in _context.EtlInventory
                                     join processes in _context.Processes
                                    on etlInventory.ProcessUid equals processes.ProcessUid
                                     where processes.Client.Trim().ToLower() == clientVal
                                        && processes.ProcessName.Trim().ToLower() == processNameVal
                                     select new EtlInventoryModel
                                     {
                                         _ETL_InventoryUID = etlInventory.EtlInventoryUid,
                                         ProcessUid = etlInventory.ProcessUid,
                                         InventoryUID = etlInventory.InventoryUid,
                                         AssetID = etlInventory.AssetId,
                                         Tag = etlInventory.Tag,
                                         Serial = etlInventory.Serial,
                                         InventoryTypeUID = etlInventory.InventoryTypeUid,
                                         InventoryTypeName = etlInventory.InventoryTypeName,
                                         ItemUID = etlInventory.ItemUid,
                                         ProductName = etlInventory.ProductName,
                                         ProductDescription = etlInventory.ProductDescription,
                                         ProductByNumber = etlInventory.ProductByNumber,
                                         ItemTypeUID = etlInventory.ItemTypeUid,
                                         ProductTypeName = etlInventory.ProductTypeName,
                                         ProductTypeDescription = etlInventory.ProductTypeDescription,
                                         ModelNumber = etlInventory.ModelNumber,
                                         ManufacturerUID = etlInventory.ManufacturerUid,
                                         ManufacturerName = etlInventory.ManufacturerName,
                                         AreaUID = etlInventory.AreaUid,
                                         AreaName = etlInventory.AreaName,
                                         SiteUID = etlInventory.SiteUid,
                                         SiteID = etlInventory.SiteId,
                                         SiteName = etlInventory.SiteName,
                                         EntityUID = etlInventory.EntityUid,
                                         EntityID = etlInventory.EntityId,
                                         EntityName = etlInventory.EntityName,
                                         EntityTypeUID = etlInventory.EntityTypeUid,
                                         EntityTypeName = etlInventory.EntityTypeName,
                                         StatusID = etlInventory.StatusId,
                                         Status = etlInventory.Status,
                                         TechDepartmentUID = etlInventory.TechDepartmentUid,
                                         DepartmentName = etlInventory.DepartmentName,
                                         DepartmentID = etlInventory.DepartmentId,
                                         FundingSourceUID = etlInventory.FundingSourceUid,
                                         FundingSource = etlInventory.FundingSource,
                                         FundingSourceDescription = etlInventory.FundingSourceDescription,
                                         PurchasePrice = etlInventory.PurchasePrice,
                                         PurchaseDate = etlInventory.PurchaseDate,
                                         ExpirationDate = etlInventory.ExpirationDate,
                                         InventoryNotes = etlInventory.InventoryNotes,
                                         ParentInventoryUID = etlInventory.ParentInventoryUid,
                                         ParentTag = etlInventory.ParentTag,
                                         InventorySourceUID = etlInventory.InventorySourceUid,
                                         InventorySourceName = etlInventory.InventorySourceName,
                                         PurchaseUID = etlInventory.PurchaseUid,
                                         OrderNumber = etlInventory.OrderNumber,
                                         PurchaseItemDetailUID = etlInventory.PurchaseItemDetailUid,
                                         LineNumber = etlInventory.LineNumber,
                                         AccountCode = etlInventory.AccountCode,
                                         VendorUID = etlInventory.VendorUid,
                                         VendorName = etlInventory.VendorName,
                                         VendorAccountNumber = etlInventory.VendorAccountNumber,
                                         PurchaseItemShipmentUID = etlInventory.PurchaseItemShipmentUid,
                                         InvoiceNumber = etlInventory.InvoiceNumber,
                                         InvoiceDate = etlInventory.InvoiceDate,
                                         InventoryExt1UID = etlInventory.InventoryExt1Uid,
                                         InventoryMeta1UID = etlInventory.InventoryMeta1Uid,
                                         CustomField1Label = etlInventory.CustomField1Label,
                                         CustomField1Value = etlInventory.CustomField1Value,
                                         InventoryExt2UID = etlInventory.InventoryExt2Uid,
                                         InventoryMeta2UID = etlInventory.InventoryMeta2Uid,
                                         CustomField2Label = etlInventory.CustomField2Label,
                                         CustomField2Value = etlInventory.CustomField2Value,
                                         InventoryExt3UID = etlInventory.InventoryExt3Uid,
                                         InventoryMeta3UID = etlInventory.InventoryMeta3Uid,
                                         CustomField3Label = etlInventory.CustomField3Label,
                                         CustomField3Value = etlInventory.CustomField3Value,
                                         InventoryExt4UID = etlInventory.InventoryExt4Uid,
                                         InventoryMeta4UID = etlInventory.InventoryMeta4Uid,
                                         CustomField4Label = etlInventory.CustomField4Label,
                                         CustomField4Value = etlInventory.CustomField4Value
                                     });

                return inventoryList.Skip(offset).Take(limit).ToList();
            }
            catch
            {
                throw;
            }
        }

        public int GetTotal(int processUid)
        {
            try
            {
                return (from etlInventory in _context.EtlInventory
                        where etlInventory.ProcessUid == processUid
                        select 1).Count();
            }
            catch
            {
                throw;
            }
        }

        public int GetTotal(string client, string processName)
        {
            try
            {
                var clientVal = client.Trim().ToLower();
                var processNameVal = processName.Trim().ToLower();

                return (from etlInventory in _context.EtlInventory
                        join processes in _context.Processes
                       on etlInventory.ProcessUid equals processes.ProcessUid
                        where processes.Client.Trim().ToLower() == clientVal
                           && processes.ProcessName.Trim().ToLower() == processNameVal
                        select 1).Count();
            }
            catch
            {
                throw;
            }
        }

        #endregion Select Methods

        #region Insert Methods

        public int Insert(EtlInventoryModel etlInventoryModel)
        {
            throw new NotImplementedException();
            try
            {
            }
            catch
            {
                throw;
            }
        }

        public bool InsertRange(List<EtlInventoryModel> etlInventoryModel)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch
            {
                throw;
            }
        }

        #endregion Insert Methods

        #region Update Methods

        public bool Update(EtlInventoryModel etlInventoryModel)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch
            {
                throw;
            }
        }

        #endregion Update Methods

        #region Delete Methods

        public bool Delete(int processUid)
        {
            throw new NotImplementedException();
            try
            {

            }
            catch
            {
                throw;
            }
        }

        public bool Delete(string client, string processName)
        {
            throw new NotImplementedException();
            try
            {
                var clientVal = client.Trim().ToLower();
                var processNameVal = processName.Trim().ToLower();

            }
            catch
            {
                throw;
            }
        }

        #endregion Delete Methods


    }
}
