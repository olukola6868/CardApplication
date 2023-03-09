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
    public class NetworkRepository : BaseRepository<Network>, INetworkRepository
    {
        public NetworkRepository(ContextClass context)
        {
            _context = context;
        }
        public async Task<Network> Get(int id)
        {
            return await _context.Networks
                .Include(c => c.Cards)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Network> Get(Expression<Func<Network, bool>> expression)
        {
            return await _context.Networks
                .Include(c => c.Cards)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IList<Network>> GetAll()
        {
            return await _context.Networks
                .Include(c => c.Cards)
                .ToListAsync();
        }
    }
}