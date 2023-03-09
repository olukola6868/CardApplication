using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarnyCardApplication.Dtos;

namespace HarnyCardApplication.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> Get(int id);
        Task<BaseResponse<IEnumerable<UserDto>>> GetAll();
        Task<BaseResponse<UserDto>> Login(LoginUserRequestModel model);
    }
}