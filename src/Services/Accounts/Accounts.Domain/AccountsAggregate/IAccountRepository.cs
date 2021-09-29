using eRewards.Services.Accounts.Domain.Seedwork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRewards.Services.Accounts.Domain.AccountsAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> Add(Account account);

        Task Update(Account account);

        Task<IEnumerable<Account>> GetAllAsync(string searchString, string orderBy, int pageNumber = 0, int pageSize = 10);

        Task<Account> GetAsync(int accountNo);

        Task<int> DeleteAsync(int accountNo);

        Task<long> Count(string searchString);
    }
}
