using Cards;
using Cards.Data;
using Cards.DeckBuild;
using Core;
using Data;
using DG.Tweening;
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
        [BoxGroup("Deck buttons")]
        [SerializeField] private Sprite selectedDeckSprite, fullDeckSprite, notCompleteDeckSprite;
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
        private List<CardFightUI> showCardsFight = new List<CardFightUI>();

        private CardUI previewCard;
        private DeckButton selectedDeckButton;

        private bool presidentsCardsNow;
        private bool alhabetFilter = true;

        private Tween createButtonTween;

        protected override void AfterInitialization()
        {
            deckController = BoxController.GetController<DeckBuildController>();
            storageCards = BoxController.GetController<DeckBuildStorageCardsController>();

            choosePresidentCards.onClick.AddListener(() => { ShowCards(true); });
            chooseFightCards.onClick.AddListener(() => { ShowCards(false); });

            alhabetFilterButton.onClick.AddListener(ClickFilterAlhabet);
            rareFilterButton.onClick.AddListener(ClickFilterRare);

            createDeckButton.onClick.AddListener(ClickCreateDeck);
            exitButton.onClick.AddListener(ClickExitButton);
        }

        protected override void BeforeShow()
        {
            ChangeDeck();

            ShowCards(true);
        }

        private void ChangeDeck()
        {
            List<DeckData> decks = deckController.Decks;

            foreach (var button in deckButtons)
            {
                button.gameObject.SetActive(false);
            }

            if (decks.Count == 0)
            { 
                createButtonTween = createDeckButton.transform.DOScale(createDeckButton.transform.localScale * 1.1f, 1f).
                    SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                for (int i = 0; i < decks.Count; i++)
                {
                    deckButtons[i].SetNameDeck = decks[i].Name;
                    deckButtons[i].gameObject.SetActive(true);
                    deckButtons[i].IdDeck = decks[i].Id;

                    if (decks[i].IsSelected)
                    {
                        selectedDeckButton = deckButtons[i];
                        deckButtons[i].SetSpriteButton(selectedDeckSprite);
                    }
                    else if (decks[i].IsComplete)
                    {
                        deckButtons[i].SetSpriteButton(fullDeckSprite);
                    }
                    else
                    {
                        deckButtons[i].SetSpriteButton(notCompleteDeckSprite);
                    }
                }

                currentPresidentsUI.RemoveAllCards();
                currentFightsUI.RemoveAllCards();

                foreach (var cardId in deckController.SelectedDeck.PresidentsId)
                {
                    CreatePresidentCardInDeck(storageCards.GetPresidentData(cardId));
                }

                foreach (var cardId in deckController.SelectedDeck.FightsId)
                {
                    CreateFightCardInDeck(storageCards.GetFightData(cardId));
                }

                ChangeStateCards();
                RedrawCountCardsText();
            }
        }

        #region CLICK_BUTTONS

        private void ShowCards(bool isPresidentCards)
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

                ChangeStateCards();
                RedrawCountCardsText();
            }
        }

        private void ClickCreateDeck()
        {
            createButtonTween.Kill();
            createDeckButton.transform.localScale = new Vector3(1, 1, 1);

            if (deckController.CanCreateDeck)
            {
                deckController.CreateDeck();
                ChangeDeck();
            }
        }

        public void ClickDeckButton(DeckButton deckButton)
        {
            if (selectedDeckButton == deckButton)
            {
                deckButton.RenameDeck();
            }
            else
            {
                selectedDeckButton = deckButton;
                deckController.SelectDeck(deckButton.IdDeck);
                ChangeDeck();
            }
        }

        public  void EndRenameDeck(string name)
        {
            deckController.RenameDeck(name);
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
                ShowCards(!presidentsCardsNow);
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
                ShowCards(!presidentsCardsNow);
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
            if (deckController.CanAddPresidentCard)
            {
                CreatePresidentCardInDeck(card.GetData);

                deckController.AddCardInDeck(card.GetData);

                RedrawCountCardsText();
                ChangeStateCards();
            }
        }

        public void SelectFightCard(CardFightUI card)
        {
            if (deckController.CanAddFightCard)
            {
                CreateFightCardInDeck(card.GetData);

                deckController.AddCardInDeck(card.GetData);

                RedrawCountCardsText();
                ChangeStateCards();
            }
        }

        public void DeSelectPresidentCard(CardPresidentUI card)
        {
            DeletePreviewCard();

            deckController.RemoveCardInDeck(card.GetData);
            currentPresidentsUI.RemoveCard(card);

            RedrawCountCardsText();
            ChangeStateCards();
        }

        public void DeSelectFightCard(CardFightUI card)
        {
            DeletePreviewCard();

            deckController.RemoveCardInDeck(card.GetData);
            currentFightsUI.RemoveCard(card);

            RedrawCountCardsText();
            ChangeStateCards();
        }

        private void RedrawCountCardsText()
        {
            if (deckController.GetCountFightCards == MainData.MAX_FIGHT_CARDS)
            {
                countFightCardsText.text = "Max";
            }
            else
            {
                countFightCardsText.text = $"{deckController.GetCountFightCards}/{MainData.MAX_FIGHT_CARDS}";
            }

            if (deckController.GetCountPresidentCards == MainData.MAX_PRESIDENT_CARDS)
            {
                countPresidentCardsText.text = "Max";
            }
            else
            {
                countPresidentCardsText.text = $"{deckController.GetCountPresidentCards}/{MainData.MAX_PRESIDENT_CARDS}";
            }
        }

        private void ChangeStateCards()
        {
            foreach (var card in showCardsFight)
            {
                card.ChangeState(deckController.CanSelectedCard(card.GetData));
            }

            foreach (var card in showCardsPresident)
            {
                card.ChangeState(deckController.CanSelectedCard(card.GetData));
            }
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

            currentPresidentsUI.AddCard(card);
        }

        private void CreateFightCardInDeck(CardFightData data)
        {
            CardFightUI card = Instantiate(fightCardPrefab, parentCards.transform);
            card.SetCardData = data;
            card.SetInDeck = true;

            currentFightsUI.AddCard(card);
        }

        #endregion
    }
}