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

            text = "";
            effect = currentEffect as RandomDefendEffect;

            maxValue = MAX_VALUE;
            chanceValue = characterData.GetValueAttribute(effect.RandomAttribute);

            CountRandom();
        }

        protected override void WinRandom()
        {
            lucky = true;

            text += $"<color=green>Рандом сработал</color>\n";
            UIManager.Instance.GetWindow<InfoWindow>().ShowText(text);
        }

        protected override void LoseRandom()
        {
            lucky = false;

            text += $"<color=red>Рандом не сработал</color>\n";
            UIManager.Instance.GetWindow<InfoWindow>().ShowText(text);
        }

        public override void StopApplyEffect()
        {
            BoxController.GetController<LogController>().LogError("Not have logic stop apply effect");
        }
    }
}