using MediatR;

namespace eRewards.Services.Transactions.Domain.Events
{
    public class ActionStatusChangedToAwaitingEligibilityValidationDomainEvent 
        : INotification
    {
        public int AccountNo { get; private set; }
        public int ActionId { get; private set; }

        public ActionStatusChangedToAwaitingEligibilityValidationDomainEvent(int customerNo, int actionId)
        {
            AccountNo = customerNo;
            ActionId = actionId;
        }
     }
}
