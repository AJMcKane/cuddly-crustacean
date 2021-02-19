using ProductApi.Models.Request;
using ProductApi.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Interfaces {
    public interface IProductSearchService {
        Task<ProductSearchResponse> SearchProducts(ProductSearchRequest request);
    }
}
