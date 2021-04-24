using MediatR;

namespace eRewards.Services.Transactions.Domain.Events
{
    public class ActionStatusChangedToAwaitingValidationDomainEvent
             : INotification
    {
        public int AccountNo { get; private set; }
        public int ActionId { get; private set; }

        public ActionStatusChangedToAwaitingValidationDomainEvent(int customerNo, int actionId)
        {
            AccountNo = customerNo;
            ActionId = actionId;
        }
    }
}
