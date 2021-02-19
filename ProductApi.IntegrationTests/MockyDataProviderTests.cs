using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using ProductApi.Core.Interfaces;
using ProductApi.DAL.Interfaces;
using ProductApi.DAL.Models;
using ProductApi.DAL.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ProductApi.Tests {
    public class MockyDataProviderTests : IClassFixture<MockyDataProviderData> {
        private MockyProductDataProviderService providerService;
        private Mock<IHttpClientFactoryService> mockClientFactory;
        private Mock<ICustomLogger> mockLogger;
        private Mock<IHttpClientWrapperService> mockHttpClient;
        private MockyDataProviderData fixture;

        public MockyDataProviderTests(MockyDataProviderData fixture) {
            this.fixture = fixture;
            this.mockClientFactory = new Mock<IHttpClientFactoryService>();
            this.mockLogger = new Mock<ICustomLogger>();
            this.mockHttpClient = new Mock<IHttpClientWrapperService>();   
            
        }

        [Fact]
        public async Task  MockyDataProviderLogsApiResponse() {
            mockHttpClient.Setup(s => s.Get(It.IsAny<string>())).Returns(async () => new HttpResponseMessage() { 
                Content = new StringContent(JsonConvert.SerializeObject(fixture)) 
            });
            mockClientFactory.Setup(p => p.CreateClient()).Returns(mockHttpClient.Object);
            providerService = new MockyProductDataProviderService(mockClientFactory.Object, mockLogger.Object, "https://google.com");


            var result = await providerService.GetAll();
            mockLogger.Verify(p => p.Log(It.IsAny<LogLevel>(), It.Is<string>(p => p.Contains("Test Item2") && p.Contains("test1"))), Times.Once);
        }
    }

    public class MockyDataProviderData {
        public MockyDataProviderData () {
            Products = new List<Product>() {
                { new Product() { Description = "test1", Price = 0, Sizes = new string[] {"small"}, Title = "Test Item1" } },
                { new Product() { Description = "test2", Price = 0, Sizes = new string[] {"small"}, Title = "Test Item2" } }
            };
        }

        public IEnumerable<Product> Products { get; set; }
    }

    
}
