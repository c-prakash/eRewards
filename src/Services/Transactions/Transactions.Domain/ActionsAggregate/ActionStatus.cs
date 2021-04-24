using System;
using System.Collections.Generic;
using eRewards.Services.Transactions.Domain.Seedwork;
using System.Linq;

namespace eRewards.Services.Transactions.Domain.ActionsAggregate
{

    public class ActionStatus
        : Enumeration
    {
        public static ActionStatus Submitted = new ActionStatus(1, nameof(Submitted).ToLowerInvariant());
        public static ActionStatus AwaitingValidation = new ActionStatus(2, nameof(AwaitingValidation).ToLowerInvariant());
        public static ActionStatus Rewarded = new ActionStatus(3, nameof(Rewarded).ToLowerInvariant());

        public ActionStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<ActionStatus> List() =>
            new[] { Submitted, AwaitingValidation, Rewarded };

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
