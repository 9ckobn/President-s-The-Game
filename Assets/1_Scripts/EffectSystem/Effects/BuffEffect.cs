using EffectSystem.SCRO;

namespace EffectSystem
{
    public class BuffEffect : Effect
    {
        public TypeBuff TypeBuff { get; private set; }
        public TypeAttribute[] TypesTargetObject { get; private set; } // ������ �� ������� ��������� ����
        public int BaseValue { get; private set; }
        public TypeAttribute TypeAttribute { get; private set; } // ��������� �������� ��������?
        public int ValueAttribute { get; private set; }

        public BuffEffect(SCRO_BuffEffect data) : base(data)
        {
            TypeBuff = data.TypeBuff;
            TypesTargetObject = data.TypesTargetObject;
            BaseValue = data.BaseValue;
            TypeAttribute = data.TypeAttribute;
            ValueAttribute = data.ValueAttribute;
        }
    }
}