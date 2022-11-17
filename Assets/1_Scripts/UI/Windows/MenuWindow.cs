using Cards;
using Cards.Data;
using Cards.DeckBuild;
using Core;
using DG.Tweening;
using NaughtyAttributes;
using System.Collections.Generic;
using UI.Buttons;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuWindow : Window
    {
        [BoxGroup("Cards")]
        [SerializeField] private CardPresidentUI presidentCardPrefab;
        [BoxGroup("Cards")]
        [SerializeField] private CardFightUI fightCardPrefab;
        [BoxGroup("Deck buttons")]
        [SerializeField] private SelectDeckButton[] selectDeckButtons;
        [BoxGroup("Deck buttons")]
        [SerializeField] private Button myCardsButton;
        [BoxGroup("Deck buttons")]
        [SerializeField] private Sprite selectedDeckSprite, fullDeckSprite;
        [BoxGroup("Current deck")]
        [SerializeField] private CurrentDeckUI currentPresidentsUI, currentFightsUI;
        [BoxGroup("Parent cards")]
        [SerializeField] private GameObject parentCards;

        private DeckBuildController deckController;
        private DeckBuildStorageCardsController storageCards;

        private SelectDeckButton selectedDeckButton;

        private Tween myCardsButtonTween;

        protected override void AfterInitialization()
        {
            deckController = BoxController.GetController<DeckBuildController>();
            storageCards = BoxController.GetController<DeckBuildStorageCardsController>();

            myCardsButton.onClick.AddListener(ClickMyCardsButton);
        }

        protected override void BeforeShow()
        {
            ChangeDeck();
        }

        public void ClickSelectDeckButton(SelectDeckButton button)
        {
            if(selectedDeckButton.IdDeck != button.IdDeck)
            {
                deckController.SelectDeck(button.IdDeck);
                ChangeDeck();
            }
        }

        private void ClickMyCardsButton()
        {
            myCardsButtonTween.Kill();

            Hide();
            UIManager.Instance.ShowWindow<DeckBuildWindow>();
        }

        private void ChangeDeck()
        {
            List<DeckData> completeDecks = new List<DeckData>();

            foreach (var deck in deckController.Decks)          
            {
                if (deck.IsComplete)
                {
                    completeDecks.Add(deck);
                }
            }

            foreach (var button in selectDeckButtons)
            {
                button.gameObject.SetActive(false);
            }

            if(completeDecks.Count == 0)
            {
                myCardsButtonTween = myCardsButton.transform.DOScale(myCardsButton.transform.localScale * 1.1f, 1f).
                    SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                for (int i = 0; i < completeDecks.Count; i++)
                {
                    if (completeDecks[i].IsComplete)
                    {
                        selectDeckButtons[i].SetNameDeck = completeDecks[i].Name;
                        selectDeckButtons[i].gameObject.SetActive(true);
                        selectDeckButtons[i].IdDeck = completeDecks[i].Id;

                        if (completeDecks[i].IsSelected)
                        {
                            selectedDeckButton = selectDeckButtons[i];
                            selectDeckButtons[i].SetSpriteButton(selectedDeckSprite);
                        }
                        else 
                        {
                            selectDeckButtons[i].SetSpriteButton(fullDeckSprite);
                        }
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
            }
        }

        private void CreatePresidentCardInDeck(CardPresidentData data)
        {
            CardPresidentUI card = Instantiate(presidentCardPrefab, parentCards.transform);
            card.SetCardData = data;
            card.SetIsCanSelected = false;

            currentPresidentsUI.AddCard(card);
        }

        private void CreateFightCardInDeck(CardFightData data)
        {
            CardFightUI card = Instantiate(fightCardPrefab, parentCards.transform);
            card.SetCardData = data;
            card.SetIsCanSelected = false;

            currentFightsUI.AddCard(card);
        }
    }
}