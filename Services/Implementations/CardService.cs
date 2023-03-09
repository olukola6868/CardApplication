using HarnyCardApplication.Dtos;
using HarnyCardApplication.Models;
using HarnyCardApplication.Repositories.Interfaces;
using HarnyCardApplication.Services.Interfaces;

namespace HarnyCardApplication.Services.Implementations
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly INetworkRepository _networkRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CardService(ICardRepository cardRepository, ICategoryRepository categoryRepository, INetworkRepository networkRepository, IHttpContextAccessor httpContextAccessor, ICustomerRepository customerRepository)
        {
            _cardRepository = cardRepository;
            _categoryRepository = categoryRepository;
            _networkRepository = networkRepository;
            _httpContextAccessor = httpContextAccessor;
            _customerRepository = customerRepository;
        }
        public async Task<BaseResponse<CardDto>> Create(CreateCardRequestModel model)
        {
            var rand = new Random();
            var category = await _categoryRepository.Get(a => a.Price == model.CategoryPrice);
            if (category == null)
            {
                return new BaseResponse<CardDto>
                {
                    Status = false,
                    Message = "category not available"
                };
            }
            var network = await _networkRepository.Get(b => b.Name == model.NetworkName);
            if (network == null)
            {
                return new BaseResponse<CardDto>
                {
                    Status = false,
                    Message = "network not available"
                };
            }
            var card = new Card
            {
                Pin = rand.NextInt64(10000000000, 99999999999).ToString(),
                CategoryId = category.Id,
                NetworkId = network.Id,
            };
            var cardCreated = await _cardRepository.Create(card);
            if (cardCreated != null) return new BaseResponse<CardDto>
            {
                Message = "successful",
                Status = true
            };
            return new BaseResponse<CardDto>
            {
                Status = false,
                Message = "not succesful"
            };
        }
        public async void Delete(int id)
        {
            var cardExist = await _cardRepository.Get(id);
            if (cardExist != null)
            {
                cardExist.IsDeleted = true;
            }
        }
        public async Task<BaseResponse<CardDto>> Get(int id)
        {
            var card = await _cardRepository.Get(id);
            if (card != null) return new BaseResponse<CardDto>
            {
                Message = "succesful",
                Status = true,
                Data = new CardDto
                {
                    Id = card.Id,
                    Pin = card.Pin,
                    CategoryId = card.CategoryId,
                    CategoryPrice = card.Category.Price,
                    NetworkId = card.NetworkId,
                    NetworkName = card.Network.Name,
                }
            };
            return new BaseResponse<CardDto>
            {
                Message = "not found",
                Status = false,
            };
        }
        public async Task<BaseResponse<CardDto>> BuyCard(BuyCardRequestModel buyCardRequest, int customerId, int cardId)
        {
            var customer = await _customerRepository.Get(customerId);
            if (customer == null)
            {
                return new BaseResponse<CardDto>
                {
                    Message = "Customer not found",
                    Status = false,
                    Data = null,
                };
            }

            var card = await _cardRepository.Get(x => x.Id == cardId);
            if (card == null)
            {
                return new BaseResponse<CardDto>
                {
                    Message = "Card not available",
                    Status = false
                };
            }
            if (customer.Wallet < card.Category.Price)
            {
                return new BaseResponse<CardDto>
                {
                    Message = "Insufficient balance!!! Please fund your wallet",
                    Status = false,
                    Data = null,
                };
            }

            card.IsUsed = true;
            card.CustomerId = customerId; card.Customer = customer;
            await _cardRepository.Update(card);

            customer.Wallet -= card.Category.Price;
            await _customerRepository.Update(customer);

            return new BaseResponse<CardDto>
            {
                Message = $"We have successfully generated your card pin and your pin is: {card.Pin} and #{card.Category.Price} deducted from your account",
                Status = true,
                Data = new CardDto
                {
                    NetworkName = card.Network.Name
                }
            };
        }
        public async Task<BaseResponse<IEnumerable<CardDto>>> GetAll()
        {
            var cards = await _cardRepository.GetAll();
            var listOfCards = cards.ToList().Select(card => new CardDto
            {
                Id = card.Id,
                Pin = card.Pin,
                CategoryId = card.CategoryId,
                CategoryPrice = card.Category.Price,
                NetworkId = card.NetworkId,
                NetworkName = card.Network.Name,
            });
            return new BaseResponse<IEnumerable<CardDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfCards,
            };
        }
        public async Task<BaseResponse<CardDto>> GetCardByPin(string pin)
        {
            var card = await _cardRepository.Get(a => a.Pin == pin);
            if (card != null) return new BaseResponse<CardDto>
            {
                Message = "succesful",
                Status = true,
                Data = new CardDto
                {
                    Id = card.Id,
                    Pin = card.Pin,
                    CategoryId = card.CategoryId,
                    CategoryPrice = card.Category.Price,
                    NetworkId = card.NetworkId,
                    NetworkName = card.Network.Name,
                }
            };
            return new BaseResponse<CardDto>
            {
                Message = "not found",
                Status = false,
            };
        }

        public async Task<BaseResponse<IEnumerable<CardDto>>> GetIsAvailableCards()
        {
            var cards = await _cardRepository.GetIsAvailableCards();
            if (cards == null)
            {
                return new BaseResponse<IEnumerable<CardDto>>
                {
                    Message = "Card not found",
                    Status = false,
                    Data = null,
                };
            }
            var listOfCards = cards.ToList().Select(card => new CardDto
            {
                Id = card.Id,
                Pin = card.Pin,
                CategoryId = card.CategoryId,
                CategoryPrice = card.Category.Price,
                NetworkId = card.NetworkId,
                NetworkName = card.Network.Name,
            });
            return new BaseResponse<IEnumerable<CardDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfCards,
            };
        }

        public async Task<BaseResponse<IEnumerable<CardDto>>> GetCustomerCards(int userId)
        {
            var customer = _customerRepository.Get(a => a.UserId == userId);
            if (customer == null)
            {
                return new BaseResponse<IEnumerable<CardDto>>
                {
                    Message = "customer not found",
                    Status = false,
                    Data = null
                };
            }
            var cards = await _cardRepository.GetCutomerCards(customer.Id);
            if (cards == null)
            {
                return new BaseResponse<IEnumerable<CardDto>>
                {
                    Message = "Card not found",
                    Status = false,
                    Data = null,
                };
            }
            var listOfCards = cards.ToList().Select(card => new CardDto
            {
                Id = card.Id,
                Pin = card.Pin,
                CategoryId = card.CategoryId,
                CategoryPrice = card.Category.Price,
                NetworkId = card.NetworkId,
                NetworkName = card.Network.Name,
            });
            return new BaseResponse<IEnumerable<CardDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfCards,
            };
        }
    }
}