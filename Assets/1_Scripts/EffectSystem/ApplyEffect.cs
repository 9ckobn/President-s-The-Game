namespace EffectSystem
{
    public abstract class ApplyEffect
    {
        public delegate void AfterApply();
        public event AfterApply EndApply;

        public abstract void Apply(Effect currentEffect);
    }
}