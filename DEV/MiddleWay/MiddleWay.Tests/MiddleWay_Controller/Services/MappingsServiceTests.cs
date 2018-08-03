using MiddleWay_Controller.Services;
using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay_Controller;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using MiddleWay_Utilities;
using Moq;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace MiddleWay.Tests.MiddleWay_Controller.Services
{
    public class MappingsServiceTests
    {
        #region Constructor

        private readonly ITestOutputHelper _outputHelper;

        public MappingsServiceTests(ITestOutputHelper output)
        {
            _outputHelper = output;
        }

        #endregion Constructor

        #region Map Tests

        [Fact]
        public void Map_dynamicToEtlInventory()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var mappingsRepository = SetupMockMappingsRepository();
            var mappings = GetMappingsData(ProcessSteps.Stage);
            var transformationService = SetupTransformationsService();

            mappingsRepository.Setup(x => x.SelectMappings(clientConfiguration.Object.Client, clientConfiguration.Object.ProcessName, ProcessSteps.Stage)).Returns(mappings.ToList());

            var _mappingsService = new MappingsService(mappingsRepository.Object, clientConfiguration.Object, transformationService);

            #region dynamicData
            var dynamicData = new ExpandoObject();
            var data = dynamicData as IDictionary<string, object>;
            data.TryAdd("Uid", 1);
            data.TryAdd("PTUid", 1);
            data.TryAdd("Row", 1);
            data.TryAdd("Asset", "A0");
            data.TryAdd("TagNumber", "Tag001");
            data.TryAdd("SerialNumber", "1234");
            data.TryAdd("Site", "001");
            data.TryAdd("Site_Name", "001 - Site");
            data.TryAdd("LocationId", "");
            data.TryAdd("LocationName", "Room: Maintenance Room");
            data.TryAdd("LocationType", "undefined");
            data.TryAdd("StatusId", "");
            data.TryAdd("Status", "Available");
            data.TryAdd("Department", "None");
            data.TryAdd("DeptID", "0");
            data.TryAdd("Funding", "None");
            data.TryAdd("FundingDesc", "");
            data.TryAdd("Price", (decimal?)10.00);
            data.TryAdd("Purchased", "");
            data.TryAdd("Expiration", "");
            data.TryAdd("Notes", "New Broom");
            data.TryAdd("PO", "");
            data.TryAdd("Vendor", "Broom's R Us");
            data.TryAdd("AccountNumber", "");
            data.TryAdd("Parent", "");
            data.TryAdd("Product", "Broom");
            data.TryAdd("ProductDesc", "Wooden Handle Broom");
            data.TryAdd("ProductNumber", "001");
            data.TryAdd("ProductType", "Broom");
            data.TryAdd("ProductTypeDesc", "Broom");
            data.TryAdd("Model", "");
            data.TryAdd("Manufacturer", "Broom's R Us");
            data.TryAdd("Area", "Floor");
            data.TryAdd("CustomValue1", "");
            data.TryAdd("CustomLabel1", "");
            data.TryAdd("CustomValue2", "");
            data.TryAdd("CustomLabel2", "");
            data.TryAdd("CustomValue3", "");
            data.TryAdd("CustomLabel3", "");
            data.TryAdd("CustomValue4", "");
            data.TryAdd("CustomLabel4", "");
            data.TryAdd("Invoice", "");
            data.TryAdd("InvoiceDate", "");
            #endregion dynamicData

            EtlInventoryModel mappedData = _mappingsService.Map<ExpandoObject, EtlInventoryModel>(dynamicData, ProcessSteps.Stage);

            Assert.NotNull(mappedData);
            if (mappedData != null)
            {
                _outputHelper.WriteLine(Utilities.ToStringObject(mappedData));
                Assert.Equal(1, mappedData._ETL_InventoryUid);
                Assert.Equal(1, mappedData.ProcessTaskUid);
                Assert.Equal(1, mappedData.RowId);
                Assert.Equal(0, mappedData.InventoryUid);
                Assert.Equal("A0", mappedData.AssetId);
                Assert.Equal("Tag001", mappedData.Tag);
                Assert.Equal("1234", mappedData.Serial);
                Assert.Equal(0, mappedData.InventoryTypeUid);
                Assert.Null(mappedData.InventoryTypeName);
                Assert.Equal(0, mappedData.ItemUid);
                Assert.Equal("Broom", mappedData.ProductName);
                Assert.Equal("Wooden Handle Broom", mappedData.ProductDescription);
                Assert.Equal("001", mappedData.ProductByNumber);
                Assert.Equal(0, mappedData.ItemTypeUid);
                Assert.Equal("Broom", mappedData.ProductTypeName);
                Assert.Equal("Broom", mappedData.ProductTypeDescription);
                Assert.Equal("", mappedData.ModelNumber);
                Assert.Equal(0, mappedData.ManufacturerUid);
                Assert.Equal("Broom's R Us", mappedData.ManufacturerName);
                Assert.Equal(0, mappedData.AreaUid);
                Assert.Equal("Floor", mappedData.AreaName);
                Assert.Equal(0, mappedData.SiteUid);
                Assert.Equal("001", mappedData.SiteId);
                Assert.Equal("001 - Site", mappedData.SiteName);
                Assert.Equal(0, mappedData.EntityUid);
                Assert.Equal("", mappedData.EntityId);
                Assert.Equal("Room: Maintenance Room", mappedData.EntityName);
                Assert.Equal(0, mappedData.EntityTypeUid);
                Assert.Equal("undefined", mappedData.EntityTypeName);
                Assert.Equal(0, mappedData.StatusId);
                Assert.Equal("Available", mappedData.Status);
                Assert.Equal(0, mappedData.TechDepartmentUid);
                Assert.Equal("None", mappedData.DepartmentName);
                Assert.Equal("0", mappedData.DepartmentId);
                Assert.Equal(0, mappedData.FundingSourceUid);
                Assert.Equal("None", mappedData.FundingSource);
                Assert.Equal("", mappedData.FundingSourceDescription);
                Assert.Equal((decimal?)10.00, mappedData.PurchasePrice);
                Assert.Null(mappedData.PurchaseDate);
                Assert.Null(mappedData.ExpirationDate);
                Assert.Equal("New Broom", mappedData.InventoryNotes);
                Assert.Null(mappedData.ParentInventoryUid);
                Assert.Equal("", mappedData.ParentTag);
                Assert.Equal(0, mappedData.InventorySourceUid);
                Assert.Null(mappedData.InventorySourceName);
                Assert.Equal(0, mappedData.PurchaseUid);
                Assert.Equal("", mappedData.OrderNumber);
                Assert.Equal(0, mappedData.PurchaseItemDetailUid);
                Assert.Equal(0, mappedData.LineNumber);
                Assert.Null(mappedData.AccountCode);
                Assert.Equal(0, mappedData.VendorUid);
                Assert.Equal("Broom's R Us", mappedData.VendorName);
                Assert.Equal("", mappedData.VendorAccountNumber);
                Assert.Equal(0, mappedData.PurchaseItemShipmentUid);
                Assert.Equal("", mappedData.InvoiceNumber);
                Assert.Null(mappedData.InvoiceDate);
                Assert.Null(mappedData.InventoryExt1Uid);
                Assert.Null(mappedData.InventoryMeta1Uid);
                Assert.Equal("", mappedData.CustomField1Label);
                Assert.Equal("", mappedData.CustomField1Value);
                Assert.Null(mappedData.InventoryExt2Uid);
                Assert.Null(mappedData.InventoryMeta2Uid);
                Assert.Equal("", mappedData.CustomField2Label);
                Assert.Equal("", mappedData.CustomField2Value);
                Assert.Null(mappedData.InventoryExt3Uid);
                Assert.Null(mappedData.InventoryMeta3Uid);
                Assert.Equal("", mappedData.CustomField3Label);
                Assert.Equal("", mappedData.CustomField3Value);
                Assert.Null(mappedData.InventoryExt4Uid);
                Assert.Null(mappedData.InventoryMeta4Uid);
                Assert.Equal("", mappedData.CustomField4Label);
                Assert.Equal("", mappedData.CustomField4Value);
            }
        }

        [Fact]
        public void Map_dynamicInvalidToEtlInventory()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var mappingsRepository = SetupMockMappingsRepository();
            var mappings = GetMappingsData(ProcessSteps.Stage);
            var transformationService = SetupTransformationsService();

            mappingsRepository.Setup(x => x.SelectMappings(clientConfiguration.Object.Client, clientConfiguration.Object.ProcessName, ProcessSteps.Stage)).Returns(mappings.ToList());

            var _mappingsService = new MappingsService(mappingsRepository.Object, clientConfiguration.Object, transformationService);

            #region dynamicData
            var dynamicData = new ExpandoObject();
            var data = dynamicData as IDictionary<string, object>;
            data.TryAdd("Uid", 1);
            data.TryAdd("PTUid", 1);
            data.TryAdd("Row", 1);
            data.TryAdd("Asset", "A0");
            data.TryAdd("TagNumber", "Tag001");
            data.TryAdd("SerialNumber", "1234");
            data.TryAdd("Site", "001");
            data.TryAdd("Site_Name", "001 - Site");
            data.TryAdd("LocationId", "");
            data.TryAdd("LocationName", "Room: Maintenance Room");
            data.TryAdd("LocationType", "undefined");
            data.TryAdd("StatusId", "");
            data.TryAdd("Status", "Available");
            data.TryAdd("Department", "None");
            data.TryAdd("DeptID", "0");
            data.TryAdd("Funding", "None");
            data.TryAdd("FundingDesc", "");
            data.TryAdd("Price", "not a price"); //(decimal?)10.00);
            data.TryAdd("Purchased", "");
            data.TryAdd("Expiration", "");
            data.TryAdd("Notes", "New Broom");
            data.TryAdd("PO", "");
            data.TryAdd("Line", null);
            data.TryAdd("Vendor", "Broom's R Us");
            data.TryAdd("AccountNumber", "");
            data.TryAdd("Parent", "");
            data.TryAdd("Product", "Broom");
            data.TryAdd("ProductDesc", "Wooden Handle Broom");
            data.TryAdd("ProductNumber", "001");
            data.TryAdd("ProductType", "Broom");
            data.TryAdd("ProductTypeDesc", "Broom");
            data.TryAdd("Model", "");
            data.TryAdd("Manufacturer", "Broom's R Us");
            data.TryAdd("Area", "Floor");
            data.TryAdd("CustomValue1", "");
            data.TryAdd("CustomLabel1", "");
            data.TryAdd("CustomValue2", "");
            data.TryAdd("CustomLabel2", "");
            data.TryAdd("CustomValue3", "");
            data.TryAdd("CustomLabel3", "");
            data.TryAdd("CustomValue4", "");
            data.TryAdd("CustomLabel4", "");
            data.TryAdd("Invoice", "");
            data.TryAdd("InvoiceDate", "");
            #endregion dynamicData

            EtlInventoryModel mappedData = _mappingsService.Map<ExpandoObject, EtlInventoryModel>(dynamicData, ProcessSteps.Stage);

            Assert.NotNull(mappedData);
            if (mappedData != null)
            {
                _outputHelper.WriteLine(Utilities.ToStringObject(mappedData));
                Assert.Equal(1, mappedData._ETL_InventoryUid);
                Assert.Equal(1, mappedData.ProcessTaskUid);
                Assert.Equal(1, mappedData.RowId);
                Assert.Equal(0, mappedData.InventoryUid);
                Assert.Equal("A0", mappedData.AssetId);
                Assert.Equal("Tag001", mappedData.Tag);
                Assert.Equal("1234", mappedData.Serial);
                Assert.Equal(0, mappedData.InventoryTypeUid);
                Assert.Null(mappedData.InventoryTypeName);
                Assert.Equal(0, mappedData.ItemUid);
                Assert.Equal("Broom", mappedData.ProductName);
                Assert.Equal("Wooden Handle Broom", mappedData.ProductDescription);
                Assert.Equal("001", mappedData.ProductByNumber);
                Assert.Equal(0, mappedData.ItemTypeUid);
                Assert.Equal("Broom", mappedData.ProductTypeName);
                Assert.Equal("Broom", mappedData.ProductTypeDescription);
                Assert.Equal("", mappedData.ModelNumber);
                Assert.Equal(0, mappedData.ManufacturerUid);
                Assert.Equal("Broom's R Us", mappedData.ManufacturerName);
                Assert.Equal(0, mappedData.AreaUid);
                Assert.Equal("Floor", mappedData.AreaName);
                Assert.Equal(0, mappedData.SiteUid);
                Assert.Equal("001", mappedData.SiteId);
                Assert.Equal("001 - Site", mappedData.SiteName);
                Assert.Equal(0, mappedData.EntityUid);
                Assert.Equal("", mappedData.EntityId);
                Assert.Equal("Room: Maintenance Room", mappedData.EntityName);
                Assert.Equal(0, mappedData.EntityTypeUid);
                Assert.Equal("undefined", mappedData.EntityTypeName);
                Assert.Equal(0, mappedData.StatusId);
                Assert.Equal("Available", mappedData.Status);
                Assert.Equal(0, mappedData.TechDepartmentUid);
                Assert.Equal("None", mappedData.DepartmentName);
                Assert.Equal("0", mappedData.DepartmentId);
                Assert.Equal(0, mappedData.FundingSourceUid);
                Assert.Equal("None", mappedData.FundingSource);
                Assert.Equal("", mappedData.FundingSourceDescription);
                Assert.Null(mappedData.PurchasePrice);
                Assert.Null(mappedData.PurchaseDate);
                Assert.Null(mappedData.ExpirationDate);
                Assert.Equal("New Broom", mappedData.InventoryNotes);
                Assert.Null(mappedData.ParentInventoryUid);
                Assert.Equal("", mappedData.ParentTag);
                Assert.Equal(0, mappedData.InventorySourceUid);
                Assert.Null(mappedData.InventorySourceName);
                Assert.Equal(0, mappedData.PurchaseUid);
                Assert.Equal("", mappedData.OrderNumber);
                Assert.Equal(0, mappedData.PurchaseItemDetailUid);
                Assert.Equal(0, mappedData.LineNumber);
                Assert.Null(mappedData.AccountCode);
                Assert.Equal(0, mappedData.VendorUid);
                Assert.Equal("Broom's R Us", mappedData.VendorName);
                Assert.Equal("", mappedData.VendorAccountNumber);
                Assert.Equal(0, mappedData.PurchaseItemShipmentUid);
                Assert.Equal("", mappedData.InvoiceNumber);
                Assert.Null(mappedData.InvoiceDate);
                Assert.Null(mappedData.InventoryExt1Uid);
                Assert.Null(mappedData.InventoryMeta1Uid);
                Assert.Equal("", mappedData.CustomField1Label);
                Assert.Equal("", mappedData.CustomField1Value);
                Assert.Null(mappedData.InventoryExt2Uid);
                Assert.Null(mappedData.InventoryMeta2Uid);
                Assert.Equal("", mappedData.CustomField2Label);
                Assert.Equal("", mappedData.CustomField2Value);
                Assert.Null(mappedData.InventoryExt3Uid);
                Assert.Null(mappedData.InventoryMeta3Uid);
                Assert.Equal("", mappedData.CustomField3Label);
                Assert.Equal("", mappedData.CustomField3Value);
                Assert.Null(mappedData.InventoryExt4Uid);
                Assert.Null(mappedData.InventoryMeta4Uid);
                Assert.Equal("", mappedData.CustomField4Label);
                Assert.Equal("", mappedData.CustomField4Value);
            }
        }


        [Fact]
        public void Map_InventoryFlatToDynamic()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var mappingsRepository = SetupMockMappingsRepository();
            var mappings = GetMappingsData(ProcessSteps.Ingest);
            var transformationService = SetupTransformationsService();

            mappingsRepository.Setup(x => x.SelectMappings(clientConfiguration.Object.Client, clientConfiguration.Object.ProcessName, ProcessSteps.Ingest)).Returns(mappings.ToList());

            var inventoryFlat = new InventoryFlatDataModel
            {
                InventoryFlatDataUid = 1,
                ProcessTaskUid = 1,
                RowId = 1,
                AssetId = "A0",
                Tag = "Tag001",
                Serial = "1234",
                ProductNumber = "",
                ProductName = "Broom",
                ProductDescription = "Wooden Handle Broom",
                ProductByNumber = "001",
                ProductTypeName = "Broom",
                ProductTypeDescription = "Broom",
                ModelNumber = "",
                ManufacturerName = "Broom's R Us",
                AreaName = "Floor",
                SiteId = "001",
                SiteName = "001 - Site",
                LocationId = "",
                LocationName = "Room: Maintenance Room",
                LocationTypeName = "Room: Maintenance Room",
                Status = "Available",
                DepartmentName = "None",
                DepartmentId = "0",
                FundingSource = "None",
                FundingSourceDescription = "",
                PurchasePrice = "10.00",
                PurchaseDate = "",
                ExpirationDate = "",
                InventoryNotes = "New Broom",
                ParentTag = "",
                ContainerNumber = "",
                OrderNumber = "",
                PurchaseSiteId = "District",
                PurchaseSiteName = "",
                VendorName = "Broom's R Us",
                VendorAccountNumber = "",
                LineNumber = "0",
                AccountCode= "",
                SiteAddedSiteId = "001",
                SiteAddedSiteName = "",
                ShippedToSiteId = "001",
                ShippedToSiteName = "",
                InvoiceNumber = "",
                InvoiceDate = "",
                CustomField1Value = "",
                CustomField1Label = "",
                CustomField2Value = "",
                CustomField2Label = "",
                CustomField3Value = "",
                CustomField3Label = "",
                CustomField4Value = "",
                CustomField4Label = ""
            };

            var _mappingsService = new MappingsService(mappingsRepository.Object, clientConfiguration.Object, transformationService);

            var mappedData = _mappingsService.Map<InventoryFlatDataModel, ExpandoObject>(inventoryFlat, ProcessSteps.Ingest);

            Assert.NotNull(mappedData);
            if (mappedData != null)
            {
                var data = mappedData as IDictionary<string, object>;
                Assert.Equal(1, data["_ETL_InventoryUid"]);
                Assert.Equal(1, data["ProcessTaskUid"]);
                Assert.Equal(1, data["RowId"]);
                Assert.Equal("A0", data["AssetId"]);
                Assert.Equal("Tag001", data["Tag"]);
                Assert.Equal("1234", data["Serial"]);
                Assert.Equal("", data["ProductNumber"]);
                Assert.Equal("Broom", data["ProductName"]);
                Assert.Equal("Wooden Handle Broom", data["ProductDescription"]);
                Assert.Equal("001", data["ProductByNumber"]);
                Assert.Equal("Broom", data["ProductTypeName"]);
                Assert.Equal("Broom", data["ProductTypeDescription"]);
                Assert.Equal("", data["ModelNumber"]);
                Assert.Equal("Broom's R Us", data["ManufacturerName"]);
                Assert.Equal("Floor", data["AreaName"]);
                Assert.Equal("001", data["SiteId"]);
                Assert.Equal("001 - Site", data["SiteName"]);
                Assert.Equal("Room: Maintenance Room", data["EntityName"]);
                Assert.Equal("Room: Maintenance Room", data["EntityTypeName"]);
                Assert.Equal("Available", data["Status"]);
                Assert.Equal("None", data["DepartmentName"]);
                Assert.Equal("0", data["DepartmentId"]);
                Assert.Equal("None", data["FundingSource"]);
                Assert.Equal("", data["FundingSourceDescription"]);
                Assert.Equal("10.00", data["PurchasePrice"]);
                Assert.Equal("", data["PurchaseDate"]);
                Assert.Equal("", data["ExpirationDate"]);
                Assert.Equal("New Broom", data["InventoryNotes"]);
                Assert.Equal("", data["ParentTag"]);
                Assert.Equal("", data["OrderNumber"]);
                Assert.Equal("Broom's R Us", data["VendorName"]);
                Assert.Equal("", data["VendorAccountNumber"]);
                Assert.Equal("", data["InvoiceNumber"]);
                Assert.Equal("", data["InvoiceDate"]);
                Assert.Equal("", data["CustomField1Label"]);
                Assert.Equal("", data["CustomField1Value"]);
                Assert.Equal("", data["CustomField2Label"]);
                Assert.Equal("", data["CustomField2Value"]);
                Assert.Equal("", data["CustomField3Label"]);
                Assert.Equal("", data["CustomField3Value"]);
                Assert.Equal("", data["CustomField4Label"]);
                Assert.Equal("", data["CustomField4Value"]);
            }
        }

        [Fact]
        public void Map_InventoryFlatToEtlInventory()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var mappingsRepository = SetupMockMappingsRepository();
            var mappings = GetMappingsData(ProcessSteps.Ingest);
            var transformationService = SetupTransformationsService();

            mappingsRepository.Setup(x => x.SelectMappings(clientConfiguration.Object.Client, clientConfiguration.Object.ProcessName, ProcessSteps.Ingest)).Returns(mappings.ToList());

            var inventoryFlat = new InventoryFlatDataModel
            {
                InventoryFlatDataUid = 1,
                ProcessTaskUid = 1,
                RowId = 1,
                AssetId = "A0",
                Tag = "Tag001",
                Serial = "1234",
                ProductNumber = "",
                ProductName = "Broom",
                ProductDescription = "Wooden Handle Broom",
                ProductByNumber = "001",
                ProductTypeName = "Broom",
                ProductTypeDescription = "Broom",
                ModelNumber = "",
                ManufacturerName = "Broom's R Us",
                AreaName = "Floor",
                SiteId = "001",
                SiteName = "001 - Site",
                LocationId = "",
                LocationName = "Room: Maintenance Room",
                LocationTypeName = "Room: Maintenance Room",
                Status = "Available",
                DepartmentName = "None",
                DepartmentId = "0",
                FundingSource = "None",
                FundingSourceDescription = "",
                PurchasePrice = "10.00",
                PurchaseDate = "",
                ExpirationDate = "",
                InventoryNotes = "New Broom",
                ParentTag = "",
                ContainerNumber = "",
                OrderNumber = "",
                PurchaseSiteId = "District",
                PurchaseSiteName = "",
                VendorName = "Broom's R Us",
                VendorAccountNumber = "",
                LineNumber = "0",
                AccountCode = "",
                SiteAddedSiteId = "001",
                SiteAddedSiteName = "",
                ShippedToSiteId = "001",
                ShippedToSiteName = "",
                InvoiceNumber = "",
                InvoiceDate = "",
                CustomField1Value = "",
                CustomField1Label = "",
                CustomField2Value = "",
                CustomField2Label = "",
                CustomField3Value = "",
                CustomField3Label = "",
                CustomField4Value = "",
                CustomField4Label = ""
            };

            var _mappingsService = new MappingsService(mappingsRepository.Object, clientConfiguration.Object, transformationService);

            var data = _mappingsService.Map<InventoryFlatDataModel, EtlInventoryModel>(inventoryFlat, ProcessSteps.Ingest);

            Assert.NotNull(data);
            if (data != null)
            {
                //var data = mappedData as IDictionary<string, object>;
                Assert.Equal(1, data._ETL_InventoryUid);
                Assert.Equal(1, data.ProcessTaskUid);
                Assert.Equal(1, data.RowId);
                Assert.Equal("A0", data.AssetId);
                Assert.Equal("Tag001", data.Tag);
                Assert.Equal("1234", data.Serial);
                Assert.Equal("Broom", data.ProductName);
                Assert.Equal("Wooden Handle Broom", data.ProductDescription);
                Assert.Equal("001", data.ProductByNumber);
                Assert.Equal("Broom", data.ProductTypeName);
                Assert.Equal("Broom", data.ProductTypeDescription);
                Assert.Equal("", data.ModelNumber);
                Assert.Equal("Broom's R Us", data.ManufacturerName);
                Assert.Equal("Floor", data.AreaName);
                Assert.Equal("001", data.SiteId);
                Assert.Equal("001 - Site", data.SiteName);
                Assert.Equal("Room: Maintenance Room", data.EntityName);
                Assert.Equal("Room: Maintenance Room", data.EntityTypeName);
                Assert.Equal("Available", data.Status);
                Assert.Equal("None", data.DepartmentName);
                Assert.Equal("0", data.DepartmentId);
                Assert.Equal("None", data.FundingSource);
                Assert.Equal("", data.FundingSourceDescription);
                Assert.Equal((decimal?)10.00, data.PurchasePrice);
                Assert.Null(data.PurchaseDate);
                Assert.Null(data.ExpirationDate);
                Assert.Equal("New Broom", data.InventoryNotes);
                Assert.Equal("", data.ParentTag);
                Assert.Equal("", data.OrderNumber);
                Assert.Equal("Broom's R Us", data.VendorName);
                Assert.Equal("", data.VendorAccountNumber);
                Assert.Equal("", data.InvoiceNumber);
                Assert.Null(data.InvoiceDate);
                Assert.Equal("", data.CustomField1Label);
                Assert.Equal("", data.CustomField1Value);
                Assert.Equal("", data.CustomField2Label);
                Assert.Equal("", data.CustomField2Value);
                Assert.Equal("", data.CustomField3Label);
                Assert.Equal("", data.CustomField3Value);
                Assert.Equal("", data.CustomField4Label);
                Assert.Equal("", data.CustomField4Value);
            }

        }

        //[Fact]
        //public void Map_()
        //{
        //    //var mappedData = _mappingsService.Map<ExpandoObject, EtlInventoryModel>(transformedData);
        //    //List<U> Map<T, U>(List<T> item) where U : new();
        //    Assert.False(true);
        //}

        #endregion Map Tests

        #region Setup MockObjects

        protected Mock<IClientConfiguration> SetupMockClientConfiguration()
        {
            var mockClientConfiguration = new Mock<IClientConfiguration>();
            mockClientConfiguration.Setup(x => x.Client).Returns("Test");
            mockClientConfiguration.Setup(x => x.ProcessName).Returns("Module");

            return mockClientConfiguration;
        }

        protected Mock<ITransformationsRepository> SetupMockTransformationsRepository()
        {
            var mockTransformationsRepository = new Mock<ITransformationsRepository>();
            return mockTransformationsRepository;
        }
        protected Mock<ITransformationLookupService> SetupMockTransformationLookupService()
        {
            return new Mock<ITransformationLookupService>();
        }

        protected TransformationsService SetupTransformationsService()
        {
            var transformationRepository = SetupMockTransformationsRepository();
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationLookupService = SetupMockTransformationLookupService();

            var transformationsService = new TransformationsService(transformationRepository.Object, clientConfiguration.Object, transformationLookupService.Object);
            return transformationsService;
        }

        protected Mock<IMappingsRepository> SetupMockMappingsRepository()
        {
            var mockMappingsRepository = new Mock<IMappingsRepository>();
            return mockMappingsRepository;
        }

        protected IQueryable<MappingsModel> GetMappingsData(ProcessSteps stepName)
        {
            if (stepName == ProcessSteps.Ingest)
            {
                return new List<MappingsModel>
                {
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "InventoryFlatDataUid", DestinationColumn = "_ETL_InventoryUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "ProcessTaskUid", DestinationColumn = "ProcessTaskUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "RowId", DestinationColumn = "RowId", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "AssetId", DestinationColumn = "AssetId", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "Tag", DestinationColumn = "Tag", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "Serial", DestinationColumn = "Serial", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "ProductNumber", DestinationColumn = "ProductNumber", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "ProductName", DestinationColumn = "ProductName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "ProductDescription", DestinationColumn = "ProductDescription", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "ProductByNumber", DestinationColumn = "ProductByNumber", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "ProductTypeName", DestinationColumn = "ProductTypeName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "ProductTypeDescription", DestinationColumn = "ProductTypeDescription", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "ModelNumber", DestinationColumn = "ModelNumber", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "ManufacturerName", DestinationColumn = "ManufacturerName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "AreaName", DestinationColumn = "AreaName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "SiteId", DestinationColumn = "SiteId", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "SiteName", DestinationColumn = "SiteName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "LocationName", DestinationColumn = "EntityName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "LocationTypeName", DestinationColumn = "EntityTypeName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "Status", DestinationColumn = "Status", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "DepartmentName", DestinationColumn = "DepartmentName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "DepartmentId", DestinationColumn = "DepartmentId", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "FundingSource", DestinationColumn = "FundingSource", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "FundingSourceDescription", DestinationColumn = "FundingSourceDescription", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "PurchasePrice", DestinationColumn = "PurchasePrice", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "PurchaseDate", DestinationColumn = "PurchaseDate", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "ExpirationDate", DestinationColumn = "ExpirationDate", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "InventoryNotes", DestinationColumn = "InventoryNotes", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "ParentTag", DestinationColumn = "ParentTag", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "OrderNumber", DestinationColumn = "OrderNumber", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "VendorName", DestinationColumn = "VendorName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "VendorAccountNumber", DestinationColumn = "VendorAccountNumber", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "InvoiceNumber", DestinationColumn = "InvoiceNumber", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "InvoiceDate", DestinationColumn = "InvoiceDate", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "CustomField1Label", DestinationColumn = "CustomField1Label", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "CustomField1Value", DestinationColumn = "CustomField1Value", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "CustomField2Label", DestinationColumn = "CustomField2Label", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "CustomField2Value", DestinationColumn = "CustomField2Value", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "CustomField3Label", DestinationColumn = "CustomField3Label", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "CustomField3Value", DestinationColumn = "CustomField3Value", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "CustomField4Label", DestinationColumn = "CustomField4Label", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), SourceColumn = "CustomField4Value", DestinationColumn = "CustomField4Value", Enabled = true }
                }.AsQueryable();
            }
            else
            {
                return new List<MappingsModel>
                {
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Uid", DestinationColumn = "_ETL_InventoryUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "PTUid", DestinationColumn = "ProcessTaskUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Row", DestinationColumn = "RowId", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Iuid", DestinationColumn = "InventoryUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Asset", DestinationColumn = "AssetId", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "TagNumber", DestinationColumn = "Tag", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "SerialNumber", DestinationColumn = "Serial", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "", DestinationColumn = "InventoryTypeUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "", DestinationColumn = "InventoryTypeName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "", DestinationColumn = "ItemUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Product", DestinationColumn = "ProductName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "ProductDesc", DestinationColumn = "ProductDescription", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "ProductNumber", DestinationColumn = "ProductByNumber", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "ProductTypeUid", DestinationColumn = "ItemTypeUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "ProductType", DestinationColumn = "ProductTypeName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "ProductTypeDesc", DestinationColumn = "ProductTypeDescription", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Model", DestinationColumn = "ModelNumber", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "ManUid", DestinationColumn = "ManufacturerUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Manufacturer", DestinationColumn = "ManufacturerName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "", DestinationColumn = "AreaUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Area", DestinationColumn = "AreaName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "SiteId", DestinationColumn = "SiteUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Site", DestinationColumn = "SiteId", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Site_Name", DestinationColumn = "SiteName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "EUID", DestinationColumn = "EntityUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "LocationId", DestinationColumn = "EntityId", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "LocationName", DestinationColumn = "EntityName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "ETUID", DestinationColumn = "EntityTypeUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "LocationType", DestinationColumn = "EntityTypeName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "", DestinationColumn = "StatusId", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Status", DestinationColumn = "Status", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "", DestinationColumn = "TechDepartmentUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Department", DestinationColumn = "DepartmentName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "DeptID", DestinationColumn = "DepartmentId", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "", DestinationColumn = "FundingSourceUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Funding", DestinationColumn = "FundingSource", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "FundingDesc", DestinationColumn = "FundingSourceDescription", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Price", DestinationColumn = "PurchasePrice", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Purchased", DestinationColumn = "PurchaseDate", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Expiration", DestinationColumn = "ExpirationDate", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Notes", DestinationColumn = "InventoryNotes", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "PIUid", DestinationColumn = "ParentInventoryUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Parent", DestinationColumn = "ParentTag", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "ISUid", DestinationColumn = "InventorySourceUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "SourceName", DestinationColumn = "InventorySourceName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "PurchaseUid", DestinationColumn = "PurchaseUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "PO", DestinationColumn = "OrderNumber", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "DetailUid", DestinationColumn = "PurchaseItemDetailUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Line", DestinationColumn = "LineNumber", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "AccountCode", DestinationColumn = "AccountCode", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Vuid", DestinationColumn = "VendorUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Vendor", DestinationColumn = "VendorName", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "AccountNumber", DestinationColumn = "VendorAccountNumber", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "", DestinationColumn = "PurchaseItemShipmentUid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "Invoice", DestinationColumn = "InvoiceNumber", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "InvoiceDate", DestinationColumn = "InvoiceDate", Enabled = true },
                    //new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomExt1", DestinationColumn = "InventoryExt1Uid", Enabled = true },
                    //new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomMeta1", DestinationColumn = "InventoryMeta1Uid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomLabel1", DestinationColumn = "CustomField1Label", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomValue1", DestinationColumn = "CustomField1Value", Enabled = true },
                    //new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomExt2", DestinationColumn = "InventoryExt2Uid", Enabled = true },
                    //new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomMeta2", DestinationColumn = "InventoryMeta2Uid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomLabel2", DestinationColumn = "CustomField2Label", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomValue2", DestinationColumn = "CustomField2Value", Enabled = true },
                    //new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomExt3", DestinationColumn = "InventoryExt3Uid", Enabled = true },
                    //new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomMeta3", DestinationColumn = "InventoryMeta3Uid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomLabel3", DestinationColumn = "CustomField3Label", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomValue3", DestinationColumn = "CustomField3Value", Enabled = true },
                    //new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomExt4", DestinationColumn = "InventoryExt4Uid", Enabled = true },
                    //new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomMeta4", DestinationColumn = "InventoryMeta4Uid", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomLabel4", DestinationColumn = "CustomField4Label", Enabled = true },
                    new MappingsModel { MappingsUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Stage.ToString(), SourceColumn = "CustomValue4", DestinationColumn = "CustomField4Value", Enabled = true }
                }.AsQueryable();
            }
        }

        #endregion Setup MockObjects
    }
}