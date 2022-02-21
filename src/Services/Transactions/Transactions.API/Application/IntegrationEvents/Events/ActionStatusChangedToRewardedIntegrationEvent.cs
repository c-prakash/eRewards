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
        public int ActionRecordId { get; set; }

        public string Sender { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionRecordId"></param>
        public ActionStatusChangedToRewardedIntegrationEvent(int accountNo, int actionRecordId, string sender)
        {

            AccountNo = accountNo;
            ActionRecordId = actionRecordId;
            Sender = sender;
        }
    }
}
