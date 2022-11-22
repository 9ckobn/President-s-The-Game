using EffectSystem;

namespace Data
{
    public class DataBuffPresidents
    {
        public TypeAttribute Attribute { get; private set; }
        public int Value { get; private set; }

        public DataBuffPresidents(TypeAttribute attribute, int value)
        {
            Attribute = attribute;
            Value = value;
        }
    }
}