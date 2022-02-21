using ezLoyalty.Services.Actions.Domain.Seedwork;
using System;
using System.ComponentModel.DataAnnotations;

namespace ezLoyalty.Services.Actions.Domain.ActionsAggregate
{
    public class ActionMetadata
        :BaseAuditableEntity, IAggregateRoot
    {
        [Key]
        public override int Id { get; protected set; }

        public string Name { get;set; }

        public string Description { get; set; } 

        //public RewardType RewardType { get; set; }

        public int PossiblePoints { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }

    }

    //public enum RewardType
    //{
    //    Points,
    //    Bucks
    //}
}
