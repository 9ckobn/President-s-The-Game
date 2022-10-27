using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.Container
{
    public class ContainerPresidentCards : ContainerCards
    {
        private List<CardPresidentModel> cards;

        public void AddCard(CardPresidentModel card)
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