using System;

namespace App.Models
{
    public class ModelBase
    {
        public Guid Id { get; set; }

        private bool Equals(ModelBase other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ModelBase) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}