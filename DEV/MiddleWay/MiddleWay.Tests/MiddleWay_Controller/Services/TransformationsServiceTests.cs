using Microsoft.EntityFrameworkCore;
using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_Controller.Repositories;
using MiddleWay_Controller.Services;
using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using MiddleWay_Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace MiddleWay.Tests.MiddleWay_Controller.Services
{
    public class TransformationsServiceTests
    {
        #region Constructor

        private readonly ITestOutputHelper _outputHelper;

        public TransformationsServiceTests(ITestOutputHelper output)
        {
            _outputHelper = output;
        }

        #endregion Constructor

        [Fact]
        public void HasTransformations_Valid()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            transformationsRepository.Setup(x => x.HasTransformations(clientConfiguration.Object.Client, clientConfiguration.Object.ProcessName, ProcessSteps.Ingest)).Returns(true);
            var transformationLookupService = SetupMockTransformationLookupService();

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.HasTransformations(ProcessSteps.Ingest);

            Assert.True(result);
        }
        [Fact]
        public void HasTransformations_Invalid()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            transformationsRepository.Setup(x => x.HasTransformations(clientConfiguration.Object.Client, clientConfiguration.Object.ProcessName, ProcessSteps.Ingest)).Returns(true);
            var transformationLookupService = SetupMockTransformationLookupService();

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.HasTransformations(ProcessSteps.CleanUp);

            Assert.False(result);
        }

        #region Has Transformations

        #endregion Has Transformations

        #region Transform Tests

        //List<ExpandoObject> Transform<T>(List<T> items);
        [Fact]
        public void Transform_SingleInventoryFlat()
        {
            var process = GetMockProcessData();
            var transformations = GetMockTransformationData();
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository(process, transformations);
            var transformationLookupService = SetupMockTransformationLookupService();

            var service = new TransformationsService(transformationsRepository, clientConfiguration.Object, transformationLookupService.Object);

            var inventoryFlat = new InventoryFlatDataModel
            {
                InventoryFlatDataUid = 1,
                ProcessUid = 1,
                RowId = 1,
                AssetId = "A0",
                Tag = "Tag001",
                Serial = "1234",
                SiteId = "001",
                SiteName = "001 - Site",
                Location = "Room: Maintenance Room",
                LocationType = "Room: Maintenance Room",
                Status = "Available",
                DepartmentName = "tc",
                DepartmentId = "A",
                FundingSource = "N",
                FundingSourceDescription = "",
                PurchasePrice = "10.00",
                PurchaseDate = "",
                ExpirationDate = "",
                InventoryNotes = "New Broom",
                OrderNumber = "",
                VendorName = "Broom's R Us",
                VendorAccountNumber = "",
                ParentTag = "",
                ProductName = "Broom",
                ProductDescription = "Wooden Handle Broom",
                ProductByNumber = "001",
                ProductTypeName = "Broom",
                ProductTypeDescription = "Broom",
                ModelNumber = "",
                ManufacturerName = "Broom's R Us",
                AreaName = "Floor",
                CustomField1Value = "",
                CustomField1Label = "",
                CustomField2Value = "",
                CustomField2Label = "",
                CustomField3Value = "",
                CustomField3Label = "",
                CustomField4Value = "",
                CustomField4Label = "",
                InvoiceNumber = "",
                InvoiceDate = ""
            };

            var result = service.Transform(inventoryFlat, ProcessSteps.Ingest);

            if (inventoryFlat.Rejected)
            {
                _outputHelper.WriteLine(inventoryFlat.RejectedNotes);
            }
            if (result != null)
            {
                _outputHelper.WriteLine(Utilities.ToStringObject(result));
            }
            Assert.NotNull(result);
        }
        [Fact]
        public void TransformToDynamic_SingleInventoryFlat()
        {
            var process = GetMockProcessData();
            var transformations = GetMockTransformationData();
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository(process, transformations);
            var transformationLookupService = SetupMockTransformationLookupService();

            var service = new TransformationsService(transformationsRepository, clientConfiguration.Object, transformationLookupService.Object);

            var inventoryFlat = new InventoryFlatDataModel
            {
                InventoryFlatDataUid = 1,
                ProcessUid = 1,
                RowId = 1,
                AssetId = "A0",
                Tag = "Tag001",
                Serial = "1234",
                SiteId = "001",
                SiteName = "001 - Site",
                Location = "Room: Maintenance Room",
                LocationType = "Room: Maintenance Room",
                Status = "Available",
                DepartmentName = "None",
                DepartmentId = "0",
                FundingSource = "None",
                FundingSourceDescription = "",
                PurchasePrice = "10.00",
                PurchaseDate = "",
                ExpirationDate = "",
                InventoryNotes = "New Broom",
                OrderNumber = "",
                VendorName = "Broom's R Us",
                VendorAccountNumber = "",
                ParentTag = "",
                ProductName = "Broom",
                ProductDescription = "Wooden Handle Broom",
                ProductByNumber = "001",
                ProductTypeName = "Broom",
                ProductTypeDescription = "Broom",
                ModelNumber = "",
                ManufacturerName = "Broom's R Us",
                AreaName = "Floor",
                CustomField1Value = "",
                CustomField1Label = "",
                CustomField2Value = "",
                CustomField2Label = "",
                CustomField3Value = "",
                CustomField3Label = "",
                CustomField4Value = "",
                CustomField4Label = "",
                InvoiceNumber = "",
                InvoiceDate = ""
            };

            var result = service.TransformToDynamic(inventoryFlat, ProcessSteps.Ingest);

            Assert.NotNull(result);
            _outputHelper.WriteLine(Utilities.ToStringObject(result));
        }
        [Fact]
        public void Transform_ListInventoryFlat()
        {
            var process = GetMockProcessData();
            var transformations = GetMockTransformationData();
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository(process, transformations);
            var transformationLookupService = SetupMockTransformationLookupService();

            var service = new TransformationsService(transformationsRepository, clientConfiguration.Object, transformationLookupService.Object);

            var inventoryFlatList = new List<InventoryFlatDataModel> {
                new InventoryFlatDataModel() {
                    InventoryFlatDataUid = 0,
                    ProcessUid = 1,
                    RowId = 1,
                    AssetId = "A0",
                    Tag = "Tag",
                    Serial = "1234",
                    SiteId = "001",
                    SiteName = "001 - Site",
                    Location = "Maintenance Room",
                    LocationType = "Maintenance Room",
                    Status = "Available",
                    DepartmentName = "None",
                    DepartmentId = "0",
                    FundingSource = "None",
                    FundingSourceDescription = "",
                    PurchasePrice = "10.00",
                    PurchaseDate = "",
                    ExpirationDate = "",
                    InventoryNotes = "New Broom",
                    OrderNumber = "",
                    VendorName = "Broom's R Us",
                    VendorAccountNumber = "",
                    ParentTag = "",
                    ProductName = "Broom",
                    ProductDescription = "Wooden Handle Broom",
                    ProductByNumber = "001",
                    ProductTypeName = "Broom",
                    ProductTypeDescription = "Broom",
                    ModelNumber = "",
                    ManufacturerName = "Broom's R Us",
                    AreaName = "Floor",
                    CustomField1Value = "",
                    CustomField1Label = "",
                    CustomField2Value = "",
                    CustomField2Label = "",
                    CustomField3Value = "",
                    CustomField3Label = "",
                    CustomField4Value = "",
                    CustomField4Label = "",
                    InvoiceNumber = "",
                    InvoiceDate = ""
                },
                new InventoryFlatDataModel() {
                    InventoryFlatDataUid = 0,
                    ProcessUid = 1,
                    RowId = 2,
                    AssetId = "A1",
                    Tag = "Tag1",
                    Serial = "12345",
                    SiteId = "001",
                    SiteName = "001 - Site",
                    Location = "Maintenance Room",
                    LocationType = "Maintenance Room",
                    Status = "Available",
                    DepartmentName = "None",
                    DepartmentId = "0",
                    FundingSource = "None",
                    FundingSourceDescription = "",
                    PurchasePrice = "10.00",
                    PurchaseDate = "",
                    ExpirationDate = "",
                    InventoryNotes = "New Broom",
                    OrderNumber = "",
                    VendorName = "Broom's R Us",
                    VendorAccountNumber = "",
                    ParentTag = "",
                    ProductName = "Broom",
                    ProductDescription = "Wooden Handle Broom",
                    ProductByNumber = "001",
                    ProductTypeName = "Broom",
                    ProductTypeDescription = "Broom",
                    ModelNumber = "",
                    ManufacturerName = "Broom's R Us",
                    AreaName = "Floor",
                    CustomField1Value = "Nothing",
                    CustomField1Label = "Cost",
                    CustomField2Value = "0.0",
                    CustomField2Label = "Value",
                    CustomField3Value = "01234",
                    CustomField3Label = "Special Value",
                    CustomField4Value = "true",
                    CustomField4Label = "Expired",
                    InvoiceNumber = "000",
                    InvoiceDate = "2018/01/01"
                },
                new InventoryFlatDataModel() {
                    InventoryFlatDataUid = 0,
                    ProcessUid = 1,
                    RowId = 3,
                    AssetId = "0",
                    Tag = "Dustpan1",
                    Serial = "",
                    SiteId = "001",
                    SiteName = "001 - Site",
                    Location = "Maintenance Room",
                    LocationType = "Maintenance Room",
                    Status = "Available",
                    DepartmentName = "None",
                    DepartmentId = "0",
                    FundingSource = "None",
                    FundingSourceDescription = "",
                    PurchasePrice = "1.00",
                    PurchaseDate = "",
                    ExpirationDate = "",
                    InventoryNotes = "Old Dustpan",
                    OrderNumber = "",
                    VendorName = "Broom's R Us",
                    VendorAccountNumber = "",
                    ParentTag = "",
                    ProductName = "Broom",
                    ProductDescription = "Rusted Dustpan",
                    ProductByNumber = "001",
                    ProductTypeName = "Dustpan",
                    ProductTypeDescription = "Metal Dustpa",
                    ModelNumber = "001",
                    ManufacturerName = "Broom's R Us",
                    AreaName = "Floor",
                    CustomField1Value = "Yes",
                    CustomField1Label = "Rusted",
                    CustomField2Value = "",
                    CustomField2Label = "",
                    CustomField3Value = "",
                    CustomField3Label = "",
                    CustomField4Value = "",
                    CustomField4Label = "",
                    InvoiceNumber = "",
                    InvoiceDate = ""
                }
            };

            var result = service.Transform(inventoryFlatList, ProcessSteps.Ingest);

            Assert.NotNull(result);
            _outputHelper.WriteLine(Utilities.ToStringObject(result));
        }

        [Fact]
        public void TransformToDynamic_ListInventoryFlat()
        {
            var process = GetMockProcessData();
            var transformations = GetMockTransformationData();
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository(process, transformations);
            var transformationLookupService = SetupMockTransformationLookupService();

            var service = new TransformationsService(transformationsRepository, clientConfiguration.Object, transformationLookupService.Object);

            var inventoryFlatList = new List<InventoryFlatDataModel> {
                new InventoryFlatDataModel() {
                    InventoryFlatDataUid = 0,
                    ProcessUid = 1,
                    RowId = 1,
                    AssetId = "A0",
                    Tag = "Tag",
                    Serial = "1234",
                    SiteId = "001",
                    SiteName = "001 - Site",
                    Location = "Maintenance Room",
                    LocationType = "Maintenance Room",
                    Status = "Available",
                    DepartmentName = "None",
                    DepartmentId = "0",
                    FundingSource = "None",
                    FundingSourceDescription = "",
                    PurchasePrice = "10.00",
                    PurchaseDate = "",
                    ExpirationDate = "",
                    InventoryNotes = "New Broom",
                    OrderNumber = "",
                    VendorName = "Broom's R Us",
                    VendorAccountNumber = "",
                    ParentTag = "",
                    ProductName = "Broom",
                    ProductDescription = "Wooden Handle Broom",
                    ProductByNumber = "001",
                    ProductTypeName = "Broom",
                    ProductTypeDescription = "Broom",
                    ModelNumber = "",
                    ManufacturerName = "Broom's R Us",
                    AreaName = "Floor",
                    CustomField1Value = "",
                    CustomField1Label = "",
                    CustomField2Value = "",
                    CustomField2Label = "",
                    CustomField3Value = "",
                    CustomField3Label = "",
                    CustomField4Value = "",
                    CustomField4Label = "",
                    InvoiceNumber = "",
                    InvoiceDate = ""
                },
                new InventoryFlatDataModel() {
                    InventoryFlatDataUid = 0,
                    ProcessUid = 1,
                    RowId = 2,
                    AssetId = "A1",
                    Tag = "Tag1",
                    Serial = "12345",
                    SiteId = "001",
                    SiteName = "001 - Site",
                    Location = "Maintenance Room",
                    LocationType = "Maintenance Room",
                    Status = "Available",
                    DepartmentName = "None",
                    DepartmentId = "0",
                    FundingSource = "None",
                    FundingSourceDescription = "",
                    PurchasePrice = "10.00",
                    PurchaseDate = "",
                    ExpirationDate = "",
                    InventoryNotes = "New Broom",
                    OrderNumber = "",
                    VendorName = "Broom's R Us",
                    VendorAccountNumber = "",
                    ParentTag = "",
                    ProductName = "Broom",
                    ProductDescription = "Wooden Handle Broom",
                    ProductByNumber = "001",
                    ProductTypeName = "Broom",
                    ProductTypeDescription = "Broom",
                    ModelNumber = "",
                    ManufacturerName = "Broom's R Us",
                    AreaName = "Floor",
                    CustomField1Value = "Nothing",
                    CustomField1Label = "Cost",
                    CustomField2Value = "0.0",
                    CustomField2Label = "Value",
                    CustomField3Value = "01234",
                    CustomField3Label = "Special Value",
                    CustomField4Value = "true",
                    CustomField4Label = "Expired",
                    InvoiceNumber = "000",
                    InvoiceDate = "2018/01/01"
                },
                new InventoryFlatDataModel() {
                    InventoryFlatDataUid = 0,
                    ProcessUid = 1,
                    RowId = 3,
                    AssetId = "0",
                    Tag = "Dustpan1",
                    Serial = "",
                    SiteId = "001",
                    SiteName = "001 - Site",
                    Location = "Maintenance Room",
                    LocationType = "Maintenance Room",
                    Status = "Available",
                    DepartmentName = "None",
                    DepartmentId = "0",
                    FundingSource = "None",
                    FundingSourceDescription = "",
                    PurchasePrice = "1.00",
                    PurchaseDate = "",
                    ExpirationDate = "",
                    InventoryNotes = "Old Dustpan",
                    OrderNumber = "",
                    VendorName = "Broom's R Us",
                    VendorAccountNumber = "",
                    ParentTag = "",
                    ProductName = "Broom",
                    ProductDescription = "Rusted Dustpan",
                    ProductByNumber = "001",
                    ProductTypeName = "Dustpan",
                    ProductTypeDescription = "Metal Dustpa",
                    ModelNumber = "001",
                    ManufacturerName = "Broom's R Us",
                    AreaName = "Floor",
                    CustomField1Value = "Yes",
                    CustomField1Label = "Rusted",
                    CustomField2Value = "",
                    CustomField2Label = "",
                    CustomField3Value = "",
                    CustomField3Label = "",
                    CustomField4Value = "",
                    CustomField4Label = "",
                    InvoiceNumber = "",
                    InvoiceDate = ""
                }
            };

            var result = service.TransformToDynamic(inventoryFlatList, ProcessSteps.Ingest);

            Assert.NotNull(result);
            _outputHelper.WriteLine(Utilities.ToStringObject(result));
        }

        #endregion Transform Tests

        #region ApplyTransformation Tests

        //U ApplyTransformation<T, U>(T inputEntity, U outputEntity, string function, string parameters, T value);
        [Fact]
        public void ApplyTransformation_DefaultInteger()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("default", "", 10);

            Assert.Equal(10, result);
        }
        [Fact]
        public void ApplyTransformation_DefaultIntegerWithParam()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("default", "1", -11);

            Assert.Equal(-11, result);
        }
        [Fact]
        public void ApplyTransformation_DefaultIntegerInvalid()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            int? invalidInt = null;
            var result = service.ApplyTransformation("default", "", invalidInt);

            Assert.Null(result);
        }
        [Fact]
        public void ApplyTransformation_DefaultIntegerInvalidWithParam()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            int? invalidInt = null;
            var result = service.ApplyTransformation("default", "1", invalidInt);

            Assert.Equal(1, result);
        }
        [Fact]
        public void ApplyTransformation_DefaultDouble()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("default", "", 4.5);

            Assert.Equal(4.5, result);
        }
        [Fact]
        public void ApplyTransformation_DefaultDoubleWithParam()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("default", "1.0", 10.0);

            Assert.Equal(10.0, result);
        }
        [Fact]
        public void ApplyTransformation_DefaultDoubleInvalid()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            double? invalidDouble = null;
            var result = service.ApplyTransformation("default", "", invalidDouble);

            Assert.Null(result);
        }
        [Fact]
        public void ApplyTransformation_DefaultDoubleInvalidWithParam()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            double? invalidDouble = null;
            var result = service.ApplyTransformation("default", "-1.1", invalidDouble);

            Assert.Equal(-1.1, result);
        }
        [Fact]
        public void ApplyTransformation_DefaultBool()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("default", "", true);

            Assert.True(Boolean.Parse(result.ToString()));
        }
        [Fact]
        public void ApplyTransformation_DefaultBoolWithParam()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("default", "true", false);

            Assert.False(Boolean.Parse(result.ToString()));
        }
        [Fact]
        public void ApplyTransformation_DefaultBoolInvalid()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            bool? invalidBool = null;
            var result = service.ApplyTransformation("default", "", invalidBool);

            Assert.Null(result);
        }
        [Fact]
        public void ApplyTransformation_DefaultBoolInvalidWithParam()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            bool? invalidBool = null;
            var result = service.ApplyTransformation("default", "true", invalidBool);

            Assert.True((bool)result);
        }
        [Fact]
        public void ApplyTransformation_LookupValid()
        {
            var processes = GetMockProcessData();
            var transformations = GetMockTransformationData();
            var transformationLookups = GetMockTransformationLookupData();

            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository(processes, transformations);
            var transformationLookupRepository = SetupMockTransformationLookupRepository(processes, transformationLookups);
            var transformationLookupService = SetupMockTransformationLookupService(transformationLookupRepository, clientConfiguration);

            var service = new TransformationsService(transformationsRepository, clientConfiguration.Object, transformationLookupService);

            var result = service.ApplyTransformation("lookup", "[test]", 1);

            Assert.Equal("one", result);
        }
        [Fact]
        public void ApplyTransformation_LookupInvalid()
        {
            var processes = GetMockProcessData();
            var transformations = GetMockTransformationData();
            var transformationLookups = GetMockTransformationLookupData();

            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository(processes, transformations);
            var transformationLookupRepository = SetupMockTransformationLookupRepository(processes, transformationLookups);
            var transformationLookupService = SetupMockTransformationLookupService(transformationLookupRepository, clientConfiguration);

            var service = new TransformationsService(transformationsRepository, clientConfiguration.Object, transformationLookupService);

            var result = service.ApplyTransformation("lookup", "[test]", -1);

            Assert.Null(result);
        }
        [Fact]
        public void ApplyTransformation_SplitValid()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("split", "[,]", "first,second,fourth,third");

            Assert.Equal("first", result);
        }
        [Fact]
        public void ApplyTransformation_SplitValidWithIndices()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("split", "[,][0,1,3,2]", "first,second,fourth,third");

            Assert.Equal("firstsecondthirdfourth", result);
        }
        [Fact]
        public void ApplyTransformation_SplitInvalid()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("split", "[,]", "nosplit");

            Assert.Equal("nosplit", result);
        }
        [Fact]
        public void ApplyTransformation_SplitInvalidParameters()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<ArgumentException>(() => service.ApplyTransformation("split", "[]", "nosplit"));
        }
        [Fact]
        public void ApplyTransformation_TruncateValidLessThanLenght()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("truncate", "[10]", "this_is_a_string_to_truncate");

            Assert.Equal("this_is_a_", result);
        }
        [Fact]
        public void ApplyTransformation_TruncateValidGreaterThanLenght()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("truncate", "[30]", "this_is_a_string_to_truncate");

            Assert.Equal("this_is_a_string_to_truncate", result);
        }
        [Fact]
        public void ApplyTransformation_TruncateInvalid()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("truncate", "[]", "this_is_a_string_to_truncate");

            Assert.Equal("this_is_a_string_to_truncate", result);
        }
        [Fact]
        public void ApplyTransformation_RoundDownValid()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("rounddown", "", 13.05);

            Assert.Equal(13, result);
        }
        [Fact]
        public void ApplyTransformation_RoundDownInvalid()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("rounddown", "", 10);

            Assert.Equal(10, result);
        }
        [Fact]
        public void ApplyTransformation_RoundUp()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("roundup", "", 7.08);

            Assert.Equal(8, result);
        }
        [Fact]
        public void ApplyTransformation_InvalidFunction()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.ApplyTransformation("invalid", "", 9);

            Assert.Equal(9, result);
        }

        #endregion ApplyTransformation Tests

        #region QuickCast Tests

        //T QuickCast<T>(string value);
        [Fact]
        public void QuickCast_StringToInteger()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<string, int>("2");

            Assert.Equal(2, result);
        }
        [Fact]
        public void QuickCast_InvalidStringToInteger()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<FormatException>(() => service.QuickCast<string, int>("invalid"));
        }
        [Fact]
        public void QuickCast_StringToBoolean()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<string, bool>("false");

            Assert.False(result);
        }
        [Fact]
        public void QuickCast_InvalidStringToBoolean()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<FormatException>(() => service.QuickCast<string, bool>("blablabla"));
        }
        [Fact]
        public void QuickCast_StringToDouble()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<string, double>("3.17");

            Assert.Equal(3.17, result);
        }
        [Fact]
        public void QuickCast_StringInvalidToDouble()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<FormatException>(() => service.QuickCast<string, double>("not a double"));
        }
        [Fact]
        public void QuickCast_StringToDate()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<string, DateTime>("2015/03/14");

            Assert.Equal(new DateTime(2015, 3, 14), result);
        }
        [Fact]
        public void QuickCast_InvalidStringToDate()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<FormatException>(() => service.QuickCast<string, DateTime>("not a date value"));
        }
        [Fact]
        public void QuickCast_IntegerToString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<int, string>(28);

            Assert.Equal("28", result);
        }
        [Fact]
        public void QuickCast_IntegerToBoolean()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<int, bool>(23);

            Assert.True(result);
        }
        [Fact]
        public void QuickCast_IntegerNullableToBoolean()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            int? value = (int?)23;
            var result = service.QuickCast<int?, bool>(value);

            Assert.True(result);
        }
        [Fact]
        public void QuickCast_IntegerZeroToBoolean()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<int, bool>(0);

            Assert.False(result);
        }
        [Fact]
        public void QuickCast_IntegerNegativeToBoolean()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<int, bool>(0);

            Assert.False(result);
        }
        [Fact]
        public void QuickCast_IntegerToDate()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<InvalidCastException>(() => service.QuickCast<int, DateTime>(231651));

        }
        [Fact]
        public void QuickCast_DateToInteger()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<InvalidCastException>(() => service.QuickCast<DateTime, int>(new DateTime(2018, 12, 31)));
        }
        [Fact]
        public void QuickCast_DateToString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<DateTime, string>(new DateTime(2018, 12, 31));

            Assert.Equal("12/31/2018 12:00:00 AM", result);
            Assert.IsType<string>(result);
        }
        [Fact]
        public void QuickCast_ObjectToString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Object obj = (object)"String Object";
            var result = service.QuickCast<object, string>(obj);

            Assert.Equal("String Object", result);
        }
        [Fact]
        public void QuickCast_ObjectNullToString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<object, string>(null);

            Assert.Null(result);
        }
        [Fact]
        public void QuickCast_StringToObject()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<string, object>("String Object");

            Assert.NotNull(result);
        }

        #endregion QuickCast Tests

        #region Default Tests

        //U Default<T, U>(T value, List<string> parameters);
        [Fact]
        public void Default_EmptyStringToIntegerNullParameters()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            var transformationLookupService = SetupMockTransformationLookupService();

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<FormatException>(() => service.Default<string, int>(string.Empty, null));
        }
        [Fact]
        public void Default_NullToIntegerEmptyParameters()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            var transformationLookupService = SetupMockTransformationLookupService();

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var parameters = new List<string>();
            var result = service.Default<string, int>(null, parameters);

            Assert.Equal(0, result);
        }
        [Fact]
        public void Default_NullToIntegerWithParameter()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            var transformationLookupService = SetupMockTransformationLookupService();

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var parameters = new List<string> { "10" };
            var result = service.Default<string, int>(null, parameters);

            Assert.Equal(10, result);
        }
        [Fact]
        public void Default_NullToBoolean()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            var transformationLookupService = SetupMockTransformationLookupService();

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var parameters = new List<string>();
            var result = service.Default<object, bool>(null, parameters);

            Assert.False(result);
        }
        [Fact]
        public void Default_NullToBooleanWithParameter()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            var transformationLookupService = SetupMockTransformationLookupService();

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var parameters = new List<string> { "true" };
            var result = service.Default<object, bool>(null, parameters);

            Assert.True(result);
        }
        [Fact]
        public void Default_NullToDate()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var parameters = new List<string>();
            var result = service.Default<object, DateTime>(null, parameters);

            Assert.Equal(new DateTime(), result);
        }
        [Fact]
        public void Default_NullToDateWithDefault()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var parameters = new List<string> { "2018/12/31" };
            var result = service.Default<object, DateTime>(null, parameters);

            Assert.Equal(new DateTime(2018, 12, 31), result);
        }
        [Fact]
        public void Default_NullToString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var parameters = new List<string>();
            var result = service.Default<object, string>(null, parameters);

            Assert.Null(result);
        }
        [Fact]
        public void Default_NullToStringWithDefault()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var parameters = new List<string> { "Empty" };
            var result = service.Default<object, string>(null, parameters);

            Assert.Equal("Empty", result);
        }
        [Fact]
        public void Default_NullToDouble()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var parameters = new List<string>();
            var result = service.Default<object, double>(null, parameters);

            Assert.Equal(0.0, result);
        }
        [Fact]
        public void Default_NullToDoubleWithDefault()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var parameters = new List<string> { "2.1" };
            var result = service.Default<object, double>(null, parameters);

            Assert.Equal(2.1, result);
        }

        #endregion Default Tests

        #region Lookup Tests

        //string Lookup<T>(T value, List<string> parameters);
        [Fact]
        public void Lookup_Null()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use
            transformationLookupService.Setup(x => x.GetTransformationLookupValue(string.Empty, null)).Returns(string.Empty);

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<NullReferenceException>(() => service.Lookup<string>(null, new List<string> { string.Empty }));
        }
        [Fact]
        public void Lookup_EmptyString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use
            transformationLookupService.Setup(x => x.GetTransformationLookupValue("test", string.Empty)).Returns(string.Empty);

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.Lookup(string.Empty, new List<string> { "test" });

            Assert.Equal(string.Empty, result);
        }
        [Fact]
        public void Lookup_EmptyParameters()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use
            transformationLookupService.Setup(x => x.GetTransformationLookupValue(string.Empty, "valid")).Returns(string.Empty);

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<ArgumentNullException>(() => service.Lookup("valid", new List<string>()));
        }
        [Fact]
        public void Lookup_InvalidLookupValue()
        {
            var processes = GetMockProcessData();
            var transformations = GetMockTransformationData();
            var transformationLookups = GetMockTransformationLookupData();

            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository(processes, transformations);
            var transformationLookupRepository = SetupMockTransformationLookupRepository(processes, transformationLookups);
            var transformationLookupService = SetupMockTransformationLookupService(transformationLookupRepository, clientConfiguration);

            var service = new TransformationsService(transformationsRepository, clientConfiguration.Object, transformationLookupService);

            var result = service.Lookup("invalid", new List<string> { "test" });

            Assert.Null(result);
        }
        [Fact]
        public void Lookup_ValidLookupValue()
        {
            var processes = GetMockProcessData();
            var transformations = GetMockTransformationData();
            var transformationLookups = GetMockTransformationLookupData();

            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository(processes, transformations);
            var transformationLookupRepository = SetupMockTransformationLookupRepository(processes, transformationLookups);
            var transformationLookupService = SetupMockTransformationLookupService(transformationLookupRepository, clientConfiguration);

            var service = new TransformationsService(transformationsRepository, clientConfiguration.Object, transformationLookupService);

            var result = service.Lookup("1", new List<string> { "test" });

            Assert.Equal("one", result);
        }
        [Fact]
        public void Lookup_ValidInteger()
        {
            var processes = GetMockProcessData();
            var transformations = GetMockTransformationData();
            var transformationLookups = GetMockTransformationLookupData();

            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository(processes, transformations);
            var transformationLookupRepository = SetupMockTransformationLookupRepository(processes, transformationLookups);
            var transformationLookupService = SetupMockTransformationLookupService(transformationLookupRepository, clientConfiguration);

            var service = new TransformationsService(transformationsRepository, clientConfiguration.Object, transformationLookupService);

            var result = service.Lookup(1, new List<string> { "test" });

            Assert.Equal("one", result);
        }
        [Fact]
        public void Lookup_FromEmptyList()
        {
            var processes = GetMockProcessData();
            var transformations = GetMockTransformationData();
            var transformationLookups = (new List<TransformationLookup>()).AsQueryable();

            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository(processes, transformations);
            var transformationLookupRepository = SetupMockTransformationLookupRepository(processes, transformationLookups);
            var transformationLookupService = SetupMockTransformationLookupService(transformationLookupRepository, clientConfiguration);

            var service = new TransformationsService(transformationsRepository, clientConfiguration.Object, transformationLookupService);

            var result = service.Lookup("0", new List<string> { "test" });

            Assert.Null(result);
        }

        #endregion Lookup Tests

        #region Split Tests

        //string Split<T>(T value, List<string> parameters);
        [Fact]
        public void Split_ValidSimple()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.Split("string_to_split_correctly", new List<string> { "_" });

            Assert.Equal("string", result);
        }
        [Fact]
        public void Split_ValidWithSections()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.Split("string_to_split_correctly", new List<string> { "_", "1,2" });

            Assert.Equal("tosplit", result);
        }
        [Fact]
        public void Split_ValidWithQuestionableSections()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.Split("string_to_split_correctly", new List<string> { "_", "1,2,A," });

            Assert.Equal("tosplit", result);
        }
        [Fact]
        public void Split_EmptyString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.Split(string.Empty, new List<string> { "," });

            Assert.Equal(string.Empty, result);
        }
        [Fact]
        public void Split_EmptyParameters()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<ArgumentNullException>(() => service.Split("string_to_split", new List<string>()));
        }
        [Fact]
        public void Split_BadParameters()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<ArgumentException>(() => service.Split("string_to_split", new List<string> { "", "" }));
        }
        [Fact]
        public void Split_BadSecondParameter()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<ArgumentException>(() => service.Split("string_to_split", new List<string> { "_", "" }));
        }

        #endregion Split Tests

        #region Truncate Tests

        //string Truncate<T>(T value, List<string> parameters);
        [Fact]
        public void Truncate_EmptyString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.Truncate(string.Empty, new List<string> { "10" });

            Assert.Equal(string.Empty, result);
        }
        [Fact]
        public void Truncate_Null()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            string value = null;
            var result = service.Truncate(value, new List<string> { "10" });

            Assert.Null(result);
        }
        [Fact]
        public void Truncate_EmptyParameters()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<ArgumentNullException>(() => service.Truncate("string_to_truncate", new List<string>()));
        }
        [Fact]
        public void Truncate_ZeroLenght()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.Truncate("string_to_truncate", new List<string> { "0" });

            Assert.Equal("string_to_truncate", result);
        }
        [Fact]
        public void Truncate_LengthGreaterThanString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.Truncate("string_to_truncate", new List<string> { "20" });

            Assert.Equal("string_to_truncate", result);
        }
        [Fact]
        public void Truncate_LenghtLessThanString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.Truncate("string_to_truncate", new List<string> { "10" });

            Assert.Equal("string_to_", result);
        }
        [Fact]
        public void Truncate_NegativeLenght()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.Truncate("string_to_truncate", new List<string> { "-1" });

            Assert.Equal("string_to_truncate", result);
        }

        #endregion Truncate Tests

        #region RoundDown Tests

        //int RoundDown<T>(T value);
        [Fact]
        public void RoundDown_Integer()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.RoundDown(10);

            Assert.Equal(10, result);
        }
        [Fact]
        public void RoundDown_Double()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.RoundDown(7.15);

            Assert.Equal(7, result);
        }
        [Fact]
        public void RoundDown_ValidString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.RoundDown("13");

            Assert.Equal(13, result);
        }
        [Fact]
        public void RoundDown_ValidDecimalString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.RoundDown("6.66");

            Assert.Equal(6, result);
        }
        [Fact]
        public void RoundDown_ValidNegativeDecimalString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.RoundDown("-78.0001");

            Assert.Equal(-79, result);
        }
        [Fact]
        public void RoundDown_InvalidString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<ArgumentException>(() => service.RoundDown("NotAValidString"));
        }
        [Fact]
        public void RoundDown_Null()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<ArgumentNullException>(() => service.RoundDown<object>(null));
        }

        #endregion RoundDown Tests

        #region RoundUp Tests

        //int RoundUp<T>(T value);
        [Fact]
        public void RoundUp_Integer()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.RoundUp(7);

            Assert.Equal(7, result);
        }
        [Fact]
        public void RoundUp_Double()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.RoundUp(10.21);

            Assert.Equal(11, result);
        }
        [Fact]
        public void RoundUp_ValidString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.RoundUp("3");

            Assert.Equal(3, result);
        }
        [Fact]
        public void RoundUp_ValidDecimalString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.RoundUp("3.0");

            Assert.Equal(3, result);
        }
        [Fact]
        public void RoundUp_ValidNegativeDecimalString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.RoundUp("-30.01");

            Assert.Equal(-30, result);
        }
        [Fact]
        public void RoundUp_InvalidString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<ArgumentException>(() => service.RoundUp<string>("NotANumber"));
        }
        [Fact]
        public void RoundUp_Null()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            Assert.Throws<ArgumentNullException>(() => service.RoundUp<string>(null));
        }

        #endregion RoundUp Tests

        #region Setup MockObjects

        protected Mock<IntegrationMiddleWayContext> SetupMockTransformationsContext() // IQueryable<Processes> process, IQueryable<Transformations> data)
        {
            var mockContext = new Mock<IntegrationMiddleWayContext>();

            return mockContext;
        }

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

        protected ITransformationsRepository SetupMockTransformationsRepository(IQueryable<Processes> processes, IQueryable<Transformations> transformations)
        {
            var mockContext = SetupMockTransformationsContext();

            var mockProcess = new Mock<DbSet<Processes>>();
            mockProcess.As<IQueryable<Processes>>().Setup(m => m.Provider).Returns(processes.Provider);
            mockProcess.As<IQueryable<Processes>>().Setup(m => m.Expression).Returns(processes.Expression);
            mockProcess.As<IQueryable<Processes>>().Setup(m => m.ElementType).Returns(processes.ElementType);
            mockProcess.As<IQueryable<Processes>>().Setup(m => m.GetEnumerator()).Returns(processes.GetEnumerator());

            var mockTransformations = new Mock<DbSet<Transformations>>();
            mockTransformations.As<IQueryable<Transformations>>().Setup(m => m.Provider).Returns(transformations.Provider);
            mockTransformations.As<IQueryable<Transformations>>().Setup(m => m.Expression).Returns(transformations.Expression);
            mockTransformations.As<IQueryable<Transformations>>().Setup(m => m.ElementType).Returns(transformations.ElementType);
            mockTransformations.As<IQueryable<Transformations>>().Setup(m => m.GetEnumerator()).Returns(transformations.GetEnumerator());

            mockContext.Setup(c => c.Processes).Returns(mockProcess.Object);
            mockContext.Setup(c => c.Transformations).Returns(mockTransformations.Object);

            var mockTransformationsRepository = new TransformationsRepository(mockContext.Object);
            return mockTransformationsRepository;
        }

        protected Mock<ITransformationLookupService> SetupMockTransformationLookupService()
        {
            return new Mock<ITransformationLookupService>();
        }

        protected ITransformationLookupRepository SetupMockTransformationLookupRepository(IQueryable<Processes> processes, IQueryable<TransformationLookup> transformationLookups) //IQueryable<Processes> processes
        {
            var mockContext = SetupMockTransformationsContext();

            var mockProcess = new Mock<DbSet<Processes>>();
            mockProcess.As<IQueryable<Processes>>().Setup(m => m.Provider).Returns(processes.Provider);
            mockProcess.As<IQueryable<Processes>>().Setup(m => m.Expression).Returns(processes.Expression);
            mockProcess.As<IQueryable<Processes>>().Setup(m => m.ElementType).Returns(processes.ElementType);
            mockProcess.As<IQueryable<Processes>>().Setup(m => m.GetEnumerator()).Returns(processes.GetEnumerator());

            var mockTransformationLookup = new Mock<DbSet<TransformationLookup>>();
            mockTransformationLookup.As<IQueryable<TransformationLookup>>().Setup(m => m.Provider).Returns(transformationLookups.Provider);
            mockTransformationLookup.As<IQueryable<TransformationLookup>>().Setup(m => m.Expression).Returns(transformationLookups.Expression);
            mockTransformationLookup.As<IQueryable<TransformationLookup>>().Setup(m => m.ElementType).Returns(transformationLookups.ElementType);
            mockTransformationLookup.As<IQueryable<TransformationLookup>>().Setup(m => m.GetEnumerator()).Returns(transformationLookups.GetEnumerator());

            mockContext.Setup(c => c.Processes).Returns(mockProcess.Object);
            mockContext.Setup(c => c.TransformationLookup).Returns(mockTransformationLookup.Object);

            var mockTransformationLookupRepository = new TransformationLookupRepository(mockContext.Object);

            return mockTransformationLookupRepository;
        }

        protected ITransformationLookupService SetupMockTransformationLookupService(ITransformationLookupRepository transformationLookupRepository, Mock<IClientConfiguration> clientConfiguration)
        {
            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            return service;
        }

        //protected Mock<ITransformationsService> SetupTransformationsService(Mock<ITransformationsRepository> transformationsRepository, Mock<IClientConfiguration> clientConfiguration)
        //{
        //    //var clientConfiguration = SetupClientConfiguration();
        //    //var transformationsRepository = SetupTransformationsRepository();
        //    return new Mock<ITransformationsService>(transformationsRepository.Object, clientConfiguration.Object);
        //}

        protected IQueryable<Processes> GetMockProcessData()
        {
            var processes = new List<Processes> { new Processes { ProcessUid = 1, Client = "Test", ProcessName = "Module", Description = "Test Module", Enabled = true, CreatedDate = DateTime.Now } }.AsQueryable();
            return processes;
        }

        protected IQueryable<Transformations> GetMockTransformationData()
        {
            return new List<Transformations>
                {
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "quickcast", Parameters = "", SourceColumn = "InventoryFlatDataUid", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "quickcast", Parameters = "", SourceColumn = "ProcessUid", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "quickcast", Parameters = "", SourceColumn = "RowId", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "default", Parameters = "[]", SourceColumn = "AssetId", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "Tag", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "Serial", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "SiteId", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "SiteName", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "split", Parameters = "[:][1]", SourceColumn = "Location", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "default", Parameters = "[In Use]", SourceColumn = "Status", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "lookup", Parameters = "[deptName]", SourceColumn = "DepartmentName", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "lookup", Parameters = "[deptId]", SourceColumn = "DepartmentId", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "lookup", Parameters = "[funding]", SourceColumn = "FundingSource", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "truncate", Parameters = "[100]", SourceColumn = "FundingSourceDescription", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "PurchasePrice", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "PurchaseDate", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "ExpirationDate", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "InventoryNotes", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "OrderNumber", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "VendorName", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "VendorAccountNumber", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "ParentTag", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "ProductName", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "ProductDescription", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "default", Parameters = "[\"00\"]", SourceColumn = "ProductByNumber", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "ProductTypeName", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "ProductTypeDescription", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "ModelNumber", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "ManufacturerName", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "AreaName", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "CustomField1Value", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "CustomField1Label", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "CustomField2Value", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "CustomField2Label", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "CustomField3Value", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "CustomField3Label", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "CustomField4Value", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "CustomField4Label", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "InvoiceNumber", DestinationColumn = "", Enabled = true, Order = 0 },
                    new Transformations { TransformationUid  = 0, ProcessUid = 1, StepName = ProcessSteps.Ingest.ToString(), Function = "", Parameters = "", SourceColumn = "InvoiceDate", DestinationColumn = "", Enabled = true, Order = 0 },
                }.AsQueryable();
        }

        protected IQueryable<TransformationLookup> GetMockTransformationLookupData()
        {
            var data = new List<TransformationLookup>
                        {
                            new TransformationLookup { TransformationLookupUid  = 0, ProcessUid = 1, TransformationLookupKey = "test", Key = "0", Value = "zero", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 1, ProcessUid = 1, TransformationLookupKey = "test", Key = "1", Value = "one", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 2, ProcessUid = 1, TransformationLookupKey = "test", Key = "2", Value = "two", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 3, ProcessUid = 1, TransformationLookupKey = "test", Key = "3", Value = "three", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 4, ProcessUid = 1, TransformationLookupKey = "test", Key = "4", Value = "four", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 5, ProcessUid = 1, TransformationLookupKey = "test", Key = "5", Value = "five", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 6, ProcessUid = 1, TransformationLookupKey = "deptName", Key = "at", Value = "Athletics", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 7, ProcessUid = 1, TransformationLookupKey = "deptName", Key = "tc", Value = "Technology", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 8, ProcessUid = 1, TransformationLookupKey = "deptName", Key = "n", Value = "None", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 9, ProcessUid = 1, TransformationLookupKey = "deptId", Key = "A", Value = "001", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 10, ProcessUid = 1, TransformationLookupKey = "deptId", Key = "B", Value = "002", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 11, ProcessUid = 1, TransformationLookupKey = "deptId", Key = "C", Value = "003", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 12, ProcessUid = 1, TransformationLookupKey = "funding", Key = "N", Value = "None", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 13, ProcessUid = 1, TransformationLookupKey = "funding", Key = "F", Value = "Federal", Enabled = true },
                            new TransformationLookup { TransformationLookupUid  = 14, ProcessUid = 1, TransformationLookupKey = "funding", Key = "S", Value = "State", Enabled = true },
                        }.AsQueryable();

            return data;
        }

        #endregion Setup MockObjects

    }
}