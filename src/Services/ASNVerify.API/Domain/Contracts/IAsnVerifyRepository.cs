namespace ASNVerify.API.Domain.Contracts
{
    public interface IASNVerifyRepository
    {
        ASNVerify.API.Domain.Entities.ASNVerify GetById(int id);
    }
}
