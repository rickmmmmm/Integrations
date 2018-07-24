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

        public List<InventoryFlatDataModel> Select(int processTaskUid, int offset, int limit)
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
                            where inventoryFlat.ProcessTaskUid == processTaskUid
                            select new InventoryFlatDataModel
                            {
                                InventoryFlatDataUid = inventoryFlat.InventoryFlatDataUid,
                                ProcessTaskUid = inventoryFlat.ProcessTaskUid,
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

        public List<InventoryFlatDataModel> SelectLatest(string client, string processName, int offset, int limit)
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
                            join processTask in (
                                from processes in _context.Processes
                                join latestProcessTasks in _context.ProcessTasks
                                    on processes.ProcessUid equals latestProcessTasks.ProcessUid
                                where processes.Client.Trim().ToLower() == clientVal
                                   && processes.ProcessName.Trim().ToLower() == processNameVal
                                group latestProcessTasks by new { processes.Client, processes.ProcessName } into latestProcessTasksGroup
                                select new { ProcessTaskUid = latestProcessTasksGroup.Max(x => x.ProcessTaskUid) })
                                on inventoryFlat.ProcessTaskUid equals processTask.ProcessTaskUid
                            select new InventoryFlatDataModel
                            {
                                InventoryFlatDataUid = inventoryFlat.InventoryFlatDataUid,
                                ProcessTaskUid = inventoryFlat.ProcessTaskUid,
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
                                ProcessTaskUid = inventoryFlat.ProcessTaskUid,
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

        public InventoryFlatDataModel SelectByAssetId(int processTaskUid, string assetId)
        {
            try
            {
                var assetIdVal = (assetId ?? string.Empty).Trim().ToLower();

                var data = (from inventoryFlat in _context.InventoryFlatData
                            join processTasks in _context.ProcessTasks
                               on inventoryFlat.ProcessTaskUid equals processTasks.ProcessTaskUid
                            where processTasks.ProcessTaskUid == processTaskUid
                               && inventoryFlat.AssetId.Trim().ToLower() == assetIdVal
                            select new InventoryFlatDataModel
                            {
                                InventoryFlatDataUid = inventoryFlat.InventoryFlatDataUid,
                                ProcessTaskUid = inventoryFlat.ProcessTaskUid,
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

        public InventoryFlatDataModel SelectByTag(int processTaskUid, string tag)
        {
            try
            {
                var tagVal = (tag ?? string.Empty).Trim().ToLower();

                var data = (from inventoryFlat in _context.InventoryFlatData
                            join processTasks in _context.ProcessTasks
                               on inventoryFlat.ProcessTaskUid equals processTasks.ProcessTaskUid
                            where processTasks.ProcessTaskUid == processTaskUid
                               && inventoryFlat.AssetId.Trim().ToLower() == tagVal
                            select new InventoryFlatDataModel
                            {
                                InventoryFlatDataUid = inventoryFlat.InventoryFlatDataUid,
                                ProcessTaskUid = inventoryFlat.ProcessTaskUid,
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

        public int GetTotal(int processTaskUid)
        {
            try
            {
                return (from inventoryFlat in _context.InventoryFlatData
                        where inventoryFlat.ProcessTaskUid == processTaskUid
                        select inventoryFlat).Count();
            }
            catch
            {
                throw;
            }
        }

        public int GetTotalLatest(string client, string processName)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();

                return (from inventoryFlat in _context.InventoryFlatData
                        join processTask in (
                               from processes in _context.Processes
                               join latestProcessTasks in _context.ProcessTasks
                                   on processes.ProcessUid equals latestProcessTasks.ProcessUid
                               where processes.Client.Trim().ToLower() == clientVal
                                  && processes.ProcessName.Trim().ToLower() == processNameVal
                               group latestProcessTasks by new { processes.Client, processes.ProcessName } into latestProcessTasksGroup
                               select new { ProcessTaskUid = latestProcessTasksGroup.Max(x => x.ProcessTaskUid) })
                              on inventoryFlat.ProcessTaskUid equals processTask.ProcessTaskUid
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
                if (inventoryFlatData == null)
                {
                    return -1;
                }
                else
                {
                    var inventoryFlatToInsert = new InventoryFlatData()
                    {
                        InventoryFlatDataUid = 0,
                        ProcessTaskUid = inventoryFlatData.ProcessTaskUid,
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
            }
            catch
            {
                throw;
            }
        }

        public bool InsertRange(List<InventoryFlatDataModel> inventoryFlatData)
        {
            try
            {
                if (inventoryFlatData == null || inventoryFlatData.Count == 0)
                {
                    return true;
                }
                else
                {
                    foreach (var item in inventoryFlatData)
                    {
                        var inventoryFlatDataToInsert = new InventoryFlatData()
                        {
                            InventoryFlatDataUid = 0,
                            ProcessTaskUid = item.ProcessTaskUid,
                            RowId = item.RowId,
                            AssetId = item.AssetId,
                            Tag = item.Tag,
                            Serial = item.Serial,
                            SiteId = item.SiteId,
                            SiteName = item.SiteName,
                            Location = item.Location,
                            LocationType = item.LocationType,
                            Status = item.Status,
                            DepartmentName = item.DepartmentName,
                            DepartmentId = item.DepartmentId,
                            FundingSource = item.FundingSource,
                            FundingSourceDescription = item.FundingSourceDescription,
                            PurchasePrice = item.PurchasePrice,
                            PurchaseDate = item.PurchaseDate,
                            ExpirationDate = item.ExpirationDate,
                            InventoryNotes = item.InventoryNotes,
                            OrderNumber = item.OrderNumber,
                            LineNumber = item.LineNumber,
                            VendorName = item.VendorName,
                            VendorAccountNumber = item.VendorAccountNumber,
                            ParentTag = item.ParentTag,
                            ProductName = item.ProductName,
                            ProductDescription = item.ProductTypeDescription,
                            ProductByNumber = item.ProductByNumber,
                            ProductTypeName = item.ProductTypeName,
                            ProductTypeDescription = item.ProductTypeDescription,
                            ModelNumber = item.ModelNumber,
                            ManufacturerName = item.ManufacturerName,
                            AreaName = item.AreaName,
                            CustomField1Value = item.CustomField1Value,
                            CustomField1Label = item.CustomField1Label,
                            CustomField2Value = item.CustomField2Value,
                            CustomField2Label = item.CustomField2Label,
                            CustomField3Value = item.CustomField3Value,
                            CustomField3Label = item.CustomField3Label,
                            CustomField4Value = item.CustomField4Value,
                            CustomField4Label = item.CustomField4Label,
                            InvoiceNumber = item.InvoiceNumber,
                            InvoiceDate = item.InvoiceDate,
                            Rejected = item.Rejected,
                            RejectedNotes = item.RejectedNotes
                        };

                        _context.InventoryFlatData.Add(inventoryFlatDataToInsert);
                    }
                    var result = _context.SaveChanges();

                    return result == inventoryFlatData.Count();
                }
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
                if (inventoryFlatData == null)
                {
                    return true;
                }
                else
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
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateRange(List<InventoryFlatDataModel> inventoryFlatData)
        {
            try
            {
                if (inventoryFlatData == null || inventoryFlatData.Count == 0)
                {
                    return true;
                }
                else
                {
                    var inventoryFlatDataToUpdate = (from inventoryFlat in _context.InventoryFlatData
                                                     where (inventoryFlatData.Select(x => x.InventoryFlatDataUid)).Contains(inventoryFlat.InventoryFlatDataUid)
                                                     select inventoryFlat).ToList();

                    foreach (var item in inventoryFlatDataToUpdate)
                    {
                        var sourceInventoryFlatData = inventoryFlatData.Where(row => row.InventoryFlatDataUid == item.InventoryFlatDataUid).FirstOrDefault();

                        item.AssetId = sourceInventoryFlatData.AssetId;
                        item.Tag = sourceInventoryFlatData.Tag;
                        item.Serial = sourceInventoryFlatData.Serial;
                        item.SiteId = sourceInventoryFlatData.SiteId;
                        item.SiteName = sourceInventoryFlatData.SiteName;
                        item.Location = sourceInventoryFlatData.Location;
                        item.LocationType = sourceInventoryFlatData.LocationType;
                        item.Status = sourceInventoryFlatData.Status;
                        item.DepartmentName = sourceInventoryFlatData.DepartmentName;
                        item.DepartmentId = sourceInventoryFlatData.DepartmentId;
                        item.FundingSource = sourceInventoryFlatData.FundingSource;
                        item.FundingSourceDescription = sourceInventoryFlatData.FundingSourceDescription;
                        item.PurchasePrice = sourceInventoryFlatData.PurchasePrice;
                        item.PurchaseDate = sourceInventoryFlatData.PurchaseDate;
                        item.ExpirationDate = sourceInventoryFlatData.ExpirationDate;
                        item.InventoryNotes = sourceInventoryFlatData.InventoryNotes;
                        item.OrderNumber = sourceInventoryFlatData.OrderNumber;
                        item.LineNumber = sourceInventoryFlatData.LineNumber;
                        item.VendorName = sourceInventoryFlatData.VendorName;
                        item.VendorAccountNumber = sourceInventoryFlatData.VendorAccountNumber;
                        item.ParentTag = sourceInventoryFlatData.ParentTag;
                        item.ProductName = sourceInventoryFlatData.ProductName;
                        item.ProductDescription = sourceInventoryFlatData.ProductDescription;
                        item.ProductByNumber = sourceInventoryFlatData.ProductByNumber;
                        item.ProductTypeName = sourceInventoryFlatData.ProductTypeName;
                        item.ProductTypeDescription = sourceInventoryFlatData.ProductTypeDescription;
                        item.ModelNumber = sourceInventoryFlatData.ModelNumber;
                        item.ManufacturerName = sourceInventoryFlatData.ManufacturerName;
                        item.AreaName = sourceInventoryFlatData.AreaName;
                        item.CustomField1Value = sourceInventoryFlatData.CustomField1Value;
                        item.CustomField1Label = sourceInventoryFlatData.CustomField1Label;
                        item.CustomField2Value = sourceInventoryFlatData.CustomField2Value;
                        item.CustomField2Label = sourceInventoryFlatData.CustomField2Label;
                        item.CustomField3Value = sourceInventoryFlatData.CustomField3Value;
                        item.CustomField3Label = sourceInventoryFlatData.CustomField3Label;
                        item.CustomField4Value = sourceInventoryFlatData.CustomField4Value;
                        item.CustomField4Label = sourceInventoryFlatData.CustomField4Label;
                        item.InvoiceNumber = sourceInventoryFlatData.InvoiceNumber;
                        item.InvoiceDate = sourceInventoryFlatData.InvoiceDate;
                        item.Rejected = sourceInventoryFlatData.Rejected;
                        item.RejectedNotes = sourceInventoryFlatData.RejectedNotes;
                    }

                    _context.InventoryFlatData.UpdateRange(inventoryFlatDataToUpdate);
                    var result = _context.SaveChanges();

                    return result == inventoryFlatData.Count;
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion Update Functions

        #region Delete Functions

        public bool Delete(int inventoryFlatDataUid)
        {
            try
            {
                var inventoryData = (from inventoryFlatData in _context.InventoryFlatData
                                     where inventoryFlatData.InventoryFlatDataUid == inventoryFlatDataUid
                                     select inventoryFlatData);

                _context.InventoryFlatData.RemoveRange(inventoryData);
                var result = _context.SaveChanges();
                return (result > 0);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteAll(int processTaskUid)
        {
            try
            {
                var inventoryData = (from inventoryFlatData in _context.InventoryFlatData
                                     where inventoryFlatData.ProcessTaskUid == processTaskUid
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

        public bool DeleteAll(string client, string processName)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();

                var inventoryData = (from inventoryFlatData in _context.InventoryFlatData
                                     join processTasks in _context.ProcessTasks
                                       on inventoryFlatData.ProcessTaskUid equals processTasks.ProcessTaskUid
                                     join processes in _context.Processes
                                       on processTasks.ProcessUid equals processes.ProcessUid
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
