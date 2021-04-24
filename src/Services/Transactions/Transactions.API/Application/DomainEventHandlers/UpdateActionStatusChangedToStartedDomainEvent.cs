using eRewards.Services.Transactions.API.Application.IntegrationEvents;
using eRewards.Services.Transactions.Domain.ActionsAggregate;
using eRewards.Services.Transactions.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.API.Application.DomainEventHandlers
{

    public class UpdateActionStatusChangedToStartedDomainEvent
                  : INotificationHandler<ActionsStartedDomainEvent>
    {
        private readonly IActionsRepository _actionRepository;
        private readonly ILoggerFactory _logger;
        private readonly IActionIntegrationEventService _actionIntegrationEventService;

        /*

        public UpdateActionStatusChangedToStartedDomainEvent(
            IActionsRepository actionsRepository, ILoggerFactory logger, IActionIntegrationEventService actionIntegrationEventService)
        {
            _actionRepository = actionsRepository ?? throw new ArgumentNullException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _actionIntegrationEventService = actionIntegrationEventService;
        }
        */
        public UpdateActionStatusChangedToStartedDomainEvent(
            IActionsRepository actionsRepository, ILoggerFactory logger)
        {
            _actionRepository = actionsRepository ?? throw new ArgumentNullException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public async Task Handle(ActionsStartedDomainEvent actionStartedDomainEvent, CancellationToken cancellationToken)
        {

            //_logger.CreateLogger<ActionStatusChangedToAwaitingValidationDomainEvent>()
            // .LogTrace("Action with Id: {ActionId} has been successfully updated to status {Status} ({Id})",
            //     actionStartedDomainEvent.ActionId, nameof(ActionStatus.AwaitingValidation), ActionStatus.AwaitingValidation.Id);

            //var actionToUpdate = await _actionRepository.GetAsync(actionStartedDomainEvent.ActionId);

            //actionToUpdate.SetAwaitingValidationStatus();

            //await _actionIntegrationEventService.AddAndSaveEventAsync(actionStatusChangedToAwaitingValidationIntegrationEvent);
        }
    }
}
