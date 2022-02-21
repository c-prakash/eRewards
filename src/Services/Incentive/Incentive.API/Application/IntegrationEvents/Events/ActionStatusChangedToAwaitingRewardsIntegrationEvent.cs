using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace ezLoyalty.Services.Incentive.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// ActionStatusChangedToAwaitingRewardsIntegrationEvent
    /// </summary>
    public record ActionStatusChangedToAwaitingRewardsIntegrationEvent
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

        public int ActionRecordId { get; set; }

        public decimal EarnedPoints { get; set; }

        public DateTime EarnedDate { get; set; } = DateTime.Now;

        public string Sender { get; set; }

        public ActionStatusChangedToAwaitingRewardsIntegrationEvent(int accountNo, int actionRecordId, int actionId, decimal earnedPoints, DateTime earnedDate, string sender)
        {
            AccountNo = accountNo;
            ActionRecordId = actionRecordId;
            ActionId = actionId;
            EarnedPoints = earnedPoints;
            EarnedDate = earnedDate;
            Sender = sender;
        }
    }
}
