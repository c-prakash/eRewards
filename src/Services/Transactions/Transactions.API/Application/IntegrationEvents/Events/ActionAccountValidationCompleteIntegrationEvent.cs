using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.API.Application.IntegrationEvents.Events
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
        public int AccountNo { get; private set; }

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
        /// <param name="accountNo"></param>
        /// <param name="actionId"></param>
        public ActionAccountValidationCompleteIntegrationEvent(int accountNo, int actionId)
            : base()
        {
            AccountNo = accountNo;
            ActionId = actionId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionId"></param>
        /// <param name="status"></param>
        [JsonConstructor]
        public ActionAccountValidationCompleteIntegrationEvent(int accountNo, int actionId, AccountValidationStatus status)
            : this(accountNo, actionId)
        {
            Status = status;
        }
    }
}
