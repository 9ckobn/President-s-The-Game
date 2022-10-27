using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.�ontainer
{
    public class �ontainerFightCards : �ontainerCards
    {
        private List<CardFight> cards;

        public void AddCard(CardFight card)
        {
            if (countCards + 1 > maxCards)
            {
                BoxController.GetController<LogController>().LogError($"Max cards {maxCards}. Can add card!");
            }
            else
            {
                countCards++;
                cards.Add(card);
            }
        }
    }
}