using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.API.Application.IntegrationEvents.Events
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
    public record ActionAccountValidationCompleteIntegrationEvent 
        : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public int AccountId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public int ActionId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public AccountValidationStatus Status { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="actionId"></param>
        public ActionAccountValidationCompleteIntegrationEvent(int accountId, int actionId)
            :base()
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
        [JsonConstructor]
        public ActionAccountValidationCompleteIntegrationEvent(int accountId, int actionId, AccountValidationStatus status)
            :this(accountId, actionId)
        {
            Status = status;
        }
    }
}
