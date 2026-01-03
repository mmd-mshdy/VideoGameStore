using System;
namespace VideoGameStore.Domain.Abstractions
{
    public abstract class ValueObject
    {
        public abstract IEnumerable<object> GetAtomicValue();

        public override int GetHashCode()
        {
            return GetAtomicValue().Aggregate(default(int), HashCode.Combine);
        }
        public override bool Equals(object? obj)
        {
            return obj is ValueObject other && ValuesAreEqual(other);
        }
        private bool ValuesAreEqual(ValueObject other)
        {
            return GetAtomicValue().SequenceEqual(other.GetAtomicValue());
        }
            
    }
}
