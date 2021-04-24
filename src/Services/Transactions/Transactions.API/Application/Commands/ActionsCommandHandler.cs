using eRewards.Services.Transactions.API.Application.IntegrationEvents;
using eRewards.Services.Transactions.Domain.ActionsAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.API.Application.Commands
{
    public class ActionsCommandHandler : IRequestHandler<ActionsCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IActionsRepository _actionsRepository;
        private readonly ILogger<ActionsCommandHandler> _logger;

        private readonly IActionIntegrationEventService _actionIntegrationEventService;

       
        public ActionsCommandHandler(IActionsRepository actionsRepository, ILogger<ActionsCommandHandler> logger)
        {
            _actionsRepository = actionsRepository ?? throw new ArgumentException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ActionsCommand message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Creating Actions - Actions: {@Message}", message);

            var actions = new Actions(actionName: message.Name,
                token: message.UniqueToken,
                accountNo: message.AccountNo,
                userId: message.UserID,
                payload: message.Payload,
                sender: message.Sender);

            var actionToUpdate =  _actionsRepository.Add(actions);

            actionToUpdate.SetAwaitingValidationStatus();
         
            var result = await _actionsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return result;

        }
    }


}
