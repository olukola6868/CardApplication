using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarnyCardApplication.Dtos;

namespace HarnyCardApplication.Services.Interfaces
{
    public interface IManagerService
    {
        public Task<BaseResponse<ManagerDto>> Create(CreateManagerRequestModel model);
        public Task<BaseResponse<ManagerDto>> Get(int id);
        public Task<BaseResponse<IEnumerable<ManagerDto>>> GetAll();
        public Task<BaseResponse<ManagerDto>> Update(int id, UpdateManagerRequestModel model);
        public Task<bool> Delete(int id);
    }
}