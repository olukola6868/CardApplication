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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ContextClass context)
        {
            _context = context;
        }
        public async Task<Category> Get(int id)
        {
             return await _context.Categories
                .Include(c => c.Cards)
                .ThenInclude(a => a.Network)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Category> Get(Expression<Func<Category, bool>> expression)
        {
            return await _context.Categories
                .Include(c => c.Cards)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<List<Category>> GetAll()
        {
               return await _context.Categories
                .Include(c => c.Cards)
                .ThenInclude(a => a.Network)
                .ToListAsync();
        }
    }
}