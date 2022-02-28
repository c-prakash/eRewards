using ezLoyalty.Services.Actions.Domain.ActionsAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.API.Application.Commands.ActionMetadataCommands
{
    public class UpdateActionMetadataCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PossiblePoints { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }

        public string Sender { get; set; }
    }

    public class UpdateActionMetadataCommandHandler : IRequestHandler<UpdateActionMetadataCommand, bool>
    {
        private readonly IActionsRepository _actionsRepository;
        private readonly ILogger<UpdateActionMetadataCommandHandler> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionsRepository"></param>
        /// <param name="logger"></param>
        public UpdateActionMetadataCommandHandler(IActionsRepository actionsRepository, ILogger<UpdateActionMetadataCommandHandler> logger)
        {
            _actionsRepository = actionsRepository ?? throw new ArgumentException(nameof(actionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(UpdateActionMetadataCommand message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Update ActionMetadata - ActionMetadata: {@Message}", message);

            var actionMetadata = await _actionsRepository.GetActionMetadatAsync(message.Id);

            actionMetadata.Name = message.Name;
            actionMetadata.Description = message.Description;
            actionMetadata.PossiblePoints = message.PossiblePoints;
            actionMetadata.EffectiveFrom = message.EffectiveFrom;
            actionMetadata.EffectiveTo = message.EffectiveTo;
            actionMetadata.UpdatedBy = message.Sender;
            actionMetadata.UpdatedDate = DateTime.Now;

            await _actionsRepository.Update(actionMetadata);

            return true;
        }
    }
}
