using UnityEngine;

namespace EffectSystem
{
    /// <summary>
    /// 
    /// ERROR LOGIC
    /// Buff effect never apply here
    /// Buff effect apply after attack in ApplyAttackEffect
    /// Buff cancel in CheckEffectAfterEvent
    /// 
    /// </summary>
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