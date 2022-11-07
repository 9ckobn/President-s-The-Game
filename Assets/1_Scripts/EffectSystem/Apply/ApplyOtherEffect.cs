using Core;

namespace EffectSystem
{
    public class ApplyOtherEffect : ApplyEffect
    {
        protected override void Apply(Effect currentEffect)
        {
            OtherEffect effect = currentEffect as OtherEffect;


        }

        public override void SelectTargetBuilding(TypeAttribute targetAttribute)
        {
        }

        public override void StopApplyEffect()
        {
            BoxController.GetController<LogController>().LogError("Not have logic stop apply effect");
        }
    }
}