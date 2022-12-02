using Core;
using Gameplay;
using System.Collections;
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

            bool luck = BoxController.GetController<RandomController>().CountRandom(chanceValue);

            if (luck)
            {
                int procent = (int)(characterData.GetValueAttribute(effect.TypeWinAttribute) / 100f * effect.WinProcent);

                characterData.UpAttribute(effect.TypeWinAttribute, procent);
            }
            else
            {
                int procent = (int)(characterData.GetValueAttribute(effect.TypeLoseAttribute) / 100f * effect.LoseProcent);

                characterData.DownAttribute(effect.TypeLoseAttribute, procent);
            }

            Coroutines.StartRoutine(CoShowRandom(luck));
        }

        private IEnumerator CoShowRandom(bool luck)
        {
            BoxController.GetController<CardsController>().GetSelectedCard.ShowHighlightRandom(luck);
            yield return new WaitForSeconds(1.5f);
            BoxController.GetController<CardsController>().GetSelectedCard.HideHighlightRandom();

            EndApply();

        }

        public override void StopApplyEffect()
        {
            LogManager.LogError("Not have logic stop apply effect");
        }
    }
}