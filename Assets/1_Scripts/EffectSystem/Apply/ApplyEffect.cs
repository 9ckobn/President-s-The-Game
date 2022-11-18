using Core;
using Data;
using Gameplay;
using System.Collections.Generic;

namespace EffectSystem
{
    public abstract class ApplyEffect
    {
        public delegate void AfterApply();
        public event AfterApply EndApplyEvent;

        protected bool isPlayer;
        protected List<TypeAttribute> targetAttributes;

        protected void ShowTargetBuildings(CharacterData characterData)
        {
            targetAttributes = new List<TypeAttribute>();

            targetAttributes.Add(TypeAttribute.Economic);
            targetAttributes.Add(TypeAttribute.Food);
            targetAttributes.Add(TypeAttribute.Medicine);
            targetAttributes.Add(TypeAttribute.RawMaterials);

            foreach (var target in targetAttributes)
            {
                characterData.ShowTargetAttribute(target);
            }
        }

        protected void HideTargetBuildings(CharacterData characterData)
        {
            foreach (var target in targetAttributes)
            {
                characterData.HideTargetAttribute(target);
            }
        }

        public abstract void SelectTargetBuilding(TypeAttribute targetAttribute);

        public void StartApply(Effect effect)
        {
            isPlayer = BoxController.GetController<CharactersDataController>().GetIsPlayerNow;

            Apply(effect);
        }

        public abstract void StopApplyEffect();
        protected abstract void Apply(Effect currentEffect);        

        protected void EndApply()
        {
            EndApplyEvent?.Invoke();
        }
    }
}