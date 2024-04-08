using System;

namespace Serialization.MinMax
{
    [Serializable]
    public class FloatMinMax : MinMaxValue<float>
    {
        public FloatMinMax(float min, float max) : base(min, max) { }

        public override float Random() => UnityEngine.Random.Range(Min, Max);
    }
}