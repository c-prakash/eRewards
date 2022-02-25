using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace ezLoyalty.Services.Accounts.API.Application.IntegrationEvents.Events
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
        public int ActionRecordId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionRecordId"></param>
        public ActionStatusChangedToAwaitingAccountValidationIntegrationEvent(int accountNo, int actionRecordId)
        {
            ActionRecordId = actionRecordId;
            AccountNo = accountNo;
        }
    }
}
