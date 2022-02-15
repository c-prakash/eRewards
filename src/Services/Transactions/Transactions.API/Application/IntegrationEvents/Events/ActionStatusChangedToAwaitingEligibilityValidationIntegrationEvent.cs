using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent
    /// </summary>
    public record ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent
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

        /// <summary>
        /// 
        /// </summary>
        public int ProductId { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionId"></param>
        /// <param name="productId"></param>
        public ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent(int accountNo, int actionId, int productId)
        {

            AccountNo = accountNo;
            ActionId = actionId;
            ProductId = productId;
        }
    }
}
