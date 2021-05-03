using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace eRewards.Services.Transactions.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// 
    /// </summary>
    public record ActionStatusChangedToAwaitingValidationIntegrationEvent 
        : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public int ActionId;

        /// <summary>
        /// 
        /// </summary>
        public int AccountNo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="accountNo"></param>
        public ActionStatusChangedToAwaitingValidationIntegrationEvent(int actionId, int accountNo)
        {
            this.ActionId = actionId;
            this.AccountNo = accountNo;
        }
    }
}