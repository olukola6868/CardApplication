using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarnyCardApplication.Dtos;
using HarnyCardApplication.Models;
using HarnyCardApplication.Repositories.Interfaces;
using HarnyCardApplication.Services.Interfaces;

namespace HarnyCardApplication.Services.Implementations
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUserRepository _userRepository;
        public ManagerService(IManagerRepository managerRepository, IUserRepository userRepository)
        {
            _managerRepository = managerRepository;
            _userRepository = userRepository;
        }
        public async Task<BaseResponse<ManagerDto>> Create(CreateManagerRequestModel model)
        {
            var managerExist = await _managerRepository.Get(c => c.User.Email == model.Email);
            if (managerExist != null) return new BaseResponse<ManagerDto>
            {
                Message = "customer already exist",
                Status = false,
                Data = null,
            };
            var user = new User();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password = model.Password;
            user.PhoneNumber = model.PhoneNumber;
            await _userRepository.Create(user);

            var manager = new Manager();
            manager.UserId = user.Id;
            await _managerRepository.Create(manager);


            return new BaseResponse<ManagerDto>
            {
                Message = "succesful",
                Status = true,
                Data = new ManagerDto
                {
                    Id = manager.Id,
                    UserId = manager.UserId,
                    FirstName = manager.User.FirstName,
                    LastName = manager.User.LastName,
                    Email = manager.User.Email,
                    PhoneNumber = manager.User.PhoneNumber,
                    Password = manager.User.Password,
                }
            };
        }

        public async Task<bool> Delete(int id)
        {
            var manager = await _managerRepository.Get(id);
            if (manager != null)
            {
                manager.IsDeleted = true;
                return true;
            }
            return false;
        }

        public async Task<BaseResponse<ManagerDto>> Get(int id)
        {
            var manager = await _managerRepository.Get(id);
            if (manager != null)
            {
                return new BaseResponse<ManagerDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new ManagerDto
                    {
                        Id = manager.Id,
                        UserId = manager.UserId,
                        FirstName = manager.User.FirstName,
                        LastName = manager.User.LastName,
                        Email = manager.User.Email,
                        PhoneNumber = manager.User.PhoneNumber,
                        Password = manager.User.Password,
                        Wallet = manager.Wallet,
                    }
                };
            }
            return new BaseResponse<ManagerDto>
            {
                Message = "manager not found",
                Status = false,
                Data = null
            };
        }

        public async Task<BaseResponse<IEnumerable<ManagerDto>>> GetAll()
        {
            var managers = await _managerRepository.GetAll();
            var listOfManagers = managers.ToList().Select(manager => new ManagerDto
            {
                Id = manager.Id,
                UserId = manager.UserId,
                FirstName = manager.User.FirstName,
                LastName = manager.User.LastName,
                Email = manager.User.Email,
                PhoneNumber = manager.User.PhoneNumber,
                Password = manager.User.Password,
                Wallet = manager.Wallet,
            }).ToList();

            return new BaseResponse<IEnumerable<ManagerDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfManagers,
            };
        }

        public async Task<BaseResponse<ManagerDto>> Update(int id, UpdateManagerRequestModel model)
        {
            var manager = await _managerRepository.Get(id);
            if (manager != null) return new BaseResponse<ManagerDto>
            {
                Message = "update successful",
                Status = true,
                Data = new ManagerDto
                {
                    Id = manager.Id,
                    UserId = manager.UserId,
                    FirstName = manager.User.FirstName,
                    LastName = manager.User.LastName,
                    Email = manager.User.Email,
                    PhoneNumber = manager.User.PhoneNumber,
                }
            };
            return new BaseResponse<ManagerDto>
            {
                Message = "Update failed",
                Status = false,
                Data = null,
            };
        }
    }
}