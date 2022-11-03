using EffectSystem.SCRO;

namespace EffectSystem
{
    public class AttackEffect : Effect
    {
        public TypeAttribute[] TypeTargetObject { get; private set; }
        public int BaseValue { get; private set; }
        public bool IsNeedAttribute { get; private set; }
        public TypeAttribute TypeAttribute { get; private set; }
        public int ValueAttribute { get; private set; }

        public AttackEffect(SCRO_AttackEffect data) : base(data)
        {
            TypeTargetObject = data.TypeTargetObject;
            BaseValue = data.BaseValue;
            IsNeedAttribute = data.IsNeedAttribute;
            TypeAttribute = data.TypeAttribute;
            ValueAttribute = data.ValueAttribute;
        }
    }
}