using ASNVerify.API.IntegrationEvents.Contracts;
using EventBusLib.Abstractions;
using EventBusLib.Events;
using IntegrationEventLogEF.Service;
using Microsoft.Extensions.Logging;
using Sodimac.Infrastructure.Persistence.DataAccess.Core.DBManager;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace ASNVerify.API.IntegrationEvents.Services
{
    public class ASNVerifyIntegrationEventService : IASNVerifyIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<ASNVerifyIntegrationEventService> _logger;
        private readonly ISodimacDBManager _dbManager;

        public ASNVerifyIntegrationEventService(
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory, 
            IEventBus eventBus, ILogger<ASNVerifyIntegrationEventService> logger, ISodimacDBManager dbManager)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbManager = dbManager ?? throw new ArgumentException(nameof(dbManager));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventLogService = _integrationEventLogServiceFactory(_dbManager.GetDatabase().CreateConnection());
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId_published} from {AppName} - ({@IntegrationEvent})", evt.Id, Program.AppName, evt);

                await _eventLogService.MarkEventAsInProgressAsync(evt.Id);
                _eventBus.Publish(evt);
                await _eventLogService.MarkEventAsPublishedAsync(evt.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", evt.Id, Program.AppName, evt);
                await _eventLogService.MarkEventAsFailedAsync(evt.Id);
            }
        }

        public async Task SaveEventAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- ASNVerifyIntegrationEventService - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);


            //await _eventLogService.SaveEventAsync(evt, );
        }
    }
}
