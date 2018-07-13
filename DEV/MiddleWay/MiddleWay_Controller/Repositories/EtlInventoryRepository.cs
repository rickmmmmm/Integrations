using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
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
                var inventory = (from etlInventory in _context.EtlInventory
                                 where etlInventory.EtlInventoryUid == etlInventoryUid
                                 select new EtlInventoryModel
                                 {
                                     _ETL_InventoryUid = etlInventory.EtlInventoryUid,
                                     RowId = etlInventory.RowId,
                                     ProcessUid = etlInventory.ProcessUid,
                                     InventoryUid = etlInventory.InventoryUid,
                                     AssetId = etlInventory.AssetId,
                                     Tag = etlInventory.Tag,
                                     Serial = etlInventory.Serial,
                                     InventoryTypeUid = etlInventory.InventoryTypeUid,
                                     InventoryTypeName = etlInventory.InventoryTypeName,
                                     ItemUid = etlInventory.ItemUid,
                                     ProductName = etlInventory.ProductName,
                                     ProductDescription = etlInventory.ProductDescription,
                                     ProductByNumber = etlInventory.ProductByNumber,
                                     ItemTypeUid = etlInventory.ItemTypeUid,
                                     ProductTypeName = etlInventory.ProductTypeName,
                                     ProductTypeDescription = etlInventory.ProductTypeDescription,
                                     ModelNumber = etlInventory.ModelNumber,
                                     ManufacturerUid = etlInventory.ManufacturerUid,
                                     ManufacturerName = etlInventory.ManufacturerName,
                                     AreaUid = etlInventory.AreaUid,
                                     AreaName = etlInventory.AreaName,
                                     SiteUid = etlInventory.SiteUid,
                                     SiteId = etlInventory.SiteId,
                                     SiteName = etlInventory.SiteName,
                                     EntityUid = etlInventory.EntityUid,
                                     EntityId = etlInventory.EntityId,
                                     EntityName = etlInventory.EntityName,
                                     EntityTypeUid = etlInventory.EntityTypeUid,
                                     EntityTypeName = etlInventory.EntityTypeName,
                                     StatusId = etlInventory.StatusId,
                                     Status = etlInventory.Status,
                                     TechDepartmentUid = etlInventory.TechDepartmentUid,
                                     DepartmentName = etlInventory.DepartmentName,
                                     DepartmentId = etlInventory.DepartmentId,
                                     FundingSourceUid = etlInventory.FundingSourceUid,
                                     FundingSource = etlInventory.FundingSource,
                                     FundingSourceDescription = etlInventory.FundingSourceDescription,
                                     PurchasePrice = etlInventory.PurchasePrice,
                                     PurchaseDate = etlInventory.PurchaseDate,
                                     ExpirationDate = etlInventory.ExpirationDate,
                                     InventoryNotes = etlInventory.InventoryNotes,
                                     ParentInventoryUid = etlInventory.ParentInventoryUid,
                                     ParentTag = etlInventory.ParentTag,
                                     InventorySourceUid = etlInventory.InventorySourceUid,
                                     InventorySourceName = etlInventory.InventorySourceName,
                                     PurchaseUid = etlInventory.PurchaseUid,
                                     OrderNumber = etlInventory.OrderNumber,
                                     PurchaseItemDetailUid = etlInventory.PurchaseItemDetailUid,
                                     LineNumber = etlInventory.LineNumber,
                                     AccountCode = etlInventory.AccountCode,
                                     VendorUid = etlInventory.VendorUid,
                                     VendorName = etlInventory.VendorName,
                                     VendorAccountNumber = etlInventory.VendorAccountNumber,
                                     PurchaseItemShipmentUid = etlInventory.PurchaseItemShipmentUid,
                                     InvoiceNumber = etlInventory.InvoiceNumber,
                                     InvoiceDate = etlInventory.InvoiceDate,
                                     InventoryExt1Uid = etlInventory.InventoryExt1Uid,
                                     InventoryMeta1Uid = etlInventory.InventoryMeta1Uid,
                                     CustomField1Label = etlInventory.CustomField1Label,
                                     CustomField1Value = etlInventory.CustomField1Value,
                                     InventoryExt2Uid = etlInventory.InventoryExt2Uid,
                                     InventoryMeta2Uid = etlInventory.InventoryMeta2Uid,
                                     CustomField2Label = etlInventory.CustomField2Label,
                                     CustomField2Value = etlInventory.CustomField2Value,
                                     InventoryExt3Uid = etlInventory.InventoryExt3Uid,
                                     InventoryMeta3Uid = etlInventory.InventoryMeta3Uid,
                                     CustomField3Label = etlInventory.CustomField3Label,
                                     CustomField3Value = etlInventory.CustomField3Value,
                                     InventoryExt4Uid = etlInventory.InventoryExt4Uid,
                                     InventoryMeta4Uid = etlInventory.InventoryMeta4Uid,
                                     CustomField4Label = etlInventory.CustomField4Label,
                                     CustomField4Value = etlInventory.CustomField4Value,
                                     Rejected = etlInventory.Rejected,
                                     RejectedNotes = etlInventory.RejectedNotes
                                 }).FirstOrDefault();

                return inventory;
            }
            catch
            {
                throw;
            }
        }

        public EtlInventoryModel SelectByAssetId(string client, string processName, string assetId)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var assetIdVal = (assetId ?? string.Empty).Trim().ToLower();

                var inventory = (from etlInventory in _context.EtlInventory
                                 join processes in _context.Processes
                                    on etlInventory.ProcessUid equals processes.ProcessUid
                                 where processes.Client.Trim().ToLower() == clientVal
                                    && processes.ProcessName.Trim().ToLower() == processNameVal
                                    && etlInventory.AssetId.Trim().ToLower() == assetIdVal
                                 select new EtlInventoryModel
                                 {
                                     _ETL_InventoryUid = etlInventory.EtlInventoryUid,
                                     ProcessUid = etlInventory.ProcessUid,
                                     RowId = etlInventory.RowId,
                                     InventoryUid = etlInventory.InventoryUid,
                                     AssetId = etlInventory.AssetId,
                                     Tag = etlInventory.Tag,
                                     Serial = etlInventory.Serial,
                                     InventoryTypeUid = etlInventory.InventoryTypeUid,
                                     InventoryTypeName = etlInventory.InventoryTypeName,
                                     ItemUid = etlInventory.ItemUid,
                                     ProductName = etlInventory.ProductName,
                                     ProductDescription = etlInventory.ProductDescription,
                                     ProductByNumber = etlInventory.ProductByNumber,
                                     ItemTypeUid = etlInventory.ItemTypeUid,
                                     ProductTypeName = etlInventory.ProductTypeName,
                                     ProductTypeDescription = etlInventory.ProductTypeDescription,
                                     ModelNumber = etlInventory.ModelNumber,
                                     ManufacturerUid = etlInventory.ManufacturerUid,
                                     ManufacturerName = etlInventory.ManufacturerName,
                                     AreaUid = etlInventory.AreaUid,
                                     AreaName = etlInventory.AreaName,
                                     SiteUid = etlInventory.SiteUid,
                                     SiteId = etlInventory.SiteId,
                                     SiteName = etlInventory.SiteName,
                                     EntityUid = etlInventory.EntityUid,
                                     EntityId = etlInventory.EntityId,
                                     EntityName = etlInventory.EntityName,
                                     EntityTypeUid = etlInventory.EntityTypeUid,
                                     EntityTypeName = etlInventory.EntityTypeName,
                                     StatusId = etlInventory.StatusId,
                                     Status = etlInventory.Status,
                                     TechDepartmentUid = etlInventory.TechDepartmentUid,
                                     DepartmentName = etlInventory.DepartmentName,
                                     DepartmentId = etlInventory.DepartmentId,
                                     FundingSourceUid = etlInventory.FundingSourceUid,
                                     FundingSource = etlInventory.FundingSource,
                                     FundingSourceDescription = etlInventory.FundingSourceDescription,
                                     PurchasePrice = etlInventory.PurchasePrice,
                                     PurchaseDate = etlInventory.PurchaseDate,
                                     ExpirationDate = etlInventory.ExpirationDate,
                                     InventoryNotes = etlInventory.InventoryNotes,
                                     ParentInventoryUid = etlInventory.ParentInventoryUid,
                                     ParentTag = etlInventory.ParentTag,
                                     InventorySourceUid = etlInventory.InventorySourceUid,
                                     InventorySourceName = etlInventory.InventorySourceName,
                                     PurchaseUid = etlInventory.PurchaseUid,
                                     OrderNumber = etlInventory.OrderNumber,
                                     PurchaseItemDetailUid = etlInventory.PurchaseItemDetailUid,
                                     LineNumber = etlInventory.LineNumber,
                                     AccountCode = etlInventory.AccountCode,
                                     VendorUid = etlInventory.VendorUid,
                                     VendorName = etlInventory.VendorName,
                                     VendorAccountNumber = etlInventory.VendorAccountNumber,
                                     PurchaseItemShipmentUid = etlInventory.PurchaseItemShipmentUid,
                                     InvoiceNumber = etlInventory.InvoiceNumber,
                                     InvoiceDate = etlInventory.InvoiceDate,
                                     InventoryExt1Uid = etlInventory.InventoryExt1Uid,
                                     InventoryMeta1Uid = etlInventory.InventoryMeta1Uid,
                                     CustomField1Label = etlInventory.CustomField1Label,
                                     CustomField1Value = etlInventory.CustomField1Value,
                                     InventoryExt2Uid = etlInventory.InventoryExt2Uid,
                                     InventoryMeta2Uid = etlInventory.InventoryMeta2Uid,
                                     CustomField2Label = etlInventory.CustomField2Label,
                                     CustomField2Value = etlInventory.CustomField2Value,
                                     InventoryExt3Uid = etlInventory.InventoryExt3Uid,
                                     InventoryMeta3Uid = etlInventory.InventoryMeta3Uid,
                                     CustomField3Label = etlInventory.CustomField3Label,
                                     CustomField3Value = etlInventory.CustomField3Value,
                                     InventoryExt4Uid = etlInventory.InventoryExt4Uid,
                                     InventoryMeta4Uid = etlInventory.InventoryMeta4Uid,
                                     CustomField4Label = etlInventory.CustomField4Label,
                                     CustomField4Value = etlInventory.CustomField4Value,
                                     Rejected = etlInventory.Rejected,
                                     RejectedNotes = etlInventory.RejectedNotes
                                 }).FirstOrDefault();

                return inventory;
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
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();

                var inventory = (from etlInventory in _context.EtlInventory
                                 join processes in _context.Processes
                                    on etlInventory.ProcessUid equals processes.ProcessUid
                                 where processes.Client.Trim().ToLower() == clientVal
                                    && processes.ProcessName.Trim().ToLower() == processNameVal
                                    && etlInventory.InventoryUid == inventoryUid
                                 select new EtlInventoryModel
                                 {
                                     _ETL_InventoryUid = etlInventory.EtlInventoryUid,
                                     ProcessUid = etlInventory.ProcessUid,
                                     RowId = etlInventory.RowId,
                                     InventoryUid = etlInventory.InventoryUid,
                                     AssetId = etlInventory.AssetId,
                                     Tag = etlInventory.Tag,
                                     Serial = etlInventory.Serial,
                                     InventoryTypeUid = etlInventory.InventoryTypeUid,
                                     InventoryTypeName = etlInventory.InventoryTypeName,
                                     ItemUid = etlInventory.ItemUid,
                                     ProductName = etlInventory.ProductName,
                                     ProductDescription = etlInventory.ProductDescription,
                                     ProductByNumber = etlInventory.ProductByNumber,
                                     ItemTypeUid = etlInventory.ItemTypeUid,
                                     ProductTypeName = etlInventory.ProductTypeName,
                                     ProductTypeDescription = etlInventory.ProductTypeDescription,
                                     ModelNumber = etlInventory.ModelNumber,
                                     ManufacturerUid = etlInventory.ManufacturerUid,
                                     ManufacturerName = etlInventory.ManufacturerName,
                                     AreaUid = etlInventory.AreaUid,
                                     AreaName = etlInventory.AreaName,
                                     SiteUid = etlInventory.SiteUid,
                                     SiteId = etlInventory.SiteId,
                                     SiteName = etlInventory.SiteName,
                                     EntityUid = etlInventory.EntityUid,
                                     EntityId = etlInventory.EntityId,
                                     EntityName = etlInventory.EntityName,
                                     EntityTypeUid = etlInventory.EntityTypeUid,
                                     EntityTypeName = etlInventory.EntityTypeName,
                                     StatusId = etlInventory.StatusId,
                                     Status = etlInventory.Status,
                                     TechDepartmentUid = etlInventory.TechDepartmentUid,
                                     DepartmentName = etlInventory.DepartmentName,
                                     DepartmentId = etlInventory.DepartmentId,
                                     FundingSourceUid = etlInventory.FundingSourceUid,
                                     FundingSource = etlInventory.FundingSource,
                                     FundingSourceDescription = etlInventory.FundingSourceDescription,
                                     PurchasePrice = etlInventory.PurchasePrice,
                                     PurchaseDate = etlInventory.PurchaseDate,
                                     ExpirationDate = etlInventory.ExpirationDate,
                                     InventoryNotes = etlInventory.InventoryNotes,
                                     ParentInventoryUid = etlInventory.ParentInventoryUid,
                                     ParentTag = etlInventory.ParentTag,
                                     InventorySourceUid = etlInventory.InventorySourceUid,
                                     InventorySourceName = etlInventory.InventorySourceName,
                                     PurchaseUid = etlInventory.PurchaseUid,
                                     OrderNumber = etlInventory.OrderNumber,
                                     PurchaseItemDetailUid = etlInventory.PurchaseItemDetailUid,
                                     LineNumber = etlInventory.LineNumber,
                                     AccountCode = etlInventory.AccountCode,
                                     VendorUid = etlInventory.VendorUid,
                                     VendorName = etlInventory.VendorName,
                                     VendorAccountNumber = etlInventory.VendorAccountNumber,
                                     PurchaseItemShipmentUid = etlInventory.PurchaseItemShipmentUid,
                                     InvoiceNumber = etlInventory.InvoiceNumber,
                                     InvoiceDate = etlInventory.InvoiceDate,
                                     InventoryExt1Uid = etlInventory.InventoryExt1Uid,
                                     InventoryMeta1Uid = etlInventory.InventoryMeta1Uid,
                                     CustomField1Label = etlInventory.CustomField1Label,
                                     CustomField1Value = etlInventory.CustomField1Value,
                                     InventoryExt2Uid = etlInventory.InventoryExt2Uid,
                                     InventoryMeta2Uid = etlInventory.InventoryMeta2Uid,
                                     CustomField2Label = etlInventory.CustomField2Label,
                                     CustomField2Value = etlInventory.CustomField2Value,
                                     InventoryExt3Uid = etlInventory.InventoryExt3Uid,
                                     InventoryMeta3Uid = etlInventory.InventoryMeta3Uid,
                                     CustomField3Label = etlInventory.CustomField3Label,
                                     CustomField3Value = etlInventory.CustomField3Value,
                                     InventoryExt4Uid = etlInventory.InventoryExt4Uid,
                                     InventoryMeta4Uid = etlInventory.InventoryMeta4Uid,
                                     CustomField4Label = etlInventory.CustomField4Label,
                                     CustomField4Value = etlInventory.CustomField4Value,
                                     Rejected = etlInventory.Rejected,
                                     RejectedNotes = etlInventory.RejectedNotes
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
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var tagVal = (tag ?? string.Empty).Trim().ToLower();

                var inventory = (from etlInventory in _context.EtlInventory
                                 join processes in _context.Processes
                                    on etlInventory.ProcessUid equals processes.ProcessUid
                                 where processes.Client.Trim().ToLower() == clientVal
                                    && processes.ProcessName.Trim().ToLower() == processNameVal
                                    && etlInventory.Tag.Trim().ToLower() == tagVal
                                 select new EtlInventoryModel
                                 {
                                     _ETL_InventoryUid = etlInventory.EtlInventoryUid,
                                     ProcessUid = etlInventory.ProcessUid,
                                     RowId = etlInventory.RowId,
                                     InventoryUid = etlInventory.InventoryUid,
                                     AssetId = etlInventory.AssetId,
                                     Tag = etlInventory.Tag,
                                     Serial = etlInventory.Serial,
                                     InventoryTypeUid = etlInventory.InventoryTypeUid,
                                     InventoryTypeName = etlInventory.InventoryTypeName,
                                     ItemUid = etlInventory.ItemUid,
                                     ProductName = etlInventory.ProductName,
                                     ProductDescription = etlInventory.ProductDescription,
                                     ProductByNumber = etlInventory.ProductByNumber,
                                     ItemTypeUid = etlInventory.ItemTypeUid,
                                     ProductTypeName = etlInventory.ProductTypeName,
                                     ProductTypeDescription = etlInventory.ProductTypeDescription,
                                     ModelNumber = etlInventory.ModelNumber,
                                     ManufacturerUid = etlInventory.ManufacturerUid,
                                     ManufacturerName = etlInventory.ManufacturerName,
                                     AreaUid = etlInventory.AreaUid,
                                     AreaName = etlInventory.AreaName,
                                     SiteUid = etlInventory.SiteUid,
                                     SiteId = etlInventory.SiteId,
                                     SiteName = etlInventory.SiteName,
                                     EntityUid = etlInventory.EntityUid,
                                     EntityId = etlInventory.EntityId,
                                     EntityName = etlInventory.EntityName,
                                     EntityTypeUid = etlInventory.EntityTypeUid,
                                     EntityTypeName = etlInventory.EntityTypeName,
                                     StatusId = etlInventory.StatusId,
                                     Status = etlInventory.Status,
                                     TechDepartmentUid = etlInventory.TechDepartmentUid,
                                     DepartmentName = etlInventory.DepartmentName,
                                     DepartmentId = etlInventory.DepartmentId,
                                     FundingSourceUid = etlInventory.FundingSourceUid,
                                     FundingSource = etlInventory.FundingSource,
                                     FundingSourceDescription = etlInventory.FundingSourceDescription,
                                     PurchasePrice = etlInventory.PurchasePrice,
                                     PurchaseDate = etlInventory.PurchaseDate,
                                     ExpirationDate = etlInventory.ExpirationDate,
                                     InventoryNotes = etlInventory.InventoryNotes,
                                     ParentInventoryUid = etlInventory.ParentInventoryUid,
                                     ParentTag = etlInventory.ParentTag,
                                     InventorySourceUid = etlInventory.InventorySourceUid,
                                     InventorySourceName = etlInventory.InventorySourceName,
                                     PurchaseUid = etlInventory.PurchaseUid,
                                     OrderNumber = etlInventory.OrderNumber,
                                     PurchaseItemDetailUid = etlInventory.PurchaseItemDetailUid,
                                     LineNumber = etlInventory.LineNumber,
                                     AccountCode = etlInventory.AccountCode,
                                     VendorUid = etlInventory.VendorUid,
                                     VendorName = etlInventory.VendorName,
                                     VendorAccountNumber = etlInventory.VendorAccountNumber,
                                     PurchaseItemShipmentUid = etlInventory.PurchaseItemShipmentUid,
                                     InvoiceNumber = etlInventory.InvoiceNumber,
                                     InvoiceDate = etlInventory.InvoiceDate,
                                     InventoryExt1Uid = etlInventory.InventoryExt1Uid,
                                     InventoryMeta1Uid = etlInventory.InventoryMeta1Uid,
                                     CustomField1Label = etlInventory.CustomField1Label,
                                     CustomField1Value = etlInventory.CustomField1Value,
                                     InventoryExt2Uid = etlInventory.InventoryExt2Uid,
                                     InventoryMeta2Uid = etlInventory.InventoryMeta2Uid,
                                     CustomField2Label = etlInventory.CustomField2Label,
                                     CustomField2Value = etlInventory.CustomField2Value,
                                     InventoryExt3Uid = etlInventory.InventoryExt3Uid,
                                     InventoryMeta3Uid = etlInventory.InventoryMeta3Uid,
                                     CustomField3Label = etlInventory.CustomField3Label,
                                     CustomField3Value = etlInventory.CustomField3Value,
                                     InventoryExt4Uid = etlInventory.InventoryExt4Uid,
                                     InventoryMeta4Uid = etlInventory.InventoryMeta4Uid,
                                     CustomField4Label = etlInventory.CustomField4Label,
                                     CustomField4Value = etlInventory.CustomField4Value,
                                     Rejected = etlInventory.Rejected,
                                     RejectedNotes = etlInventory.RejectedNotes
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
                                         _ETL_InventoryUid = etlInventory.EtlInventoryUid,
                                         ProcessUid = etlInventory.ProcessUid,
                                         RowId = etlInventory.RowId,
                                         InventoryUid = etlInventory.InventoryUid,
                                         AssetId = etlInventory.AssetId,
                                         Tag = etlInventory.Tag,
                                         Serial = etlInventory.Serial,
                                         InventoryTypeUid = etlInventory.InventoryTypeUid,
                                         InventoryTypeName = etlInventory.InventoryTypeName,
                                         ItemUid = etlInventory.ItemUid,
                                         ProductName = etlInventory.ProductName,
                                         ProductDescription = etlInventory.ProductDescription,
                                         ProductByNumber = etlInventory.ProductByNumber,
                                         ItemTypeUid = etlInventory.ItemTypeUid,
                                         ProductTypeName = etlInventory.ProductTypeName,
                                         ProductTypeDescription = etlInventory.ProductTypeDescription,
                                         ModelNumber = etlInventory.ModelNumber,
                                         ManufacturerUid = etlInventory.ManufacturerUid,
                                         ManufacturerName = etlInventory.ManufacturerName,
                                         AreaUid = etlInventory.AreaUid,
                                         AreaName = etlInventory.AreaName,
                                         SiteUid = etlInventory.SiteUid,
                                         SiteId = etlInventory.SiteId,
                                         SiteName = etlInventory.SiteName,
                                         EntityUid = etlInventory.EntityUid,
                                         EntityId = etlInventory.EntityId,
                                         EntityName = etlInventory.EntityName,
                                         EntityTypeUid = etlInventory.EntityTypeUid,
                                         EntityTypeName = etlInventory.EntityTypeName,
                                         StatusId = etlInventory.StatusId,
                                         Status = etlInventory.Status,
                                         TechDepartmentUid = etlInventory.TechDepartmentUid,
                                         DepartmentName = etlInventory.DepartmentName,
                                         DepartmentId = etlInventory.DepartmentId,
                                         FundingSourceUid = etlInventory.FundingSourceUid,
                                         FundingSource = etlInventory.FundingSource,
                                         FundingSourceDescription = etlInventory.FundingSourceDescription,
                                         PurchasePrice = etlInventory.PurchasePrice,
                                         PurchaseDate = etlInventory.PurchaseDate,
                                         ExpirationDate = etlInventory.ExpirationDate,
                                         InventoryNotes = etlInventory.InventoryNotes,
                                         ParentInventoryUid = etlInventory.ParentInventoryUid,
                                         ParentTag = etlInventory.ParentTag,
                                         InventorySourceUid = etlInventory.InventorySourceUid,
                                         InventorySourceName = etlInventory.InventorySourceName,
                                         PurchaseUid = etlInventory.PurchaseUid,
                                         OrderNumber = etlInventory.OrderNumber,
                                         PurchaseItemDetailUid = etlInventory.PurchaseItemDetailUid,
                                         LineNumber = etlInventory.LineNumber,
                                         AccountCode = etlInventory.AccountCode,
                                         VendorUid = etlInventory.VendorUid,
                                         VendorName = etlInventory.VendorName,
                                         VendorAccountNumber = etlInventory.VendorAccountNumber,
                                         PurchaseItemShipmentUid = etlInventory.PurchaseItemShipmentUid,
                                         InvoiceNumber = etlInventory.InvoiceNumber,
                                         InvoiceDate = etlInventory.InvoiceDate,
                                         InventoryExt1Uid = etlInventory.InventoryExt1Uid,
                                         InventoryMeta1Uid = etlInventory.InventoryMeta1Uid,
                                         CustomField1Label = etlInventory.CustomField1Label,
                                         CustomField1Value = etlInventory.CustomField1Value,
                                         InventoryExt2Uid = etlInventory.InventoryExt2Uid,
                                         InventoryMeta2Uid = etlInventory.InventoryMeta2Uid,
                                         CustomField2Label = etlInventory.CustomField2Label,
                                         CustomField2Value = etlInventory.CustomField2Value,
                                         InventoryExt3Uid = etlInventory.InventoryExt3Uid,
                                         InventoryMeta3Uid = etlInventory.InventoryMeta3Uid,
                                         CustomField3Label = etlInventory.CustomField3Label,
                                         CustomField3Value = etlInventory.CustomField3Value,
                                         InventoryExt4Uid = etlInventory.InventoryExt4Uid,
                                         InventoryMeta4Uid = etlInventory.InventoryMeta4Uid,
                                         CustomField4Label = etlInventory.CustomField4Label,
                                         CustomField4Value = etlInventory.CustomField4Value,
                                         Rejected = etlInventory.Rejected,
                                         RejectedNotes = etlInventory.RejectedNotes
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
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();

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
                                         _ETL_InventoryUid = etlInventory.EtlInventoryUid,
                                         ProcessUid = etlInventory.ProcessUid,
                                         RowId = etlInventory.RowId,
                                         InventoryUid = etlInventory.InventoryUid,
                                         AssetId = etlInventory.AssetId,
                                         Tag = etlInventory.Tag,
                                         Serial = etlInventory.Serial,
                                         InventoryTypeUid = etlInventory.InventoryTypeUid,
                                         InventoryTypeName = etlInventory.InventoryTypeName,
                                         ItemUid = etlInventory.ItemUid,
                                         ProductName = etlInventory.ProductName,
                                         ProductDescription = etlInventory.ProductDescription,
                                         ProductByNumber = etlInventory.ProductByNumber,
                                         ItemTypeUid = etlInventory.ItemTypeUid,
                                         ProductTypeName = etlInventory.ProductTypeName,
                                         ProductTypeDescription = etlInventory.ProductTypeDescription,
                                         ModelNumber = etlInventory.ModelNumber,
                                         ManufacturerUid = etlInventory.ManufacturerUid,
                                         ManufacturerName = etlInventory.ManufacturerName,
                                         AreaUid = etlInventory.AreaUid,
                                         AreaName = etlInventory.AreaName,
                                         SiteUid = etlInventory.SiteUid,
                                         SiteId = etlInventory.SiteId,
                                         SiteName = etlInventory.SiteName,
                                         EntityUid = etlInventory.EntityUid,
                                         EntityId = etlInventory.EntityId,
                                         EntityName = etlInventory.EntityName,
                                         EntityTypeUid = etlInventory.EntityTypeUid,
                                         EntityTypeName = etlInventory.EntityTypeName,
                                         StatusId = etlInventory.StatusId,
                                         Status = etlInventory.Status,
                                         TechDepartmentUid = etlInventory.TechDepartmentUid,
                                         DepartmentName = etlInventory.DepartmentName,
                                         DepartmentId = etlInventory.DepartmentId,
                                         FundingSourceUid = etlInventory.FundingSourceUid,
                                         FundingSource = etlInventory.FundingSource,
                                         FundingSourceDescription = etlInventory.FundingSourceDescription,
                                         PurchasePrice = etlInventory.PurchasePrice,
                                         PurchaseDate = etlInventory.PurchaseDate,
                                         ExpirationDate = etlInventory.ExpirationDate,
                                         InventoryNotes = etlInventory.InventoryNotes,
                                         ParentInventoryUid = etlInventory.ParentInventoryUid,
                                         ParentTag = etlInventory.ParentTag,
                                         InventorySourceUid = etlInventory.InventorySourceUid,
                                         InventorySourceName = etlInventory.InventorySourceName,
                                         PurchaseUid = etlInventory.PurchaseUid,
                                         OrderNumber = etlInventory.OrderNumber,
                                         PurchaseItemDetailUid = etlInventory.PurchaseItemDetailUid,
                                         LineNumber = etlInventory.LineNumber,
                                         AccountCode = etlInventory.AccountCode,
                                         VendorUid = etlInventory.VendorUid,
                                         VendorName = etlInventory.VendorName,
                                         VendorAccountNumber = etlInventory.VendorAccountNumber,
                                         PurchaseItemShipmentUid = etlInventory.PurchaseItemShipmentUid,
                                         InvoiceNumber = etlInventory.InvoiceNumber,
                                         InvoiceDate = etlInventory.InvoiceDate,
                                         InventoryExt1Uid = etlInventory.InventoryExt1Uid,
                                         InventoryMeta1Uid = etlInventory.InventoryMeta1Uid,
                                         CustomField1Label = etlInventory.CustomField1Label,
                                         CustomField1Value = etlInventory.CustomField1Value,
                                         InventoryExt2Uid = etlInventory.InventoryExt2Uid,
                                         InventoryMeta2Uid = etlInventory.InventoryMeta2Uid,
                                         CustomField2Label = etlInventory.CustomField2Label,
                                         CustomField2Value = etlInventory.CustomField2Value,
                                         InventoryExt3Uid = etlInventory.InventoryExt3Uid,
                                         InventoryMeta3Uid = etlInventory.InventoryMeta3Uid,
                                         CustomField3Label = etlInventory.CustomField3Label,
                                         CustomField3Value = etlInventory.CustomField3Value,
                                         InventoryExt4Uid = etlInventory.InventoryExt4Uid,
                                         InventoryMeta4Uid = etlInventory.InventoryMeta4Uid,
                                         CustomField4Label = etlInventory.CustomField4Label,
                                         CustomField4Value = etlInventory.CustomField4Value,
                                         Rejected = etlInventory.Rejected,
                                         RejectedNotes = etlInventory.RejectedNotes
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
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();

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
                    RowId = etlInventoryData.RowId,
                    InventoryUid = etlInventoryData.InventoryUid,
                    AssetId = etlInventoryData.AssetId,
                    Tag = etlInventoryData.Tag,
                    Serial = etlInventoryData.Serial,
                    InventoryTypeUid = etlInventoryData.InventoryTypeUid,
                    InventoryTypeName = etlInventoryData.InventoryTypeName,
                    ItemUid = etlInventoryData.ItemUid,
                    ProductName = etlInventoryData.ProductName,
                    ProductDescription = etlInventoryData.ProductDescription,
                    ProductByNumber = etlInventoryData.ProductByNumber,
                    ItemTypeUid = etlInventoryData.ItemTypeUid,
                    ProductTypeName = etlInventoryData.ProductTypeName,
                    ProductTypeDescription = etlInventoryData.ProductTypeDescription,
                    ModelNumber = etlInventoryData.ModelNumber,
                    ManufacturerUid = etlInventoryData.ManufacturerUid,
                    ManufacturerName = etlInventoryData.ManufacturerName,
                    AreaUid = etlInventoryData.AreaUid,
                    AreaName = etlInventoryData.AreaName,
                    SiteUid = etlInventoryData.SiteUid,
                    SiteId = etlInventoryData.SiteId,
                    SiteName = etlInventoryData.SiteName,
                    EntityUid = etlInventoryData.EntityUid,
                    EntityId = etlInventoryData.EntityId,
                    EntityName = etlInventoryData.EntityName,
                    EntityTypeUid = etlInventoryData.EntityTypeUid,
                    EntityTypeName = etlInventoryData.EntityTypeName,
                    StatusId = etlInventoryData.StatusId,
                    Status = etlInventoryData.Status,
                    TechDepartmentUid = etlInventoryData.TechDepartmentUid,
                    DepartmentName = etlInventoryData.DepartmentName,
                    DepartmentId = etlInventoryData.DepartmentId,
                    FundingSourceUid = etlInventoryData.FundingSourceUid,
                    FundingSource = etlInventoryData.FundingSource,
                    FundingSourceDescription = etlInventoryData.FundingSourceDescription,
                    PurchasePrice = etlInventoryData.PurchasePrice,
                    PurchaseDate = etlInventoryData.PurchaseDate,
                    ExpirationDate = etlInventoryData.ExpirationDate,
                    InventoryNotes = etlInventoryData.InventoryNotes,
                    ParentInventoryUid = etlInventoryData.ParentInventoryUid,
                    ParentTag = etlInventoryData.ParentTag,
                    InventorySourceUid = etlInventoryData.InventorySourceUid,
                    InventorySourceName = etlInventoryData.InventorySourceName,
                    PurchaseUid = etlInventoryData.PurchaseUid,
                    OrderNumber = etlInventoryData.OrderNumber,
                    PurchaseItemDetailUid = etlInventoryData.PurchaseItemDetailUid,
                    LineNumber = etlInventoryData.LineNumber,
                    AccountCode = etlInventoryData.AccountCode,
                    VendorUid = etlInventoryData.VendorUid,
                    VendorName = etlInventoryData.VendorName,
                    VendorAccountNumber = etlInventoryData.VendorAccountNumber,
                    PurchaseItemShipmentUid = etlInventoryData.PurchaseItemShipmentUid,
                    InvoiceNumber = etlInventoryData.InvoiceNumber,
                    InvoiceDate = etlInventoryData.InvoiceDate,
                    InventoryExt1Uid = etlInventoryData.InventoryExt1Uid,
                    InventoryMeta1Uid = etlInventoryData.InventoryMeta1Uid,
                    CustomField1Label = etlInventoryData.CustomField1Label,
                    CustomField1Value = etlInventoryData.CustomField1Value,
                    InventoryExt2Uid = etlInventoryData.InventoryExt2Uid,
                    InventoryMeta2Uid = etlInventoryData.InventoryMeta2Uid,
                    CustomField2Label = etlInventoryData.CustomField2Label,
                    CustomField2Value = etlInventoryData.CustomField2Value,
                    InventoryExt3Uid = etlInventoryData.InventoryExt3Uid,
                    InventoryMeta3Uid = etlInventoryData.InventoryMeta3Uid,
                    CustomField3Label = etlInventoryData.CustomField3Label,
                    CustomField3Value = etlInventoryData.CustomField3Value,
                    InventoryExt4Uid = etlInventoryData.InventoryExt4Uid,
                    InventoryMeta4Uid = etlInventoryData.InventoryMeta4Uid,
                    CustomField4Label = etlInventoryData.CustomField4Label,
                    CustomField4Value = etlInventoryData.CustomField4Value,
                    Rejected = etlInventoryData.Rejected,
                    RejectedNotes = etlInventoryData.RejectedNotes
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
                                        RowId = data.RowId,
                                        InventoryUid = data.InventoryUid,
                                        AssetId = data.AssetId,
                                        Tag = data.Tag,
                                        Serial = data.Serial,
                                        InventoryTypeUid = data.InventoryTypeUid,
                                        InventoryTypeName = data.InventoryTypeName,
                                        ItemUid = data.ItemUid,
                                        ProductName = data.ProductName,
                                        ProductDescription = data.ProductDescription,
                                        ProductByNumber = data.ProductByNumber,
                                        ItemTypeUid = data.ItemTypeUid,
                                        ProductTypeName = data.ProductTypeName,
                                        ProductTypeDescription = data.ProductTypeDescription,
                                        ModelNumber = data.ModelNumber,
                                        ManufacturerUid = data.ManufacturerUid,
                                        ManufacturerName = data.ManufacturerName,
                                        AreaUid = data.AreaUid,
                                        AreaName = data.AreaName,
                                        SiteUid = data.SiteUid,
                                        SiteId = data.SiteId,
                                        SiteName = data.SiteName,
                                        EntityUid = data.EntityUid,
                                        EntityId = data.EntityId,
                                        EntityName = data.EntityName,
                                        EntityTypeUid = data.EntityTypeUid,
                                        EntityTypeName = data.EntityTypeName,
                                        StatusId = data.StatusId,
                                        Status = data.Status,
                                        TechDepartmentUid = data.TechDepartmentUid,
                                        DepartmentName = data.DepartmentName,
                                        DepartmentId = data.DepartmentId,
                                        FundingSourceUid = data.FundingSourceUid,
                                        FundingSource = data.FundingSource,
                                        FundingSourceDescription = data.FundingSourceDescription,
                                        PurchasePrice = data.PurchasePrice,
                                        PurchaseDate = data.PurchaseDate,
                                        ExpirationDate = data.ExpirationDate,
                                        InventoryNotes = data.InventoryNotes,
                                        ParentInventoryUid = data.ParentInventoryUid,
                                        ParentTag = data.ParentTag,
                                        InventorySourceUid = data.InventorySourceUid,
                                        InventorySourceName = data.InventorySourceName,
                                        PurchaseUid = data.PurchaseUid,
                                        OrderNumber = data.OrderNumber,
                                        PurchaseItemDetailUid = data.PurchaseItemDetailUid,
                                        LineNumber = data.LineNumber,
                                        AccountCode = data.AccountCode,
                                        VendorUid = data.VendorUid,
                                        VendorName = data.VendorName,
                                        VendorAccountNumber = data.VendorAccountNumber,
                                        PurchaseItemShipmentUid = data.PurchaseItemShipmentUid,
                                        InvoiceNumber = data.InvoiceNumber,
                                        InvoiceDate = data.InvoiceDate,
                                        InventoryExt1Uid = data.InventoryExt1Uid,
                                        InventoryMeta1Uid = data.InventoryMeta1Uid,
                                        CustomField1Label = data.CustomField1Label,
                                        CustomField1Value = data.CustomField1Value,
                                        InventoryExt2Uid = data.InventoryExt2Uid,
                                        InventoryMeta2Uid = data.InventoryMeta2Uid,
                                        CustomField2Label = data.CustomField2Label,
                                        CustomField2Value = data.CustomField2Value,
                                        InventoryExt3Uid = data.InventoryExt3Uid,
                                        InventoryMeta3Uid = data.InventoryMeta3Uid,
                                        CustomField3Label = data.CustomField3Label,
                                        CustomField3Value = data.CustomField3Value,
                                        InventoryExt4Uid = data.InventoryExt4Uid,
                                        InventoryMeta4Uid = data.InventoryMeta4Uid,
                                        CustomField4Label = data.CustomField4Label,
                                        CustomField4Value = data.CustomField4Value,
                                        Rejected = data.Rejected,
                                        RejectedNotes = data.RejectedNotes
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
                                    where etlInventory.EtlInventoryUid == data._ETL_InventoryUid
                                    select etlInventory).FirstOrDefault();

                //dataToUpdate. = etlInventoryData.;
                dataToUpdate.ProcessUid = data.ProcessUid;
                dataToUpdate.InventoryUid = data.InventoryUid;
                dataToUpdate.RowId = data.RowId;
                dataToUpdate.AssetId = data.AssetId;
                dataToUpdate.Tag = data.Tag;
                dataToUpdate.Serial = data.Serial;
                dataToUpdate.InventoryTypeUid = data.InventoryTypeUid;
                dataToUpdate.InventoryTypeName = data.InventoryTypeName;
                dataToUpdate.ItemUid = data.ItemUid;
                dataToUpdate.ProductName = data.ProductName;
                dataToUpdate.ProductDescription = data.ProductDescription;
                dataToUpdate.ProductByNumber = data.ProductByNumber;
                dataToUpdate.ItemTypeUid = data.ItemTypeUid;
                dataToUpdate.ProductTypeName = data.ProductTypeName;
                dataToUpdate.ProductTypeDescription = data.ProductTypeDescription;
                dataToUpdate.ModelNumber = data.ModelNumber;
                dataToUpdate.ManufacturerUid = data.ManufacturerUid;
                dataToUpdate.ManufacturerName = data.ManufacturerName;
                dataToUpdate.AreaUid = data.AreaUid;
                dataToUpdate.AreaName = data.AreaName;
                dataToUpdate.SiteUid = data.SiteUid;
                dataToUpdate.SiteId = data.SiteId;
                dataToUpdate.SiteName = data.SiteName;
                dataToUpdate.EntityUid = data.EntityUid;
                dataToUpdate.EntityId = data.EntityId;
                dataToUpdate.EntityName = data.EntityName;
                dataToUpdate.EntityTypeUid = data.EntityTypeUid;
                dataToUpdate.EntityTypeName = data.EntityTypeName;
                dataToUpdate.StatusId = data.StatusId;
                dataToUpdate.Status = data.Status;
                dataToUpdate.TechDepartmentUid = data.TechDepartmentUid;
                dataToUpdate.DepartmentName = data.DepartmentName;
                dataToUpdate.DepartmentId = data.DepartmentId;
                dataToUpdate.FundingSourceUid = data.FundingSourceUid;
                dataToUpdate.FundingSource = data.FundingSource;
                dataToUpdate.FundingSourceDescription = data.FundingSourceDescription;
                dataToUpdate.PurchasePrice = data.PurchasePrice;
                dataToUpdate.PurchaseDate = data.PurchaseDate;
                dataToUpdate.ExpirationDate = data.ExpirationDate;
                dataToUpdate.InventoryNotes = data.InventoryNotes;
                dataToUpdate.ParentInventoryUid = data.ParentInventoryUid;
                dataToUpdate.ParentTag = data.ParentTag;
                dataToUpdate.InventorySourceUid = data.InventorySourceUid;
                dataToUpdate.InventorySourceName = data.InventorySourceName;
                dataToUpdate.PurchaseUid = data.PurchaseUid;
                dataToUpdate.OrderNumber = data.OrderNumber;
                dataToUpdate.PurchaseItemDetailUid = data.PurchaseItemDetailUid;
                dataToUpdate.LineNumber = data.LineNumber;
                dataToUpdate.AccountCode = data.AccountCode;
                dataToUpdate.VendorUid = data.VendorUid;
                dataToUpdate.VendorName = data.VendorName;
                dataToUpdate.VendorAccountNumber = data.VendorAccountNumber;
                dataToUpdate.PurchaseItemShipmentUid = data.PurchaseItemShipmentUid;
                dataToUpdate.InvoiceNumber = data.InvoiceNumber;
                dataToUpdate.InvoiceDate = data.InvoiceDate;
                dataToUpdate.InventoryExt1Uid = data.InventoryExt1Uid;
                dataToUpdate.InventoryMeta1Uid = data.InventoryMeta1Uid;
                dataToUpdate.CustomField1Label = data.CustomField1Label;
                dataToUpdate.CustomField1Value = data.CustomField1Value;
                dataToUpdate.InventoryExt2Uid = data.InventoryExt2Uid;
                dataToUpdate.InventoryMeta2Uid = data.InventoryMeta2Uid;
                dataToUpdate.CustomField2Label = data.CustomField2Label;
                dataToUpdate.CustomField2Value = data.CustomField2Value;
                dataToUpdate.InventoryExt3Uid = data.InventoryExt3Uid;
                dataToUpdate.InventoryMeta3Uid = data.InventoryMeta3Uid;
                dataToUpdate.CustomField3Label = data.CustomField3Label;
                dataToUpdate.CustomField3Value = data.CustomField3Value;
                dataToUpdate.InventoryExt4Uid = data.InventoryExt4Uid;
                dataToUpdate.InventoryMeta4Uid = data.InventoryMeta4Uid;
                dataToUpdate.CustomField4Label = data.CustomField4Label;
                dataToUpdate.CustomField4Value = data.CustomField4Value;
                dataToUpdate.Rejected = data.Rejected;
                dataToUpdate.RejectedNotes = data.RejectedNotes;

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
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();

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
