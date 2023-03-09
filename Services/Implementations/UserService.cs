using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarnyCardApplication.Dtos;
using HarnyCardApplication.Repositories.Interfaces;
using HarnyCardApplication.Services.Interfaces;

namespace HarnyCardApplication.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<BaseResponse<UserDto>> Get(int id)
        {
            var user = await _userRepository.Get(id);
            if (user != null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                    }
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "user not found",
                Status = false,
            };
        }

        public async Task<BaseResponse<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userRepository.GetAll();
            var listOfUsers = users.ToList().Select(user => new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
            });
            return new BaseResponse<IEnumerable<UserDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfUsers,
            };
        }

        public async Task<BaseResponse<UserDto>> Login(LoginUserRequestModel model)
        {
            var user = await _userRepository.Get(a => a.Email == model.Email && a.Password == model.Password);
            if (user == null) return new BaseResponse<UserDto>
            {
                Message = "email or password incorrect",
                Status = false
            };
            return new BaseResponse<UserDto>
            {
                Message = "login successful",
                Status = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Password = user.Password,
                    Email = user.Email,
                    Roles = user.UserRoles.Select(a => new RoleDto
                    {
                        Id = a.Role.Id,
                        Name = a.Role.Name,
                        Description = a.Role.Description,
                    }).ToList(),
                }
            };
        }
    }
}