using ezLoyalty.Services.Incentive.Domain.Events;
using ezLoyalty.Services.Incentive.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Incentive.Domain.PointsAggregate
{
    public class Points
        : BaseAuditableEntity, IAggregateRoot
    {
        public Points()
        {
            CreatedDate = DateTime.Now;

        }

        [Key]
        public override int Id { get; protected set; }
        
        public int AccountNo { get; set; }

        public int ActionRecordId { get; set; }

        public int ActionId { get; set; }

        public decimal EarnedPoints { get; set; }

        public DateTime EarnedDate { get; set; }

        public void SetIncentiveAwarded() => AddDomainEvent(new PointsAwardedDomainEvent(this));
    }
}
