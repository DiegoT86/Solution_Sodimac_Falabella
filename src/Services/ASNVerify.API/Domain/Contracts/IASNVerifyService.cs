namespace ASNVerify.API.Domain.Contracts
{
    public interface IASNVerifyService
    {
        ASNVerify.API.Domain.Entities.ASNVerify GetById(int id);
    }
}
