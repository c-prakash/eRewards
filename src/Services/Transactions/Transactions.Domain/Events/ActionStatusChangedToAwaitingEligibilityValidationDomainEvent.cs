using MediatR;

namespace ezLoyalty.Services.Actions.Domain.Events
{
    public class ActionStatusChangedToAwaitingEligibilityValidationDomainEvent
        : INotification
    {
        public int AccountNo { get; private set; }
        public int ActionRecordId { get; private set; }

        public ActionStatusChangedToAwaitingEligibilityValidationDomainEvent(int accountNo, int recordId)
        {
            AccountNo = accountNo;
            ActionRecordId = recordId;
        }
    }
}
