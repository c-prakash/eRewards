using System;
using System.Collections.Generic;
using System.Linq;
using ezLoyalty.Services.Accounts.Domain.Seedwork;

namespace ezLoyalty.Services.Accounts.Domain.AccountsAggregate
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
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for ActionStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static AccountStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new Exception($"Possible values for ActionStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
