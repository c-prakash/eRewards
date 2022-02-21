using ezLoyalty.Services.Incentive.API.Application.Commands;
using ezLoyalty.Services.Incentive.API.Application.IntegrationEvents.Events;
using MediatR;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Extensions;
using Serilog.Context;

namespace ezLoyalty.Services.Incentive.API.Application.IntegrationEvents.EventHandling
{
    /// <summary>
    /// ActionRewardsConfirmedIntegrationEventHandler
    /// </summary>
    public class ActionStatusChangedToAwaitingRewardsIntegrationEventHandler : IIntegrationEventHandler<ActionStatusChangedToAwaitingRewardsIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ActionStatusChangedToAwaitingRewardsIntegrationEventHandler> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public ActionStatusChangedToAwaitingRewardsIntegrationEventHandler(
           IMediator mediator,
           ILogger<ActionStatusChangedToAwaitingRewardsIntegrationEventHandler> logger)
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
        public async Task Handle(ActionStatusChangedToAwaitingRewardsIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                var command = new AddPointsCommand()
                {
                    AccountNo = @event.AccountNo,
                    ActionId = @event.ActionId,
                    ActionRecordId = @event.ActionRecordId,
                    EarnedPoints = @event.EarnedPoints,
                    EarnedDate = @event.EarnedDate,
                    Sender = @event.Sender
                }; 

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
