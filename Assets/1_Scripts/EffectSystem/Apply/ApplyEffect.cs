using Core;
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

        public abstract void SelectTargetBuilding(TypeAttribute targetAttribute);

        public void StartApply(Effect effect)
        {
            isPlayer = BoxController.GetController<FightSceneController>().GetIsPlayerNow;

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