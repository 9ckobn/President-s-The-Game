using Core;
using Gameplay;
using UI;

namespace EffectSystem
{
    public class ApplyRandomGetDefendEffect : ApplyRandomEffect
    {
        private RandomGetDefendEffect effect;

        protected override void Apply(Effect currentEffect)
        {
            characterData = BoxController.GetController<FightSceneController>().GetCurrentCharacter;

            text = "";
            effect = currentEffect as RandomGetDefendEffect;

            maxValue = MAX_VALUE;
            chanceValue = characterData.GetValueAttribute(effect.RandomAttribute);

            CountRandom();
        }

        protected override void WinRandom()
        {
            foreach (var defendEffect in effect.DefendAttributes)
            {
                characterData.AddTemporaryEffect(effect);
                characterData.ShowDefend(defendEffect);
            }

            text += $"<color=green>������ ��������</color>\n";
            UIManager.Instance.GetWindow<RandomWindow>().ShowText(text);
        }

        protected override void LoseRandom()
        {
            text += $"<color=red>������ �� ��������</color>\n";
            UIManager.Instance.GetWindow<RandomWindow>().ShowText(text);
        }
    }
}