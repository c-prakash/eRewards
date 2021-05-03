
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace eRewards.Services.Accounts.API.Application.IntegrationEvents.Events
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
    public record ActionAccountValidationIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public int AccountId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int ActionId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AccountValidationStatus Status { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="actionId"></param>
        public ActionAccountValidationIntegrationEvent(int accountId, int actionId)
        {
            AccountId = accountId;
            ActionId = actionId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="actionId"></param>
        /// <param name="status"></param>
        public ActionAccountValidationIntegrationEvent(int accountId, int actionId, AccountValidationStatus status)
        {
            AccountId = accountId;
            ActionId = actionId;
            Status = status;
        }
    }
}
