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
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(ContextClass context)
        {
            _context = context;
        }
        public async Task<Card> Get(int id)
        {
            return await _context.Cards
                .Include(c => c.Category)
                .Include(b => b.Network)
                .Include(a => a.Customer).ThenInclude(a => a.User)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Card> Get(Expression<Func<Card, bool>> expression)
        {
            return await _context.Cards
                .Include(c => c.Category)
                .Include(b => b.Network)
                .Include(a => a.Customer).ThenInclude(a => a.User)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IList<Card>> GetAll()
        {
            return await _context.Cards
                .Include(c => c.Category)
                .Include(b => b.Network)
                .Include(a => a.Customer).ThenInclude(a => a.User)
                .ToListAsync();
        }
          public async Task<IList<Card>> GetCutomerCards(int customerId)
        {
            return await _context.Cards
                .Include(c => c.Category)
                .Include(b => b.Network)
                .Include(a => a.Customer).ThenInclude(a => a.User).Where(a  => a.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IList<Card>> GetIsAvailableCards()
        {
            return await _context.Cards
                .Include(c => c.Category)
                .Include(b => b.Network)
                .Include(a => a.Customer).ThenInclude(a => a.User).Where(a => a.IsUsed == false)
                .ToListAsync();
        }
    }
}