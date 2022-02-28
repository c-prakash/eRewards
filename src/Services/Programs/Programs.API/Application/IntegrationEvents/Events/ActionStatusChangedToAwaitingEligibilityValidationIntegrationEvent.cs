using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace ezloyalty.Services.Programs.API.Application.IntegrationEvents.Events
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
        public int ProgramId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionRecordId"></param>
        /// <param name="programId"></param>
        public ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent(int accountNo, int actionRecordId, int programId)
        {
            this.AccountNo = accountNo;
            this.ActionRecordId = actionRecordId;
            this.ProgramId = programId;
        }
    }
}
