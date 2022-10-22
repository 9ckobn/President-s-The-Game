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
        [SerializeField] private CardPresident presidentCardPrefab;
        [BoxGroup("Cards")]
        [SerializeField] private CardFight fightCardPrefab;
        [BoxGroup("Buttons")]
        [SerializeField] private Button choosePresidentCards, chooseFightCards;
        [BoxGroup("Scroll rect cards")]
        [SerializeField] private ScrollCards scrollCards;
        [BoxGroup("Deck buttons")]
        [SerializeField] private DeckButton[] deckButtons;

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

            scrollCards.SetCards(deckController.GetSelectedDeck.PresidentsData)
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

        }

        private void ShowFightCards()
        {

        }
    }
}