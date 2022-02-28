using ezloyalty.Services.Programs.API.Application.IntegrationEvents.Events;
using ezloyalty.Services.Programs.Domain.ProgramsAggregate;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace ezloyalty.Services.Programs.API.Application.IntegrationEvents.EventHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionStatusChangedToAwaitingEligibilityValidationIntegrationEventHandler : IIntegrationEventHandler<ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent>
    {
        private readonly IProgramRepository _programRepository;
        private readonly ILogger<ActionStatusChangedToAwaitingEligibilityValidationIntegrationEventHandler> _logger;
        private readonly IProgramIntegrationEventService _programIntegrationEventService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="programRepository"></param>
        /// <param name="logger"></param>
        /// <param name="programIntegrationEventService"></param>
        public ActionStatusChangedToAwaitingEligibilityValidationIntegrationEventHandler(
           IProgramRepository programRepository,
           ILogger<ActionStatusChangedToAwaitingEligibilityValidationIntegrationEventHandler> logger, IProgramIntegrationEventService programIntegrationEventService)
        {
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _programIntegrationEventService = programIntegrationEventService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task Handle(ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-Programs.Domain"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "Programs.Domain", @event);



                // TODO 
                // add program validation and any activity under the program available

                /*
                 * var resultProgram = await _programRepository.GetAsync(@event.ProgramId);
                */

                // temp add account id based validation and dummy points - needs to be removed,
                var eligibilityValidationIntegrationEvent = @event.AccountNo%2 != 1
                    ? (IntegrationEvent) new ProgramEligibilityRejectedIntegrationEvent(@event.AccountNo, @event.ActionRecordId)
                    : new ProgramEligibilityConfirmedIntegrationEvent(@event.AccountNo, @event.ActionRecordId);


                await _programIntegrationEventService.SaveEventAndAccountContextChangesAsync(eligibilityValidationIntegrationEvent);
                await _programIntegrationEventService.PublishThroughEventBusAsync(eligibilityValidationIntegrationEvent);
            }
        }
    }
}
