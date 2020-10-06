using ASNVerify.API.IntegrationEvents.Events;
using EventBusLib.Abstractions;
using System.Threading.Tasks;

namespace ASNVerify.API.IntegrationEvents.Handlers
{
    public class ASNVerifyReceivedIntegrationEventHandler : IIntegrationEventHandler<ASNVerifyReceivedIntegrationEvent>
    {
        public Task Handle(ASNVerifyReceivedIntegrationEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}
