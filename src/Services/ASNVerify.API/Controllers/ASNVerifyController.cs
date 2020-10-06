using ASNVerify.API.Domain.Contracts;
using ASNVerify.API.IntegrationEvents.Contracts;
using ASNVerify.API.IntegrationEvents.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace ASNVerify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ASNVerifyController : ControllerBase
    {
        private readonly IASNVerifyService _asnVerifyService;
        private readonly ILogger<ASNVerifyController> _logger;
        private readonly IASNVerifyIntegrationEventService _asnVerifyIntegrationEventService;

        public ASNVerifyController(IASNVerifyService asnVerifyService, 
            ILogger<ASNVerifyController> logger,
            IASNVerifyIntegrationEventService asnVerifyIntegrationEventService)
        {
            _asnVerifyService = asnVerifyService;
            _logger = logger;
            _asnVerifyIntegrationEventService = asnVerifyIntegrationEventService;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ASNVerify.API.Domain.Entities.ASNVerify), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var asnv = await _asnVerifyService.GetByIdAsync(id);
            if (asnv != null)
                return Ok(asnv);

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ASNVerify.API.Domain.Entities.ASNVerify asnv)
        {
            if (!ModelState.IsValid) return BadRequest();

            var asnVerifySendedIntegrationEvent = new ASNVerifySendedIntegrationEvent(asnv.Id, asnv.CodASN, asnv.Details);

            // registrar el evento en la db
            
            await _asnVerifyIntegrationEventService.PublishThroughEventBusAsync(asnVerifySendedIntegrationEvent);

            return Ok();
        }
    }
}
