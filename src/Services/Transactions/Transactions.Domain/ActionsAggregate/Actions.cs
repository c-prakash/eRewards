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

        public ActionStatus ActionStatus { get; private set; }

        private int _actionStatusId;


        public Actions()
        {
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

        public void SetAwaitingAccountValidationStatus()
        {
            if (_actionStatusId == ActionStatus.Submitted.Id)
            {
                AddDomainEvent(new ActionStatusChangedToAwaitingAccountValidationDomainEvent(this.AccountNo, this.Id));
                _actionStatusId = ActionStatus.AwaitingAccountValidation.Id;
            }
        }

        public void SetAwaitingEligibilityValidation()
        {
            if (_actionStatusId == ActionStatus.AwaitingAccountValidation.Id)
            {
                AddDomainEvent(new ActionStatusChangedToAwaitingEligibilityValidationDomainEvent(this.AccountNo, this.Id));
                _actionStatusId = ActionStatus.AwaitingEligibilityValidation.Id;
            }
        }

        public void SetRewardedStatus(int points)
        {
            if (_actionStatusId == ActionStatus.AwaitingEligibilityValidation.Id)
            {
                AddDomainEvent(new ActionStatusChangedToRewardedDomainEvent(this.AccountNo, this.Id));
                _actionStatusId = ActionStatus.Rewarded.Id;
            }
        }

        public void SetAccountRejected()
        {
            if (_actionStatusId == ActionStatus.AwaitingAccountValidation.Id)
            {
                //AddDomainEvent(new ActionStatusChangedToAwaitingEligibilityValidationDomainEvent(this.AccountNo, this.Id));
                _actionStatusId = ActionStatus.AccountRejected.Id;
            }
        }

        public void SetEligibilityRejectedStatus()
        {
            if (_actionStatusId == ActionStatus.AwaitingEligibilityValidation.Id)
            {
                //AddDomainEvent(new ActionStatusChangedToAwaitingEligibilityValidationDomainEvent(this.AccountNo, this.Id));
                _actionStatusId = ActionStatus.EligibilityRejected.Id;
            }
        }

        private void AddActionsStartedDomainEvent()
        {
            var orderStartedDomainEvent = new ActionsStartedDomainEvent(this);

            this.AddDomainEvent(orderStartedDomainEvent);
        }
    }
}
