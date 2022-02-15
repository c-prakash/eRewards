using MediatR;

namespace ezLoyalty.Services.Actions.Domain.Events
{
    public class ActionStatusChangedToAwaitingEligibilityValidationDomainEvent
        : INotification
    {
        public int AccountNo { get; private set; }
        public int ActionId { get; private set; }

        public ActionStatusChangedToAwaitingEligibilityValidationDomainEvent(int accountNo, int actionId)
        {
            AccountNo = accountNo;
            ActionId = actionId;
        }
    }
}
