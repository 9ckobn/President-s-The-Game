using EffectSystem.SCRO;

namespace EffectSystem
{
    public class RandomGetDefendEffect : Effect
    {
        public TypeSelectTarget TypeSelectTarget; // Кто выбирает цель эффекта?
        public TypeAttribute[] DefendAttributes; // Какие объекты под защитой?
        public TypeAttribute RandomAttribute; // Какой атрибут использовать для рандома?

        public RandomGetDefendEffect(SCRO_RandomGetDefendEffect data) : base(data)
        {
            TypeSelectTarget = data.TypeSelectTarget;
            DefendAttributes = data.ProtectAttributes;
            RandomAttribute = data.RandomAttribute;
        }

        public object TypeWinAttribute { get; internal set; }
    }
}