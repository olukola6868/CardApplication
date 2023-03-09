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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
         public UserRepository(ContextClass context)
        {
            _context = context;
        }
        public async Task<User> Get(int id)
        {
            return await _context.Users
                .Include(c => c.UserRoles).ThenInclude(a => a.Role)
                .Include(c => c.Customer)
                .Include(c => c.Manager)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<User> Get(Expression<Func<User, bool>> expression)
        {
            return await _context.Users
                .Include(c => c.UserRoles).ThenInclude(a => a.Role)
                .Include(c => c.Customer)
                .Include(c => c.Manager)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IList<User>> GetAll()
        {
            return await _context.Users
                .Include(c => c.UserRoles).ThenInclude(a => a.Role)
                .Include(c => c.Customer)
                .Include(c => c.Manager)
                .ToListAsync();
        }
    }
}