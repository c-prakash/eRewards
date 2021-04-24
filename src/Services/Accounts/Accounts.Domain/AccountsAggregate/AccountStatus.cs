using System;
using System.Collections.Generic;
using eRewards.Services.Accounts.Domain.Seedwork;
using System.Linq;

namespace eRewards.Services.Accounts.Domain.AccountsAggregate
{

    public class AccountStatus
        : Enumeration
    {
        public static AccountStatus Created = new AccountStatus(1, nameof(Created).ToLowerInvariant());
        public static AccountStatus Deleted = new AccountStatus(2, nameof(Deleted).ToLowerInvariant());
        public static AccountStatus Updated = new AccountStatus(3, nameof(Updated).ToLowerInvariant());

        public AccountStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<AccountStatus> List() =>
            new[] { Created, Deleted, Updated };

        public static AccountStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for ActionStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static AccountStatus From(int id)
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
