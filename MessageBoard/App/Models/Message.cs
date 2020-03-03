using System;

namespace App.Models
{
    public class Message : ModelBase
    {
        public string Value { get; set; }
        public string User { get; set; }
        public DateTime Created { get; set; }

        private bool Equals(Message other)
        {
            return Value == other.Value && User == other.User && Created.Equals(other.Created);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Message) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, User, Created);
        }
    }
}