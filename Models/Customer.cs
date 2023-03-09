using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Models
{
    public class Customer : BaseEntity
    {
        public int UserId{get;set;}
        public User User{get; set;}
        public double Wallet{get; set;}
        public IList<Card> Cards{get;set;}
    }
}