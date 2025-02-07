﻿using ezLoyalty.Services.Actions.API.Application.IntegrationEvents;
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
    /// 
    /// </summary>
    public class ActionStatusChangedToAwaitingAccountValidationDomainEventHandler
                   : INotificationHandler<ActionStatusChangedToAwaitingAccountValidationDomainEvent>
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
        public ActionStatusChangedToAwaitingAccountValidationDomainEventHandler(
            IActionsRepository actionsRepository, ILoggerFactory logger, IActionIntegrationEventService actionIntegrationEventService)
        {
            _actionRepository = actionsRepository ?? throw new ArgumentNullException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _actionIntegrationEventService = actionIntegrationEventService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionStatusChangedToAwaitingValidationDomainEvent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(ActionStatusChangedToAwaitingAccountValidationDomainEvent actionStatusChangedToAwaitingValidationDomainEvent, CancellationToken cancellationToken)
        {

            _logger.CreateLogger<ActionStatusChangedToAwaitingAccountValidationDomainEvent>()
             .LogTrace("Action with Token: {ActionId} has been successfully updated to status {Status} ({Id})",
                 actionStatusChangedToAwaitingValidationDomainEvent.ActionRecordId, nameof(ActionStatus.AwaitingAccountValidation), ActionStatus.AwaitingAccountValidation.Id);

            var actionToUpdate = await _actionRepository.GetAsync(actionStatusChangedToAwaitingValidationDomainEvent.ActionRecordId);

            var actionStatusChangedToAwaitingValidationIntegrationEvent = new ActionStatusChangedToAwaitingAccountValidationIntegrationEvent(
               actionToUpdate.AccountNo, actionToUpdate.Id);

            await _actionIntegrationEventService.AddAndSaveEventAsync(actionStatusChangedToAwaitingValidationIntegrationEvent);
        }
    }
}
