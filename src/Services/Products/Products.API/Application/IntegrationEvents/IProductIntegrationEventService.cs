using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace eRewards.Services.Products.API.Application.IntegrationEvents
{

    /// <summary>
    /// 
    /// </summary>
    public interface IProductIntegrationEventService
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
