using eRewards.Services.Accounts.Domain.Seedwork;
using System.Threading.Tasks;

namespace eRewards.Services.Accounts.Domain.AccountsAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> Add(Account account);

        Task Update(Account account);


        Task<Account> GetAsync(int accountId);
    }
}
