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
        private CheckEffectAfterEvent checkEvent;

        private List<Effect> effects;
        private Effect currentEffect;
        private ApplyEffect currentApply;
        private CardFightModel currentCardFight;
        private int counterEffects = 0;

        private TypeTimeApply currentTimeApply;
        private CharacterData currentCharacter;
        private TypeCondition typeEvent;

        public override void OnInitialize()
        {
            canApply = new CanApplyEffect();
            attackApply = new ApplyAttackEffect();
            defendApply = new ApplyDefendEffect();
            randomUpAttributeApply = new ApplyRandomUpAttrinuteEffect();
            otherApply = new ApplyOtherEffect();
            buffApply = new ApplyBuffEffect();
            checkEvent = new CheckEffectAfterEvent();
        }

        #region START_APPLY_EFFECT

        public void ClickFightCard(CardFightModel card)
        {
            counterEffects = 0;
            currentCardFight = card;
            effects = card.GetEffects;
            currentTimeApply = TypeTimeApply.RightNow;
            currentCharacter = BoxController.GetController<CharactersDataController>().GetCurrentCharacter;

            NextEffect();
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
            typeEvent = TypeCondition.None;

            if (currentEffect is AttackEffect)
            {
                currentApply = attackApply;
                typeEvent = TypeCondition.Attack;
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
                typeEvent = TypeCondition.Defend;
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
            currentEffect.Apply();

            if (currentApply != null)
            {
                currentApply.EndApplyEvent -= EndApplyEffect;
            }
            
            if (currentEffect.TimeApply != TypeTimeApply.RightNow || currentEffect.TimeCancel != TypeTimeApply.RightNow)
            {
                currentCharacter.AddTemporaryEffect(currentEffect);
            }

            if(typeEvent == TypeCondition.Attack)
            {
                checkEvent.CheckEvent(currentCharacter, TypeCondition.Attack);
            }

            NextEffect();
        }

        private void EndApplyAllEffects()
        {
            PayCostFightCard();

            // Block card
            BoxController.GetController<CardsController>().EndUseCard(currentCardFight);
        }

        private void PayCostFightCard()
        {
            List<BuffEffect> buffEffects = BoxController.GetController<CharactersDataController>().GetCurrentCharacter.GetBuffEffects();

            // Pay cost fight card
            foreach (var typeCost in currentCardFight.GetTypeCost)
            {
                bool haveDiscount = false;

                foreach (var effect in buffEffects)
                {
                    if (effect.TypeBuff == TypeBuff.Discount)
                    {
                        foreach (var type in effect.TypesTargetObject)
                        {
                            if (type == typeCost)
                            {
                                haveDiscount = true;
                            }
                        }
                    }
                }

                if (!haveDiscount)
                {
                    currentCharacter.DownAttribute(typeCost, currentCardFight.GetValueCost);
                }
            }
        }

        #endregion

        public void EndRound()
        {
            checkEvent.CheckEndRound(BoxController.GetController<CharactersDataController>().GetEnemyData);
            checkEvent.CheckEndRound(BoxController.GetController<CharactersDataController>().GetPlayerData);
        }
    }
}