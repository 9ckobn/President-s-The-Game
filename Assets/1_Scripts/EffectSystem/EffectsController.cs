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
        private ApplyOtherEffect otherApply;
        private ApplyBuffEffect buffApply;
        private CancelEffect cancelEffect;

        private List<Effect> effects;
        private Effect currentEffect;
        private ApplyEffect currentApply;
        private CardFightModel currentCardFight;
        private int counterEffects = 0;

        private TypeTimeApply currentTimeApply;
        private TypeCondition currentCondition;

        public override void OnInitialize()
        {
            canApply = new CanApplyEffect();
            attackApply = new ApplyAttackEffect();
            defendApply = new ApplyDefendEffect();
            randomUpAttributeApply = new ApplyRandomUpAttrinuteEffect();
            otherApply = new ApplyOtherEffect();
            cancelEffect = new CancelEffect();
            buffApply = new ApplyBuffEffect();
        }

        #region START_APPLY_EFFECT

        public void ClickFightCard(CardFightModel card)
        {
            counterEffects = 0;
            currentCardFight = card;
            effects = card.GetEffects;
            currentTimeApply = TypeTimeApply.RightNow;

            NextEffect();
        }

        public void ApplyAfterCondition(TypeCondition condition, List<Effect> effects)
        {
            this.effects = effects;
            currentCondition = condition;
            
        }

        public void StopUseFightCard()
        {
            currentApply.StopApplyEffect();

            currentApply.EndApplyEvent -= EndApplyEffect;
        }

        public void SelectTargetBuilding(TypeAttribute type)
        {
            currentApply.SelectTargetBuilding(type);
        }

        #endregion

        #region APPLY_EFFECT

        private void NextEffect()
        {
            if (counterEffects < effects.Count)
            {
                currentEffect = effects[counterEffects];
                counterEffects++;

                if (canApply.CheckApply(currentEffect))
                {
                    if (currentEffect.TimeApply == currentTimeApply)
                    {
                        ApplyEffect();
                    }
                    else
                    {
                        EndApplyEffect();
                    }
                }
            }
            else
            {
                EndApplyAllEffects();
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
                currentApply = buffApply;
            }
            else if (currentEffect is OtherEffect)
            {
                currentApply = otherApply;
            }
            else if (currentEffect is DefendEffect)
            {
                currentApply = defendApply;
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

            if (currentEffect.TimeApply != TypeTimeApply.RightNow || currentEffect.TimeCancel != TypeTimeApply.RightNow)
            {
                CharacterData characterData = BoxController.GetController<FightSceneController>().GetCurrentCharacter;
                characterData.AddTemporaryEffect(currentEffect);
            }

            NextEffect();
        }

        private void EndApplyAllEffects()
        {
            // if effect apply after use card
            if (currentTimeApply == TypeTimeApply.RightNow)
            {
                // Pay cost fight card
                CharacterData characterData = BoxController.GetController<FightSceneController>().GetCurrentCharacter;
                foreach (var typeCost in currentCardFight.GetTypeCost)
                {
                    characterData.DownAttribute(typeCost, currentCardFight.GetValueCost);
                }

                // Block card
                BoxController.GetController<CardsController>().EndUseCard(currentCardFight);
            }
        }

        #endregion

        public List<Effect> CheckCancelEffectsAfterEndRound(CharacterData characterData, List<Effect> effects)
        {
            List<Effect> activeEffects = new List<Effect>();

            foreach (var effect in effects)
            {
                activeEffects.Add(effect);
            }

            foreach (var effect in effects)
            {
                if (effect.TimeCancel == TypeTimeApply.AfterTime)
                {
                    effect.DecreaseCurrentTimeDuration();

                    if (effect.CurrentTimeDuration <= 0)
                    {
                        activeEffects.Remove(effect);                    
                        cancelEffect.Cancel(characterData, effect);
                    }
                }
            }

            return activeEffects;
        }
    }
}