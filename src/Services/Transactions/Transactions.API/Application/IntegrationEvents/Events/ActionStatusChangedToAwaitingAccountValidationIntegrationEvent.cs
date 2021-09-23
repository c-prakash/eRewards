using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace eRewards.Services.Transactions.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// 
    /// </summary>
    public record ActionStatusChangedToAwaitingAccountValidationIntegrationEvent
        : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public int AccountNo;

        /// <summary>
        /// 
        /// </summary>
        public int ActionId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionId"></param>
        public ActionStatusChangedToAwaitingAccountValidationIntegrationEvent(int accountNo, int actionId)
        {
            this.AccountNo = accountNo;
            this.ActionId = actionId;
        }
    }
}