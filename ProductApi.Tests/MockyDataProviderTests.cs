using Microsoft.Extensions.Logging;
using Moq;
using ProductApi.Core.Interfaces;
using ProductApi.Core.Services;
using ProductApi.DAL.Interfaces;
using ProductApi.DAL.Services;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ProductApi.IntegrationTests {
    public class MockyDataProviderTests {
        private MockyProductDataProviderService providerService;
        private Mock<IHttpClientFactoryService> mockClientFactory;
        private Mock<ICustomLogger> mockLogger;

        public MockyDataProviderTests() {
            //This is just an example of a quick integration test, in actuality we'd load in the same config as our target environment and get our conn strings
            //and endpoints from there to ensure stability.
            mockClientFactory = new Mock<IHttpClientFactoryService>();
            mockClientFactory.Setup(p => p.CreateClient()).Returns(new HttpClientWrapperService(new HttpClient()));
            mockLogger = new Mock<ICustomLogger>();
            this.providerService = new MockyProductDataProviderService(mockClientFactory.Object, mockLogger.Object, "https://www.mocky.io/v2/5e307edf3200005d00858b49");
        }

        [Fact]
        public async Task EnsureEndpointReturns48Items() {            
            var results = await providerService.GetAll();

            Assert.Equal(48, results.Count());
        }
    }
}
