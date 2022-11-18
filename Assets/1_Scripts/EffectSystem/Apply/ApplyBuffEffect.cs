using Core;
using Data;
using Gameplay;

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
        private BuffEffect effect;
        private CharacterData characterData;

        protected override void Apply(Effect currentEffect)
        {
            effect = currentEffect as BuffEffect;
            characterData = BoxController.GetController<CharactersDataController>().GetCurrentCharacter;

            if (effect.TypeBuff == TypeBuff.UpAttribute)
            {
                int buff = effect.BaseValue;
                buff += (int)(characterData.GetValueAttribute(effect.TypeAttribute) / 100f * effect.ValueAttribute);

                foreach (var typeTarget in effect.TypesTargetObject)
                {
                    characterData.UpAttribute(typeTarget, buff);
                }
            }

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