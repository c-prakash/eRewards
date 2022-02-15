using MediatR;

namespace ezLoyalty.Services.Actions.Domain.Events
{
    public class ActionStatusChangedToAwaitingRewardsDomainEvent
        : INotification
    {
        public int AccountNo { get; private set; }
        public int ActionId { get; private set; }

        public ActionStatusChangedToAwaitingRewardsDomainEvent(int accountNo, int actionId)
        {
            AccountNo = accountNo;
            ActionId = actionId;
        }
    }
}
