using System.Threading.Tasks;

namespace EventBusLib.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
