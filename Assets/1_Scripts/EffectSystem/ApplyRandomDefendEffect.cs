using Core;
using Gameplay;
using UI;

namespace EffectSystem
{
    public class ApplyRandomDefendEffect : ApplyRandomEffect
    {
        private RandomDefendEffect effect;

        private bool lucky;
        public bool GetLucky { get => lucky; }

        protected override void Apply(Effect currentEffect)
        {
            characterData = BoxController.GetController<FightSceneController>().GetCurrentCharacter;

            effect = currentEffect as RandomDefendEffect;

            //maxValue = MAX_VALUE;
            //chanceValue = characterData.GetValueAttribute(effect.RandomAttribute);

        }

        public override void StopApplyEffect()
        {
            BoxController.GetController<LogController>().LogError("Not have logic stop apply effect");
        }
    }
}