using Moq;
using ProductApi.Core.Interfaces;
using ProductApi.DAL.Interfaces;
using ProductApi.DAL.Models;
using ProductApi.Models;
using ProductApi.Models.Request;
using ProductApi.Models.Response;
using ProductApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductApi.Tests {
    public class SearchServiceTests : IClassFixture<SearchServiceStorageFixture> {
        private SearchServiceStorageFixture fixture;
        private ProductSearchService searchService;
        private Mock<IStorageService<Product>> mockStorageService;
        private Mock<ICustomLogger> mockLogger;

        /// <summary>
        /// This is just an example of some of the testing that'd be required to test this service, it is not intended to be 
        /// 100% (or anywhere close to full test coverage)
        /// </summary>
        /// <param name="fixture"></param>
        public SearchServiceTests(SearchServiceStorageFixture fixture) {
            this.fixture = fixture;
            mockStorageService = new Mock<IStorageService<Product>>();
            mockLogger = new Mock<ICustomLogger>();

        }

        public static IEnumerable<object[]> RequestAndResponse = new List<object[]>() {            
            new object[] {
                new ProductSearchRequest() {
                    Highlight = "desc1",
                    MaxPrice = 1,
                    MinPrice = 0,
                    Size = "medium"
                },
                new ProductSearchResponse() {
                    Products = new List<Product>() {
                        new Product() {
                            Description = @"<em>Desc1</em>",
                            Price = 1,
                            Sizes = new string[] {"small, medium"},
                            Title = "Title1"
                        }
                    },
                    Metadata = new SearchMetadata() {
                        MaxAvailablePrice = 1
                    }
                }
            },
            new object[] {
                new ProductSearchRequest() {
                    Highlight = null,
                    MaxPrice = 20,
                    MinPrice = 11,
                    Size = "large"
                },
                new ProductSearchResponse() {
                    Products = new List<Product>() {
                        new Product() {
                            Description = @"Desc3",
                            Price = 20,
                            Sizes = new string[] {"large"},
                            Title = "Title3"
                        }
                    },
                    Metadata = new SearchMetadata() {
                        MaxAvailablePrice = 20
                    }
                }
            }            
        };



        [Theory]
        [MemberData(nameof(RequestAndResponse))]
        public async Task SearchServiceReturnsFilteredResults(ProductSearchRequest request, ProductSearchResponse expectedResponse) {
            mockStorageService.Setup(p => p.GetAll()).Returns(async () => fixture.Products);
            searchService = new ProductSearchService(mockLogger.Object, mockStorageService.Object);
            var result = await searchService.SearchProducts(request);

            Assert.Equal(expectedResponse.Products.First().Price, result.Products.First().Price);
            if (!string.IsNullOrEmpty(request.Highlight)) {
                Assert.Contains("em", result.Products.First().Description);
            }

            var expectedMaxPriceForRange = fixture.Products.Where(p => p.Price <= request.MaxPrice)
                                                           .OrderByDescending(p => p.Price)
                                                           .First()
                                                           .Price;
            Assert.Equal(expectedResponse.Metadata.MaxAvailablePrice, expectedMaxPriceForRange);
        }

        [Fact]
        public async Task SearchServiceReturnsAllResultsWithNoFilter() {
            mockStorageService.Setup(p => p.GetAll()).Returns(async () => fixture.Products);
            searchService = new ProductSearchService(mockLogger.Object, mockStorageService.Object);
            var result = await searchService.SearchProducts(new ProductSearchRequest());

            Assert.Equal(fixture.Products.Count(), result.Products.Count());
        }
    }

    public class SearchServiceStorageFixture {
        public SearchServiceStorageFixture() {
            Products = new List<Product>() {
                 new Product() {Description = "Desc1", Price = 1, Sizes = new string[] {"small", "medium" }, Title ="Title1" },
                 new Product() {Description = "Desc2", Price = 10, Sizes = new string[] {"small", "medium" }, Title ="Title2" },
                 new Product() {Description = "Desc3", Price = 20, Sizes = new string[] {"large" }, Title ="Title3" }
            };
        }

        public IEnumerable<Product> Products { get; set; }
    }
}
