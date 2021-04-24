using eRewards.Services.Transactions.API.Application.Commands;
using eRewards.Services.Transactions.API.Application.IntegrationEvents.Events;
using MediatR;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Extensions;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.API.Application.IntegrationEvents.EventHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionsReceivedIntegrationEventHandler : IIntegrationEventHandler<ActionReceivedIntegrationEvents>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ActionsReceivedIntegrationEventHandler> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public ActionsReceivedIntegrationEventHandler(
           IMediator mediator,
           ILogger<ActionsReceivedIntegrationEventHandler> logger)
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
                    using (LogContext.PushProperty("IdentifiedCommandId", @event.Name))
                    {
                        var actionsCommand = new ActionsCommand(@event.Name, @event.UniqueToken,@event.AccountNo, @event.UserID, @event.Payload, @event.Sender);

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
