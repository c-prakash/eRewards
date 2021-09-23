using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace eRewards.Services.Accounts.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// 
    /// </summary>
    public record ActionStatusChangedToAwaitingAccountValidationIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public int AccountNo { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int ActionId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionId"></param>
        public ActionStatusChangedToAwaitingAccountValidationIntegrationEvent(int accountNo, int actionId)
        {
            this.ActionId = actionId;
            this.AccountNo = accountNo;
        }
    }
}
