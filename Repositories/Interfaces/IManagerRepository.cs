using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HarnyCardApplication.Models;

namespace HarnyCardApplication.Repositories.Interfaces
{
    public interface IManagerRepository : IBaseRepository<Manager>
    {
        Task<Manager> Get(int id);
        Task<Manager> Get(Expression<Func<Manager, bool>> expression);
        Task<List<Manager>> GetAll();
    }
}