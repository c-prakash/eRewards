using MediatR;

namespace ezLoyalty.Services.Actions.Domain.Events
{
    public class ActionStatusChangedToRewardedDomainEvent : INotification
    {
        public int AccountNo { get; private set; }
        public int ActionRecordId { get; private set; }

        public string Sender { get; private set; }

        public ActionStatusChangedToRewardedDomainEvent(int customerNo, int recordId, string sender)
        {
            AccountNo = customerNo;
            ActionRecordId = recordId;
            Sender = sender;
        }
    }
}