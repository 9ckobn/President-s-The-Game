using Core;
using Gameplay;
using UI;

namespace EffectSystem
{
    public class ApplyRandomUpAttrinuteEffect : ApplyRandomEffect
    {
        private RandomUpAttributeEffect effect;

        protected override void Apply(Effect currentEffect)
        {
            characterData = BoxController.GetController<FightSceneController>().GetCurrentCharacter;

            text = "";
            effect = currentEffect as RandomUpAttributeEffect;

            maxValue = MAX_VALUE;
            chanceValue = effect.Value;

            if (effect.IsNeedAttribute)
            {
                chanceValue += characterData.GetValueAttribute(effect.TypeAttribute);
            }

            CountRandom();
        }

        protected override void WinRandom()
        {
            characterData.UpAttribute(effect.TypeWinAttribute, effect.WinProcent);

            text += $"<color=green>добавляем : {effect.TypeWinAttribute} {effect.WinProcent}</color>\n";
            UIManager.Instance.GetWindow<RandomWindow>().ShowText(text);
        }

        protected override void LoseRandom()
        {
            characterData.DownAttribute(effect.TypeLoseAttribute, effect.LoseProcent);
            characterData.ShowDamage(effect.TypeLoseAttribute);

            text += $"<color=red>отнимаем : {effect.TypeLoseAttribute} {effect.LoseProcent}</color>\n";
            UIManager.Instance.GetWindow<RandomWindow>().ShowText(text);
        }
    }
}