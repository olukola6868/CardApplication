using HarnyCardApplication.Dtos;
using HarnyCardApplication.Models;
using HarnyCardApplication.Repositories.Interfaces;
using HarnyCardApplication.Services.Interfaces;

namespace HarnyCardApplication.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryrepository;
        private readonly INetworkRepository _networkRepository;
        public CategoryService(ICategoryRepository categoryrepository, INetworkRepository networkRepository)
        {
            _categoryrepository = categoryrepository;
            _networkRepository = networkRepository;
        }
        public async Task<BaseResponse<CategoryDto>> Create(CreateCategoryRequestModel model)
        {
            var categoryExist = await _categoryrepository.Get(a => a.Price == model.Price);
            var networks = await _networkRepository.GetAll();
            if (categoryExist != null) return new BaseResponse<CategoryDto>
            {
                Message = " already exists",
                Status = false,
            };

            var category = new Category();
            category.Name = model.Name;
            category.Price = model.Price;
            var cate = await _categoryrepository.Create(category);

            foreach (var network in networks)
            {
                var networkCategory = new NetworkCategory
                {
                    CategoryId = cate.Id,
                    NetworkId = network.Id
                };
                cate.NetworkCategories.Add(networkCategory);
            }
            await _categoryrepository.Update(cate);
            return new BaseResponse<CategoryDto>
            {
                Message = "successful",
                Status = true,
                Data = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Price = category.Price,
                    Networks = category.NetworkCategories.Select(x => new NetworkDto
                    {
                        Id = x.Id,
                        Name = x.Network.Name,
                    }).ToList(),

                }
            };
        }

        public async Task<BaseResponse<CategoryDto>> Delete(int id)
        {
            var category = await _categoryrepository.Get(id);
            if (category != null)
            {
                category.IsDeleted = true;
               await  _categoryrepository.Update(category);
                return new BaseResponse<CategoryDto>
                {
                    Message = "Successfully deleted",
                    Status = true,
                };
            }
            else return new BaseResponse<CategoryDto>
            {
                Message = "Category not found",
                Status = false,
            };
        }

        public async Task<BaseResponse<CategoryDto>> Get(int id)
        {
            var category = await _categoryrepository.Get(id);
            if (category != null) return new BaseResponse<CategoryDto>
            {
                Message = "ok",
                Status = true,
                Data = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Price = category.Price,
                }
            };
            return new BaseResponse<CategoryDto>
            {
                Message = "not found",
                Status = false,
            };
        }

        public async Task<BaseResponse<CategoryDto>> Update(int id, UpdateCategoryRequestModel model)
        {
            var category = await _categoryrepository.Get(id);
            if (category != null)
            {
                category.Name = model.Name;
                category.Price = model.Price;
            }
            return new BaseResponse<CategoryDto>
            {
                Message = "succesful",
                Status = true,
            };
        }
    }
}