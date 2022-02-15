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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        Task<IEnumerable<Action>> GetActionsAsync(int accountNo);
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

        /// <summary>
        /// GetActionsAsync
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Action>> GetActionsAsync(int accountNo)
        {
            return await _actionsRepository.GetByAccount(accountNo);
        }
    }
}
