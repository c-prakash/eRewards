using eRewards.Services.Transactions.Domain.ActionsAggregate;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace eRewards.Services.Transactions.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionCompleteIntegrationEvents : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public Actions Actions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actions"></param>
        public ActionCompleteIntegrationEvents(Actions actions)
        {
            this.Actions = actions;
        }
    }
}
