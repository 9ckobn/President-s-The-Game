using EffectSystem.SCRO;

namespace EffectSystem
{
    public class RandomDefendEffect : DefendEffect
    {
        public TypeAttribute RandomAttribute; //  акой атрибут использовать дл€ рандома?

        public RandomDefendEffect(SCRO_RandomDefendEffect data) : base(data)
        {
            RandomAttribute = data.RandomAttribute;
        }
    }
}