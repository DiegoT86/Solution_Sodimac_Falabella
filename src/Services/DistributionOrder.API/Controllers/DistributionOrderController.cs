using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistributionOrder.API.Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistributionOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributionOrderController : ControllerBase
    {
        private readonly IDistributionOrderService _distributionOrderService;

        public DistributionOrderController(IDistributionOrderService distributionOrderService)
        {
            _distributionOrderService = distributionOrderService;
        }
    }
}
