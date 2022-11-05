using Core;
using Gameplay;

namespace EffectSystem
{
    public class ApplyRandomUpAttrinuteEffect : ApplyRandomEffect
    {
        private RandomUpAttributeEffect effect;

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as RandomUpAttributeEffect;

            characterData = BoxController.GetController<FightSceneController>().GetCurrentCharacter;

            int chanceValue = effect.Value;

            if (effect.IsNeedAttribute)
            {
                chanceValue += characterData.GetValueAttribute(effect.TypeAttribute);
            }

            if (BoxController.GetController<RandomController>().CountRandom(chanceValue))
            {
                characterData.UpAttribute(effect.TypeWinAttribute, effect.WinProcent);
            }
            else
            {
                characterData.DownAttribute(effect.TypeLoseAttribute, effect.LoseProcent);
            }

            EndApply();
        }

        public override void StopApplyEffect()
        {
            BoxController.GetController<LogController>().LogError("Not have logic stop apply effect");
        }
    }
}