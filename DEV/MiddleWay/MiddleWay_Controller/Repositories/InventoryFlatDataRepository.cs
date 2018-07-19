using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWay_Controller.Repositories
{
    public class InventoryFlatDataRepository : IInventoryFlatDataRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public InventoryFlatDataRepository(IntegrationMiddleWayContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        public List<InventoryFlatDataModel> Select(int processUid, int offset, int limit)
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

                var data = (from inventoryFlat in _context.InventoryFlatData
                            where inventoryFlat.ProcessUid == processUid
                            select new InventoryFlatDataModel
                            {
                                InventoryFlatDataUid = inventoryFlat.InventoryFlatDataUid,
                                ProcessUid = inventoryFlat.ProcessUid,
                                RowId = inventoryFlat.RowId,
                                AssetId = inventoryFlat.AssetId,
                                Tag = inventoryFlat.Tag,
                                Serial = inventoryFlat.Serial,
                                SiteId = inventoryFlat.SiteId,
                                SiteName = inventoryFlat.SiteName,
                                Location = inventoryFlat.Location,
                                LocationType = inventoryFlat.LocationType,
                                Status = inventoryFlat.Status,
                                DepartmentName = inventoryFlat.DepartmentName,
                                DepartmentId = inventoryFlat.DepartmentId,
                                FundingSource = inventoryFlat.FundingSource,
                                FundingSourceDescription = inventoryFlat.FundingSourceDescription,
                                PurchasePrice = inventoryFlat.PurchasePrice,
                                PurchaseDate = inventoryFlat.PurchaseDate,
                                ExpirationDate = inventoryFlat.ExpirationDate,
                                InventoryNotes = inventoryFlat.InventoryNotes,
                                OrderNumber = inventoryFlat.OrderNumber,
                                LineNumber = inventoryFlat.LineNumber,
                                VendorName = inventoryFlat.VendorName,
                                VendorAccountNumber = inventoryFlat.VendorAccountNumber,
                                ParentTag = inventoryFlat.ParentTag,
                                ProductName = inventoryFlat.ProductName,
                                ProductDescription = inventoryFlat.ProductTypeDescription,
                                ProductByNumber = inventoryFlat.ProductByNumber,
                                ProductTypeName = inventoryFlat.ProductTypeName,
                                ProductTypeDescription = inventoryFlat.ProductTypeDescription,
                                ModelNumber = inventoryFlat.ModelNumber,
                                ManufacturerName = inventoryFlat.ManufacturerName,
                                AreaName = inventoryFlat.AreaName,
                                CustomField1Value = inventoryFlat.CustomField1Value,
                                CustomField1Label = inventoryFlat.CustomField1Label,
                                CustomField2Value = inventoryFlat.CustomField2Value,
                                CustomField2Label = inventoryFlat.CustomField2Label,
                                CustomField3Value = inventoryFlat.CustomField3Value,
                                CustomField3Label = inventoryFlat.CustomField3Label,
                                CustomField4Value = inventoryFlat.CustomField4Value,
                                CustomField4Label = inventoryFlat.CustomField4Label,
                                InvoiceNumber = inventoryFlat.InvoiceNumber,
                                InvoiceDate = inventoryFlat.InvoiceDate,
                                Rejected = inventoryFlat.Rejected,
                                RejectedNotes = inventoryFlat.RejectedNotes
                            });

                return data.Skip(offset).Take(limit).ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<InventoryFlatDataModel> Select(string client, string processName, int offset, int limit)
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

                var data = (from inventoryFlat in _context.InventoryFlatData
                            join processes in _context.Processes
                                on inventoryFlat.ProcessUid equals processes.ProcessUid
                            where processes.Client.Trim().ToLower() == clientVal
                               && processes.ProcessName.Trim().ToLower() == processNameVal
                            select new InventoryFlatDataModel
                            {
                                InventoryFlatDataUid = inventoryFlat.InventoryFlatDataUid,
                                ProcessUid = inventoryFlat.ProcessUid,
                                RowId = inventoryFlat.RowId,
                                AssetId = inventoryFlat.AssetId,
                                Tag = inventoryFlat.Tag,
                                Serial = inventoryFlat.Serial,
                                SiteId = inventoryFlat.SiteId,
                                SiteName = inventoryFlat.SiteName,
                                Location = inventoryFlat.Location,
                                LocationType = inventoryFlat.LocationType,
                                Status = inventoryFlat.Status,
                                DepartmentName = inventoryFlat.DepartmentName,
                                DepartmentId = inventoryFlat.DepartmentId,
                                FundingSource = inventoryFlat.FundingSource,
                                FundingSourceDescription = inventoryFlat.FundingSourceDescription,
                                PurchasePrice = inventoryFlat.PurchasePrice,
                                PurchaseDate = inventoryFlat.PurchaseDate,
                                ExpirationDate = inventoryFlat.ExpirationDate,
                                InventoryNotes = inventoryFlat.InventoryNotes,
                                OrderNumber = inventoryFlat.OrderNumber,
                                LineNumber = inventoryFlat.LineNumber,
                                VendorName = inventoryFlat.VendorName,
                                VendorAccountNumber = inventoryFlat.VendorAccountNumber,
                                ParentTag = inventoryFlat.ParentTag,
                                ProductName = inventoryFlat.ProductName,
                                ProductDescription = inventoryFlat.ProductTypeDescription,
                                ProductByNumber = inventoryFlat.ProductByNumber,
                                ProductTypeName = inventoryFlat.ProductTypeName,
                                ProductTypeDescription = inventoryFlat.ProductTypeDescription,
                                ModelNumber = inventoryFlat.ModelNumber,
                                ManufacturerName = inventoryFlat.ManufacturerName,
                                AreaName = inventoryFlat.AreaName,
                                CustomField1Value = inventoryFlat.CustomField1Value,
                                CustomField1Label = inventoryFlat.CustomField1Label,
                                CustomField2Value = inventoryFlat.CustomField2Value,
                                CustomField2Label = inventoryFlat.CustomField2Label,
                                CustomField3Value = inventoryFlat.CustomField3Value,
                                CustomField3Label = inventoryFlat.CustomField3Label,
                                CustomField4Value = inventoryFlat.CustomField4Value,
                                CustomField4Label = inventoryFlat.CustomField4Label,
                                InvoiceNumber = inventoryFlat.InvoiceNumber,
                                InvoiceDate = inventoryFlat.InvoiceDate,
                                Rejected = inventoryFlat.Rejected,
                                RejectedNotes = inventoryFlat.RejectedNotes
                            });

                return data.Skip(offset).Take(limit).ToList();
            }
            catch
            {
                throw;
            }
        }

        public InventoryFlatDataModel Select(int inventoryFlatDataUid)
        {
            try
            {
                var data = (from inventoryFlat in _context.InventoryFlatData
                            where inventoryFlat.InventoryFlatDataUid == inventoryFlatDataUid
                            select new InventoryFlatDataModel
                            {
                                InventoryFlatDataUid = inventoryFlat.InventoryFlatDataUid,
                                ProcessUid = inventoryFlat.ProcessUid,
                                RowId = inventoryFlat.RowId,
                                AssetId = inventoryFlat.AssetId,
                                Tag = inventoryFlat.Tag,
                                Serial = inventoryFlat.Serial,
                                SiteId = inventoryFlat.SiteId,
                                SiteName = inventoryFlat.SiteName,
                                Location = inventoryFlat.Location,
                                LocationType = inventoryFlat.LocationType,
                                Status = inventoryFlat.Status,
                                DepartmentName = inventoryFlat.DepartmentName,
                                DepartmentId = inventoryFlat.DepartmentId,
                                FundingSource = inventoryFlat.FundingSource,
                                FundingSourceDescription = inventoryFlat.FundingSourceDescription,
                                PurchasePrice = inventoryFlat.PurchasePrice,
                                PurchaseDate = inventoryFlat.PurchaseDate,
                                ExpirationDate = inventoryFlat.ExpirationDate,
                                InventoryNotes = inventoryFlat.InventoryNotes,
                                OrderNumber = inventoryFlat.OrderNumber,
                                LineNumber = inventoryFlat.LineNumber,
                                VendorName = inventoryFlat.VendorName,
                                VendorAccountNumber = inventoryFlat.VendorAccountNumber,
                                ParentTag = inventoryFlat.ParentTag,
                                ProductName = inventoryFlat.ProductName,
                                ProductDescription = inventoryFlat.ProductTypeDescription,
                                ProductByNumber = inventoryFlat.ProductByNumber,
                                ProductTypeName = inventoryFlat.ProductTypeName,
                                ProductTypeDescription = inventoryFlat.ProductTypeDescription,
                                ModelNumber = inventoryFlat.ModelNumber,
                                ManufacturerName = inventoryFlat.ManufacturerName,
                                AreaName = inventoryFlat.AreaName,
                                CustomField1Value = inventoryFlat.CustomField1Value,
                                CustomField1Label = inventoryFlat.CustomField1Label,
                                CustomField2Value = inventoryFlat.CustomField2Value,
                                CustomField2Label = inventoryFlat.CustomField2Label,
                                CustomField3Value = inventoryFlat.CustomField3Value,
                                CustomField3Label = inventoryFlat.CustomField3Label,
                                CustomField4Value = inventoryFlat.CustomField4Value,
                                CustomField4Label = inventoryFlat.CustomField4Label,
                                InvoiceNumber = inventoryFlat.InvoiceNumber,
                                InvoiceDate = inventoryFlat.InvoiceDate,
                                Rejected = inventoryFlat.Rejected,
                                RejectedNotes = inventoryFlat.RejectedNotes
                            }).FirstOrDefault();

                return data;
            }
            catch
            {
                throw;
            }
        }

        public InventoryFlatDataModel SelectByAssetId(string client, string processName, string assetId)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var assetIdVal = (assetId ?? string.Empty).Trim().ToLower();

                var data = (from inventoryFlat in _context.InventoryFlatData
                            join processes in _context.Processes
                               on inventoryFlat.ProcessUid equals processes.ProcessUid
                            where processes.Client.Trim().ToLower() == clientVal
                               && processes.ProcessName.Trim().ToLower() == processNameVal
                               && inventoryFlat.AssetId.Trim().ToLower() == assetIdVal
                            select new InventoryFlatDataModel
                            {
                                InventoryFlatDataUid = inventoryFlat.InventoryFlatDataUid,
                                ProcessUid = inventoryFlat.ProcessUid,
                                RowId = inventoryFlat.RowId,
                                AssetId = inventoryFlat.AssetId,
                                Tag = inventoryFlat.Tag,
                                Serial = inventoryFlat.Serial,
                                SiteId = inventoryFlat.SiteId,
                                SiteName = inventoryFlat.SiteName,
                                Location = inventoryFlat.Location,
                                LocationType = inventoryFlat.LocationType,
                                Status = inventoryFlat.Status,
                                DepartmentName = inventoryFlat.DepartmentName,
                                DepartmentId = inventoryFlat.DepartmentId,
                                FundingSource = inventoryFlat.FundingSource,
                                FundingSourceDescription = inventoryFlat.FundingSourceDescription,
                                PurchasePrice = inventoryFlat.PurchasePrice,
                                PurchaseDate = inventoryFlat.PurchaseDate,
                                ExpirationDate = inventoryFlat.ExpirationDate,
                                InventoryNotes = inventoryFlat.InventoryNotes,
                                OrderNumber = inventoryFlat.OrderNumber,
                                LineNumber = inventoryFlat.LineNumber,
                                VendorName = inventoryFlat.VendorName,
                                VendorAccountNumber = inventoryFlat.VendorAccountNumber,
                                ParentTag = inventoryFlat.ParentTag,
                                ProductName = inventoryFlat.ProductName,
                                ProductDescription = inventoryFlat.ProductTypeDescription,
                                ProductByNumber = inventoryFlat.ProductByNumber,
                                ProductTypeName = inventoryFlat.ProductTypeName,
                                ProductTypeDescription = inventoryFlat.ProductTypeDescription,
                                ModelNumber = inventoryFlat.ModelNumber,
                                ManufacturerName = inventoryFlat.ManufacturerName,
                                AreaName = inventoryFlat.AreaName,
                                CustomField1Value = inventoryFlat.CustomField1Value,
                                CustomField1Label = inventoryFlat.CustomField1Label,
                                CustomField2Value = inventoryFlat.CustomField2Value,
                                CustomField2Label = inventoryFlat.CustomField2Label,
                                CustomField3Value = inventoryFlat.CustomField3Value,
                                CustomField3Label = inventoryFlat.CustomField3Label,
                                CustomField4Value = inventoryFlat.CustomField4Value,
                                CustomField4Label = inventoryFlat.CustomField4Label,
                                InvoiceNumber = inventoryFlat.InvoiceNumber,
                                InvoiceDate = inventoryFlat.InvoiceDate,
                                Rejected = inventoryFlat.Rejected,
                                RejectedNotes = inventoryFlat.RejectedNotes
                            }).FirstOrDefault();

                return data;
            }
            catch
            {
                throw;
            }
        }

        public InventoryFlatDataModel SelectByTag(string client, string processName, string tag)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var tagVal = (tag ?? string.Empty).Trim().ToLower();

                var data = (from inventoryFlat in _context.InventoryFlatData
                            join processes in _context.Processes
                               on inventoryFlat.ProcessUid equals processes.ProcessUid
                            where processes.Client.Trim().ToLower() == clientVal
                               && processes.ProcessName.Trim().ToLower() == processNameVal
                               && inventoryFlat.AssetId.Trim().ToLower() == tagVal
                            select new InventoryFlatDataModel
                            {
                                InventoryFlatDataUid = inventoryFlat.InventoryFlatDataUid,
                                ProcessUid = inventoryFlat.ProcessUid,
                                RowId = inventoryFlat.RowId,
                                AssetId = inventoryFlat.AssetId,
                                Tag = inventoryFlat.Tag,
                                Serial = inventoryFlat.Serial,
                                SiteId = inventoryFlat.SiteId,
                                SiteName = inventoryFlat.SiteName,
                                Location = inventoryFlat.Location,
                                LocationType = inventoryFlat.LocationType,
                                Status = inventoryFlat.Status,
                                DepartmentName = inventoryFlat.DepartmentName,
                                DepartmentId = inventoryFlat.DepartmentId,
                                FundingSource = inventoryFlat.FundingSource,
                                FundingSourceDescription = inventoryFlat.FundingSourceDescription,
                                PurchasePrice = inventoryFlat.PurchasePrice,
                                PurchaseDate = inventoryFlat.PurchaseDate,
                                ExpirationDate = inventoryFlat.ExpirationDate,
                                InventoryNotes = inventoryFlat.InventoryNotes,
                                OrderNumber = inventoryFlat.OrderNumber,
                                LineNumber = inventoryFlat.LineNumber,
                                VendorName = inventoryFlat.VendorName,
                                VendorAccountNumber = inventoryFlat.VendorAccountNumber,
                                ParentTag = inventoryFlat.ParentTag,
                                ProductName = inventoryFlat.ProductName,
                                ProductDescription = inventoryFlat.ProductTypeDescription,
                                ProductByNumber = inventoryFlat.ProductByNumber,
                                ProductTypeName = inventoryFlat.ProductTypeName,
                                ProductTypeDescription = inventoryFlat.ProductTypeDescription,
                                ModelNumber = inventoryFlat.ModelNumber,
                                ManufacturerName = inventoryFlat.ManufacturerName,
                                AreaName = inventoryFlat.AreaName,
                                CustomField1Value = inventoryFlat.CustomField1Value,
                                CustomField1Label = inventoryFlat.CustomField1Label,
                                CustomField2Value = inventoryFlat.CustomField2Value,
                                CustomField2Label = inventoryFlat.CustomField2Label,
                                CustomField3Value = inventoryFlat.CustomField3Value,
                                CustomField3Label = inventoryFlat.CustomField3Label,
                                CustomField4Value = inventoryFlat.CustomField4Value,
                                CustomField4Label = inventoryFlat.CustomField4Label,
                                InvoiceNumber = inventoryFlat.InvoiceNumber,
                                InvoiceDate = inventoryFlat.InvoiceDate,
                                Rejected = inventoryFlat.Rejected,
                                RejectedNotes = inventoryFlat.RejectedNotes
                            }).FirstOrDefault();

                return data;
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
                return (from inventoryFlat in _context.InventoryFlatData
                        where inventoryFlat.ProcessUid == processUid
                        select inventoryFlat).Count();
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

                return (from inventoryFlat in _context.InventoryFlatData
                        join processes in _context.Processes
                               on inventoryFlat.ProcessUid equals processes.ProcessUid
                        where processes.Client.Trim().ToLower() == clientVal
                           && processes.ProcessName.Trim().ToLower() == processNameVal
                        select inventoryFlat).Count();
            }
            catch
            {
                throw;
            }
        }

        #endregion Select Functions

        #region Insert Functions

        public int Insert(InventoryFlatDataModel inventoryFlatData)
        {
            try
            {
                var inventoryFlatToInsert = new InventoryFlatData()
                {
                    InventoryFlatDataUid = 0,
                    ProcessUid = inventoryFlatData.ProcessUid,
                    RowId = inventoryFlatData.RowId,
                    AssetId = inventoryFlatData.AssetId,
                    Tag = inventoryFlatData.Tag,
                    Serial = inventoryFlatData.Serial,
                    SiteId = inventoryFlatData.SiteId,
                    SiteName = inventoryFlatData.SiteName,
                    Location = inventoryFlatData.Location,
                    LocationType = inventoryFlatData.LocationType,
                    Status = inventoryFlatData.Status,
                    DepartmentName = inventoryFlatData.DepartmentName,
                    DepartmentId = inventoryFlatData.DepartmentId,
                    FundingSource = inventoryFlatData.FundingSource,
                    FundingSourceDescription = inventoryFlatData.FundingSourceDescription,
                    PurchasePrice = inventoryFlatData.PurchasePrice,
                    PurchaseDate = inventoryFlatData.PurchaseDate,
                    ExpirationDate = inventoryFlatData.ExpirationDate,
                    InventoryNotes = inventoryFlatData.InventoryNotes,
                    OrderNumber = inventoryFlatData.OrderNumber,
                    LineNumber = inventoryFlatData.LineNumber,
                    VendorName = inventoryFlatData.VendorName,
                    VendorAccountNumber = inventoryFlatData.VendorAccountNumber,
                    ParentTag = inventoryFlatData.ParentTag,
                    ProductName = inventoryFlatData.ProductName,
                    ProductDescription = inventoryFlatData.ProductTypeDescription,
                    ProductByNumber = inventoryFlatData.ProductByNumber,
                    ProductTypeName = inventoryFlatData.ProductTypeName,
                    ProductTypeDescription = inventoryFlatData.ProductTypeDescription,
                    ModelNumber = inventoryFlatData.ModelNumber,
                    ManufacturerName = inventoryFlatData.ManufacturerName,
                    AreaName = inventoryFlatData.AreaName,
                    CustomField1Value = inventoryFlatData.CustomField1Value,
                    CustomField1Label = inventoryFlatData.CustomField1Label,
                    CustomField2Value = inventoryFlatData.CustomField2Value,
                    CustomField2Label = inventoryFlatData.CustomField2Label,
                    CustomField3Value = inventoryFlatData.CustomField3Value,
                    CustomField3Label = inventoryFlatData.CustomField3Label,
                    CustomField4Value = inventoryFlatData.CustomField4Value,
                    CustomField4Label = inventoryFlatData.CustomField4Label,
                    InvoiceNumber = inventoryFlatData.InvoiceNumber,
                    InvoiceDate = inventoryFlatData.InvoiceDate,
                    Rejected = inventoryFlatData.Rejected,
                    RejectedNotes = inventoryFlatData.RejectedNotes
                };

                _context.InventoryFlatData.Add(inventoryFlatToInsert);
                var result = _context.SaveChanges();

                if (result == 1)
                {
                    return inventoryFlatToInsert.InventoryFlatDataUid;
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

        public bool InsertRange(List<InventoryFlatDataModel> inventoryFlatDataBatch)
        {
            try
            {
                foreach (var inventoryFlatData in inventoryFlatDataBatch)
                {
                    var inventoryFlatDataToInsert = new InventoryFlatData()
                    {
                        InventoryFlatDataUid = 0,
                        ProcessUid = inventoryFlatData.ProcessUid,
                        RowId = inventoryFlatData.RowId,
                        AssetId = inventoryFlatData.AssetId,
                        Tag = inventoryFlatData.Tag,
                        Serial = inventoryFlatData.Serial,
                        SiteId = inventoryFlatData.SiteId,
                        SiteName = inventoryFlatData.SiteName,
                        Location = inventoryFlatData.Location,
                        LocationType = inventoryFlatData.LocationType,
                        Status = inventoryFlatData.Status,
                        DepartmentName = inventoryFlatData.DepartmentName,
                        DepartmentId = inventoryFlatData.DepartmentId,
                        FundingSource = inventoryFlatData.FundingSource,
                        FundingSourceDescription = inventoryFlatData.FundingSourceDescription,
                        PurchasePrice = inventoryFlatData.PurchasePrice,
                        PurchaseDate = inventoryFlatData.PurchaseDate,
                        ExpirationDate = inventoryFlatData.ExpirationDate,
                        InventoryNotes = inventoryFlatData.InventoryNotes,
                        OrderNumber = inventoryFlatData.OrderNumber,
                        LineNumber = inventoryFlatData.LineNumber,
                        VendorName = inventoryFlatData.VendorName,
                        VendorAccountNumber = inventoryFlatData.VendorAccountNumber,
                        ParentTag = inventoryFlatData.ParentTag,
                        ProductName = inventoryFlatData.ProductName,
                        ProductDescription = inventoryFlatData.ProductTypeDescription,
                        ProductByNumber = inventoryFlatData.ProductByNumber,
                        ProductTypeName = inventoryFlatData.ProductTypeName,
                        ProductTypeDescription = inventoryFlatData.ProductTypeDescription,
                        ModelNumber = inventoryFlatData.ModelNumber,
                        ManufacturerName = inventoryFlatData.ManufacturerName,
                        AreaName = inventoryFlatData.AreaName,
                        CustomField1Value = inventoryFlatData.CustomField1Value,
                        CustomField1Label = inventoryFlatData.CustomField1Label,
                        CustomField2Value = inventoryFlatData.CustomField2Value,
                        CustomField2Label = inventoryFlatData.CustomField2Label,
                        CustomField3Value = inventoryFlatData.CustomField3Value,
                        CustomField3Label = inventoryFlatData.CustomField3Label,
                        CustomField4Value = inventoryFlatData.CustomField4Value,
                        CustomField4Label = inventoryFlatData.CustomField4Label,
                        InvoiceNumber = inventoryFlatData.InvoiceNumber,
                        InvoiceDate = inventoryFlatData.InvoiceDate,
                        Rejected = inventoryFlatData.Rejected,
                        RejectedNotes = inventoryFlatData.RejectedNotes
                    };
                    _context.InventoryFlatData.Add(inventoryFlatDataToInsert);
                }
                var result = _context.SaveChanges();

                return result == inventoryFlatDataBatch.Count();
            }
            catch
            {
                throw;
            }
        }

        #endregion Insert Functions

        #region Update Functions

        public bool Update(InventoryFlatDataModel inventoryFlatData)
        {
            try
            {
                var inventoryFlatDataToUpdate = (from inventoryFlat in _context.InventoryFlatData
                                                 where inventoryFlat.InventoryFlatDataUid == inventoryFlatData.InventoryFlatDataUid
                                                 select inventoryFlat).FirstOrDefault();

                inventoryFlatDataToUpdate.RowId = inventoryFlatData.RowId;
                inventoryFlatDataToUpdate.AssetId = inventoryFlatData.AssetId;
                inventoryFlatDataToUpdate.Tag = inventoryFlatData.Tag;
                inventoryFlatDataToUpdate.Serial = inventoryFlatData.Serial;
                inventoryFlatDataToUpdate.SiteId = inventoryFlatData.SiteId;
                inventoryFlatDataToUpdate.SiteName = inventoryFlatData.SiteName;
                inventoryFlatDataToUpdate.Location = inventoryFlatData.Location;
                inventoryFlatDataToUpdate.LocationType = inventoryFlatData.LocationType;
                inventoryFlatDataToUpdate.Status = inventoryFlatData.Status;
                inventoryFlatDataToUpdate.DepartmentName = inventoryFlatData.DepartmentName;
                inventoryFlatDataToUpdate.DepartmentId = inventoryFlatData.DepartmentId;
                inventoryFlatDataToUpdate.FundingSource = inventoryFlatData.FundingSource;
                inventoryFlatDataToUpdate.FundingSourceDescription = inventoryFlatData.FundingSourceDescription;
                inventoryFlatDataToUpdate.PurchasePrice = inventoryFlatData.PurchasePrice;
                inventoryFlatDataToUpdate.PurchaseDate = inventoryFlatData.PurchaseDate;
                inventoryFlatDataToUpdate.ExpirationDate = inventoryFlatData.ExpirationDate;
                inventoryFlatDataToUpdate.InventoryNotes = inventoryFlatData.InventoryNotes;
                inventoryFlatDataToUpdate.OrderNumber = inventoryFlatData.OrderNumber;
                inventoryFlatDataToUpdate.LineNumber = inventoryFlatData.LineNumber;
                inventoryFlatDataToUpdate.VendorName = inventoryFlatData.VendorName;
                inventoryFlatDataToUpdate.VendorAccountNumber = inventoryFlatData.VendorAccountNumber;
                inventoryFlatDataToUpdate.ParentTag = inventoryFlatData.ParentTag;
                inventoryFlatDataToUpdate.ProductName = inventoryFlatData.ProductName;
                inventoryFlatDataToUpdate.ProductDescription = inventoryFlatData.ProductTypeDescription;
                inventoryFlatDataToUpdate.ProductByNumber = inventoryFlatData.ProductByNumber;
                inventoryFlatDataToUpdate.ProductTypeName = inventoryFlatData.ProductTypeName;
                inventoryFlatDataToUpdate.ProductTypeDescription = inventoryFlatData.ProductTypeDescription;
                inventoryFlatDataToUpdate.ModelNumber = inventoryFlatData.ModelNumber;
                inventoryFlatDataToUpdate.ManufacturerName = inventoryFlatData.ManufacturerName;
                inventoryFlatDataToUpdate.AreaName = inventoryFlatData.AreaName;
                inventoryFlatDataToUpdate.CustomField1Value = inventoryFlatData.CustomField1Value;
                inventoryFlatDataToUpdate.CustomField1Label = inventoryFlatData.CustomField1Label;
                inventoryFlatDataToUpdate.CustomField2Value = inventoryFlatData.CustomField2Value;
                inventoryFlatDataToUpdate.CustomField2Label = inventoryFlatData.CustomField2Label;
                inventoryFlatDataToUpdate.CustomField3Value = inventoryFlatData.CustomField3Value;
                inventoryFlatDataToUpdate.CustomField3Label = inventoryFlatData.CustomField3Label;
                inventoryFlatDataToUpdate.CustomField4Value = inventoryFlatData.CustomField4Value;
                inventoryFlatDataToUpdate.CustomField4Label = inventoryFlatData.CustomField4Label;
                inventoryFlatDataToUpdate.InvoiceNumber = inventoryFlatData.InvoiceNumber;
                inventoryFlatDataToUpdate.InvoiceDate = inventoryFlatData.InvoiceDate;
                inventoryFlatDataToUpdate.Rejected = inventoryFlatData.Rejected;
                inventoryFlatDataToUpdate.RejectedNotes = inventoryFlatData.RejectedNotes;

                _context.InventoryFlatData.Update(inventoryFlatDataToUpdate);
                var result = _context.SaveChanges();

                return result == 1;
            }
            catch
            {
                throw;
            }
        }


        #endregion Update Functions

        #region Delete Functions

        public bool Delete(int processUid)
        {
            try
            {
                var inventoryData = (from inventoryFlatData in _context.InventoryFlatData
                                     where inventoryFlatData.ProcessUid == processUid
                                     select inventoryFlatData);

                _context.InventoryFlatData.RemoveRange(inventoryData);
                var result = _context.SaveChanges();
                return (result >= 0);
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

                var inventoryData = (from inventoryFlatData in _context.InventoryFlatData
                                     join processes in _context.Processes
                                       on inventoryFlatData.ProcessUid equals processes.ProcessUid
                                     where processes.Client.Trim().ToLower() == clientVal
                                        && processes.ProcessName.Trim().ToLower() == processNameVal
                                     select inventoryFlatData);

                _context.InventoryFlatData.RemoveRange(inventoryData);
                var result = _context.SaveChanges();
                return (result >= 0);
            }
            catch
            {
                throw;
            }
        }

        #endregion Delete Functions
    }
}
