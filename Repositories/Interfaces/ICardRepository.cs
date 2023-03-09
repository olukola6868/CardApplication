using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HarnyCardApplication.Models;

namespace HarnyCardApplication.Repositories.Interfaces
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        Task<Card> Get(int id);
        Task<Card> Get(Expression<Func<Card, bool>> expression);
        Task<IList<Card>> GetAll();
        Task<IList<Card>> GetIsAvailableCards();
        Task<IList<Card>> GetCutomerCards(int customerId);
    }
}