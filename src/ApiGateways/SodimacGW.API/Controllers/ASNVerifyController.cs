using Agents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sodimac.Infrastructure.Crosscutting.Helper;
using SodimacGW.API.DTOs;
using SodimacGW.API.Models;
using System.Threading.Tasks;

namespace SodimacGW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ASNVerifyController : ControllerBase
    {
        private readonly IApiCaller _apiCaller;

        public ASNVerifyController(IApiCaller apiCaller)
        {
            _apiCaller = apiCaller;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            IConfigurationRoot configuration = ConfigurationHelper.GetConfiguration();

            var asnUri = configuration.GetValue("ASNVerifyUri");
            var res = await _apiCaller.GetServiceResponseById<ASNVerifyDTO>(asnUri, "ASNVerify", id);
            
            return Ok(res);
        }
    }
}
