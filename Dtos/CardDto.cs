using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }
        public string Pin { get; set; }
        public bool IsUsed { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int NetworkId { get; set; }
        public string NetworkName { get; set; }
        public int CategoryId { get; set; }
        public double CategoryPrice { get; set; }
    }
    public class CreateCardRequestModel
    {
        public string NetworkName { get; set; }
        public double CategoryPrice { get; set; }
    }
    public class BuyCardRequestModel
    {
        public string NetworkName { get; set; }
        public double Price { get; set; }
    }
}