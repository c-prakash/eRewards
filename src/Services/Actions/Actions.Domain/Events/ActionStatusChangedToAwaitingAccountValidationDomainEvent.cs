using MediatR;

namespace ezLoyalty.Services.Actions.Domain.Events
{
    public class ActionStatusChangedToAwaitingAccountValidationDomainEvent
             : INotification
    {
        public int AccountNo { get; private set; }

        public int ActionRecordId { get; private set; }

        public ActionStatusChangedToAwaitingAccountValidationDomainEvent(int customerNo, int recordId)
        {
            AccountNo = customerNo;
            ActionRecordId = recordId;
        }
    }
}
