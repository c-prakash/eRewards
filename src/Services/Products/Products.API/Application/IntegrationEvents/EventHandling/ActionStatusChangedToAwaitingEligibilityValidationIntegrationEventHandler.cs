using eRewards.Services.Products.API.Application.IntegrationEvents.Events;
using eRewards.Services.Products.Domain.ProductsAggregate;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace eRewards.Services.Products.API.Application.IntegrationEvents.EventHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionStatusChangedToAwaitingEligibilityValidationIntegrationEventHandler : IIntegrationEventHandler<ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ActionStatusChangedToAwaitingEligibilityValidationIntegrationEventHandler> _logger;
        private readonly IProductIntegrationEventService _productIntegrationEventService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productRepository"></param>
        /// <param name="logger"></param>
        /// <param name="productIntegrationEventService"></param>
        public ActionStatusChangedToAwaitingEligibilityValidationIntegrationEventHandler(
           IProductRepository productRepository,
           ILogger<ActionStatusChangedToAwaitingEligibilityValidationIntegrationEventHandler> logger, IProductIntegrationEventService productIntegrationEventService)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productIntegrationEventService = productIntegrationEventService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task Handle(ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-Products.Domain"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "Products.Domain", @event);



                // TODO 
                // add product validation and any activity under the product available

                /*
                 * var resultProduct = await _productRepository.GetAsync(@event.ProductId);
                */

                // temp add account id based validation and dummy points - needs to be removed,
                var eligibilityValidationIntegrationEvent = @event.AccountNo != 1
                    ? (IntegrationEvent) new ProductEligibilityRejectedIntegrationEvent(@event.AccountNo, @event.ActionRecordId)
                    : new ProductEligibilityConfirmedIntegrationEvent(@event.AccountNo, @event.ActionRecordId);


                await _productIntegrationEventService.SaveEventAndAccountContextChangesAsync(eligibilityValidationIntegrationEvent);
                await _productIntegrationEventService.PublishThroughEventBusAsync(eligibilityValidationIntegrationEvent);
            }
        }
    }
}
