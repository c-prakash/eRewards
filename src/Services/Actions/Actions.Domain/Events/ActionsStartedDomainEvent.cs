using ezLoyalty.Services.Actions.Domain.ActionsAggregate;
using MediatR;

namespace ezLoyalty.Services.Actions.Domain.Events
{
    public class ActionsStartedDomainEvent : INotification
    {
        public Action Action { get; private set; }

        public ActionsStartedDomainEvent(Action action)
        {
            Action = action;
        }
    }
}
