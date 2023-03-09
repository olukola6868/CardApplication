using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarnyCardApplication.Dtos;

namespace HarnyCardApplication.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<BaseResponse<CategoryDto>> Create(CreateCategoryRequestModel model);
        public Task<BaseResponse<CategoryDto>> Get(int id);
        public Task<BaseResponse<CategoryDto>> Update(int id, UpdateCategoryRequestModel model);
        public Task<BaseResponse<CategoryDto>> Delete(int id);
    }
}