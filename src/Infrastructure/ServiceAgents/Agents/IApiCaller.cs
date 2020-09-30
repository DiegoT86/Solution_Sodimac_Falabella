using System.Threading.Tasks;

namespace Agents
{
    public interface IApiCaller
    {
        Task<T> GetServiceResponseById<T>(string baseAddress, string controller, int id);
    }
}
