using ProductApi.Core.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductApi.Core.Services {
    public class HttpClientWrapperService : IHttpClientWrapperService { 
        private HttpClient httpClient;

        public HttpClientWrapperService(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> Get(string requestUri) {
            return await httpClient.GetAsync(requestUri);
        }
    }
}
