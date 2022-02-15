using ezLoyalty.Services.Actions.Domain.Events;
using ezLoyalty.Services.Actions.Domain.Seedwork;
using System;

namespace ezLoyalty.Services.Actions.Domain.ActionsAggregate
{
    public class Action
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


        public Action()
        {
            CreatedAt = DateTime.Now;
        }

        public Action(string actionName, string token, int accountNo, string userId, string payload, string sender)
        {
            Name = actionName;
            UniqueToken = token;
            AccountNo = accountNo;
            UserID = userId;
            Payload = payload;
            Sender = sender;
            _actionStatusId = ActionStatus.Submitted.Id;

            CreatedAt = DateTime.Now;

            AddActionsStartedDomainEvent();
        }

        public void SetAwaitingAccountValidationStatus()
        {
            if (_actionStatusId == ActionStatus.Submitted.Id)
            {
                AddDomainEvent(new ActionStatusChangedToAwaitingAccountValidationDomainEvent(AccountNo, Id));
                _actionStatusId = ActionStatus.AwaitingAccountValidation.Id;
            }
        }

        public void SetAwaitingEligibilityValidation()
        {
            if (_actionStatusId == ActionStatus.AwaitingAccountValidation.Id)
            {
                AddDomainEvent(new ActionStatusChangedToAwaitingEligibilityValidationDomainEvent(AccountNo, Id));
                _actionStatusId = ActionStatus.AwaitingEligibilityValidation.Id;
            }
        }

        public void SetAwaitingRewardsStatus()
        {
            if (_actionStatusId == ActionStatus.AwaitingEligibilityValidation.Id)
            {
                AddDomainEvent(new ActionStatusChangedToAwaitingRewardsDomainEvent(AccountNo, Id));
                _actionStatusId = ActionStatus.AwaitingRewards.Id;
            }
        }

        public void SetRewardedStatus()
        {
            if (_actionStatusId == ActionStatus.AwaitingRewards.Id)
            {
                AddDomainEvent(new ActionStatusChangedToRewardedDomainEvent(AccountNo, Id));
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

        public void SetRewardsRejectedStatus()
        {
            if (_actionStatusId == ActionStatus.AwaitingRewards.Id)
            {
                //AddDomainEvent(new ActionStatusChangedToRewardsRejectedDomainEvent(this.AccountNo, this.Id));
                _actionStatusId = ActionStatus.RewardsRejected.Id;
            }
        }

        private void AddActionsStartedDomainEvent()
        {
            var orderStartedDomainEvent = new ActionsStartedDomainEvent(this);

            AddDomainEvent(orderStartedDomainEvent);
        }
    }
}
