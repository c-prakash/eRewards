
namespace ezLoyalty.Services.Incentive.Domain.Seedwork
{
    public abstract class BaseAuditableEntity : Entity
    {
        private string createdBy;
        private DateTime createdDate;
        private string updatedBy;
        private DateTime? updatedDate;

        public string CreatedBy { get => createdBy; set => createdBy = value; }
        public DateTime CreatedDate { get => createdDate; set => createdDate = value; }
        public string UpdatedBy { get => updatedBy; set => updatedBy = value; }
        public DateTime? UpdatedDate { get => updatedDate; set => updatedDate = value; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BaseAuditableEntity))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (GetType() != obj.GetType())
                return false;
            BaseAuditableEntity item = (BaseAuditableEntity)obj;
            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item.Id == Id;
        }

        public static bool operator ==(BaseAuditableEntity left, BaseAuditableEntity right)
        {
            if (Equals(left, null))
                return Equals(right, null) ? true : false;
            else
                return left.Equals(right);
        }
        public static bool operator !=(BaseAuditableEntity left, BaseAuditableEntity right)
        {
            return !(left == right);
        }
    }
}
