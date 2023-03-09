using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HarnyCardApplication.Models;

namespace HarnyCardApplication.Repositories.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer> Get(int id);
        Task<Customer> Get(Expression<Func<Customer, bool>> expression);
        Task<IList<Customer>> GetAll();

    }
}