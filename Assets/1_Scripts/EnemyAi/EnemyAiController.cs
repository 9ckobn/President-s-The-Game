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
                LogManager.Log($"Not have selected card are selected targets!");
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
                else
                {
                    activeCards = new List<CardFightModel>();

                    foreach (var checkCard in myCards)
                    {
                        bool needAdd = true;
                        foreach (var priorityCard in priorityCards.AttackCardsPriority)
                        {
                            if (checkCard.GetId == priorityCard.Id)
                            {
                                needAdd = false;
                                break;
                            }
                        }

                        if (needAdd)
                        {
                            activeCards.Add(checkCard);
                        }
                    }

                    // TODO: Check need use Strategic Loan 

                    foreach (var priorityCard in priorityCards.HealthCardsPriority)
                    {
                        activeCards.Remove(activeCards.FirstOrDefault(card => card.GetId == priorityCard.Id));
                    }

                    selectedCard = activeCards[Random.Range(0, activeCards.Count - 1)];
                }
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
                Debug.Log($"OtherEffect select target");

                characterData = BoxController.GetController<CharactersDataController>().GetEnemyData;
            }

            TypeAttribute target = MainData.TYPE_BUILDINGS[0];
            int valueTarget = characterData.GetValueAttribute(target) + characterData.GetValueDefend(target);

            for (int i = 0; i < MainData.TYPE_BUILDINGS.Length; i++)
            {
                TypeAttribute attribute = MainData.TYPE_BUILDINGS[i];
                if (i != 0)
                {
                    if (characterData.GetValueAttribute(attribute) + characterData.GetValueDefend(attribute) < valueTarget)
                    {
                        break;
                    }
                }

                target = attribute;
                valueTarget = characterData.GetValueAttribute(attribute) + characterData.GetValueDefend(attribute);
            }

            Coroutines.StartRoutine(CoUseCard(characterData.GetBuilding(target)));
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