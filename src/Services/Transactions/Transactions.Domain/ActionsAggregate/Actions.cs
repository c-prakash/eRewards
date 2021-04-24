using eRewards.Services.Transactions.Domain.Events;
using eRewards.Services.Transactions.Domain.Seedwork;
using System;

namespace eRewards.Services.Transactions.Domain.ActionsAggregate
{
    public class Actions
        : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public string UniqueToken { get; set; }

        public int AccountNo { get; set; }

        public string UserID { get; set; }

        public string Payload { get; set; }

        public string Sender { get; set; }

        public DateTime CreatedAt { get; set; }


        private int _actionStatusId;


        public Actions() {
            this.CreatedAt = DateTime.Now;
        }

        public Actions(string actionName, string token, int accountNo, string userId, string payload, string sender)
        {
            this.Name = actionName;
            this.UniqueToken = token;
            this.AccountNo = accountNo;
            this.UserID = userId;
            this.Payload = payload;
            this.Sender = sender;
            _actionStatusId = ActionStatus.Submitted.Id;

            this.CreatedAt = DateTime.Now;

            AddActionsStartedDomainEvent();
        }

        public void SetAwaitingValidationStatus()
        {
            if (_actionStatusId == ActionStatus.Submitted.Id)
            {
                AddDomainEvent(new ActionStatusChangedToAwaitingValidationDomainEvent(this.AccountNo, this.Id));
                _actionStatusId = ActionStatus.AwaitingValidation.Id;
            }
        }

        public void SetRewardedStatus()
        {
            if (_actionStatusId == ActionStatus.AwaitingValidation.Id)
            {
                AddDomainEvent(new ActionStatusChangedToRewardedDomainEvent(this.AccountNo, this.Id));
                _actionStatusId = ActionStatus.Rewarded.Id;
            }
        }

        private void AddActionsStartedDomainEvent()
        {
            var orderStartedDomainEvent = new ActionsStartedDomainEvent(this);

            this.AddDomainEvent(orderStartedDomainEvent);
        }
    }
}
