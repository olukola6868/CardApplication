using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Models
{
    public class User : BaseEntity
    {
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public string Email{get;set;}
        public string Password{get;set;}
        public string PhoneNumber{get;set;} 
        public IList<UserRole> UserRoles{get;set;} = new List<UserRole>();
        public Customer Customer{get; set;} 
        public Manager Manager{get; set;}
    }
}