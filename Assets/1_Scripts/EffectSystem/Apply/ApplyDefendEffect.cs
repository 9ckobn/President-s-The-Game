using Core;
using Data;
using Gameplay;

namespace EffectSystem
{
    public class ApplyDefendEffect : ApplyEffect
    {
        private DefendEffect effect;

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as DefendEffect;

            CharacterData characterData = BoxController.GetController<CharactersDataController>().GetCurrentCharacter;
            foreach (var typeDefend in effect.TypeDefends)
            {
                if (effect.IsGodDefend)
                {
                    characterData.AddGodDefend(typeDefend);
                }
                else
                {
                    int attributeValue = (int)(characterData.GetValueAttribute(effect.TypeNeedAttribute) / 100f * effect.ValueAttribute);
                    characterData.AddDefend(typeDefend, effect.BaseValue + attributeValue);
                }
            }

            EndApply();
        }

        public override void SelectTargetBuilding(TypeAttribute targetAttribute)
        {
        }

        public override void StopApplyEffect()
        {
            BoxController.GetController<LogController>().LogError("Not have logic stop apply effect");
        }
    }
}