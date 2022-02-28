using ezLoyalty.Services.Actions.Domain.ActionsAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.API.Application.Commands
{
    /// <summary>
    /// SetRewardsRejectedActionStatusCommandHandler
    /// </summary>
    public class SetRewardsRejectedActionStatusCommandHandler : IRequestHandler<SetRewardsRejectedActionStatusCommand, bool>
    {
        private readonly IActionsRepository _actionsRepository;
        private readonly ILogger<SetRewardsRejectedActionStatusCommandHandler> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionsRepository"></param>
        /// <param name="logger"></param>
        public SetRewardsRejectedActionStatusCommandHandler(IActionsRepository actionsRepository, ILogger<SetRewardsRejectedActionStatusCommandHandler> logger)
        {
            _actionsRepository = actionsRepository ?? throw new ArgumentException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handle
        /// SetRewardsRejectedActionStatusCommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(SetRewardsRejectedActionStatusCommand command, CancellationToken cancellationToken)
        {

            var actionToUpdate = await _actionsRepository.GetAsync(command.ActionRecordId);
            if (actionToUpdate == null)
            {
                return false;
            }

            actionToUpdate.SetRewardsRejectedStatus();

            var result = await _actionsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return result;

        }
    }
}
