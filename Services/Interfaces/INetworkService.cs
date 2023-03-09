using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarnyCardApplication.Dtos;

namespace HarnyCardApplication.Services.Interfaces
{
    public interface INetworkService 
    {
        public Task<BaseResponse<NetworkDto>> Create(CreateNetworkRequestModel model);
        public Task<BaseResponse<NetworkDto>> Get(int id);
        public Task<BaseResponse<IEnumerable<NetworkDto>>> GetAll();
        public Task<BaseResponse<NetworkDto>> Update(int id, UpdateNetworkRequestModel model);
        public Task<bool> Delete(int id);
    }
}