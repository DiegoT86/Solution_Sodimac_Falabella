using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShipConfirm.API.Domain.Contracts;

namespace ShipConfirm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipConfirmController : ControllerBase
    {
        private readonly IShipConfirmService _shipConfirmService;
        private readonly ILogger<ShipConfirmController> _logger;

        public ShipConfirmController(IShipConfirmService shipConfirmService, ILogger<ShipConfirmController> logger)
        {
            _shipConfirmService = shipConfirmService;
            _logger = logger;
        }
    }
}
