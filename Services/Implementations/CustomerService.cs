using HarnyCardApplication.Dtos;
using HarnyCardApplication.Models;
using HarnyCardApplication.Repositories.Interfaces;
using HarnyCardApplication.Services.Interfaces;

namespace HarnyCardApplication.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public CustomerService(ICustomerRepository customerRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<BaseResponse<CustomerDto>> Create(CreateCustomerRequestModel model)
        {
            var customerExists = await _customerRepository.Get(c => c.User.Email == model.Email);
            if (customerExists != null) return new BaseResponse<CustomerDto>
            {
                Message = "customer already exist",
                Status = false,
                Data = null,
            };

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
            };
            await _userRepository.Create(user);
            var role = await _roleRepository.Get(r => r.Name == "Customer");
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
            };
            user.UserRoles.Add(userRole);
            await _userRepository.Update(user);

            var customer = new Customer();
            customer.UserId = user.Id;
            customer.Wallet = 0;
            await _customerRepository.Create(customer);

            return new BaseResponse<CustomerDto>
            {
                Message = "succesful",
                Status = true,
                Data = new CustomerDto
                {
                    Id = customer.Id,
                    UserId = customer.UserId,
                    FirstName = customer.User.FirstName,
                    LastName = customer.User.LastName,
                    Email = customer.User.Email,
                    PhoneNumber = customer.User.PhoneNumber,
                    Password = customer.User.Password,
                    Wallet = customer.Wallet,
                }
            };
        }
        public async Task<bool> Delete(int id)
        {
            var customer = await _customerRepository.Get(id);
            if (customer != null)
            {
                customer.IsDeleted = true;
                return true;
            }
            return false;
        }
        public async Task<bool> FundWallet(FundWalletRequestModel model, int id)
        {
            var customer = await _customerRepository.Get(id);
            if (customer != null)
            {
                customer.Wallet += model.Amount;
                await _customerRepository.Update(customer);
                return true;
            }
            return false;
        }
        public async Task<BaseResponse<CustomerDto>> Get(int id)
        {
            var customer = await _customerRepository.Get(x => x.UserId == id);
            if (customer != null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new CustomerDto
                    {
                        Id = customer.Id,
                        UserId = customer.UserId,
                        FirstName = customer.User.FirstName,
                        LastName = customer.User.LastName,
                        Email = customer.User.Email,
                        PhoneNumber = customer.User.PhoneNumber,
                        Password = customer.User.Password,
                        Wallet = customer.Wallet,
                        Cards = customer.Cards.Select(card => new CardDto
                        {
                            Id = card.Id,
                            CategoryId = card.CategoryId,
                            NetworkId = card.NetworkId,
                            CategoryPrice = card.Category.Price,
                            NetworkName = card.Network.Name,

                        }).ToList(),
                    }
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = "customer not found",
                Status = false,
            };
        }
        public async Task<BaseResponse<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _customerRepository.GetAll();
            var listOfCustomers = customers.ToList().Select(customer => new CustomerDto
            {
                Id = customer.Id,
                UserId = customer.UserId,
                FirstName = customer.User.FirstName,
                LastName = customer.User.LastName,
                Email = customer.User.Email,
                PhoneNumber = customer.User.PhoneNumber,
                Password = customer.User.Password,
                Wallet = customer.Wallet,
                Cards = customer.Cards.Select(card => new CardDto
                {
                    Id = card.Id,
                    CategoryId = card.CategoryId,
                    NetworkId = card.NetworkId,
                    CategoryPrice = card.Category.Price,
                    NetworkName = card.Network.Name,

                }).ToList(),

            });
            return new BaseResponse<IEnumerable<CustomerDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfCustomers,
            };
        }
        public async Task<BaseResponse<CustomerDto>> Update(int id, UpdateCustomerRequestModel model)
        {
            var customer = await _customerRepository.Get(id);
            if (customer != null)
            {
                customer.User.FirstName = model.FirstName;
                customer.User.LastName = model.LastName;
                customer.User.PhoneNumber = model.PhoneNumber;
                await _customerRepository.Update(customer);

                return new BaseResponse<CustomerDto>
                {
                    Message = "Update successfully",
                    Status = true,
                    Data = new CustomerDto
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        Email = customer.User.Email,
                        Password = customer.User.Password
                    }
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = "Update failed",
                Status = false,
                Data = null,
            };
        }
    }
}