using Buildings;
using Cards;
using Core;
using Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{
    [CreateAssetMenu(fileName = "EffectsController", menuName = "Controllers/Gameplay/EffectsController")]
    public class EffectsController : BaseController
    {
        private CanApplyEffect canApply;
        private ApplyAttackEffect attackApply;
        private ApplyDefendEffect defendApply;
        private ApplyRandomEffect randomApply;

        private List<Effect> effects;
        private Effect currentEffect;
        private ApplyEffect currentApply;
        private CardFightModel currentCardFight;
        private int counterEffects = 0;

        public CardFightModel GetCurrentCardFight { get => currentCardFight; }

        public override void OnInitialize()
        {
            canApply = new CanApplyEffect();
            attackApply = new ApplyAttackEffect();
            defendApply = new ApplyDefendEffect();
            randomApply = new ApplyRandomEffect();
        }

        public void ApplyFightCardEffects(CardFightModel card)
        {
            counterEffects = 0;
            currentCardFight = card;
            effects = card.GetFightData.GetEffects;

            NextEffect();
        }

        public void SelectTargetBuilding(Building building)
        {
            currentApply.SelectTargetBuilding(building);
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
                counterEffects++;

                if (canApply.CheckApply(currentEffect))
                {
                    ApplyEffect();
                }
            }
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
            else if (currentEffect is DefendEffect)
            {
                currentApply = defendApply;
            }
            else if (currentEffect is RandomGetProtectEffect)
            {
            }
            else if (currentEffect is RandomUpAttributeEffect)
            {
                currentApply = randomApply;
            }

            if (currentApply == null)
            {
                BoxController.GetController<LogController>().LogError($"Not have apply effect! {currentEffect}");
            }
            else
            {
                currentApply.EndApplyEvent += EndApplyEffect;
                currentApply.Apply(currentEffect);
            }
        }

        private void EndApplyEffect()
        {
            currentApply.EndApplyEvent -= EndApplyEffect;

            NextEffect();
        }

        private void EndApplyAllEffects()
        {
            BoxController.GetController<CardsController>().EndUseCard(currentCardFight);
        }
    }
}