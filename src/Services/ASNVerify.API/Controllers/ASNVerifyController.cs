using ASNVerify.API.Domain.Contracts;
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

        public ASNVerifyController(IASNVerifyService asnVerifyService, ILogger<ASNVerifyController> logger)
        {
            _asnVerifyService = asnVerifyService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ASNVerify.API.Domain.Entities.ASNVerify), (int)HttpStatusCode.OK)]
        public ActionResult<ASNVerify.API.Domain.Entities.ASNVerify>  GetById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var asnv = _asnVerifyService.GetById(id);
            if (asnv != null)
                return Ok(asnv);

            return NotFound();
        }
    }
}
