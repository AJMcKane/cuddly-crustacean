using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductApi.Core.Interfaces {
    public interface IHttpClientFactoryService {
        public IHttpClientWrapperService CreateClient();
    }
}
