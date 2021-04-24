using eRewards.Services.Transactions.Domain.Seedwork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.Domain.ActionsAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IActionsRepository : IRepository<Actions>
    {
        Actions Add(Actions actions);

        void Update(Actions actions);

        Task<IEnumerable<Actions>> GetByAccount(int accountNo);

        Task<Actions> GetAsync(int actionId);
    }
}
