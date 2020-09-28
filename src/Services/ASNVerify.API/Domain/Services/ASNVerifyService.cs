using Agents;
using ASNVerify.API.Domain.Contracts;

namespace ASNVerify.API.Domain.Services
{
    public class ASNVerifyService : IASNVerifyService
    {
        private readonly IApiCaller _apiCaller;
        private readonly IASNVerifyRepository _asnRepository;

        public ASNVerifyService(IASNVerifyRepository asnRepository, IApiCaller apiCaller)
        {
            _asnRepository = asnRepository;
            _apiCaller = apiCaller;
        }
    }
}
