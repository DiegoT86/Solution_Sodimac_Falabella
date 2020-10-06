using EventBusLib.Events;
using System.Threading.Tasks;

namespace ASNVerify.API.IntegrationEvents.Contracts
{
    public interface IASNVerifyIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
