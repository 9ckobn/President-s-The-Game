using EffectSystem;

namespace Data
{
    public class AttributeTextData
    {
        public int Value;
        public TypeAttribute TypeAttribute;

        public AttributeTextData(TypeAttribute typeAttribute, int value)
        {
            TypeAttribute = typeAttribute;
            Value = value;
        }
    }
}