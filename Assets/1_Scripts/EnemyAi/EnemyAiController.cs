using Cards;
using Core;
using Gameplay;
using System.Collections.Generic;
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

        public override void OnStart()
        {
            myCards = BoxController.GetController<CardsController>().EnemyFightCards;
        }

        public void StartRound()
        {
            LogManager.Log("enemy start round");
            SelectCard();

            if (selectedCard != null)
            {
                UseCard();
            }
            else
            {
                LogManager.Log("Not have selected card!");
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
                if(selectedCard != null)
                {
                    return;
                }

                if (countAttack < MAX_ATTACK)
                {
                    foreach (var priorityCard in priorityCards.AttackCardsPriority)
                    {
                        if (card.GetId == priorityCard.Id)
                        {
                            countAttack++;
                            selectedCard = card;
                            break;
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

        private void UseCard()
        {
            selectedCard.OnMouseEnter();
        }
    }
}