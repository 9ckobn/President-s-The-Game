using Core;
using Data;
using Gameplay;
using System.Collections.Generic;

namespace EffectSystem
{
    public class ApplyDefendEffect : ApplyEffect
    {
        private DefendEffect effect;
        private CharacterData characterData;

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as DefendEffect;
            targetAttributes = new List<TypeAttribute>();

            characterData = BoxController.GetController<CharactersDataController>().GetCurrentCharacter;

            if (effect.TypeSelectTarget == TypeSelectTarget.Game)
            {
                foreach (var typeDefend in effect.TypeDefends)
                {
                    ApplyDefend(typeDefend);
                }

                EndApply();
            }
            else if (effect.TypeSelectTarget == TypeSelectTarget.Player)
            {
                ShowTargetBuildings(characterData);
            }
        }

        public override void SelectTargetBuilding(TypeAttribute targetAttribute)
        {
            effect.SelectedPlayerBuilding = targetAttribute;

            HideTargetBuildings(characterData);
            ApplyDefend(targetAttribute);

            EndApply();
        }

        public override void StopApplyEffect()
        {
            HideTargetBuildings(characterData);

        }

        private void ApplyDefend(TypeAttribute type)
        {
            if (effect.IsGodDefend)
            {
                characterData.AddGodDefend(type);
            }
            else
            {
                int attributeValue = (int)(characterData.GetValueAttribute(effect.TypeNeedAttribute) / 100f * effect.ValueAttribute);
                characterData.AddDefend(type, effect.BaseValue + attributeValue);
            }
        }
    }
}