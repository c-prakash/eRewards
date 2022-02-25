using ezLoyalty.Services.Incentive.Domain.PointsAggregate;
using MediatR;

namespace ezLoyalty.Services.Incentive.API.Application.Commands
{
    public class GetPointsCommand : IRequest<IEnumerable<Points>>
    {
        public int AccountNo    { get; set; }
        public int ActionId { get; set; }
        public DateTime EarnedDate { get; set; }
    }
}