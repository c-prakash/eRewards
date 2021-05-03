using eRewards.Services.Transactions.Domain.ActionsAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.API.Application.Queries
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
        Task<IEnumerable<Actions>> GetActionsAsync(int accountNo);
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
            _actionsRepository = actionsRepository ?? throw new ArgumentException(nameof(actionsRepository));
        }

        /// <summary>
        /// GetActionsAsync
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Actions>> GetActionsAsync(int accountNo)
        {
            return await _actionsRepository.GetByAccount(accountNo);
        }
    }
}
