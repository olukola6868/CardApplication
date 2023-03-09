using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Dtos
{
    public class ManagerDto
    {
        public int Id{get; set;}
        public int UserId{get; set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public string Email{get;set;}
        public string Password{get;set;}
        public string PhoneNumber{get;set;} 
        public double Wallet{get; set;}
        public IList<RoleDto> Roles{get;set;}
    }

    public class CreateManagerRequestModel
    {
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public string Email{get;set;}
        public string Password{get;set;}
        public string PhoneNumber{get;set;}
    }

    public class UpdateManagerRequestModel
    {
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public string PhoneNumber{get;set;}
    }
}