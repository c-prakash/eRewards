using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace ezLoyalty.Services.Incentive.API.Application.IntegrationEvents.Events
{
    public record ActionRewardsRejectedIntegrationEvent : IntegrationEvent
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
        public ActionRewardsRejectedIntegrationEvent(int accountNo, int actionRecordId)
        {
            AccountNo = accountNo;
            ActionRecordId = actionRecordId;
        }
    }
}
