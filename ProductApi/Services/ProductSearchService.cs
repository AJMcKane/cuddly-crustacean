using Microsoft.Extensions.Logging;
using ProductApi.Core.Interfaces;
using ProductApi.DAL.Interfaces;
using ProductApi.DAL.Models;
using ProductApi.Extensions;
using ProductApi.Interfaces;
using ProductApi.Models;
using ProductApi.Models.Request;
using ProductApi.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Services {
    public class ProductSearchService : IProductSearchService {
        private readonly ICustomLogger logger;
        private readonly IStorageService<Product> storageService;
        private const string HIGHLIGHT_DELIMETER = ",";

        public ProductSearchService(ICustomLogger logger, IStorageService<Product> storageService) {
            this.logger = logger;
            this.storageService = storageService;
        }

        public async Task<ProductSearchResponse> SearchProducts(ProductSearchRequest request) {
            logger.Log(LogLevel.Information, "Starting search");
            var products = await storageService.GetAll();
            if (!request.IsEmpty) {
                products = products.Where(p => MatchesCriteria(p, request));
            }

            var result = new ProductSearchResponse() {
                Metadata = new SearchMetadata() {
                    AvailableSizes = products.SelectMany(p => p.Sizes).Distinct(),
                    MaxAvailablePrice = products.Max(p => p.Price),
                    MinAvailablePrice = products.Min(p => p.Price),
                    CommonTerms = GetMostCommonWords(5, 10, products.SelectMany(p => p.Description?.Split(" ")))
                },
                Products = request.Highlight?.Count() > 0 ? await products.HighlightProductDescriptions(request.Highlight.Split(HIGHLIGHT_DELIMETER)) : products
            };

            logger.Log(LogLevel.Information, $"Search MetaData: {result.Metadata}");
            return result;
        }

        private IEnumerable<string> GetMostCommonWords(int numberOfTopWordsToSkip, int numberofTopWordsToTake, IEnumerable<string> words) {
            return words?.Select(word => string.Concat(word.Where(c => !char.IsPunctuation(c))))
                        .GroupBy(word => word, StringComparer.InvariantCultureIgnoreCase)
                        .OrderByDescending(group => group.Count())
                        .Select(group => group.Key)
                        .Skip(numberOfTopWordsToSkip)
                        .Take(numberofTopWordsToTake);
        }

        private bool MatchesCriteria(Product product, ProductSearchRequest request) {
            bool isMatch = false;
            // we won't return negatively priced items
            isMatch = product.Price >= request.MinPrice;
            if (request.MaxPrice > 0 && isMatch) {
                isMatch = product.Price <= request.MaxPrice;
            }

            if (!string.IsNullOrEmpty(request.Size) && isMatch) {
                isMatch = product.Sizes.Any(p => p.Equals(request.Size, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(request.Highlight) && isMatch) {
                //The ugly cheat we discussed
                var highlights = request.Highlight.Split(HIGHLIGHT_DELIMETER);
                foreach (var highlight in highlights) {
                    isMatch = product.Description.Contains(highlight, StringComparison.InvariantCultureIgnoreCase);
                    if (isMatch) {
                        break;
                    }
                }
            }

            return isMatch;
        }
    }
}
