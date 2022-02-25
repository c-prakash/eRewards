using ezLoyalty.Services.Incentive.Domain.PointsAggregate;
using MediatR;

namespace ezLoyalty.Services.Incentive.API.Application.Commands
{
    public class GetPointsCommandHandler : IRequestHandler<GetPointsCommand, IEnumerable<Points>>
    {
        private readonly IPointsRepository _pointsRepository;
        private readonly ILogger<GetPointsCommandHandler> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pointsRepository"></param>
        /// <param name="logger"></param>
        public GetPointsCommandHandler(IPointsRepository pointsRepository, ILogger<GetPointsCommandHandler> logger)
        {
            _pointsRepository = pointsRepository ?? throw new ArgumentException(nameof(pointsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Points>> Handle(GetPointsCommand message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Adding points - points: {@Message}", message);

            var result = await _pointsRepository.GetAsync(message.AccountNo, message.ActionId, message.EarnedDate);

            return result;

        }
    }
}
