using ezLoyalty.Services.Actions.Domain.Seedwork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.Domain.ActionsAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IActionsRepository : IRepository<Action>
    {
        // Metadata
        Task<ActionMetadata> Add(ActionMetadata action);

        Task Update(ActionMetadata action);

        Task<IEnumerable<ActionMetadata>> GetAllActionMetadataAsync();

        Task<ActionMetadata> GetActionMetadatAsync(int actionId);

        // Actions
        Action Add(Action actions);

        void Update(Action actions);

        Task<IEnumerable<Action>> GetByAccount(int accountNo);

        Task<IEnumerable<Action>> GetByAccount(int accountNo, int actionId);

        Task<Action> GetAsync(int recordId);

        Task<Action> GetAsync(string token);

    }
}
