using Cards;
using Core;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "EffectsController", menuName = "Controllers/Gameplay/EffectsController")]
    public class EffectsController : BaseController
    {
        private CanApplyEffect canApply;
        private ApplyAttackEffect attackApply;

        private List<Effect> effects;
        private Effect currentEffect;
        private ApplyEffect currentApply;
        private int counterEffects = 0;

        public override void OnInitialize()
        {
            canApply = new CanApplyEffect();
            attackApply = new ApplyAttackEffect();
        }

        public void ApplyFightCardEffects(CardFightModel card)
        {
            effects = card.GetFightData.GetEffects;

            NextEffect();
        }

        private void NextEffect()
        {
            if(counterEffects >= effects.Count)
            {
                EndApplyAllEffects();
            }
            else
            {
                currentEffect = effects[counterEffects];

                if (canApply.CheckApply(currentEffect))
                {
                    ApplyEffect();
                }
            }

            counterEffects++;
        }

        private void ApplyEffect()
        {
            currentApply = null;

            if (currentEffect is AttackEffect)
            {
                currentApply = attackApply;
            }
            else if (currentEffect is BuffEffect)
            {
            }
            else if (currentEffect is OtherEffect)
            {
            }
            else if (currentEffect is ProtectEffect)
            {
            }
            else if (currentEffect is RandomGetProtectEffect)
            {
            }
            else if (currentEffect is RandomUpAttributeEffect)
            {
            }

            if (currentApply == null)
            {
                BoxController.GetController<LogController>().LogError($"Not have apply effect! {currentEffect}");
            }
            else
            {
                currentApply.EndApply += EndApplyEffect;
                currentApply.Apply(currentEffect);
            }
        }

        private void EndApplyEffect()
        {
            currentApply.EndApply -= EndApplyEffect;

            NextEffect();
        }

        private void EndApplyAllEffects()
        {

        }
    }
}