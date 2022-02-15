using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace ezLoyalty.Services.Actions.API.Application.IntegrationEvents.Events
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
            AccountNo = accountNo;
            ActionId = actionId;
        }
    }
}