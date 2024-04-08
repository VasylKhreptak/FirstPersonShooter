using System;

namespace Serialization.MinMax
{
    [Serializable]
    public abstract class MinMaxValue<T>
    {
        public readonly T Min;
        public readonly T Max;

        protected MinMaxValue(T min, T max)
        {
            Min = min;
            Max = max;
        }

        public abstract T Random();
    }
}