using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace ezLoyalty.Services.Actions.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// ActionStatusChangedToRewardedIntegrationEvent
    /// </summary>
    public record ActionStatusChangedToRewardedIntegrationEvent
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
        public ActionStatusChangedToRewardedIntegrationEvent(int accountNo, int actionId)
        {

            AccountNo = accountNo;
            ActionId = actionId;
        }
    }
}
