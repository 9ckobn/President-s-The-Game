using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.�ontainer
{
    public class �ontainerPresidentCards : �ontainerCards
    {
        private List<CardPresident> cards;

        public void AddCard(CardPresident card)
        {
            if(countCards + 1 > maxCards)
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