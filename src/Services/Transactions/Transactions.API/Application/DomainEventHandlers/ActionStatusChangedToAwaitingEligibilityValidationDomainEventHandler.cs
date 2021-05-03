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
    /// ActionStatusChangedToAwaitingEligibilityValidationDomainEventHandler
    /// </summary>
    public class ActionStatusChangedToAwaitingEligibilityValidationDomainEventHandler
                   : INotificationHandler<ActionStatusChangedToAwaitingEligibilityValidationDomainEvent>
    {
        private readonly IActionsRepository _actionRepository;
        private readonly ILoggerFactory _logger;
        private readonly IActionIntegrationEventService _actionIntegrationEventService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="actionsRepository"></param>
        /// <param name="logger"></param>
        /// <param name="actionIntegrationEventService"></param>
        public ActionStatusChangedToAwaitingEligibilityValidationDomainEventHandler(
            IActionsRepository actionsRepository, ILoggerFactory logger, IActionIntegrationEventService actionIntegrationEventService)
        {
            _actionRepository = actionsRepository ?? throw new ArgumentNullException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _actionIntegrationEventService = actionIntegrationEventService;
        }

        /// <summary>
        /// Handle
        /// ActionStatusChangedToAwaitingEligibilityValidationDomainEvent
        /// </summary>
        /// <param name="actionStatusChangedToAwaitingEligibilityValidationDomainEvent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(ActionStatusChangedToAwaitingEligibilityValidationDomainEvent actionStatusChangedToAwaitingEligibilityValidationDomainEvent, CancellationToken cancellationToken)
        {

            _logger.CreateLogger<ActionStatusChangedToAwaitingEligibilityValidationDomainEvent>()
             .LogTrace("Action with Id: {ActionId} has been successfully updated to status {Status} ({Id})",
                 actionStatusChangedToAwaitingEligibilityValidationDomainEvent.ActionId, nameof(ActionStatus.AwaitingEligibilityValidation), ActionStatus.AwaitingEligibilityValidation.Id);

            var actionToUpdate = await _actionRepository.GetAsync(actionStatusChangedToAwaitingEligibilityValidationDomainEvent.ActionId);

            var actionStatusChangedToAwaitingEligibilityValidationIntegrationEvent = new ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent(
               actionToUpdate.Id, actionToUpdate.AccountNo);

            await _actionIntegrationEventService.AddAndSaveEventAsync(actionStatusChangedToAwaitingEligibilityValidationIntegrationEvent);
        }
    }
}
