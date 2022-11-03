using EffectSystem.SCRO;

namespace EffectSystem
{
    public class RandomGetDefendEffect : Effect
    {
        public TypeSelectTarget TypeSelectTarget; // ��� �������� ���� �������?
        public TypeAttribute[] DefendAttributes; // ����� ������� ��� �������?
        public TypeAttribute RandomAttribute; // ����� ������� ������������ ��� �������?

        public RandomGetDefendEffect(SCRO_RandomGetDefendEffect data) : base(data)
        {
            TypeSelectTarget = data.TypeSelectTarget;
            DefendAttributes = data.ProtectAttributes;
            RandomAttribute = data.RandomAttribute;
        }

        public object TypeWinAttribute { get; internal set; }
    }
}