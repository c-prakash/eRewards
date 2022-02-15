using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace ezLoyalty.Services.Actions.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// ActionStatusChangedToAwaitingRewardsIntegrationEvent
    /// </summary>
    public record ActionStatusChangedToAwaitingRewardsIntegrationEvent
        : IntegrationEvent
    {
        /// <summary>
        /// AccountId
        /// </summary>
        public int AccountNo { get; set; }

        /// <summary>
        /// ActionId
        /// </summary>
        public int ActionId { get; set; }


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionId"></param>
        public ActionStatusChangedToAwaitingRewardsIntegrationEvent(int accountNo, int actionId)
        {

            AccountNo = accountNo;
            ActionId = actionId;
        }
    }
}
