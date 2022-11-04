using Buildings;
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
        protected List<TypeAttribute> targetAttributes = new List<TypeAttribute>();

        public abstract void SelectTargetBuilding(Building building);

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