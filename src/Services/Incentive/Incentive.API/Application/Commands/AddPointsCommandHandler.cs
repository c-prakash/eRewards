using ezLoyalty.Services.Incentive.Domain.PointsAggregate;
using ezLoyalty.Services.Incentive.API.Application.Commands;
using MediatR;
using ezLoyalty.Services.Incentive.API.Application.IntegrationEvents;

namespace ezLoyalty.Services.Incentive.API.Application.Commands
{

    public class AddPointsCommandHandler : IRequestHandler<AddPointsCommand, bool>
    {
        private readonly IPointsRepository _pointsRepository;
        private readonly ILogger<AddPointsCommandHandler> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pointsRepository"></param>
        /// <param name="logger"></param>
        public AddPointsCommandHandler(IPointsRepository pointsRepository, ILogger<AddPointsCommandHandler> logger)
        {
            _pointsRepository = pointsRepository ?? throw new ArgumentException(nameof(pointsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(AddPointsCommand message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Adding points - points: {@Message}", message);

            var actionPoints = new Points
            {
                AccountNo = message.AccountNo,
                ActionRecordId = message.ActionRecordId,
                ActionId = message.ActionId,
                EarnedPoints = message.EarnedPoints,
                EarnedDate = message.EarnedDate,
                CreatedBy = message.Sender,
                CreatedDate = DateTime.Now
            };

            var pointsToUpdate = _pointsRepository.Add(actionPoints);

            pointsToUpdate.SetIncentiveAwarded();

            var result = await _pointsRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return result;

        }
    }
}
