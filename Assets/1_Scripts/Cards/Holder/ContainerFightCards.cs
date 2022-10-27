using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.Container
{
    public class ContainerFightCards : ContainerCards
    {
        private List<CardFightModel> cards;

        public void AddCard(CardFightModel card)
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