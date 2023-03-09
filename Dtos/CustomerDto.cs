using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public IList<RoleDto> Roles { get; set; }
        public double Wallet { get; set; }
        public IList<CardDto> Cards { get; set; }
    }

    public class CreateCustomerRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UpdateCustomerRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class FundWalletRequestModel
    {
        public double Amount { get; set; }
    }
    public class CustomerResponseModel : BaseResponse<CustomerDto>
    {

    }
    public class CustomersResponseModels : BaseResponse<List<CustomerDto>>
    {

    }
}