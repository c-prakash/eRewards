using ezLoyalty.Services.Actions.Domain.ActionsAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.API.Application.Commands.ActionMetadataCommands
{
    public class NewActionMetadataCommand : IRequest<ActionMetadata>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int PossiblePoints { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }

        public string Sender { get; set; }
    }

    public class NewActionMetadataCommandHandler : IRequestHandler<NewActionMetadataCommand, ActionMetadata>
    {
        private readonly IActionsRepository _actionsRepository;
        private readonly ILogger<NewActionMetadataCommandHandler> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionsRepository"></param>
        /// <param name="logger"></param>
        public NewActionMetadataCommandHandler(IActionsRepository actionsRepository, ILogger<NewActionMetadataCommandHandler> logger)
        {
            _actionsRepository = actionsRepository ?? throw new ArgumentException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ActionMetadata> Handle(NewActionMetadataCommand message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Update ActionMetadata - ActionMetadata: {@Message}", message);

            var actionMetadata = new ActionMetadata
            {

                Name = message.Name,
                Description = message.Description,
                PossiblePoints = message.PossiblePoints,
                EffectiveFrom = message.EffectiveFrom,
                EffectiveTo = message.EffectiveTo,
                CreatedBy = message.Sender
            };

            var result = await _actionsRepository.Add(actionMetadata);

            return result;
        }
    }
}
