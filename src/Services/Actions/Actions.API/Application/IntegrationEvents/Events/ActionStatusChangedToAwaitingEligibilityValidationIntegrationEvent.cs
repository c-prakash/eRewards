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
        public int ActionRecordId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProgramId { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionRecordId"></param>
        /// <param name="programId"></param>
        public ActionStatusChangedToAwaitingEligibilityValidationIntegrationEvent(int accountNo, int actionRecordId, int programId)
        {

            AccountNo = accountNo;
            ActionRecordId = actionRecordId;
            ProgramId = programId;
        }
    }
}
