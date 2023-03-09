using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HarnyCardApplication.Models;

namespace HarnyCardApplication.Repositories.Interfaces
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> Get(int id);
        Task<Role> Get(Expression<Func<Role, bool>> expression);
        Task<IList<Role>> GetAll();
    }
}