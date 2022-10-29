using EffectSystem.SCRO;

namespace EffectSystem
{
    public class RandomGetProtectEffect : Effect
    {
        public TypeSelectTarget TypeSelectTarget; // ��� �������� ���� �������?
        public TypeAttribute[] ProtectAttributes; // ����� ������� ��� �������?
        public TypeAttribute RandomAttribute; // ����� ������� ������������ ��� �������?

        public RandomGetProtectEffect(SCRO_RandomGetProtectEffect data) : base(data)
        {
            TypeSelectTarget = data.TypeSelectTarget;
            ProtectAttributes = data.ProtectAttributes;
            RandomAttribute = data.RandomAttribute;
        }
    }
}