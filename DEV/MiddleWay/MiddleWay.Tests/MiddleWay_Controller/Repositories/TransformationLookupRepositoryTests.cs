using Microsoft.EntityFrameworkCore;
using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_Controller.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MiddleWay.Tests.MiddleWay_Controller.Repositories
{
    public class TransformationLookupRepositoryTests
    {
        /*https://msdn.microsoft.com/en-us/library/dn314429%28v=vs.113%29.aspx?f=255&MSPPError=-2147217396
            var data = new List<Blog> 
            { 
                new Blog { Name = "BBB" }, 
                new Blog { Name = "ZZZ" }, 
                new Blog { Name = "AAA" }, 
            }.AsQueryable(); 

            var mockSet = new Mock<DbSet<Blog>>(); 
            mockSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(data.Provider); 
            mockSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(data.Expression); 
            mockSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(data.ElementType); 
            mockSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(0 => data.GetEnumerator()); 

            var mockContext = new Mock<BloggingContext>(); 
            mockContext.Setup(c => c.Blogs).Returns(mockSet.Object); 
            mockContext.DefaultValue = DefaultValue.Mock; // To allow moq to create default non-null values for non-sealed classes
            mockContext.SetupProperty(x => x.[PROPERTY]); // to allow mock to track changes done to objects for later reference (i.e. in assets)
            mockContext.SetupAllProperties(); // Same as above but for all properties of the mocked object

            var service = new BlogService(mockContext.Object); 
            var blogs = service.GetAllBlogs(); 

            Assert.AreEqual(3, blogs.Count); 
            Assert.AreEqual("AAA", blogs[0].Name); 
            Assert.AreEqual("BBB", blogs[1].Name); 
            Assert.AreEqual("ZZZ", blogs[2].Name); 

         */

        [Fact]
        public void LookupInvalidByUid()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookup(10);

            Assert.Null(lookup);
        }

        [Fact]
        public void LookupInvalidByKeys()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookup(1, "test", "invalid");

            Assert.Null(lookup);
        }

        [Fact]
        public void LookupInvalidByProcessAndKeys()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookup("Test", "Module", "test", "invalid");

            Assert.Null(lookup);
        }

        [Fact]
        public void LookupNull()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookup(1, "test", null);

            Assert.Null(lookup);
        }

        [Fact]
        public void LookupEmptyString()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookup(1, "test", string.Empty);

            Assert.Null(lookup);
        }

        [Fact]
        public void LookupValidByUid()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookup(1);

            Assert.NotNull(lookup);
        }

        [Fact]
        public void LookupValidByKeys()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookup(1, "test", "2");

            Assert.NotNull(lookup);
        }

        [Fact]
        public void LookupValidByProcessAndKeys()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookup("Test", "Module", "test", "2");

            Assert.NotNull(lookup);
        }

        [Fact]
        public void LookupEmptyList()
        {
            var lookupRepository = SetupMockLookupRepository(true);

            var lookup = lookupRepository.SelectTransformationLookup(1, "test", "not empty");

            Assert.Null(lookup);
        }

        [Fact]
        public void LookupValueValidByUid()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookupValue(1);

            Assert.Equal("one", lookup);
        }

        [Fact]
        public void LookupValueValidByKeys()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookupValue(1, "test", "1");

            Assert.Equal("one", lookup);
        }

        [Fact]
        public void LookupValueValidByProcessAndKeys()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookupValue("Test", "Module", "test", "1");

            Assert.Equal("one", lookup);
        }

        [Fact]
        public void LookupValueNull()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookupValue(1, "test", null);

            Assert.Null(lookup);
        }

        [Fact]
        public void LookupValueEmptyString()
        {
            var lookupRepository = SetupMockLookupRepository();

            var lookup = lookupRepository.SelectTransformationLookupValue(1, "test", string.Empty);

            Assert.Null(lookup);
        }

        [Fact]
        public void LookupValueEmptyList()
        {
            var lookupRepository = SetupMockLookupRepository(true);

            var lookup = lookupRepository.SelectTransformationLookupValue(1, "test", "1");

            Assert.Null(lookup);
        }

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

        protected TransformationLookupRepository SetupMockLookupRepository(bool useEmptyList = false)
        {
            IQueryable<Processes> processes = new List<Processes> { new Processes { ProcessUid = 1, Client = "Test", ProcessName = "Module", Description = "Test Module", Enabled = true, CreatedDate = DateTime.Now } }.AsQueryable();
            IQueryable<TransformationLookup> data;

            if (useEmptyList)
            {
                data = new List<TransformationLookup>().AsQueryable();
            }
            else
            {
                data = new List<TransformationLookup>
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
            }

            var context = SetupTransformationLookupContext(processes, data);

            return new TransformationLookupRepository(context.Object);

        }

    }
}
