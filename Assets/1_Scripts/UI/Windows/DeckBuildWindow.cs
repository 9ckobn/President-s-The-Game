using Cards;
using Core;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DeckBuildWindow : Window
    {
        [BoxGroup("Cards")]
        [SerializeField] private CardPresident presidentCardPrefab;
        [BoxGroup("Cards")]
        [SerializeField] private CardFight fightCardPrefab;
        [BoxGroup("Parent")]
        [SerializeField] private GameObject parentCardPresident, parentCardFight;

        private List<CardPresident> cardsPresident;

        protected override void BeforeShow()
        {
            List<CardPresidentData> dataCards = BoxController.GetController<StorageCardsController>().GetCardsPresidentData;

            foreach (var data in dataCards)
            {
                CardPresident card = Instantiate(presidentCardPrefab, parentCardPresident.transform);
                cardsPresident.Add(card);
                card.SetCardData = data;
            }
        }
    }
}