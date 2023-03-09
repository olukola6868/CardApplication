using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarnyCardApplication.Dtos;

namespace HarnyCardApplication.Services.Interfaces
{
    public interface ICardService
    {
        public Task<BaseResponse<CardDto>> Create(CreateCardRequestModel model);
        public Task<BaseResponse<CardDto>> Get(int id);
        public Task<BaseResponse<CardDto>> BuyCard(BuyCardRequestModel buyCardRequest , int id , int Id);
        public Task<BaseResponse<CardDto>> GetCardByPin(string pin);
        public Task<BaseResponse<IEnumerable<CardDto>>> GetAll();
        public Task<BaseResponse<IEnumerable<CardDto>>> GetIsAvailableCards();
        public Task<BaseResponse<IEnumerable<CardDto>>> GetCustomerCards(int customerId);
        public void Delete(int id);
    }
}