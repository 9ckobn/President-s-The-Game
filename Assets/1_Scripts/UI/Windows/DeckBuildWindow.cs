using Cards;
using Cards.Data;
using Cards.DeckBuild;
using Core;
using Data;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
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
        [BoxGroup("Buttons Type")]
        [SerializeField] private Button choosePresidentCards, chooseFightCards;
        [BoxGroup("Buttons Type")]
        [SerializeField] private Sprite enableChooseSprite, disableChooseSprite;
        [BoxGroup("Scroll rect cards")]
        [SerializeField] private ScrollCards scrollCards;
        [BoxGroup("Deck buttons")]
        [SerializeField] private Button createDeckButton;
        [BoxGroup("Deck buttons")]
        [SerializeField] private DeckButton[] deckButtons;
        [BoxGroup("Filter buttons")]
        [SerializeField] private Button alhabetFilterButton, rareFilterButton;
        [BoxGroup("Filter buttons")]
        [SerializeField] private Sprite enableFilterSprite, disableFilterSprite;
        [BoxGroup("Parent cards")]
        [SerializeField] private GameObject parentCards, parentPreviewCard;
        [BoxGroup("Current deck")]
        [SerializeField] private CurrentDeckUI currentPresidentsUI, currentFightsUI;
        [BoxGroup("Current deck")]
        [SerializeField] private Text countFightCardsText, countPresidentCardsText;
        [BoxGroup("Exit button")]
        [SerializeField] private Button exitButton;

        private DeckBuildController deckController;
        private DeckBuildStorageCardsController storageCards;

        private List<CardPresidentUI> showCardsPresident = new List<CardPresidentUI>();
        private List<CardPresidentUI> deckCardsPresident = new List<CardPresidentUI>();
        private List<CardFightUI> showCardsFight = new List<CardFightUI>();
        private List<CardFightUI> deckCardsFight = new List<CardFightUI>();

        private CardUI previewCard;
        private DeckButton selectedDeckButton;

        private bool presidentsCardsNow;
        private bool alhabetFilter = true;

        protected override void AfterInitialization()
        {
            deckController = BoxController.GetController<DeckBuildController>();
            storageCards = BoxController.GetController<DeckBuildStorageCardsController>();

            choosePresidentCards.onClick.AddListener(() => { ClickShowCards(true); });
            chooseFightCards.onClick.AddListener(() => { ClickShowCards(false); });

            alhabetFilterButton.onClick.AddListener(ClickFilterAlhabet);
            rareFilterButton.onClick.AddListener(ClickFilterRare);

            createDeckButton.onClick.AddListener(ClickCreateDeck);
            exitButton.onClick.AddListener(ClickExitButton);
        }

        protected override void BeforeShow()
        {
            List<DeckData> decks = deckController.Decks;

            for (int i = 0; i < decks.Count; i++)
            {
                deckButtons[i].SetNameDeck = decks[i].Name;
            }

            foreach (var cardId in deckController.SelectedDeck.PresidentsId)
            {
                CreatePresidentCardInDeck(storageCards.GetPresidentData(cardId));
            }

            foreach (var cardId in deckController.SelectedDeck.FightsId)
            {
                CreateFightCardInDeck(storageCards.GetFightData(cardId));
            }

            ClickShowCards(true);
        }

        #region CLICK_BUTTONS

        private void ClickShowCards(bool isPresidentCards)
        {
            if (presidentsCardsNow != isPresidentCards)
            {
                presidentsCardsNow = isPresidentCards;

                if (showCardsFight.Count > 0)
                {
                    foreach (var card in showCardsFight)
                    {
                        Destroy(card.gameObject);
                    }

                    showCardsFight.Clear();
                }

                if (showCardsPresident.Count > 0)
                {
                    foreach (var card in showCardsPresident)
                    {

                        Destroy(card.gameObject);
                    }

                    showCardsPresident.Clear();
                }

                scrollCards.ClearLines();

                if (presidentsCardsNow)
                {
                    CreatePresidentCards();
                }
                else
                {
                    CreateFightCards();
                }

                choosePresidentCards.GetComponent<Image>().sprite = presidentsCardsNow ? enableChooseSprite : disableChooseSprite;
                chooseFightCards.GetComponent<Image>().sprite = !presidentsCardsNow ? enableChooseSprite : disableChooseSprite;
            }
        }

        private void ClickCreateDeck()
        {
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

        public void PointerEnterOnCard(CardUI cardUI)
        {
            if (cardUI is CardPresidentUI)
            {
                CardPresidentUI card = Instantiate(presidentCardPrefab, parentPreviewCard.transform);
                card.transform.position = parentPreviewCard.transform.position;
                card.SetCardData = (cardUI as CardPresidentUI).GetData;

                previewCard = card;
            }
            else
            {
                CardFightUI card = Instantiate(fightCardPrefab, parentPreviewCard.transform);
                card.transform.position = parentPreviewCard.transform.position;
                card.SetCardData = (cardUI as CardFightUI).GetData;

                previewCard = card;
            }
        }

        public void DeletePreviewCard()
        {
            Destroy(previewCard.gameObject);
        }

        private void ClickFilterAlhabet()
        {
            if (!alhabetFilter)
            {
                alhabetFilter = true;

                alhabetFilterButton.GetComponent<Image>().sprite = enableFilterSprite;
                rareFilterButton.GetComponent<Image>().sprite = disableFilterSprite;

                presidentsCardsNow = !presidentsCardsNow;
                ClickShowCards(!presidentsCardsNow);
            }
        }

        private void ClickFilterRare()
        {
            if (alhabetFilter)
            {
                alhabetFilter = false;

                alhabetFilterButton.GetComponent<Image>().sprite = disableFilterSprite;
                rareFilterButton.GetComponent<Image>().sprite = enableFilterSprite;

                presidentsCardsNow = !presidentsCardsNow;
                ClickShowCards(!presidentsCardsNow);
            }
        }

        private void ClickExitButton()
        {
            DataBaseManager.Instance.SaveDecksData();
        }

        #endregion

        #region SELECT_DESELECT_CARD

        public void SelectPresidentCard(CardPresidentUI card)
        {
            if (deckController.CanAddCard(card.GetData))
            {
                CreatePresidentCardInDeck(card.GetData);

                deckController.AddCardInDeck(card.GetData);

                RedrawCountCardsText();
            }
        }

        public void SelectFightCard(CardFightUI card)
        {
            if (deckController.CanAddCard(card.GetData))
            {
                CreateFightCardInDeck(card.GetData);

                deckController.AddCardInDeck(card.GetData);

                RedrawCountCardsText();
            }
        }

        public void DeSelectPresidentCard(CardPresidentUI card)
        {
            DeletePreviewCard();

            deckController.RemoveCardInDeck(card.GetData);
            deckCardsPresident.Remove(card);
            currentPresidentsUI.RemoveCard(card);

            RedrawCountCardsText();
        }

        public void DeSelectFightCard(CardFightUI card)
        {
            DeletePreviewCard();

            deckController.RemoveCardInDeck(card.GetData);
            deckCardsFight.Remove(card);
            currentFightsUI.RemoveCard(card);

            RedrawCountCardsText();
        }

        private void RedrawCountCardsText()
        {
            countFightCardsText.text = $"{deckController.GetCountFightCards}/{MainData.MAX_FIGHT_CARDS}";
            countPresidentCardsText.text = $"{deckController.GetCountPresidentCards}/{MainData.MAX_PRESIDENT_CARDS}";
        }

        #endregion

        #region CREATE_CARDS_UI

        private void CreatePresidentCards()
        {
            List<CardPresidentData> cardsData = storageCards.CardsPresidentData;
            showCardsPresident = new List<CardPresidentUI>();

            if (alhabetFilter)
            {
                cardsData = cardsData.OrderBy(card => card.Name).ToList();
            }
            else
            {
                cardsData = cardsData.OrderBy(card => card.Rarityrank).ToList();
            }

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
            List<CardFightData> cardsData = storageCards.CardsFightData;
            showCardsFight = new List<CardFightUI>();

            if (alhabetFilter)
            {
                cardsData = cardsData.OrderBy(card => card.Name).ToList();
            }
            else
            {
                cardsData = cardsData.OrderBy(card => card.Cost).ToList();
            }

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

            currentPresidentsUI.AddCard(card);
        }

        private void CreateFightCardInDeck(CardFightData data)
        {
            CardFightUI card = Instantiate(fightCardPrefab, parentCards.transform);
            card.SetCardData = data;
            card.SetInDeck = true;
            deckCardsFight.Add(card);

            currentFightsUI.AddCard(card);
        }

        #endregion
    }
}