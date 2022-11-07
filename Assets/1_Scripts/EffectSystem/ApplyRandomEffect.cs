using Buildings;
using Data;

namespace EffectSystem
{
    public abstract class ApplyRandomEffect : ApplyEffect
    {
        protected CharacterData characterData;

        public override void SelectTargetBuilding(TypeAttribute targetAttribute)
        {
        }
    }
}