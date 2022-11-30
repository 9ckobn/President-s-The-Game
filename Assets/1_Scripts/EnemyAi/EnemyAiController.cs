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
        [SerializeField] private SCRO_PriorityCardAi priorityCards;

        private List<CardFightModel> myCards;
        private List<CardFightModel> activeCards;
        private CardFightModel selectedCard;

        public override void OnStart()
        {
            myCards = BoxController.GetController<CardsController>().EnemyFightCards;
        }

        public void StartRound()
        {
            SelectCard();
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

            foreach (var priorityCard in priorityCards.AttackCardsPriority)
            {
                if (selectedCard != null)
                {
                    break;
                }

                foreach (var card in activeCards)
                {
                    if (card.GetId == priorityCard.Id)
                    {
                        selectedCard = card;
                        break;
                    }
                }
            }
        }
    }
}