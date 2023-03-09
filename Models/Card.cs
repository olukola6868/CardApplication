using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Models
{
    public class Card : BaseEntity
    {
        public string Pin{get; set;}
        public bool IsUsed{get; set;}
        public int? CustomerId{get; set;}
        public Customer Customer{get; set;}
        public int NetworkId{get; set;}
        public Network Network{get; set;}
        public int CategoryId{get;set;}
        public Category Category{get; set;}

    }
}