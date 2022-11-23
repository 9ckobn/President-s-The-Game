using EffectSystem;

namespace Data
{
    public class DataBuffPresidents
    {
        public TypeAttribute TypeAttribute { get; private set; }
        public int Value { get; private set; }

        public DataBuffPresidents(TypeAttribute attribute, int value)
        {
            TypeAttribute = attribute;
            Value = value;
        }
    }
}