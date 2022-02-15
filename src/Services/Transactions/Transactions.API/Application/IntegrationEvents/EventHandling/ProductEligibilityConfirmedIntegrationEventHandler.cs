using ezLoyalty.Services.Actions.API.Application.Commands;
using ezLoyalty.Services.Actions.API.Application.IntegrationEvents.Events;
using MediatR;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Extensions;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.API.Application.IntegrationEvents.EventHandling
{
    /// <summary>
    /// ProductEligibilityConfirmedIntegrationEventHandler
    /// </summary>
    public class ProductEligibilityConfirmedIntegrationEventHandler : IIntegrationEventHandler<ProductEligibilityConfirmedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductEligibilityConfirmedIntegrationEventHandler> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public ProductEligibilityConfirmedIntegrationEventHandler(
           IMediator mediator,
           ILogger<ProductEligibilityConfirmedIntegrationEventHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task Handle(ProductEligibilityConfirmedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                var command = new SetProductEligibilityConfirmedActionStatusCommand(@event.AccountNo, @event.ActionId);

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
