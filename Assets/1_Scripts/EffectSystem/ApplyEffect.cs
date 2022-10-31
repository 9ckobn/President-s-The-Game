using Buildings;

namespace EffectSystem
{
    public abstract class ApplyEffect
    {
        public delegate void AfterApply();
        public event AfterApply EndApplyEvent;

        public abstract void Apply(Effect currentEffect);
        public abstract void SelectTargetBuilding(Building building);

        protected void EndApply()
        {
            EndApplyEvent?.Invoke();
        }
    }
}