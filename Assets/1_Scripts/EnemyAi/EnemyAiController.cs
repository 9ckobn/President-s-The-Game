using Buildings;
using Cards;
using Core;
using Data;
using EffectSystem;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemyAI
{
    [CreateAssetMenu(fileName = "EnemyAiController", menuName = "Controllers/Gameplay/EnemyAiController")]
    public class EnemyAiController : BaseController
    {
        private const float VALUE_USE_HEALTH = 0.7f;
        private const int MAX_ATTACK = 2;

        [SerializeField] private SCRO_PriorityCardAi priorityCards;

        private List<CardFightModel> myCards;
        private List<CardFightModel> activeCards;
        private CardFightModel selectedCard;
        private CardFightModel prevCard;

        private int countAttack = 0;

        public void StartRound()
        {
            myCards = BoxController.GetController<CardsController>().EnemyFightCards;
            countAttack = 0;

            NextCard();
        }

        public void NextCard()
        {
            SelectCard();

            if (selectedCard != null)
            {
                Debug.Log($"selectedCard = {selectedCard.GetId}");
                Coroutines.StartRoutine(CoUseCard());
            }
            else
            {
                BoxController.GetController<FightSceneController>().EnemyAiSkipRound();
            }
        }

        private void SelectCard()
        {
            List<CardFightModel> activeCards = new List<CardFightModel>();
            selectedCard = null;

            foreach (var card in myCards)
            {
                if (card.CheckCanUseCard())
                {
                    activeCards.Add(card);
                }
            }


            // Check use fight card
            foreach (var card in activeCards)
            {
                if (countAttack < MAX_ATTACK)
                {
                    foreach (var priorityCard in priorityCards.AttackCardsPriority)
                    {
                        if (card.GetId == priorityCard.Id && (prevCard == null || prevCard.GetId != card.GetId))
                        {
                            countAttack++;
                            prevCard = selectedCard = card;
                            return;
                        }
                    }
                }

            }


            // Use health card
            if (CheckNeedUseHealth())
            {
                selectedCard = activeCards.FirstOrDefault(card => card.GetId == priorityCards.HealthCardsPriority[0].Id);

                if (selectedCard != null)
                {
                    return;
                }
            }


            // Use any card except attck and health cards
            activeCards = new List<CardFightModel>();

            List<string> exceptionId = new List<string>();

            foreach (var attackCard in priorityCards.AttackCardsPriority)
            {
                exceptionId.Add(attackCard.Id);
            }

            foreach (var healthCard in priorityCards.HealthCardsPriority)
            {
                exceptionId.Add(healthCard.Id);
            }

            foreach (var card in myCards)
            {
                if (card.CheckCanUseCard() && !exceptionId.Contains(card.GetId))
                {
                    activeCards.Add(card);
                }
            }

            if (activeCards.Count == 0)
            {
                selectedCard = myCards.FirstOrDefault(card => card.CheckCanUseCard());
            }
            else
            {
                selectedCard = activeCards[Random.Range(0, activeCards.Count - 1)];
            }
        }

        private IEnumerator CoUseCard()
        {
            yield return new WaitForSeconds(1f);

            selectedCard.AiUseCard();
        }

        public void SelectTarget(Effect effect)
        {
            CharacterData characterData = null;

            if (effect is AttackEffect)
            {
                characterData = BoxController.GetController<CharactersDataController>().GetPlayerData;
            }
            else if (effect is OtherEffect)
            {
                characterData = BoxController.GetController<CharactersDataController>().GetEnemyData;
            }

            TypeAttribute target = MainData.TYPE_BUILDINGS[0];

            if (effect is AttackEffect)
            {
                int valueTarget = characterData.GetValueAttribute(target) + characterData.GetValueDefend(target);

                for (int i = 1; i < MainData.TYPE_BUILDINGS.Length; i++)
                {
                    TypeAttribute attribute = MainData.TYPE_BUILDINGS[i];

                    if (characterData.GetValueAttribute(attribute) + characterData.GetValueDefend(attribute) < valueTarget)
                    {
                        break;
                    }

                    target = attribute;
                    valueTarget = characterData.GetValueAttribute(attribute) + characterData.GetValueDefend(attribute);
                }
            }
            else if (effect is OtherEffect && (effect as OtherEffect).TypeOtherEffect == TypeOtherEffect.Loan)
            {
                int valueDifference = characterData.GetStartValueAttribute(target) - characterData.GetValueAttribute(target);

                for (int i = 1; i < MainData.TYPE_BUILDINGS.Length; i++)
                {
                    TypeAttribute attribute = MainData.TYPE_BUILDINGS[i];

                    if (valueDifference > characterData.GetStartValueAttribute(attribute) - characterData.GetValueAttribute(attribute))
                    {
                        break;
                    }

                    target = attribute;
                    valueDifference = characterData.GetStartValueAttribute(target) - characterData.GetValueAttribute(target);
                }
            }

            Debug.Log($"stategic lion - target = {target}");

            Coroutines.StartRoutine(CoUseCard(characterData.GetBuilding(target)));
        }

        private bool CheckNeedUseHealth()
        {
            CharacterData characterData = BoxController.GetController<CharactersDataController>().GetEnemyData;

            foreach (var typeBuilding in MainData.TYPE_BUILDINGS)
            {
                if (characterData.GetStartValueAttribute(typeBuilding) * VALUE_USE_HEALTH > characterData.GetValueAttribute(typeBuilding))
                {
                    return true;
                }
            }

            return false;
        }

        private IEnumerator CoUseCard(Building building)
        {
            building.EnableStateTarget();
            building.OnMouseOver();

            yield return new WaitForSeconds(1.5f);
            building.OnMouseDown();
            building.DisableStateTarget();
        }
    }
}