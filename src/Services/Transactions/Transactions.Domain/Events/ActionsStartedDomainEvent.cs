using eRewards.Services.Transactions.Domain.ActionsAggregate;
using MediatR;

namespace eRewards.Services.Transactions.Domain.Events
{
    public class ActionsStartedDomainEvent : INotification
    {
        public Actions Action { get; private set; }

        public ActionsStartedDomainEvent(Actions action)
        {
            this.Action = action;
        }
    }
}
