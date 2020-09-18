using Microsoft.AspNetCore.Mvc;

namespace SodimacGW.API.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet()]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
