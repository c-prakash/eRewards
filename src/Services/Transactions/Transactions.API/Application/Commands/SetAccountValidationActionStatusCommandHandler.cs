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
    public class SetAccountValidationActionStatusCommandHandler : IRequestHandler<SetAccountValidationActionStatusCommand, bool>
    {
        private readonly IActionsRepository _actionsRepository;
        private readonly ILogger<SetAccountValidationActionStatusCommandHandler> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionsRepository"></param>
        /// <param name="logger"></param>
        public SetAccountValidationActionStatusCommandHandler(IActionsRepository actionsRepository, ILogger<SetAccountValidationActionStatusCommandHandler> logger)
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
        public async Task<bool> Handle(SetAccountValidationActionStatusCommand command, CancellationToken cancellationToken)
        {

            var actionToUpdate = await _actionsRepository.GetAsync(command.ActionId);
            if (actionToUpdate == null)
            {
                return false;
            }

            if (command.Status)
                actionToUpdate.SetAwaitingEligibilityValidation();
            else
                actionToUpdate.SetAccountRejected();

            var result = await _actionsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return result;

        }
    }
}
