using HarnyCardApplication.Dtos;
using HarnyCardApplication.Models;
using HarnyCardApplication.Repositories.Interfaces;
using HarnyCardApplication.Services.Interfaces;

namespace HarnyCardApplication.Services.Implementations
{
    public class NetworkService : INetworkService
    {
        private readonly INetworkRepository _networkRepository;
        public NetworkService(INetworkRepository repository, INetworkRepository networkRepository)
        {
            _networkRepository = networkRepository;
        }

        public async Task<BaseResponse<NetworkDto>> Create(CreateNetworkRequestModel model)
        {
            var networkExist = await _networkRepository.Get(a => a.Name == model.Name);
            if (networkExist != null)
            {
                return new BaseResponse<NetworkDto>
                {
                    Message = "already exist",
                    Status = false,
                };
            }
            var network = new Network();
            network.Name = model.Name;
            var net = await _networkRepository.Create(network);

            return new BaseResponse<NetworkDto>
            {
                Message = "successful",
                Status = true,
                Data = new NetworkDto
                {
                    Id = network.Id,
                    Name = network.Name
                }
            };
        }

        public async Task<bool> Delete(int id)
        {
            var network = await _networkRepository.Get(id);
            if (network != null)
            {
                network.IsDeleted = true;
                return true;
            }
            return false;
        }

        public async Task<BaseResponse<NetworkDto>> Get(int id)
        {
            var network = await _networkRepository.Get(id);
            if (network != null) return new BaseResponse<NetworkDto>
            {
                Message = "ok",
                Status = true,
                Data = new NetworkDto
                {
                    Id = network.Id,
                    Name = network.Name
                }
            };
            return new BaseResponse<NetworkDto>
            {
                Message = "not found",
                Status = false,
            };
        }

        public async Task<BaseResponse<IEnumerable<NetworkDto>>> GetAll()
        {
            var networks = await _networkRepository.GetAll();
            var listOfNetworks = networks.ToList().Select(network => new NetworkDto
            {
                Id = network.Id,
                Name = network.Name
            }).ToList();
            return new BaseResponse<IEnumerable<NetworkDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfNetworks
            };
        }

        public async Task<BaseResponse<NetworkDto>> Update(int id, UpdateNetworkRequestModel model)
        {
            var network = await _networkRepository.Get(id);
            if (network != null) return new BaseResponse<NetworkDto>
            {
                Message = "update successful",
                Status = true,
                Data = new NetworkDto
                {
                    Id = network.Id,
                    Name = network.Name
                }
            };
            await _networkRepository.Update(network);
            return new BaseResponse<NetworkDto>
            {
                Message = "Update failed",
                Status = false,
                Data = null,
            };
        }
    }
}