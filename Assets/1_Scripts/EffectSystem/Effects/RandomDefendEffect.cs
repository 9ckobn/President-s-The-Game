using EffectSystem.SCRO;

namespace EffectSystem
{
    public class RandomDefendEffect : DefendEffect
    {
        public TypeAttribute RandomAttribute; // ����� ������� ������������ ��� �������?

        public RandomDefendEffect(SCRO_RandomDefendEffect data) : base(data)
        {
            RandomAttribute = data.RandomAttribute;
        }
    }
}