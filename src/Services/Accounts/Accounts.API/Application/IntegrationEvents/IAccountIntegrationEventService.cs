using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Accounts.API.Application.IntegrationEvents
{

    /// <summary>
    /// 
    /// </summary>
    public interface IAccountIntegrationEventService
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
