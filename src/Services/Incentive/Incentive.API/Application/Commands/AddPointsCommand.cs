using ezLoyalty.Services.Incentive.Domain.PointsAggregate;
using MediatR;

namespace ezLoyalty.Services.Incentive.API.Application.Commands
{
    public class AddPointsCommand : IRequest<bool>
    {
        public int AccountNo { get; set; }

        public int ActionId { get; set; }
        public decimal EarnedPoints { get; set; }

        public DateTime EarnedDate { get; set; } = DateTime.Now;

        public string User { get; set; }    
    }
}
