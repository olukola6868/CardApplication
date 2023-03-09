using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarnyCardApplication.Dtos;
using HarnyCardApplication.Models;

namespace HarnyCardApplication.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<BaseResponse<RoleDto>> Create(CreateRoleRequestModel model);
        public Task<BaseResponse<RoleDto>> Get(int id);
        public Task<BaseResponse<IEnumerable<RoleDto>>> GetAll();
        public Task<BaseResponse<RoleDto>> Update(int id, UpdateRoleRequestModel model);
        public Task<bool> Delete(int id);
    }
}