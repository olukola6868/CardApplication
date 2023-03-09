using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Models
{
    public class Manager : BaseEntity
    {
        public int UserId{get; set;}
        public User User{get; set;}
        public double Wallet{get; set;}

    }
}