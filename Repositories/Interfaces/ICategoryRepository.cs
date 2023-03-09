using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HarnyCardApplication.Models;

namespace HarnyCardApplication.Repositories.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> Get (int id);
        Task<Category> Get (Expression<Func<Category, bool>> expression);
        Task<List<Category>> GetAll();
    }
}