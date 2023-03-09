using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Models
{
    public class Network : BaseEntity
    {
        public string Name{get; set;}
        public IList<Card> Cards{get; set;}
        public IList<NetworkCategory> NetworkCategories{get; set;}
    }
}