using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Models.Request {
    public class ProductSearchRequest {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Size { get; set; }
        public string Highlight { get; set; }

        public bool IsEmpty {
            get { return MinPrice == 0 && MaxPrice == 0 && Size == null && Highlight == null; }
        }

        public override string ToString() {
            return $"Search Request Details: Min Price: {MinPrice}, Max Price: {MaxPrice}, Size: {Size}, Highlight: {Highlight}";
        }
    }
}
