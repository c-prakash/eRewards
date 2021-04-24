using eRewards.Services.Transactions.Domain.ActionsAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.API.Application.Queries
{
    public interface IActionsQueries
    {
        Task<IEnumerable<Actions>> GetActionsAsync(int accountNo);
    }
    public class ActionsQueries : IActionsQueries
    {
        private readonly IActionsRepository _actionsRepository;

        public ActionsQueries(IActionsRepository actionsRepository)
        {
            _actionsRepository = actionsRepository ?? throw new ArgumentException(nameof(actionsRepository));
        }


        public async Task<IEnumerable<Actions>> GetActionsAsync(int accountNo)
        {
            return await _actionsRepository.GetByAccount(accountNo);
        }
    }
}
