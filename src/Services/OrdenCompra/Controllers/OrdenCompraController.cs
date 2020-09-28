using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdenCompra.API.Domain.Contracts;

namespace OrdenCompra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenCompraController : ControllerBase
    {
        private readonly IOrdenCompraService _ordenCompraService;
        private readonly ILogger<OrdenCompraController> _logger;

        public OrdenCompraController(IOrdenCompraService ordenCompraService, ILogger<OrdenCompraController> logger)
        {
            _ordenCompraService = ordenCompraService;
            _logger = logger;
        }
    }
}
