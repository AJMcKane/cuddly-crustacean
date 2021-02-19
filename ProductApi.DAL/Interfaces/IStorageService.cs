using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.DAL.Interfaces {
    public interface IStorageService<T> where T : class  {
        public Task<IEnumerable<T>> GetAll();
    }
}
