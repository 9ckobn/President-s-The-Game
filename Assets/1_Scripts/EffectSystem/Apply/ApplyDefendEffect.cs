using Buildings;
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

            CharacterData characterData = BoxController.GetController<FightSceneController>().GetCurrentCharacter;

            foreach (var typeDefend in effect.TypeDefends)
            {
                characterData.AddDefend(typeDefend, false, effect.ValueDefend);
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