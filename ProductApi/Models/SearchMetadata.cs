using System.Collections;
using System.Collections.Generic;

namespace ProductApi.Models {
    public class SearchMetadata {
        public decimal MinAvailablePrice { get; set; }
        public decimal MaxAvailablePrice { get; set; }
        public IEnumerable<string> AvailableSizes { get; set; }
        public IEnumerable<string> CommonTerms { get; set; }

        public override string ToString() {
            return $"Min Available Price: {MinAvailablePrice}, " +
                $"Max Available Price: {MaxAvailablePrice}," +
                $" Available Sizes: {string.Join(", ", AvailableSizes)}, " +
                $"Common Terms: {string.Join(", ", CommonTerms)}";
        }
    }
}
