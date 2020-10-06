using EventBusLib.Events;

namespace ASNVerify.API.IntegrationEvents.Events
{
    public class ASNVerifySendedIntegrationEvent : IntegrationEvent
    {
        public int ASNId { get; set; }
        public string CodASN { get; set; }
        public string Details { get; set; }

        public ASNVerifySendedIntegrationEvent(int id, string codASN, string details)
        {
            ASNId = id;
            CodASN = codASN;
            Details = details;
        }
    }
}
