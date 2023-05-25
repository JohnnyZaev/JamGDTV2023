using System;

namespace Utility
{
    
    public interface IOptionalValue { }
    
    [Serializable]
    public struct OptionalValue<T> : IOptionalValue
    {
        public bool hasValue;
        public T value;

        public static implicit operator T(OptionalValue<T> optionalValue)
        {
            return optionalValue.value;
        }
    }
}
