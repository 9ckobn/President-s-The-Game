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
        private List<CardFightModel> myCards;

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

            foreach (var card in myCards)
            {
                if (card.CheckCanUseCard())
                {
                    activeCards.Add(card);
                }
            }
        }
    }
}