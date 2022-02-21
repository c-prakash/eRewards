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
    /// 
    /// </summary>
    public class ActionReceivedIntegrationEventHandler : IIntegrationEventHandler<ActionReceivedIntegrationEvents>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ActionReceivedIntegrationEventHandler> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public ActionReceivedIntegrationEventHandler(
           IMediator mediator,
           ILogger<ActionReceivedIntegrationEventHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Integration event handler which starts the create order process
        /// </summary>
        /// <param name="@event">
        /// Integration event message which is sent by the
        /// basket.api once it has successfully process the 
        /// order items.
        /// </param>
        /// <returns></returns>
        public async Task Handle(ActionReceivedIntegrationEvents @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-Transactions.Domain"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "Transactions.Domain", @event);

                var result = false;

                if (@event.RequestId != Guid.Empty)
                {
                    using (LogContext.PushProperty("IdentifiedCommandId", @event.ActionId))
                    {
                        var actionsCommand = new NewActionCommand(@event.ActionId, @event.UniqueToken, @event.AccountNo, @event.UserID, @event.Payload, @event.Sender, @event.CreatedAt);

                        _logger.LogInformation(
                            "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                            actionsCommand.GetGenericTypeName(),
                            nameof(actionsCommand.UniqueToken),
                            actionsCommand.UniqueToken,
                            actionsCommand);

                        result = await _mediator.Send(actionsCommand);

                        if (result)
                        {
                            _logger.LogInformation("----- ActionsCommand suceeded - RequestId: {RequestId}", @event.RequestId);
                        }
                        else
                        {
                            _logger.LogWarning("ActionsCommand failed - RequestId: {RequestId}", @event.RequestId);
                        }
                    }
                }
                else
                {
                    _logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", @event);
                }
            }
        }
    }
}
