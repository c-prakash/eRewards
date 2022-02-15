using ezLoyalty.Services.Actions.Domain.Seedwork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.Domain.ActionsAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IActionsRepository : IRepository<Action>
    {
        Action Add(Action actions);

        void Update(Action actions);

        Task<IEnumerable<Action>> GetByAccount(int accountNo);

        Task<Action> GetAsync(int actionId);

        Task<Action> GetAsync(string token);

    }
}
