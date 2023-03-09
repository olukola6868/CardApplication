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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ContextClass context)
        {
            _context = context;
        }
        public async Task<Role> Get(int id)
        {
            return await _context.Roles
                .Include(c => c.UserRoles).ThenInclude(d=> d.User)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Role> Get(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles
              .Include(c => c.UserRoles).ThenInclude(d=> d.User)
              .FirstOrDefaultAsync(expression);
        }

        public async Task<IList<Role>> GetAll()
        {
            return await _context.Roles
             .Include(c => c.UserRoles).ThenInclude(d=> d.User)
             .ToListAsync();
        }
    }
}