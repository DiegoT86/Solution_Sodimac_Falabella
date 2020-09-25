using Agents;
using DistributionOrder.API.Domain.Contracts;

namespace DistributionOrder.API.Domain.Services
{
    public class DistributionOrderService : IDistributionOrderService
    {
        private readonly IApiCaller _apiCaller;
        private readonly IDistributionOrderRepository _distributionOrderRepository;

        public DistributionOrderService(IDistributionOrderRepository distributionOrderRepository, IApiCaller apiCaller)
        {
            _distributionOrderRepository = distributionOrderRepository;
            _apiCaller = apiCaller;
        }
    }
}
