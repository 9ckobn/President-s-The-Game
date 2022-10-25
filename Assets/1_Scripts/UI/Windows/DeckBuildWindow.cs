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
        [BoxGroup("Current deck")]
        [SerializeField] private CurrentDeckUI currentDeckUI;

        private DeckBuildController deckController;

        private List<CardPresidentUI> showCardsPresident = new List<CardPresidentUI>();
        private List<CardPresidentUI> deckCardsPresident = new List<CardPresidentUI>();
        private List<CardFightUI> showCardsFight = new List<CardFightUI>();
        private List<CardFightUI> deckCardsFight = new List<CardFightUI>();

        private DeckButton selectedDeckButton;
        private bool presidentsCardsNow;

        protected override void AfterInitialization()
        {
            deckController = BoxController.GetController<DeckBuildController>();

            choosePresidentCards.onClick.AddListener(ClickShowPresidentsCards);
            chooseFightCards.onClick.AddListener(ClickShowFightCards);
        }

        protected override void BeforeShow()
        {
            List<DeckData> decks = deckController.GetAllDecks;

            for (int i = 0; i < decks.Count; i++)
            {
                deckButtons[i].SetNameDeck = decks[i].Name;
            }

            ClickShowPresidentsCards();
        }

        #region CLICK_BUTTONS

        private void ClickShowPresidentsCards()
        {
            if (!presidentsCardsNow)
            {
                presidentsCardsNow = true;

                foreach (var card in showCardsFight)
                {
                    Destroy(card.gameObject);
                }

                scrollCards.ClearLines();
                CreatePresidentCards();
            }
        }

        private void ClickShowFightCards()
        {
            if (presidentsCardsNow)
            {
                presidentsCardsNow = false;

                foreach (var card in showCardsPresident)
                {
                    Destroy(card.gameObject);
                }

                scrollCards.ClearLines();
                CreateFightCards();
            }
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

        #endregion

        #region SELECT_DESELECT_CARD

        public void SelectPresidentCard(CardPresidentUI card)
        {
            if (deckController.CanAddCard(card.GetData))
            {
                CreatePresidentCardInDeck(card.GetData);

                deckController.AddCardInDeck(card.GetData);
            }
        }

        public void SelectFightCard(CardFightUI card)
        {
            if (deckController.CanAddCard(card.GetData))
            {
                CreateFightCardInDeck(card.GetData);

                deckController.AddCardInDeck(card.GetData);
            }
        }

        public void DeSelectPresidentCard(CardPresidentUI card)
        {
            CardPresidentUI deleteCard = null;

            foreach (var deckCard in deckCardsPresident)
            {
                if (deckCard.GetData == card.GetData)
                {
                    deleteCard = deckCard;
                }
            }

            if (deleteCard == null)
            {
                BoxController.GetController<LogController>().LogError($"Not have card for delete in deck president UI. card id = {card.GetData.ID}");
            }
            else
            {
                deckController.RemoveCardInDeck(deleteCard.GetData);
                deckCardsPresident.Remove(deleteCard);
                Destroy(deleteCard.gameObject);
            }
        }

        public void DeSelectFightCard(CardFightUI card)
        {
            CardFightUI deleteCard = null;

            foreach (var deckCard in deckCardsFight)
            {
                if (deckCard.GetData == card.GetData)
                {
                    deleteCard = deckCard;
                }
            }

            if (deleteCard == null)
            {
                BoxController.GetController<LogController>().LogError($"Not have card for delete in deck fight UI. card id = {card.GetData.ID}");
            }
            else
            {
                deckController.RemoveCardInDeck(deleteCard.GetData);
                deckCardsFight.Remove(deleteCard);
                Destroy(deleteCard);
            }
        }

        #endregion

        #region CREATE_CARDS_UI

        private void CreatePresidentCards()
        {
            List<CardPresidentData> cardsData = BoxController.GetController<StorageCardsController>().GetCardsPresidentData;
            showCardsPresident = new List<CardPresidentUI>();

            foreach (var cardData in cardsData)
            {
                CardPresidentUI card = Instantiate(presidentCardPrefab, parentCards.transform);

                card.SetCardData = cardData;
                card.SetInDeck = false;
                showCardsPresident.Add(card);
            }

            scrollCards.AddCards(showCardsPresident);
        }

        private void CreateFightCards()
        {
            List<CardFightData> cardsData = BoxController.GetController<StorageCardsController>().GetCardsFightData;
            showCardsFight = new List<CardFightUI>();

            foreach (var cardData in cardsData)
            {
                CardFightUI card = Instantiate(fightCardPrefab, parentCards.transform);
                card.SetCardData = cardData;
                card.SetInDeck = false;
                showCardsFight.Add(card);
            }

            scrollCards.AddCards(showCardsFight);
        }

        private void CreatePresidentCardInDeck(CardPresidentData data)
        {
            CardPresidentUI card = Instantiate(presidentCardPrefab, parentCards.transform);
            card.SetCardData = data;
            card.SetInDeck = true;
            deckCardsPresident.Add(card);

            currentDeckUI.AddCard(card.gameObject);
        }

        private void CreateFightCardInDeck(CardFightData data)
        {
            CardFightUI card = Instantiate(fightCardPrefab, parentCards.transform);
            card.SetCardData = data;
            card.SetInDeck = true;
            deckCardsFight.Add(card);

            currentDeckUI.AddCard(card.gameObject);
        }

        #endregion
    }
}