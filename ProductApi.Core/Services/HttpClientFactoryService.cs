using ProductApi.Core.Interfaces;
using System.Net.Http;

namespace ProductApi.Core.Services {
    public class HttpClientFactoryService : IHttpClientFactoryService {
        private readonly IHttpClientFactory httpClientFactory;
        public HttpClientFactoryService(IHttpClientFactory httpClientFactory) {
            this.httpClientFactory = httpClientFactory;
        }
        public IHttpClientWrapperService CreateClient() {
            return new HttpClientWrapperService(httpClientFactory.CreateClient());
        }
    }
}
