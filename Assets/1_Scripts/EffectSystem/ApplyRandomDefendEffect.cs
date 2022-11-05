using Core;
using Gameplay;

namespace EffectSystem
{
    public class ApplyRandomDefendEffect : ApplyRandomEffect
    {
        private RandomDefendEffect effect;

        private bool lucky;
        public bool GetLucky { get => lucky; }

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as RandomDefendEffect;
            characterData = BoxController.GetController<FightSceneController>().GetCurrentCharacter;

            foreach (var typeDefend in effect.TypeDefends)
            {
                int procentRandom = characterData.GetAttribute(effect.RandomAttribute).Value;

                characterData.GetAttribute(typeDefend).SetDefend(true, effect.ValueDefend, procentRandom);
                characterData.ShowGetDefend(typeDefend, procentRandom);
            }

            EndApply();
        }

        public override void StopApplyEffect()
        {
            BoxController.GetController<LogController>().LogError("Not have logic stop apply effect");
        }
    }
}