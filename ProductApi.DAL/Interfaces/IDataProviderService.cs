using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.DAL.Interfaces {
    public interface IDataProviderService<T> where T : class {
        public Task<IEnumerable<T>> GetAll();
    }
}
