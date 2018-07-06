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
            throw new NotImplementedException();
            try
            {
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
            throw new NotImplementedException();
            try
            {
                var clientVal = client.Trim().ToLower();
                var processNameVal = processName.Trim().ToLower();
                var assetIdVal = assetId.Trim().ToLower();
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

        public int Insert(EtlInventoryModel etlInventoryData)
        {
            try
            {
                var etlInventoryToInsert = new EtlInventory
                {
                    EtlInventoryUid = 0,
                    ProcessUid = etlInventoryData.ProcessUid,
                    InventoryUid = etlInventoryData.InventoryUID,
                    AssetId = etlInventoryData.AssetID,
                    Tag = etlInventoryData.Tag,
                    Serial = etlInventoryData.Serial,
                    InventoryTypeUid = etlInventoryData.InventoryTypeUID,
                    InventoryTypeName = etlInventoryData.InventoryTypeName,
                    ItemUid = etlInventoryData.ItemUID,
                    ProductName = etlInventoryData.ProductName,
                    ProductDescription = etlInventoryData.ProductDescription,
                    ProductByNumber = etlInventoryData.ProductByNumber,
                    ItemTypeUid = etlInventoryData.ItemTypeUID,
                    ProductTypeName = etlInventoryData.ProductTypeName,
                    ProductTypeDescription = etlInventoryData.ProductTypeDescription,
                    ModelNumber = etlInventoryData.ModelNumber,
                    ManufacturerUid = etlInventoryData.ManufacturerUID,
                    ManufacturerName = etlInventoryData.ManufacturerName,
                    AreaUid = etlInventoryData.AreaUID,
                    AreaName = etlInventoryData.AreaName,
                    SiteUid = etlInventoryData.SiteUID,
                    SiteId = etlInventoryData.SiteID,
                    SiteName = etlInventoryData.SiteName,
                    EntityUid = etlInventoryData.EntityUID,
                    EntityId = etlInventoryData.EntityID,
                    EntityName = etlInventoryData.EntityName,
                    EntityTypeUid = etlInventoryData.EntityTypeUID,
                    EntityTypeName = etlInventoryData.EntityTypeName,
                    StatusId = etlInventoryData.StatusID,
                    Status = etlInventoryData.Status,
                    TechDepartmentUid = etlInventoryData.TechDepartmentUID,
                    DepartmentName = etlInventoryData.DepartmentName,
                    DepartmentId = etlInventoryData.DepartmentID,
                    FundingSourceUid = etlInventoryData.FundingSourceUID,
                    FundingSource = etlInventoryData.FundingSource,
                    FundingSourceDescription = etlInventoryData.FundingSourceDescription,
                    PurchasePrice = etlInventoryData.PurchasePrice,
                    PurchaseDate = etlInventoryData.PurchaseDate,
                    ExpirationDate = etlInventoryData.ExpirationDate,
                    InventoryNotes = etlInventoryData.InventoryNotes,
                    ParentInventoryUid = etlInventoryData.ParentInventoryUID,
                    ParentTag = etlInventoryData.ParentTag,
                    InventorySourceUid = etlInventoryData.InventorySourceUID,
                    InventorySourceName = etlInventoryData.InventorySourceName,
                    PurchaseUid = etlInventoryData.PurchaseUID,
                    OrderNumber = etlInventoryData.OrderNumber,
                    PurchaseItemDetailUid = etlInventoryData.PurchaseItemDetailUID,
                    LineNumber = etlInventoryData.LineNumber,
                    AccountCode = etlInventoryData.AccountCode,
                    VendorUid = etlInventoryData.VendorUID,
                    VendorName = etlInventoryData.VendorName,
                    VendorAccountNumber = etlInventoryData.VendorAccountNumber,
                    PurchaseItemShipmentUid = etlInventoryData.PurchaseItemShipmentUID,
                    InvoiceNumber = etlInventoryData.InvoiceNumber,
                    InvoiceDate = etlInventoryData.InvoiceDate,
                    InventoryExt1Uid = etlInventoryData.InventoryExt1UID,
                    InventoryMeta1Uid = etlInventoryData.InventoryMeta1UID,
                    CustomField1Label = etlInventoryData.CustomField1Label,
                    CustomField1Value = etlInventoryData.CustomField1Value,
                    InventoryExt2Uid = etlInventoryData.InventoryExt2UID,
                    InventoryMeta2Uid = etlInventoryData.InventoryMeta2UID,
                    CustomField2Label = etlInventoryData.CustomField2Label,
                    CustomField2Value = etlInventoryData.CustomField2Value,
                    InventoryExt3Uid = etlInventoryData.InventoryExt3UID,
                    InventoryMeta3Uid = etlInventoryData.InventoryMeta3UID,
                    CustomField3Label = etlInventoryData.CustomField3Label,
                    CustomField3Value = etlInventoryData.CustomField3Value,
                    InventoryExt4Uid = etlInventoryData.InventoryExt4UID,
                    InventoryMeta4Uid = etlInventoryData.InventoryMeta4UID,
                    CustomField4Label = etlInventoryData.CustomField4Label,
                    CustomField4Value = etlInventoryData.CustomField4Value
                };

                _context.EtlInventory.Add(etlInventoryToInsert);
                var result = _context.SaveChanges();
                if (result == 1)
                {
                    return etlInventoryToInsert.EtlInventoryUid;
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool InsertRange(List<EtlInventoryModel> etlInventoryData)
        {
            try
            {
                var dataToInsert = (from data in etlInventoryData
                                    select new EtlInventory
                                    {
                                        EtlInventoryUid = 0,
                                        ProcessUid = data.ProcessUid,
                                        InventoryUid = data.InventoryUID,
                                        AssetId = data.AssetID,
                                        Tag = data.Tag,
                                        Serial = data.Serial,
                                        InventoryTypeUid = data.InventoryTypeUID,
                                        InventoryTypeName = data.InventoryTypeName,
                                        ItemUid = data.ItemUID,
                                        ProductName = data.ProductName,
                                        ProductDescription = data.ProductDescription,
                                        ProductByNumber = data.ProductByNumber,
                                        ItemTypeUid = data.ItemTypeUID,
                                        ProductTypeName = data.ProductTypeName,
                                        ProductTypeDescription = data.ProductTypeDescription,
                                        ModelNumber = data.ModelNumber,
                                        ManufacturerUid = data.ManufacturerUID,
                                        ManufacturerName = data.ManufacturerName,
                                        AreaUid = data.AreaUID,
                                        AreaName = data.AreaName,
                                        SiteUid = data.SiteUID,
                                        SiteId = data.SiteID,
                                        SiteName = data.SiteName,
                                        EntityUid = data.EntityUID,
                                        EntityId = data.EntityID,
                                        EntityName = data.EntityName,
                                        EntityTypeUid = data.EntityTypeUID,
                                        EntityTypeName = data.EntityTypeName,
                                        StatusId = data.StatusID,
                                        Status = data.Status,
                                        TechDepartmentUid = data.TechDepartmentUID,
                                        DepartmentName = data.DepartmentName,
                                        DepartmentId = data.DepartmentID,
                                        FundingSourceUid = data.FundingSourceUID,
                                        FundingSource = data.FundingSource,
                                        FundingSourceDescription = data.FundingSourceDescription,
                                        PurchasePrice = data.PurchasePrice,
                                        PurchaseDate = data.PurchaseDate,
                                        ExpirationDate = data.ExpirationDate,
                                        InventoryNotes = data.InventoryNotes,
                                        ParentInventoryUid = data.ParentInventoryUID,
                                        ParentTag = data.ParentTag,
                                        InventorySourceUid = data.InventorySourceUID,
                                        InventorySourceName = data.InventorySourceName,
                                        PurchaseUid = data.PurchaseUID,
                                        OrderNumber = data.OrderNumber,
                                        PurchaseItemDetailUid = data.PurchaseItemDetailUID,
                                        LineNumber = data.LineNumber,
                                        AccountCode = data.AccountCode,
                                        VendorUid = data.VendorUID,
                                        VendorName = data.VendorName,
                                        VendorAccountNumber = data.VendorAccountNumber,
                                        PurchaseItemShipmentUid = data.PurchaseItemShipmentUID,
                                        InvoiceNumber = data.InvoiceNumber,
                                        InvoiceDate = data.InvoiceDate,
                                        InventoryExt1Uid = data.InventoryExt1UID,
                                        InventoryMeta1Uid = data.InventoryMeta1UID,
                                        CustomField1Label = data.CustomField1Label,
                                        CustomField1Value = data.CustomField1Value,
                                        InventoryExt2Uid = data.InventoryExt2UID,
                                        InventoryMeta2Uid = data.InventoryMeta2UID,
                                        CustomField2Label = data.CustomField2Label,
                                        CustomField2Value = data.CustomField2Value,
                                        InventoryExt3Uid = data.InventoryExt3UID,
                                        InventoryMeta3Uid = data.InventoryMeta3UID,
                                        CustomField3Label = data.CustomField3Label,
                                        CustomField3Value = data.CustomField3Value,
                                        InventoryExt4Uid = data.InventoryExt4UID,
                                        InventoryMeta4Uid = data.InventoryMeta4UID,
                                        CustomField4Label = data.CustomField4Label,
                                        CustomField4Value = data.CustomField4Value
                                    });

                _context.EtlInventory.AddRange(dataToInsert);
                var result = _context.SaveChanges();
                return (result > 0);

            }
            catch
            {
                throw;
            }
        }

        #endregion Insert Methods

        #region Update Methods

        public bool Update(EtlInventoryModel data)
        {
            try
            {
                var dataToUpdate = (from etlInventory in _context.EtlInventory
                                    where etlInventory.EtlInventoryUid == data._ETL_InventoryUID
                                    select etlInventory).FirstOrDefault();

                //dataToUpdate. = etlInventoryData.;

                dataToUpdate.ProcessUid = data.ProcessUid;
                dataToUpdate.InventoryUid = data.InventoryUID;
                dataToUpdate.AssetId = data.AssetID;
                dataToUpdate.Tag = data.Tag;
                dataToUpdate.Serial = data.Serial;
                dataToUpdate.InventoryTypeUid = data.InventoryTypeUID;
                dataToUpdate.InventoryTypeName = data.InventoryTypeName;
                dataToUpdate.ItemUid = data.ItemUID;
                dataToUpdate.ProductName = data.ProductName;
                dataToUpdate.ProductDescription = data.ProductDescription;
                dataToUpdate.ProductByNumber = data.ProductByNumber;
                dataToUpdate.ItemTypeUid = data.ItemTypeUID;
                dataToUpdate.ProductTypeName = data.ProductTypeName;
                dataToUpdate.ProductTypeDescription = data.ProductTypeDescription;
                dataToUpdate.ModelNumber = data.ModelNumber;
                dataToUpdate.ManufacturerUid = data.ManufacturerUID;
                dataToUpdate.ManufacturerName = data.ManufacturerName;
                dataToUpdate.AreaUid = data.AreaUID;
                dataToUpdate.AreaName = data.AreaName;
                dataToUpdate.SiteUid = data.SiteUID;
                dataToUpdate.SiteId = data.SiteID;
                dataToUpdate.SiteName = data.SiteName;
                dataToUpdate.EntityUid = data.EntityUID;
                dataToUpdate.EntityId = data.EntityID;
                dataToUpdate.EntityName = data.EntityName;
                dataToUpdate.EntityTypeUid = data.EntityTypeUID;
                dataToUpdate.EntityTypeName = data.EntityTypeName;
                dataToUpdate.StatusId = data.StatusID;
                dataToUpdate.Status = data.Status;
                dataToUpdate.TechDepartmentUid = data.TechDepartmentUID;
                dataToUpdate.DepartmentName = data.DepartmentName;
                dataToUpdate.DepartmentId = data.DepartmentID;
                dataToUpdate.FundingSourceUid = data.FundingSourceUID;
                dataToUpdate.FundingSource = data.FundingSource;
                dataToUpdate.FundingSourceDescription = data.FundingSourceDescription;
                dataToUpdate.PurchasePrice = data.PurchasePrice;
                dataToUpdate.PurchaseDate = data.PurchaseDate;
                dataToUpdate.ExpirationDate = data.ExpirationDate;
                dataToUpdate.InventoryNotes = data.InventoryNotes;
                dataToUpdate.ParentInventoryUid = data.ParentInventoryUID;
                dataToUpdate.ParentTag = data.ParentTag;
                dataToUpdate.InventorySourceUid = data.InventorySourceUID;
                dataToUpdate.InventorySourceName = data.InventorySourceName;
                dataToUpdate.PurchaseUid = data.PurchaseUID;
                dataToUpdate.OrderNumber = data.OrderNumber;
                dataToUpdate.PurchaseItemDetailUid = data.PurchaseItemDetailUID;
                dataToUpdate.LineNumber = data.LineNumber;
                dataToUpdate.AccountCode = data.AccountCode;
                dataToUpdate.VendorUid = data.VendorUID;
                dataToUpdate.VendorName = data.VendorName;
                dataToUpdate.VendorAccountNumber = data.VendorAccountNumber;
                dataToUpdate.PurchaseItemShipmentUid = data.PurchaseItemShipmentUID;
                dataToUpdate.InvoiceNumber = data.InvoiceNumber;
                dataToUpdate.InvoiceDate = data.InvoiceDate;
                dataToUpdate.InventoryExt1Uid = data.InventoryExt1UID;
                dataToUpdate.InventoryMeta1Uid = data.InventoryMeta1UID;
                dataToUpdate.CustomField1Label = data.CustomField1Label;
                dataToUpdate.CustomField1Value = data.CustomField1Value;
                dataToUpdate.InventoryExt2Uid = data.InventoryExt2UID;
                dataToUpdate.InventoryMeta2Uid = data.InventoryMeta2UID;
                dataToUpdate.CustomField2Label = data.CustomField2Label;
                dataToUpdate.CustomField2Value = data.CustomField2Value;
                dataToUpdate.InventoryExt3Uid = data.InventoryExt3UID;
                dataToUpdate.InventoryMeta3Uid = data.InventoryMeta3UID;
                dataToUpdate.CustomField3Label = data.CustomField3Label;
                dataToUpdate.CustomField3Value = data.CustomField3Value;
                dataToUpdate.InventoryExt4Uid = data.InventoryExt4UID;
                dataToUpdate.InventoryMeta4Uid = data.InventoryMeta4UID;
                dataToUpdate.CustomField4Label = data.CustomField4Label;
                dataToUpdate.CustomField4Value = data.CustomField4Value;

                _context.EtlInventory.Update(dataToUpdate);
                var result = _context.SaveChanges();
                return result == 1;
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
            try
            {
                var dataToDelete = (from etlinventory in _context.EtlInventory
                                    where etlinventory.ProcessUid == processUid
                                    select etlinventory);

                _context.EtlInventory.RemoveRange(dataToDelete);
                var result = _context.SaveChanges();

                return result > 1;
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(string client, string processName)
        {
            try
            {
                var clientVal = client.Trim().ToLower();
                var processNameVal = processName.Trim().ToLower();

                var dataToDelete = (from etlinventory in _context.EtlInventory
                                    join processes in _context.Processes
                                        on etlinventory.ProcessUid equals processes.ProcessUid
                                    where processes.Client.Trim().ToLower() == clientVal
                                       && processes.ProcessName.Trim().ToLower() == processNameVal
                                    select etlinventory);

                _context.EtlInventory.RemoveRange(dataToDelete);
                var result = _context.SaveChanges();

                return result > 1;
            }
            catch
            {
                throw;
            }
        }

        #endregion Delete Methods


    }
}
