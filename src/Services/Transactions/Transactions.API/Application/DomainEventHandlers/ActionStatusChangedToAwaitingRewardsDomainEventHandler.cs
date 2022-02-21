using ezLoyalty.Services.Actions.API.Application.IntegrationEvents;
using ezLoyalty.Services.Actions.API.Application.IntegrationEvents.Events;
using ezLoyalty.Services.Actions.Domain.ActionsAggregate;
using ezLoyalty.Services.Actions.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.API.Application.DomainEventHandlers
{
    /// <summary>
    /// ActionStatusChangedToAwaitingRewardsDomainEventHandler
    /// </summary>
    public class ActionStatusChangedToAwaitingRewardsDomainEventHandler
                   : INotificationHandler<ActionStatusChangedToAwaitingRewardsDomainEvent>
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
        public ActionStatusChangedToAwaitingRewardsDomainEventHandler(
            IActionsRepository actionsRepository, ILoggerFactory logger, IActionIntegrationEventService actionIntegrationEventService)
        {
            _actionRepository = actionsRepository ?? throw new ArgumentNullException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _actionIntegrationEventService = actionIntegrationEventService;
        }

        /// <summary>
        /// Handle
        /// ActionStatusChangedToAwaitingRewardDomainEvent
        /// </summary>
        /// <param name="actionStatusChangedToAwaitingRewardsDomainEvents"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(ActionStatusChangedToAwaitingRewardsDomainEvent actionStatusChangedToAwaitingRewardsDomainEvents, CancellationToken cancellationToken)
        {

            _logger.CreateLogger<ActionStatusChangedToAwaitingEligibilityValidationDomainEvent>()
             .LogTrace("Action with Id: {ActionId} has been successfully updated to status {Status} ({Id})",
                 actionStatusChangedToAwaitingRewardsDomainEvents.ActionRecordId, nameof(ActionStatus.AwaitingRewards), ActionStatus.AwaitingRewards.Id);

            var actionToUpdate = await _actionRepository.GetAsync(actionStatusChangedToAwaitingRewardsDomainEvents.ActionRecordId);
            var actionMetadata = await _actionRepository.GetActionMetadatAsync(actionToUpdate.ActionId);

            var actionStatusChangedToAwaitingRewardsIntegrationEvent = new ActionStatusChangedToAwaitingRewardsIntegrationEvent(
               actionToUpdate.AccountNo, actionToUpdate.Id, actionToUpdate.ActionId, actionMetadata.PossiblePoints, actionToUpdate.CreatedAt, actionToUpdate.CreatedBy);

            await _actionIntegrationEventService.AddAndSaveEventAsync(actionStatusChangedToAwaitingRewardsIntegrationEvent);
        }
    }
}
