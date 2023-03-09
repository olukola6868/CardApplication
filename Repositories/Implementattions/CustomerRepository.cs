using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HarnyCardApplication.ApplicationContext;
using HarnyCardApplication.Models;
using HarnyCardApplication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HarnyCardApplication.Repositories.Implementattions
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ContextClass context)
        {
            _context = context;
        }
        public async Task<Customer> Get(int id)
        {
            return await _context.Customers
                .Include(c => c.User)
                .Include(c => c.Cards)
                .FirstOrDefaultAsync(d => d.UserId == id);
        }

        public async Task<Customer> Get(Expression<Func<Customer, bool>> expression)
        {
            return await _context.Customers
                .Include(c => c.User)
                .Include(c => c.Cards)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IList<Customer>> GetAll()
        {
            return await _context.Customers
                .Include(c => c.User)
                .Include(c => c.Cards)
                .ToListAsync();
        }
    }
}