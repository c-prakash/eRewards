using System;
using System.Collections.Generic;
using eRewards.Services.Transactions.Domain.Seedwork;
using System.Linq;

namespace eRewards.Services.Transactions.Domain.ActionsAggregate
{

    public class ActionStatus
        : Enumeration
    {
        public static ActionStatus Submitted = new (1, nameof(Submitted).ToLowerInvariant());
        public static ActionStatus AwaitingAccountValidation = new (2, nameof(AwaitingAccountValidation).ToLowerInvariant());
        public static ActionStatus AwaitingEligibilityValidation = new (3, nameof(AwaitingEligibilityValidation).ToLowerInvariant());
        public static ActionStatus Rewarded = new(4, nameof(Rewarded).ToLowerInvariant());

        public static ActionStatus AccountRejected = new(91, nameof(AccountRejected).ToLowerInvariant());
        public static ActionStatus EligibilityRejected = new(92, nameof(EligibilityRejected).ToLowerInvariant());

        public ActionStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<ActionStatus> List() =>
            new[] { Submitted, AwaitingAccountValidation, AwaitingEligibilityValidation, Rewarded, AccountRejected, EligibilityRejected };

        public static ActionStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for ActionStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static ActionStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new Exception($"Possible values for ActionStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
