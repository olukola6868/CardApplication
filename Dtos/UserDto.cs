using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public IList<RoleDto> Roles { get; set; }
    }

    public class LoginUserRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public IList<RoleDto> Roles { get; set; }
    }
}