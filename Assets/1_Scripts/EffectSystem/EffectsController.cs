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
        }

        public void ApplyFightCardEffects(CardFightModel card)
        {
            effects = card.GetFightData.GetEffects;

            NextEffect();
        }

        private void NextEffect()
        {
            counterEffects++;

            if(counterEffects >= effects.Count)
            {
                EndApplyAllEffects();
            }
            else
            {
                currentEffect = effects[counterEffects];

                if (canApply.CheckApply(currentEffect))
                {
                    ApplyEffect(currentEffect);
                }
            }
        }

        private void ApplyEffect(Effect effect)
        {
            currentApply = null;

            if (effect is AttackEffect)
            {
                currentApply = attackApply;
            }
            else if (effect is BuffEffect)
            {
            }
            else if (effect is OtherEffect)
            {
            }
            else if (effect is ProtectEffect)
            {
            }
            else if (effect is RandomGetProtectEffect)
            {
            }
            else if (effect is RandomUpAttributeEffect)
            {
            }

            if(currentApply == null)
            {
                BoxController.GetController<LogController>().LogError($"Not have apply effect! {effect}");
            }
            else
            {
                currentApply.EndApply += EndApplyEffect;
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