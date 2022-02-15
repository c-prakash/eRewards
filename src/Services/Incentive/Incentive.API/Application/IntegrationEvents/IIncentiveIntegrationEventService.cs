using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Incentive.API.Application.IntegrationEvents
{

    public interface IIncentiveIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
