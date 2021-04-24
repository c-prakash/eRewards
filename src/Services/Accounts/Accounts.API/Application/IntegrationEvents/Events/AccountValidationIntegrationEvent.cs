
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
    public class AccountValidationIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public int AccountId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AccountValidationStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        public AccountValidationIntegrationEvent(int accountId)
        {
            AccountId = accountId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="status"></param>
        public AccountValidationIntegrationEvent(int accountId, AccountValidationStatus status)
        {
            AccountId = accountId;
            Status = status;
        }
    }
}
