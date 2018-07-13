using Microsoft.EntityFrameworkCore;
using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_Controller.Repositories;
using MiddleWay_Controller.Services;
using MiddleWay_DTO.Models.MiddleWay;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MiddleWay.Tests.MiddleWay_Controller.Services
{
    public class TransformationsServiceTests
    {
        #region Transform Tests

        //List<dynamic> Transform<T>(List<T> items);
        [Fact]
        public void Transform_SingleInventoryFlat()
        {
            var process = GetMockProcessData();
            var transformations = GetMockTransformationData();
            //var transformations = GetMockTransformationData();
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository(process, transformations); //SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository, clientConfiguration.Object, transformationLookupService.Object);

            InventoryFlatDataModel inventoryFlat = new InventoryFlatDataModel
            {
                InventoryFlatDataUid = 1,
                ProcessUid = 1,
                RowId = 1,
                AssetId = "A0",
                Tag = "Tag",
                Serial = "",
                SiteId = "",
                SiteName = "",
                Location = "",
                Status = "",
                DepartmentName = "",
                DepartmentId = "",
                FundingSource = "",
                FundingSourceDescription = "",
                PurchasePrice = "",
                PurchaseDate = "",
                ExpirationDate = "",
                InventoryNotes = "",
                OrderNumber = "",
                VendorName = "",
                VendorAccountNumber = "",
                ParentTag = "",
                ProductName = "",
                ProductDescription = "",
                ProductByNumber = "",
                ProductTypeName = "",
                ProductTypeDescription = "",
                ModelNumber = "",
                ManufacturerName = "",
                AreaName = "",
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

            //var result = service.Transform();

            //Assert.Equal(, result);
        }
        [Fact]
        public void Transform_ListInventoryFlat()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            //var result = service.Transform();

            //Assert.Equal(, result);
        }

        #endregion Transform Tests

        #region ApplyTransformation Tests

        //U ApplyTransformation<T, U>(T inputEntity, U outputEntity, string function, string parameters, T value);
        [Fact]
        public void ApplyTransformation_Default()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            //var result = service.ApplyTransformation();

            //Assert.Equal(, result);
        }
        [Fact]
        public void ApplyTransformation_Lookup()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            //var result = service.ApplyTransformation();

            //Assert.Equal(, result);
        }
        [Fact]
        public void ApplyTransformation_Split()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            //var result = service.ApplyTransformation();

            //Assert.Equal(, result);
        }
        [Fact]
        public void ApplyTransformation_Truncate()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            //var result = service.ApplyTransformation();

            //Assert.Equal(, result);
        }
        [Fact]
        public void ApplyTransformation_RoundDown()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            //var result = service.ApplyTransformation();

            //Assert.Equal(, result);
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

            //var result = service.ApplyTransformation();

            //Assert.Equal(, result);
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

            //var result = service.ApplyTransformation();

            //Assert.Equal(, result);
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
        public void QuickCast_StringtoDouble()
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
        public void QuickCast_InvalidStringtoDouble()
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

            var result = service.QuickCast<int, DateTime>(231651); //TODO: Does not do a good transformation

            //Assert.NotNull(result);
            Assert.IsType<DateTime>(result);
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

            var result = service.QuickCast<DateTime, int>(new DateTime(2018, 12, 31));

            //Assert.NotNull(result);
            Assert.IsType<int>(result);
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
        public void QuickCast_StringToObject()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<string, object>("String Object"); //TODO: Is this a valid test?

            Assert.IsType<object>(result);
        }
        [Fact]
        public void QuickCast_NullObjectToString()
        {
            var clientConfiguration = SetupMockClientConfiguration();
            var transformationsRepository = SetupMockTransformationsRepository();
            //Setup method calls to use
            var transformationLookupService = SetupMockTransformationLookupService();
            //Setup method calls to use

            var service = new TransformationsService(transformationsRepository.Object, clientConfiguration.Object, transformationLookupService.Object);

            var result = service.QuickCast<object, string>(null);

            Assert.Null(result);
            //Assert.IsType<string>(result);
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

            Assert.Equal(null, result);
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

            //Assert.Equal(0, result);
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

            //Assert.Equal(0, result);
        }

        #endregion RoundUp Tests

        #region Setup MockObjects

        protected Mock<IntegrationMiddleWayContext> SetupMockTransformationsContext() // IQueryable<Processes> process, IQueryable<Transformations> data)
        {
            //var mockProcess = new Mock<DbSet<Processes>>();
            //mockProcess.As<IQueryable<Processes>>().Setup(m => m.Provider).Returns(process.Provider);
            //mockProcess.As<IQueryable<Processes>>().Setup(m => m.Expression).Returns(process.Expression);
            //mockProcess.As<IQueryable<Processes>>().Setup(m => m.ElementType).Returns(process.ElementType);
            //mockProcess.As<IQueryable<Processes>>().Setup(m => m.GetEnumerator()).Returns(process.GetEnumerator());

            //var mockTransformations = new Mock<DbSet<Transformations>>();
            //mockTransformations.As<IQueryable<Transformations>>().Setup(m => m.Provider).Returns(data.Provider);
            //mockTransformations.As<IQueryable<Transformations>>().Setup(m => m.Expression).Returns(data.Expression);
            //mockTransformations.As<IQueryable<Transformations>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //mockTransformations.As<IQueryable<Transformations>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IntegrationMiddleWayContext>();
            //mockContext.Setup(c => c.Processes).Returns(mockProcess.Object);
            //mockContext.Setup(c => c.Transformations).Returns(mockTransformations.Object);

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
            var mockContext = SetupMockTransformationsContext(); // processes, transformations);

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
            var mockContext = SetupMockTransformationsContext(); // processes, transformations);

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

        //protected I Get() { }

        protected IQueryable<Processes> GetMockProcessData()
        {
            var processes = new List<Processes> { new Processes { ProcessUid = 1, Client = "Test", ProcessName = "Module", Description = "Test Module", Enabled = true, CreatedDate = DateTime.Now } }.AsQueryable();
            return processes;
        }

        protected IQueryable<Transformations> GetMockTransformationData()
        {
            var data = new List<Transformations>
                        {
                            new Transformations {
                                TransformationUid  = 0,
                                ProcessUid = 1,
                                Function = "rounddown",
                                Parameters = "",
                                SourceColumn = "number",
                                DestinationColumn = "unknown",
                                Enabled = true,
                                Order = 0
                            },
                            new Transformations {
                                TransformationUid  = 1,
                                ProcessUid = 1,
                                Function = "default",
                                Parameters = "[0]",
                                SourceColumn = "value",
                                DestinationColumn = "clone",
                                Enabled = true,
                                Order =1
                            },
                            new Transformations {
                                TransformationUid  = 2,
                                ProcessUid = 1,
                                Function = "split",
                                Parameters = "[;][0,3]",
                                SourceColumn = "largeString",
                                DestinationColumn = "compoundnumber",
                                Enabled = true,
                                Order =1
                            },
                            new Transformations {
                                TransformationUid  = 2,
                                ProcessUid = 1,
                                Function = "default",
                                Parameters = "",
                                SourceColumn = "largestring",
                                DestinationColumn = "compoundnumber",
                                Enabled = true,
                                Order =2
                            },
                            new Transformations {
                                TransformationUid  = 2,
                                ProcessUid = 1,
                                Function = "rounddown",
                                Parameters = "",
                                SourceColumn = "largestring",
                                DestinationColumn = "compoundnumber",
                                Enabled = true,
                                Order =3
                            },
                            new Transformations {
                                TransformationUid  = 2,
                                ProcessUid = 1,
                                Function = "lookup",
                                Parameters = "",
                                SourceColumn = "reference",
                                DestinationColumn = "referenced",
                                Enabled = true,
                                Order =3
                            },
                        }.AsQueryable();

            return data;
        }

        protected IQueryable<TransformationLookup> GetMockTransformationLookupData()
        {
            var data = new List<TransformationLookup>
                        {
                            new TransformationLookup {
                                TransformationLookupUid  = 0,
                                ProcessUid = 1,
                                TransformationLookupKey = "test",
                                Key = "0",
                                Value = "zero",
                                Enabled = true
                            },
                            new TransformationLookup {
                                TransformationLookupUid  = 1,
                                ProcessUid = 1,
                                TransformationLookupKey = "test",
                                Key = "1",
                                Value = "one",
                                Enabled = true
                            },
                            new TransformationLookup {
                                TransformationLookupUid  = 2,
                                ProcessUid = 1,
                                TransformationLookupKey = "test",
                                Key = "2",
                                Value = "two",
                                Enabled = true
                            },
                            new TransformationLookup {
                                TransformationLookupUid  = 3,
                                ProcessUid = 1,
                                TransformationLookupKey = "test",
                                Key = "3",
                                Value = "three",
                                Enabled = true
                            },
                            new TransformationLookup {
                                TransformationLookupUid  = 4,
                                ProcessUid = 1,
                                TransformationLookupKey = "test",
                                Key = "4",
                                Value = "four",
                                Enabled = true
                            },
                            new TransformationLookup {
                                TransformationLookupUid  = 5,
                                ProcessUid = 1,
                                TransformationLookupKey = "test",
                                Key = "5",
                                Value = "five",
                                Enabled = true
                            },
                        }.AsQueryable();

            return data;
        }

        #endregion Setup MockObjects

    }
}