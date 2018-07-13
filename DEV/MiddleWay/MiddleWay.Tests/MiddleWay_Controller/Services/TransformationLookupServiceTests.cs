using MiddleWay_Controller.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using MiddleWay_DTO.Models.MiddleWay;
using System.Linq;
using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_Controller.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MiddleWay.Tests.MiddleWay_Controller.Services
{
    public class TransformationLookupServiceTests
    {
        #region Properties and Variables

        //TransformationLookupService _transformationLookupService;

        #endregion Properties and Variables

        //#region Constructor

        //public TransformationLookupServiceTests(TransformationLookupService transformationLookupService)
        //{
        //    _transformationLookupService = transformationLookupService;
        //}

        //#endregion Constructor

        #region Transformation Lookup Tests

        protected TransformationLookupModel ReturnNull()
        {
            return null;
        }

        [Fact]
        public void GetTransformationLookup_GetInvalidTransformationByUid()
        {
            var clientConfiguration = SetupClientConfiguration();
            var transformationLookupRepository = SetupTransformationLookupRepository(GetMockProcessData(), GetMockTransformationLookupData());

            //transformationLookupRepository.Setup(x => x.SelectTransformationLookup(0)).Returns(ReturnNull());

            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            var lookup = service.GetTransformationLookup(-1);

            Assert.Null(lookup);
        }
        //TransformationLookupModel (int transformationLookupUid);
        [Fact]
        public void GetTransformationLookup_GetValidTransformationByUid()
        {
            var clientConfiguration = SetupClientConfiguration();
            var transformationLookupRepository = SetupTransformationLookupRepository(GetMockProcessData(), GetMockTransformationLookupData());
            //var transformationLookupService = SetupTransformationLookupService();

            //transformationLookupRepository.Setup(x => x.GetTransformationLookup()).Return();

            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            var lookup = service.GetTransformationLookup(1);

            Assert.NotNull(lookup);
        }
        //TransformationLookupModel (int transformationLookupUid);

        [Fact]
        public void GetTransformationLookupValue_GetInvalidTransformationByUid()
        {
            var clientConfiguration = SetupClientConfiguration();
            var transformationLookupRepository = SetupTransformationLookupRepository(GetMockProcessData(), GetMockTransformationLookupData());

            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            var lookup = service.GetTransformationLookupValue(-1);

            Assert.Null(lookup);
        }
        // string (int transformationLookupUid);
        [Fact]
        public void GetTransformationLookupValue_GetValidTransformationByUid()
        {
            var clientConfiguration = SetupClientConfiguration();
            var transformationLookupRepository = SetupTransformationLookupRepository(GetMockProcessData(), GetMockTransformationLookupData());

            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            var lookup = service.GetTransformationLookupValue(1);

            Assert.NotNull(lookup);
        }
        // string (int transformationLookupUid);

        [Fact]
        public void GetTransformationLookup_GetInvalidByProcessUidAndLookupKeys()
        {
            var clientConfiguration = SetupClientConfiguration();
            var transformationLookupRepository = SetupTransformationLookupRepository(GetMockProcessData(), GetMockTransformationLookupData());

            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            var lookup = service.GetTransformationLookup(1, "test", "invalid");

            Assert.Null(lookup);
        } // TransformationLookupModel (int processUid, string transformationLookupKey, string key);

        [Fact]
        public void GetTransformationLookup_GetValidByProcessUidAndLookupKeys()
        {
            var clientConfiguration = SetupClientConfiguration();
            var transformationLookupRepository = SetupTransformationLookupRepository(GetMockProcessData(), GetMockTransformationLookupData());

            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            var lookup = service.GetTransformationLookup(1, "test", "1");

            Assert.NotNull(lookup);
        }

        [Fact]
        public void GetTransformationLookupValue_GetValidByProcessUidAndLookupKeys()
        {
            var clientConfiguration = SetupClientConfiguration();
            var transformationLookupRepository = SetupTransformationLookupRepository(GetMockProcessData(), GetMockTransformationLookupData());

            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            var lookup = service.GetTransformationLookupValue(1, "test", "invalid");

            Assert.Null(lookup);
        } // TransformationLookupModel (int processUid, string transformationLookupKey, string key);

        [Fact]
        public void GetTransformationLookupValue_GetInvalidByProcessUidAndLookupKeys()
        {
            var clientConfiguration = SetupClientConfiguration();
            var transformationLookupRepository = SetupTransformationLookupRepository(GetMockProcessData(), GetMockTransformationLookupData());

            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            var lookup = service.GetTransformationLookupValue(1, "test", "0");

            Assert.NotNull(lookup);
        } // string (int processUid, string transformationLookupKey, string key);

        //[Fact]
        //public void GetTransformationLookupValue_GetInvalidTransformationByUid()
        //{
        //    var mockTransformationLookupRepository = new Mock<ITransformationLookupRepository>();
        //    var mockClientConfiguration = new Mock<IClientConfiguration>();

        //    mockTransformationLookupRepository.Setup(x => x.SelectTransformationLookupValue(0)).Returns("Test");

        //    var sut = new TransformationLookupService(mockTransformationLookupRepository.Object, mockClientConfiguration.Object);

        //    var result = sut.GetTransformationLookupValue(0);


        //    //Assert.Equal(null, result);
        //    Assert.Null(result);

        //}

        [Fact]
        public void GetTransformationLookupData_ValidByProcessUidAndKeys()
        {
            var clientConfiguration = SetupClientConfiguration();
            var transformationLookupRepository = SetupTransformationLookupRepository(GetMockProcessData(), GetMockTransformationLookupData());

            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            var lookup = service.GetTransformationLookupData(1, "Test");

            Assert.True(lookup.Count > 0);
        } // string GetTransformationLookupValue(int processUid, string transformationLookupKey, string key);

        [Fact]
        public void GetTransformationLookupData_InvalidByProcessUidAndKeys()
        {
            var clientConfiguration = SetupClientConfiguration();
            var transformationLookupRepository = SetupTransformationLookupRepository(GetMockProcessData(), GetMockTransformationLookupData());

            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            var lookup = service.GetTransformationLookupData(1, "Invalid");

            Assert.True(lookup.Count == 0);
        } // TransformationLookupModel GetTransformationLookup(string transformationLookupKey, string key);

        [Fact]
        public void GetTransformationLookupData_ValidByKeys()
        {
            var clientConfiguration = SetupClientConfiguration();
            var transformationLookupRepository = SetupTransformationLookupRepository(GetMockProcessData(), GetMockTransformationLookupData());

            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            var lookup = service.GetTransformationLookupData("Test");

            Assert.True(lookup.Count > 0);
        } // TransformationLookupModel GetTransformationLookup(string transformationLookupKey, string key);

        [Fact]
        public void GetTransformationLookupData_InvalidByKeys()
        {
            var clientConfiguration = SetupClientConfiguration();
            var transformationLookupRepository = SetupTransformationLookupRepository(GetMockProcessData(), GetMockTransformationLookupData());

            var service = new TransformationLookupService(transformationLookupRepository, clientConfiguration.Object);

            var lookup = service.GetTransformationLookupData("Invalid");

            Assert.True(lookup.Count == 0);
        } // string GetTransformationLookupValue(string transformationLookupKey, string key);

        #endregion Transformation Lookup Tests

        #region Setup MockObjects

        protected Mock<IntegrationMiddleWayContext> SetupTransformationLookupContext(IQueryable<Processes> process, IQueryable<TransformationLookup> data)
        {
            var mockProcess = new Mock<DbSet<Processes>>();
            mockProcess.As<IQueryable<Processes>>().Setup(m => m.Provider).Returns(process.Provider);
            mockProcess.As<IQueryable<Processes>>().Setup(m => m.Expression).Returns(process.Expression);
            mockProcess.As<IQueryable<Processes>>().Setup(m => m.ElementType).Returns(process.ElementType);
            mockProcess.As<IQueryable<Processes>>().Setup(m => m.GetEnumerator()).Returns(process.GetEnumerator());

            var mockLookups = new Mock<DbSet<TransformationLookup>>();
            mockLookups.As<IQueryable<TransformationLookup>>().Setup(m => m.Provider).Returns(data.Provider);
            mockLookups.As<IQueryable<TransformationLookup>>().Setup(m => m.Expression).Returns(data.Expression);
            mockLookups.As<IQueryable<TransformationLookup>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockLookups.As<IQueryable<TransformationLookup>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IntegrationMiddleWayContext>();
            mockContext.Setup(c => c.Processes).Returns(mockProcess.Object);
            mockContext.Setup(c => c.TransformationLookup).Returns(mockLookups.Object);

            return mockContext;
        }

        protected Mock<IClientConfiguration> SetupClientConfiguration()
        {
            var mockClientConfiguration = new Mock<IClientConfiguration>();
            mockClientConfiguration.Setup(x => x.Client).Returns("Test");
            mockClientConfiguration.Setup(x => x.ProcessName).Returns("Module");

            return mockClientConfiguration;
        }

        protected ITransformationLookupRepository SetupTransformationLookupRepository(IQueryable<Processes> processes, IQueryable<TransformationLookup> data)
        {
            var context = SetupTransformationLookupContext(processes, data);
            //return new Mock<ITransformationLookupRepository>(context.Object);
            //return new TransformationLookupRepository(context.Object);

            var mockTransformationLookupRepository = new TransformationLookupRepository(context.Object);
            return mockTransformationLookupRepository;
        }

        //protected Mock<ITransformationLookupRepository> SetupTransformationLookupRepository()
        //{
        //    var mockTransformationLookupRepository = new Mock<ITransformationLookupRepository>();
        //    //mockTransformationLookupRepository.Setup(x => x.SelectTransformationLookup(val)).Return(returnValue);
        //    //mockTransformationLookupRepository.Setup(x => x.SelectTransformationLookups()).Return(returnValue);
        //    //mockTransformationLookupRepository.Setup(x => x.SelectTransformationLookupValue()).Return(returnValue);
        //    return mockTransformationLookupRepository;
        //}

        //protected Mock<ITransformationLookupService> SetupTransformationLookupService(Mock<ITransformationLookupRepository> transformationLookupRepository, Mock<IClientConfiguration> clientConfiguration)
        //{
        //    //var clientConfiguration = SetupClientConfiguration();
        //    //var transformationLookupRepository = SetupTransformationLookupRepository();
        //    return new Mock<ITransformationLookupService>(transformationLookupRepository.Object, clientConfiguration.Object);
        //}

        //protected I Get() { }

        protected IQueryable<Processes> GetMockProcessData()
        {
            var processes = new List<Processes> { new Processes { ProcessUid = 1, Client = "Test", ProcessName = "Module", Description = "Test Module", Enabled = true, CreatedDate = DateTime.Now } }.AsQueryable();
            return processes;
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
                        }.AsQueryable();

            return data;
        }

        #endregion Setup MockObjects

    }
}