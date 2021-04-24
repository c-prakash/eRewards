using eRewards.Services.Accounts.Infrastructure;
using IntegrationEventLogEF.Services;
using IntegrationEventLogEF.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace eRewards.Services.Accounts.API.Application.IntegrationEvents
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountIntegrationEventService : IAccountIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly AccountDbContext _accountContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<AccountIntegrationEventService> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventBus"></param>
        /// <param name="accountContext"></param>
        /// <param name="integrationEventLogServiceFactory"></param>
        /// <param name="logger"></param>
        public AccountIntegrationEventService(IEventBus eventBus,
            AccountDbContext accountContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            ILogger<AccountIntegrationEventService> logger)
        {
            _accountContext = accountContext ?? throw new ArgumentNullException(nameof(accountContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_accountContext.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        public async Task SaveEventAndAccountContextChangesAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- AccountIntegrationEventService - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);

            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_accountContext).ExecuteAsync(async () =>
            {
                // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
                await _accountContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(evt, _accountContext.Database.CurrentTransaction);
            });
        }
    }
}
