using ezLoyalty.Services.Incentive.Domain.PointsAggregate;
using MediatR;

namespace ezLoyalty.Services.Incentive.API.Application.Commands
{
    public class GetPointsCommand : IRequest<Points>
    {
        public int AccountNo    { get; set; }
        public int ActionId { get; set; }
        public DateOnly earnedDate { get; set; }
    }
}