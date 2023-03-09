using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Models
{
    public class NetworkCategory : BaseEntity
    {
        public int NetworkId{get; set;}
        public Network Network{get; set;}
        public int CategoryId{get; set;}
        public Category Category{get; set;}
    }
}