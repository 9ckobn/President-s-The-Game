using Core;

namespace EffectSystem
{
    public class ApplyOtherEffect : ApplyEffect
    {
        /// <summary>
        /// 
        /// ERROR LOGIC
        /// Other effect never apply here
        /// Other effect apply after attack in ApplyAttackEffect
        /// Other cancel in CheckEffectAfterEvent
        /// 
        /// </summary>
        protected override void Apply(Effect currentEffect)
        {
            EndApply();
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