using ezLoyalty.Services.Incentive.Domain.PointsAggregate;

namespace ezLoyalty.Services.Incentive.Domain.Events
{

    public class PointsAwardedDomainEvent : MediatR.INotification
    {
        public Points AwardedPoints { get; private set; }

        public PointsAwardedDomainEvent(Points awardedPoints)
        {
            AwardedPoints = awardedPoints;
        }
    }
}
