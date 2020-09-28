using ASNVerify.API.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASNVerify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ASNVerifyController : ControllerBase
    {
        private readonly IASNVerifyService _asnVerifyService;
        private readonly ILogger<ASNVerifyController> _logger;

        public ASNVerifyController(IASNVerifyService asnVerifyService, ILogger<ASNVerifyController> logger)
        {
            _asnVerifyService = asnVerifyService;
            _logger = logger;
        }
    }
}
