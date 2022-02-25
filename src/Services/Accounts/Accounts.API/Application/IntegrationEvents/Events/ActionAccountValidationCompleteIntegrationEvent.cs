using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace ezLoyalty.Services.Accounts.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// 
    /// </summary>
    public enum AccountValidationStatus
    {
        /// <summary>
        /// NonFound
        /// </summary>
        NonFound,

        /// <summary>
        /// Found
        /// </summary>
        Found
    }

    /// <summary>
    /// 
    /// </summary>
    public record ActionAccountValidationCompleteIntegrationEvent : IntegrationEvent
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
        public AccountValidationStatus Status { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionRecordId"></param>
        public ActionAccountValidationCompleteIntegrationEvent(int accountNo, int actionRecordId)
        {
            AccountNo = accountNo;
            ActionRecordId = actionRecordId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionRecordId"></param>
        /// <param name="status"></param>
        public ActionAccountValidationCompleteIntegrationEvent(int accountNo, int actionRecordId, AccountValidationStatus status)
            : this(accountNo, actionRecordId)
        {
            Status = status;
        }
    }
}
