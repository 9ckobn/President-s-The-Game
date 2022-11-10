using UnityEngine;

namespace EffectSystem
{
    public class ApplyBuffEffect : ApplyEffect
    {
        protected override void Apply(Effect currentEffect)
        {
            EndApply();
        }

        public override void SelectTargetBuilding(TypeAttribute targetAttribute)
        {
        }

        public override void StopApplyEffect()
        {
        }
    }
}