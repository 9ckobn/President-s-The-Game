using Core;
using Gameplay;
using UnityEngine;

namespace EffectSystem
{
    public class ApplyRandomUpAttrinuteEffect : ApplyRandomEffect
    {
        private RandomUpAttributeEffect effect;

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as RandomUpAttributeEffect;

            characterData = BoxController.GetController<CharactersDataController>().GetCurrentCharacter;

            int chanceValue = effect.Value;

            if (effect.IsNeedAttribute)
            {
                chanceValue += characterData.GetValueAttribute(effect.TypeAttribute);
            }

            if (BoxController.GetController<RandomController>().CountRandom(chanceValue))
            {
                int procent = (int)(characterData.GetValueAttribute(effect.TypeWinAttribute) / 100f * effect.WinProcent);

                characterData.UpAttribute(effect.TypeWinAttribute, procent);
            }
            else
            {
                int procent = (int)(characterData.GetValueAttribute(effect.TypeLoseAttribute) / 100f * effect.LoseProcent);

                characterData.DownAttribute(effect.TypeLoseAttribute, procent);
            }

            EndApply();
        }

        public override void StopApplyEffect()
        {
            LogManager.LogError("Not have logic stop apply effect");
        }
    }
}