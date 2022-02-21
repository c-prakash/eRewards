using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace eRewards.Services.Products.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// 
    /// </summary>
    public record ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent : IntegrationEvent
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
        public int ProductId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionRecordId"></param>
        /// <param name="productId"></param>
        public ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent(int accountNo, int actionRecordId, int productId)
        {
            this.AccountNo = accountNo;
            this.ActionRecordId = actionRecordId;
            this.ProductId = productId;
        }
    }
}
