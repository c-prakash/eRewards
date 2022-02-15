using ezLoyalty.Services.Actions.Domain.ActionsAggregate;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace ezLoyalty.Services.Actions.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// 
    /// </summary>
    public record ActionCompleteIntegrationEvents : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actions"></param>
        public ActionCompleteIntegrationEvents(Action action)
        {
            Action = action;
        }
    }
}
