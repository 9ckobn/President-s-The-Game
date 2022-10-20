using Cards;
using Core;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DeckBuildWindow : Window
    {
        [BoxGroup("Cards")]
        [SerializeField] private CardPresident presidentCardPrefab;
        [BoxGroup("Cards")]
        [SerializeField] private CardFight fightCardPrefab;
        [BoxGroup("Cards")]
        [SerializeField] private GameObject parentCardPresident, parentCardFight;
        [BoxGroup("Buttons")]
        [SerializeField] private Button choosePresidentCards, chooseFightCards;
        [BoxGroup("Scroll rect cards")]
        [SerializeField] private ScrollRect scrollRectCards;

        private List<CardPresident> cardsPresident = new List<CardPresident>();
        private List<CardFight> cardsFight = new List<CardFight>();

        protected override void AfterInitialization()
        {
            choosePresidentCards.onClick.AddListener(ShowPresidentCards);
            chooseFightCards.onClick.AddListener(ShowFightCards);
        }

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

        private void ShowPresidentCards()
        {

        }

        private void ShowFightCards()
        {

        }
    }
}