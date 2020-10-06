using Agents;
using ASNVerify.API.Domain.Contracts;
using System.Threading.Tasks;

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

        public Entities.ASNVerify GetById(int id)
        {
            return _asnRepository.GetById(id);
        }

        public async Task<Entities.ASNVerify> GetByIdAsync(int id)
        {
            return await _asnRepository.GetByIdAsync(id);
        }
    }
}
