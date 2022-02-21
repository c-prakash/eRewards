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
        public int ActionRecordId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public AccountValidationStatus Status { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionRecordId"></param>
        public ActionAccountValidationCompleteIntegrationEvent(int accountNo, int actionRecordId)
            : base()
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
        [JsonConstructor]
        public ActionAccountValidationCompleteIntegrationEvent(int accountNo, int actionRecordId, AccountValidationStatus status)
            : this(accountNo, actionRecordId)
        {
            Status = status;
        }
    }
}
