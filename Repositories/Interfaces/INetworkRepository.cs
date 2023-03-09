using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HarnyCardApplication.Models;

namespace HarnyCardApplication.Repositories.Interfaces
{
    public interface INetworkRepository : IBaseRepository<Network>
    {
        Task<Network> Get (int id);
        Task<Network> Get (Expression<Func<Network, bool>> expression);
        Task<IList<Network>> GetAll();
    }
}