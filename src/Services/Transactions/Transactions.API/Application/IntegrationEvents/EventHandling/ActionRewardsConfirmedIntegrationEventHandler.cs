using ezLoyalty.Services.Actions.API;
using ezLoyalty.Services.Actions.API.Application.Commands;
using ezLoyalty.Services.Actions.API.Application.IntegrationEvents.Events;
using MediatR;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Extensions;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.API.Application.IntegrationEvents.EventHandling
{
    /// <summary>
    /// ActionRewardsConfirmedIntegrationEventHandler
    /// </summary>
    public class ActionRewardsConfirmedIntegrationEventHandler : IIntegrationEventHandler<ActionRewardsConfirmedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ActionRewardsConfirmedIntegrationEventHandler> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public ActionRewardsConfirmedIntegrationEventHandler(
           IMediator mediator,
           ILogger<ActionRewardsConfirmedIntegrationEventHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handle 
        /// ActionRewardsConfirmedIntegrationEvent
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task Handle(ActionRewardsConfirmedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                var command = new SetRewardsConfirmedActionStatusCommand(@event.AccountNo, @event.ActionId);

                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    command.GetGenericTypeName(),
                    nameof(command.ActionId),
                    command.ActionId,
                    command);

                await _mediator.Send(command);
            }
        }
    }
}
