using ProductApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Models.Response {
    public class ProductSearchResponse {
        public IEnumerable<Product> Products { get; set; }
        public SearchMetadata Metadata { get; set; }
    }
}
