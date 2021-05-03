using eRewards.Services.Accounts.API.Application.IntegrationEvents.Events;
using eRewards.Services.Accounts.Domain.AccountsAggregate;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace eRewards.Services.Accounts.API.Application.IntegrationEvents.EventHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionStatusChangedToAwaitingValidationIntegrationEventHandler : IIntegrationEventHandler<ActionStatusChangedToAwaitingValidationIntegrationEvent>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<ActionStatusChangedToAwaitingValidationIntegrationEventHandler> _logger;
        private readonly IAccountIntegrationEventService _accountIntegrationEventService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountRepository"></param>
        /// <param name="logger"></param>
        /// <param name="accountIntegrationEventService"></param>
        public ActionStatusChangedToAwaitingValidationIntegrationEventHandler(
           IAccountRepository accountRepository,
           ILogger<ActionStatusChangedToAwaitingValidationIntegrationEventHandler> logger, IAccountIntegrationEventService accountIntegrationEventService)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _accountIntegrationEventService = accountIntegrationEventService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task Handle(ActionStatusChangedToAwaitingValidationIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-Accounts.Domain"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "Accounts.Domain", @event);

                var resultAccount = await _accountRepository.GetAsync(@event.AccountNo);

                var accountValidationIntegrationEvent = new ActionAccountValidationIntegrationEvent(@event.AccountNo, @event.ActionId);

                if (resultAccount == null || resultAccount.Id == 0)
                {
                    accountValidationIntegrationEvent.Status = AccountValidationStatus.NonFound;
                }
                else
                {
                    accountValidationIntegrationEvent.Status = AccountValidationStatus.Found;
                }

                await _accountIntegrationEventService.SaveEventAndAccountContextChangesAsync(accountValidationIntegrationEvent);
                await _accountIntegrationEventService.PublishThroughEventBusAsync(accountValidationIntegrationEvent);
            }
        }
    }
}
