using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace ezLoyalty.Services.Incentive.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// ActionRewardsConfirmedIntegrationEvent
    /// </summary>
    public record ActionRewardsConfirmedIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public int AccountNo { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int ActionId { get; private set; }

        public decimal Points { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionId"></param>
        public ActionRewardsConfirmedIntegrationEvent(int accountNo, int actionId, decimal points)
        {
            AccountNo = accountNo;
            ActionId = actionId;
            Points = points;
        }
    }
}
