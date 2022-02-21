using ezLoyalty.Services.Incentive.API.Application.IntegrationEvents;
using ezLoyalty.Services.Incentive.API.Application.IntegrationEvents.Events;
using ezLoyalty.Services.Incentive.Domain.Events;
using ezLoyalty.Services.Incentive.Domain.PointsAggregate;
using MediatR;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace ezLoyalty.Services.Incentive.API.Application.DomainEventHandlers
{

    public class PointsAwardedDomainEventHandler
                  : INotificationHandler<PointsAwardedDomainEvent>
    {
        private readonly IPointsRepository _pointsRepository;
        private readonly ILoggerFactory _logger;
        private readonly IIncentiveIntegrationEventService _incentiveIntegrationEventService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="actionsRepository"></param>
        /// <param name="logger"></param>
        /// <param name="incentiveIntegrationEventService"></param>
        public PointsAwardedDomainEventHandler(
            IPointsRepository actionsRepository, ILoggerFactory logger, IIncentiveIntegrationEventService incentiveIntegrationEventService)
        {
            _pointsRepository = actionsRepository ?? throw new ArgumentNullException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _incentiveIntegrationEventService = incentiveIntegrationEventService;
        }

        /// <summary>
        /// Handle
        /// ActionStatusChangedToAwaitingRewardDomainEvent
        /// </summary>
        /// <param name="pointsAwardedDomainEvent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(PointsAwardedDomainEvent pointsAwardedDomainEvent, CancellationToken cancellationToken)
        {

            _logger.CreateLogger<PointsAwardedDomainEvent>()
             .LogTrace("Action with Id: {ActionId} has been successfully rewarded with points {points} ({Id})",
                 pointsAwardedDomainEvent.AwardedPoints.ActionId, pointsAwardedDomainEvent.AwardedPoints.EarnedPoints, pointsAwardedDomainEvent.AwardedPoints.Id);

            var pointsToUpdate = await _pointsRepository.GetAsync(pointsAwardedDomainEvent.AwardedPoints.AccountNo, pointsAwardedDomainEvent.AwardedPoints.ActionId, pointsAwardedDomainEvent.AwardedPoints.ActionRecordId);

            var actionRewardsIntegrationEvent = (pointsToUpdate != null) ?
                (IntegrationEvent) new ActionRewardsConfirmedIntegrationEvent(pointsToUpdate.AccountNo, pointsToUpdate.ActionRecordId)
                : new ActionRewardsRejectedIntegrationEvent(pointsAwardedDomainEvent.AwardedPoints.AccountNo, pointsAwardedDomainEvent.AwardedPoints.ActionRecordId);

            await _incentiveIntegrationEventService.AddAndSaveEventAsync(actionRewardsIntegrationEvent);
        }
    }
}
