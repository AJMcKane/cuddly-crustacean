using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductApi.Core.Interfaces;
using ProductApi.Core.Services;
using ProductApi.Core.Utilities;
using ProductApi.DAL.Interfaces;
using ProductApi.DAL.Models;
using ProductApi.DAL.Services;
using ProductApi.Interfaces;
using ProductApi.Services;

namespace ProductApi {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddOptions<ProductApiConfig>().Bind(Configuration);
            services.AddControllers();
            services.AddHttpClient();
            services.AddHealthChecks();
            services.AddSingleton<IHttpClientFactoryService, HttpClientFactoryService>()
                    .AddSingleton<IDataProviderService<Product>>(p => new MockyProductDataProviderService(
                        p.GetRequiredService<IHttpClientFactoryService>(),
                        p.GetRequiredService<ICustomLogger>(),
                        p.GetRequiredService<IOptions<ProductApiConfig>>().Value.REMOTE_PRODUCT_ENDPOINT)
                    )
                    .AddScoped<ICustomLogger, ConsoleLogger>()
                    .AddScoped<IStorageService<Product>, ProductStorageService>()
                    .AddScoped<IProductSearchService, ProductSearchService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();
            app.UseAuthorization();
            app.UseHealthChecks("/status");
            

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
