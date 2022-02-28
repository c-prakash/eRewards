using ezLoyalty.Services.Actions.Domain.ActionsAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.API.Application.Queries
{
    /// <summary>
    /// IActionsQueries
    /// </summary>
    public interface IActionsQueries
    {
        // ActionMetadata

        Task<IEnumerable<ActionMetadata>> GetAllActionMetadataAsync();

        Task<ActionMetadata> GetActionMetadatAsync(int actionId);

        // Actions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        Task<IEnumerable<Action>> GetActionsForAccountAsync(int accountNo);

        Task<IEnumerable<Action>> GetActionsForAccountAsync(int accountNo, int actionId);


    }

    /// <summary>
    /// ActionsQueries
    /// </summary>
    public class ActionsQueries : IActionsQueries
    {
        private readonly IActionsRepository _actionsRepository;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="actionsRepository"></param>
        public ActionsQueries(IActionsRepository actionsRepository)
        {
            _actionsRepository = actionsRepository ?? throw new System.ArgumentException(nameof(actionsRepository));
        }

        // Action Metadata

        public async Task<ActionMetadata> GetActionMetadatAsync(int actionId)
        {
            return await _actionsRepository.GetActionMetadatAsync(actionId);
        }

        public async Task<IEnumerable<ActionMetadata>> GetAllActionMetadataAsync()
        {
            return await _actionsRepository.GetAllActionMetadataAsync();
        }

        // Actions

        /// <summary>
        /// GetActionsForAccountAsync
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Action>> GetActionsForAccountAsync(int accountNo)
        {
            return await _actionsRepository.GetByAccount(accountNo);
        }

        public async Task<IEnumerable<Action>> GetActionsForAccountAsync(int accountNo, int actionId)
        {
            return await _actionsRepository.GetByAccount(accountNo, actionId);
        }
    }
}
