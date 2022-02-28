using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace ezloyalty.Services.Programs.API.Application.IntegrationEvents
{

    /// <summary>
    /// 
    /// </summary>
    public interface IProgramIntegrationEventService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        Task PublishThroughEventBusAsync(IntegrationEvent evt);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        Task SaveEventAndAccountContextChangesAsync(IntegrationEvent evt);
    }
}
