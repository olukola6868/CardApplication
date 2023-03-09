using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<T> Create(T entity);
        Task<T> Update(T entity);

    }
}