using Cards;
using Cards.Data;
using Core;
using Gameplay;
using NaughtyAttributes;
using System.Collections.Generic;
using UI.Buttons;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DeckBuildWindow : Window
    {
        [BoxGroup("Cards")]
        [SerializeField] private CardPresidentUI presidentCardPrefab;
        [BoxGroup("Cards")]
        [SerializeField] private CardFightUI fightCardPrefab;
        [BoxGroup("Buttons")]
        [SerializeField] private Button choosePresidentCards, chooseFightCards;
        [BoxGroup("Scroll rect cards")]
        [SerializeField] private ScrollCards scrollCards;
        [BoxGroup("Deck buttons")]
        [SerializeField] private DeckButton[] deckButtons;
        [BoxGroup("Parent cards")]
        [SerializeField] private GameObject parentCards;

        private DeckBuildController deckController;

        private List<CardPresident> cardsPresident = new List<CardPresident>();
        private List<CardFight> cardsFight = new List<CardFight>();

        private DeckButton selectedDeckButton;

        protected override void AfterInitialization()
        {
            deckController = BoxController.GetController<DeckBuildController>();

            choosePresidentCards.onClick.AddListener(ShowPresidentCards);
            chooseFightCards.onClick.AddListener(ShowFightCards);
        }

        protected override void BeforeShow()
        {
            List<DeckData> decks = deckController.GetAllDecks;

            for (int i = 0; i < decks.Count; i++)
            {
                deckButtons[i].SetNameDeck = decks[i].Name;
            }

            ShowPresidentCards();
        }

        public void ClickDeckButton(DeckButton deckButton)
        {
            if (selectedDeckButton == deckButton)
            {
                // TODO: rename deck
            }
            else
            {
                selectedDeckButton = deckButton;
            }
        }

        private void ShowPresidentCards()
        {
            List<CardPresidentData> cardsData = deckController.GetSelectedDeck.PresidentsData;
            List<CardPresidentUI> cardsUI = new List<CardPresidentUI>();

            foreach (var cardData in cardsData)
            {
                CardPresidentUI card = Instantiate(presidentCardPrefab, parentCards.transform);

                card.SetCardData = cardData;
                cardsUI.Add(card);
            }

            scrollCards.SetCards(cardsUI);
        }

        private void ShowFightCards()
        {
            List<CardFightData> cardsData = deckController.GetSelectedDeck.FightsData;
            List<CardFightUI> cardsUI = new List<CardFightUI>();

            foreach (var cardData in cardsData)
            {
                CardFightUI card = Instantiate(fightCardPrefab, parentCards.transform);
                card.SetCardData = cardData;
                cardsUI.Add(card);
            }

            scrollCards.SetCards(cardsUI);
        }
    }
}