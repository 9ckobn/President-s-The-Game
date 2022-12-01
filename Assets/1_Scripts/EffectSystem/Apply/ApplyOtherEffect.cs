using Core;
using Data;
using Gameplay;
using UnityEngine;

namespace EffectSystem
{
    public class ApplyOtherEffect : ApplyEffect
    {
        private OtherEffect effect;
        private CharacterData characterData;

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as OtherEffect;
            characterData = BoxController.GetController<CharactersDataController>().GetCurrentCharacter;

            if (effect.TypeOtherEffect == TypeOtherEffect.Loan)
            {
                ShowTargetBuildings(characterData, effect);
            }
            else
            {
                EndApply();
            }
        }

        public override void SelectTargetBuilding(TypeAttribute targetAttribute)
        {
            HideTargetBuildings(characterData);

            if (effect.TypeOtherEffect == TypeOtherEffect.Loan)
            {
                int buff = (int)(characterData.GetValueAttribute(effect.TypeAttributeLoan) / 100f * effect.ProcentLoan);
                characterData.UpAttribute(targetAttribute, buff);
                effect.ValueDamageLoan = (int)(buff / 100f * effect.ValueProcentDamageLoan);
            }

            EndApply();
        }

        public override void StopApplyEffect()
        {
            LogManager.LogError("Not have logic stop apply effect");
        }
    }
}