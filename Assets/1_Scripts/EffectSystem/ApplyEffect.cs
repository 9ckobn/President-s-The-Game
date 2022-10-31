using Buildings;
using System.Collections.Generic;

namespace EffectSystem
{
    public abstract class ApplyEffect
    {
        public delegate void AfterApply();
        public event AfterApply EndApplyEvent;

        protected List<TypeAttribute> targetAttributes = new List<TypeAttribute>();

        public abstract void Apply(Effect currentEffect);
        public abstract void SelectTargetBuilding(Building building);

        protected void EndApply()
        {
            EndApplyEvent?.Invoke();
        }
    }
}