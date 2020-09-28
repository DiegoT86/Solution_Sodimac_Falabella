using DistributionOrder.API.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DistributionOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributionOrderController : ControllerBase
    {
        private readonly IDistributionOrderService _distributionOrderService;
        private readonly ILogger<DistributionOrderController> _logger;

        public DistributionOrderController(IDistributionOrderService distributionOrderService, ILogger<DistributionOrderController> logger)
        {
            _distributionOrderService = distributionOrderService;
            _logger = logger;
        }
    }
}
