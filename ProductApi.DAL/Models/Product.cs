using System;
using System.Collections;
using System.Collections.Generic;

namespace ProductApi.DAL.Models {
    
    /// <summary>
    /// For this demo we're using this simple product representation everywhere,
    /// however in a fully fleshed out solution we'd separate the Raw Product model from our DTOs
    /// in order to decouple them / allow SDK specific annotations
    /// </summary>
    public class Product {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> Sizes {get; set;}
        public string Description { get; set; }
    }
}
