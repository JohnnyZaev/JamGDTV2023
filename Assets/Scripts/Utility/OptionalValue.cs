using System;

namespace Utility
{
    [Serializable]
    public struct OptionalValue<T>
    {
        public bool hasValue;
        public T value;

        public static implicit operator T(OptionalValue<T> optionalValue)
        {
            return optionalValue.value;
        }
    }
}
