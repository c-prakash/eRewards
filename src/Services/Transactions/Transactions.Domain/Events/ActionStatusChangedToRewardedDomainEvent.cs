using MediatR;

namespace eRewards.Services.Transactions.Domain.Events
{
    public class ActionStatusChangedToRewardedDomainEvent : INotification
    {
        public int AccountNo { get; private set; }
        public int ActionId { get; private set; }

        public ActionStatusChangedToRewardedDomainEvent(int customerNo, int actionId)
        {
            AccountNo = customerNo;
            ActionId = actionId;
        }
    }
}