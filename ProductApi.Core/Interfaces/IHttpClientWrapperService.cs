using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Core.Interfaces {
    public interface IHttpClientWrapperService {
        public Task<HttpResponseMessage> Get(string requestUri);
    }
}
