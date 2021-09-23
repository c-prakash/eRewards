using eRewards.Services.Transactions.API.Application.IntegrationEvents;
using eRewards.Services.Transactions.Domain.ActionsAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class NewActionCommandHandler : IRequestHandler<NewActionCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IActionsRepository _actionsRepository;
        private readonly ILogger<NewActionCommandHandler> _logger;

        private readonly IActionIntegrationEventService _actionIntegrationEventService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionsRepository"></param>
        /// <param name="logger"></param>
        public NewActionCommandHandler(IActionsRepository actionsRepository, ILogger<NewActionCommandHandler> logger)
        {
            _actionsRepository = actionsRepository ?? throw new ArgumentException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(NewActionCommand message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Creating Actions - Actions: {@Message}", message);

            var actions = new Actions(actionName: message.Name,
                token: message.UniqueToken,
                accountNo: message.AccountNo,
                userId: message.UserID,
                payload: message.Payload,
                sender: message.Sender);

            var actionToUpdate =  _actionsRepository.Add(actions);

            actionToUpdate.SetAwaitingAccountValidationStatus();
         
            var result = await _actionsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return result;

        }
    }


}
