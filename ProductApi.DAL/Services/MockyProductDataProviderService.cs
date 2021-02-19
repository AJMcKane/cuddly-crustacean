using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductApi.Core.Interfaces;
using ProductApi.DAL.Interfaces;
using ProductApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApi.DAL.Services {
    public class MockyProductDataProviderService : IDataProviderService<Product> {
        private readonly IHttpClientWrapperService httpClient;
        private readonly string dataSourceEndpoint;
        private readonly ICustomLogger logger;
        public MockyProductDataProviderService(IHttpClientFactoryService httpClientFactory, ICustomLogger logger, string dataSourceEndpoint) {
            this.httpClient = httpClientFactory.CreateClient();
            this.dataSourceEndpoint = dataSourceEndpoint;
            this.logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAll() {
            var response = await httpClient.Get(dataSourceEndpoint);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                logger.Log(LogLevel.Information, content);
                var results = JsonConvert.DeserializeObject<ProductWrapper>(content);
                return results.Products;
            }

            throw new ApplicationException("Unable to source data.");            
        }

        private class ProductWrapper {
            public IEnumerable<Product> Products { get; set; }
        }
    }
}
