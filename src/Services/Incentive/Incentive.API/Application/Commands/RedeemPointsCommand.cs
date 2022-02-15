using ezLoyalty.Services.Incentive.Domain.PointsAggregate;
using MediatR;

namespace ezLoyalty.Services.Incentive.API.Application.Commands
{
    public class RedeemPointsCommand : IRequest<Points>
    {
        public int AccountNo    { get; set; }
        
        public int RedeemPoints { get; set; }

        public DateOnly earnedDate { get; set; }

    }
}