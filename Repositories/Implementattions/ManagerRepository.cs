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
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
         public ManagerRepository(ContextClass context)
        {
            _context = context;
        }
        public async Task<Manager> Get(int id)
        {
             return await _context.Managers
                .Include(c => c.User)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Manager> Get(Expression<Func<Manager, bool>> expression)
        {
            return await _context.Managers
                .Include(c => c.User)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<List<Manager>> GetAll()
        {
            return await _context.Managers
                .Include(c => c.User)
                .ToListAsync();
        }
    }
}