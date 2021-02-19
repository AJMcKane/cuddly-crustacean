using ProductApi.DAL.Interfaces;
using ProductApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductApi.DAL.Services {
    /// <summary>
    /// Dummy Storage service class that'd be an IRepository or IEntityService pattern around a real data provider
    /// </summary>
    public class ProductStorageService : IStorageService<Product> {

        private IEnumerable<Product> Products { 
            get {
                //We'd really have a Remote data source not this mocked 
                //in memory collection with a blocking Task execution
                return product ??= productDataProvider.GetAll().Result;
            }
            set { 
                product = value;
            }
        }

        private IEnumerable<Product> product;

        private IDataProviderService<Product> productDataProvider;

        public ProductStorageService(IDataProviderService<Product> productDataProvider) {
            this.productDataProvider = productDataProvider;
        }

        public async Task<IEnumerable<Product>> GetAll() {
            return Products;
        }
    }
}
