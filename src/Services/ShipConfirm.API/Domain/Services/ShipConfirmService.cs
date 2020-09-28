using Agents;
using ShipConfirm.API.Domain.Contracts;

namespace ShipConfirm.API.Domain.Services
{
    public class ShipConfirmService : IShipConfirmService
    {
        public readonly IShipConfirmRepository _shipConfirmRepository;
        public readonly IApiCaller _apiCaller;

        public ShipConfirmService(IShipConfirmRepository shipConfirmRepository, IApiCaller apiCaller)
        {
            _shipConfirmRepository = shipConfirmRepository;
            _apiCaller = apiCaller;
        }
    }
}
