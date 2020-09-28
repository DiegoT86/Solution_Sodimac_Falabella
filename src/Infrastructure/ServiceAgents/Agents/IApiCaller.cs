using System.Threading.Tasks;

namespace Agents
{
    public interface IApiCaller
    {
        Task<T> GetServiceResponseById<T>(string controller, int id);
    }
}
