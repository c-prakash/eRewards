using eRewards.Services.Transactions.API.Application.IntegrationEvents;
using eRewards.Services.Transactions.API.Application.IntegrationEvents.Events;
using eRewards.Services.Transactions.Domain.ActionsAggregate;
using eRewards.Services.Transactions.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.API.Application.DomainEventHandlers
{
    
    /// <summary>
    /// 
    /// </summary>
    public class UpdateActionStatusChangedToAwaitingValidationDomainEvent
                   : INotificationHandler<ActionStatusChangedToAwaitingValidationDomainEvent>
    {
        private readonly IActionsRepository _actionRepository;
        private readonly ILoggerFactory _logger;
        private readonly IActionIntegrationEventService _actionIntegrationEventService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionsRepository"></param>
        /// <param name="logger"></param>
        /// <param name="actionIntegrationEventService"></param>
        public UpdateActionStatusChangedToAwaitingValidationDomainEvent(
            IActionsRepository actionsRepository, ILoggerFactory logger, IActionIntegrationEventService actionIntegrationEventService)
        {
            _actionRepository = actionsRepository ?? throw new ArgumentNullException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _actionIntegrationEventService = actionIntegrationEventService;
        }


        public async Task Handle(ActionStatusChangedToAwaitingValidationDomainEvent actionStatusChangedToAwaitingValidationDomainEvent, CancellationToken cancellationToken)
        {

            _logger.CreateLogger<ActionStatusChangedToAwaitingValidationDomainEvent>()
             .LogTrace("Action with Id: {ActionId} has been successfully updated to status {Status} ({Id})",
                 actionStatusChangedToAwaitingValidationDomainEvent.ActionId, nameof(ActionStatus.AwaitingValidation), ActionStatus.AwaitingValidation.Id);

            var actionToUpdate = await _actionRepository.GetAsync(actionStatusChangedToAwaitingValidationDomainEvent.ActionId);

            var actionStatusChangedToAwaitingValidationIntegrationEvent = new ActionStatusChangedToAwaitingValidationIntegrationEvent(
               actionToUpdate.Id, actionToUpdate.AccountNo);

            await _actionIntegrationEventService.AddAndSaveEventAsync(actionStatusChangedToAwaitingValidationIntegrationEvent);
        }
    }
}
