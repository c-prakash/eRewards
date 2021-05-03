using MediatR;

namespace eRewards.Services.Transactions.Domain.Events
{
    public class ActionStatusChangedToAwaitingAccountValidationDomainEvent
             : INotification
    {
        public int AccountNo { get; private set; }
        public int ActionId { get; private set; }

        public ActionStatusChangedToAwaitingAccountValidationDomainEvent(int customerNo, int actionId)
        {
            AccountNo = customerNo;
            ActionId = actionId;
        }
    }
}
