using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductApi.Core.Interfaces;
using ProductApi.DAL.Interfaces;
using ProductApi.DAL.Models;
using ProductApi.Interfaces;
using ProductApi.Models.Request;

namespace ProductApi.Controllers {
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class ProductsController : ControllerBase {
        private readonly ICustomLogger logger;
        private IProductSearchService searchService;

        public ProductsController(ICustomLogger logger, IStorageService<Product> storageService, IProductSearchService searchService) {
            this.logger = logger;
            this.searchService = searchService;
        }

        [HttpGet]
        [Route("filter")]
        //If we're locking into comma delimiting our array of params for a field, we'd take the time to implement a custom model binder here
        //for time's sake I'm going to cheat and just split on "," downstream
        public async Task<IActionResult> Filter([FromQuery] ProductSearchRequest searchParams) {
            logger.Log(LogLevel.Information, $"Search Passed in: {searchParams}");
            var result = await searchService.SearchProducts(searchParams); 
            return Ok(result);
        }
    }
}
