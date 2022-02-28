using MediatR;

namespace ezLoyalty.Services.Actions.Domain.Events
{
    public class ActionStatusChangedToAwaitingRewardsDomainEvent
        : INotification
    {
        public int AccountNo { get; private set; }
        public int ActionRecordId { get; private set; }

        public string Sender { get; set; }

        public ActionStatusChangedToAwaitingRewardsDomainEvent(int accountNo, int recordId, string sender)
        {
            AccountNo = accountNo;
            ActionRecordId = recordId;
            Sender = sender;
        }
    }
}
