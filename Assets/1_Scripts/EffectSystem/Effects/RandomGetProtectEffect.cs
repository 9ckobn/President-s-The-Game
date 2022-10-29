using EffectSystem.SCRO;

namespace EffectSystem
{
    public class RandomGetProtectEffect : Effect
    {
        public TypeSelectTarget TypeSelectTarget; // Кто выбирает цель эффекта?
        public TypeAttribute[] ProtectAttributes; // Какие объекты под защитой?
        public TypeAttribute RandomAttribute; // Какой атрибут использовать для рандома?

        public RandomGetProtectEffect(SCRO_RandomGetProtectEffect data) : base(data)
        {
            TypeSelectTarget = data.TypeSelectTarget;
            ProtectAttributes = data.ProtectAttributes;
            RandomAttribute = data.RandomAttribute;
        }
    }
}