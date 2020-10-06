using System.Threading.Tasks;

namespace ASNVerify.API.Domain.Contracts
{
    public interface IASNVerifyService
    {
        ASNVerify.API.Domain.Entities.ASNVerify GetById(int id);
        Task<ASNVerify.API.Domain.Entities.ASNVerify> GetByIdAsync(int id);
    }
}
