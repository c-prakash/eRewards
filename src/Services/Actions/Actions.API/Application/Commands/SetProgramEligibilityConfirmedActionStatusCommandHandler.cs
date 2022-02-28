using ezLoyalty.Services.Actions.Domain.ActionsAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class SetProgramEligibilityConfirmedActionStatusCommandHandler : IRequestHandler<SetProgramEligibilityConfirmedActionStatusCommand, bool>
    {
        private readonly IActionsRepository _actionsRepository;
        private readonly ILogger<SetProgramEligibilityConfirmedActionStatusCommandHandler> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionsRepository"></param>
        /// <param name="logger"></param>
        public SetProgramEligibilityConfirmedActionStatusCommandHandler(IActionsRepository actionsRepository, ILogger<SetProgramEligibilityConfirmedActionStatusCommandHandler> logger)
        {
            _actionsRepository = actionsRepository ?? throw new ArgumentException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(SetProgramEligibilityConfirmedActionStatusCommand command, CancellationToken cancellationToken)
        {

            var actionToUpdate = await _actionsRepository.GetAsync(command.ActionRecordId);
            if (actionToUpdate == null)
            {
                return false;
            }

            actionToUpdate.SetAwaitingRewardsStatus();

            var result = await _actionsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return result;

        }
    }
}
