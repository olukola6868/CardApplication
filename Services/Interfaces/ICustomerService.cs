using HarnyCardApplication.Dtos;

namespace HarnyCardApplication.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<BaseResponse<CustomerDto>> Create(CreateCustomerRequestModel model);
        public Task<BaseResponse<CustomerDto>> Get(int id);
        public Task<BaseResponse<IEnumerable<CustomerDto>>> GetAll();
        public Task<BaseResponse<CustomerDto>> Update(int id, UpdateCustomerRequestModel model);
        public Task<bool> FundWallet(FundWalletRequestModel model , int id);
        public Task<bool> Delete(int id);
    }
}