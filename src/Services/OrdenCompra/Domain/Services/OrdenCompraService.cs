using Agents;
using OrdenCompra.API.Domain.Contracts;

namespace OrdenCompra.API.Domain.Services
{
    public class OrdenCompraService : IOrdenCompraService
    {
        public readonly IOrdenCompraRepository _ordenCompraRepository;
        public readonly IApiCaller _apiCaller;

        public OrdenCompraService(IOrdenCompraRepository ordenCompraRepository, IApiCaller apiCaller)
        {
            _ordenCompraRepository = ordenCompraRepository;
            _apiCaller = apiCaller;
        }
    }
}
