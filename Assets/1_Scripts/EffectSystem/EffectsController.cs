using Buildings;
using Cards;
using Core;
using Data;
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
        private ApplyRandomUpAttrinuteEffect randomUpAttributeApply;
        private ApplyRandomDefendEffect randomDefendApply;

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
            randomUpAttributeApply = new ApplyRandomUpAttrinuteEffect();
            randomDefendApply = new ApplyRandomDefendEffect();
        }

        #region EFFECTS_AFTER_CKICK_CARD

        public void ApplyFightCardEffects(CardFightModel card)
        {
            counterEffects = 0;
            currentCardFight = card;
            effects = card.GetEffects;

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
            else if (currentEffect is RandomDefendEffect)
            {
                currentApply = randomDefendApply;
            }
            else if (currentEffect is RandomUpAttributeEffect)
            {
                currentApply = randomUpAttributeApply;
            }

            if (currentApply == null)
            {
                BoxController.GetController<LogController>().LogError($"Not have apply effect! {currentEffect}");
            }
            else
            {
                currentApply.EndApplyEvent += EndApplyEffect;
                currentApply.StartApply(currentEffect);
            }
        }

        private void EndApplyEffect()
        {
            currentApply.EndApplyEvent -= EndApplyEffect;

            NextEffect();
        }

        private void EndApplyAllEffects()
        {
            // Pay cost fight card

            CharacterData characterData = BoxController.GetController<FightSceneController>().GetCurrentCharacter;

            foreach (var typeCost in currentCardFight.GetTypeCost)
            {
                characterData.DownAttribute(typeCost, currentCardFight.GetValueCost);
            }

            BoxController.GetController<CardsController>().EndUseCard(currentCardFight);
        }

        #endregion

        #region CHECK_RANDOM_EFFECTS

        public bool CherkRandomDefendEffect(RandomDefendEffect effect)
        {
            randomDefendApply.StartApply(effect);

            return randomDefendApply.GetLucky;
        }

        #endregion
    }
}