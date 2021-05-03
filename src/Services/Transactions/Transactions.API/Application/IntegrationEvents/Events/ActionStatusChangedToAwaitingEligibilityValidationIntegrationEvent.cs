using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent
    /// </summary>
    public record ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent 
        : IntegrationEvent
    {
        /// <summary>
        /// ActionId
        /// </summary>
        public int ActionId { get; set; }

        /// <summary>
        /// AccountId
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="accountId"></param>
        public ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent(int actionId, int accountId)
        {
            this.ActionId = actionId;
            this.AccountId = actionId;
        }
    }
}
