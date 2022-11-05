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

            //text = "";
            //effect = currentEffect as RandomUpAttributeEffect;

            //maxValue = MAX_VALUE;
            //chanceValue = effect.Value;

            //if (effect.IsNeedAttribute)
            //{
            //    chanceValue += characterData.GetValueAttribute(effect.TypeAttribute);
            //}

            //CountRandom();

            //if (lucky)
            //{
            //    characterData.UpAttribute(effect.TypeWinAttribute, effect.WinProcent);

            //}
            //else
            //{
            //    characterData.DownAttribute(effect.TypeLoseAttribute, effect.LoseProcent);
            //    characterData.ShowDamage(effect.TypeLoseAttribute);
            //}
        }

        public override void StopApplyEffect()
        {
            BoxController.GetController<LogController>().LogError("Not have logic stop apply effect");
        }
    }
}