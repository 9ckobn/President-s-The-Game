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

        private int countAttack = 0;

        public void StartRound()
        {
            myCards = BoxController.GetController<CardsController>().EnemyFightCards;

            LogManager.Log("enemy start round");
            SelectCard();

            if (selectedCard != null)
            {
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
                        if (card.GetId == priorityCard.Id)
                        {
                            countAttack++;
                            selectedCard = card;
                            return;
                        }
                    }
                }
                else
                {
                    LogManager.Log("Not have logic select card not attack");
                    countAttack = 0;
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
            CharacterData playerData = BoxController.GetController<CharactersDataController>().GetPlayerData;
            TypeAttribute target = MainData.TYPE_BUILDINGS[0];
            int valueTarget = playerData.GetValueAttribute(target) + playerData.GetValueDefend(target);

            for (int i = 0; i < MainData.TYPE_BUILDINGS.Length; i++)
            {
                TypeAttribute attribute = MainData.TYPE_BUILDINGS[0];
                if (i != 0)
                {
                    if (playerData.GetValueAttribute(attribute) + playerData.GetValueDefend(attribute) < valueTarget)
                    {
                        break;
                    }
                }

                target = attribute;
                valueTarget = playerData.GetValueAttribute(attribute) + playerData.GetValueDefend(attribute);
            }

            Coroutines.StartRoutine(CoSelectTarget(playerData.GetBuilding(target)));
        }

        private IEnumerator CoSelectTarget(Building building)
        {
            yield return new WaitForSeconds(1.5f);
            building.EnableStateTarget();
            building.OnMouseOver();

            yield return new WaitForSeconds(1.5f);
            building.OnMouseDown();
            building.DisableStateTarget();
        }
    }
}