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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<BaseResponse<RoleDto>> Create(CreateRoleRequestModel model)
        {
            var roleExist = await _roleRepository.Get(a => a.Name == model.Name);
            if (roleExist != null) return new BaseResponse<RoleDto>
            {
                Message = "role already exist",
                Status = false,
                Data = null,
            };
            var role = new Role();
            role.Name = model.Name;
            role.Description = model.Description;

            return new BaseResponse<RoleDto>
            {
                Message = "succesful",
                Status = true,
                Data = new RoleDto
                {
                    Name = role.Name,
                    Description = role.Description,
                }
            };
        }

        public async Task<bool> Delete(int id)
        {
            var role = await _roleRepository.Get(id);
            if (role != null)
            {
                role.IsDeleted = true;
                return true;
            }
            return false;
        }

        public async Task<BaseResponse<RoleDto>> Get(int id)
        {
            var role = await _roleRepository.Get(id);
            if (role != null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new RoleDto
                    {
                        Name = role.Name,
                        Description = role.Description
                    }
                };
            }
            return new BaseResponse<RoleDto>
            {
                Message = "customer not found",
                Status = false,
            };

        }

        public async Task<BaseResponse<IEnumerable<RoleDto>>> GetAll()
        {
            var roles  = await _roleRepository.GetAll();
            var listOfRoles = roles.ToList().Select(role => new RoleDto{
                Name = role.Name,
                Description = role.Description
            });
            return new BaseResponse<IEnumerable<RoleDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfRoles,
            };
        }

        public async Task<BaseResponse<RoleDto>> GetRoleByName(string Name)
        {
            var role = await _roleRepository.Get(a => a.Name == Name);
            if (role != null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new RoleDto
                    {
                        Name = role.Name,
                        Description = role.Description
                    }
                };
            }
            return new BaseResponse<RoleDto>
            {
                Message = "customer not found",
                Status = false,
            };
        }

        public async Task<BaseResponse<RoleDto>> Update(int id, UpdateRoleRequestModel model)
        {
            var role = await _roleRepository.Get(id);
            if (role != null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new RoleDto
                    {
                        Name = role.Name,
                        Description = role.Description
                    }
                };
            }
            return new BaseResponse<RoleDto>
            {
                Message = "update failed",
                Status = false,
            };
        }
    }
}