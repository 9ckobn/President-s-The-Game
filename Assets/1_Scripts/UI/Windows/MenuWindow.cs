using Cards;
using Cards.Data;
using Cards.DeckBuild;
using Core;
using DG.Tweening;
using NaughtyAttributes;
using System.Collections.Generic;
using UI.Buttons;
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

        private DeckBuildController deckController;

        private List<CardPresidentUI> showCardsPresident = new List<CardPresidentUI>();
        private List<CardFightUI> showCardsFight = new List<CardFightUI>();
        private SelectDeckButton selectedDeckButton;

        private Tween createButtonTween;

        protected override void AfterInitialization()
        {
            deckController = BoxController.GetController<DeckBuildController>();
        }

        private void ChangeDeck()
        {
            List<DeckData> decks = deckController.Decks;

            foreach (var button in selectDeckButtons)
            {
                button.gameObject.SetActive(false);
            }

            if(decks.Count == 0)
            {
                createButtonTween = myCardsButton.transform.DOScale(myCardsButton.transform.localScale * 1.1f, 1f).
                    SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                for (int i = 0; i < selectDeckButtons.Length; i++)
                {
                    selectDeckButtons[i].SetNameDeck = decks[i].Name;
                    selectDeckButtons[i].gameObject.SetActive(true);
                    selectDeckButtons[i].IdDeck = decks[i].Id;

                    if (decks[i].IsSelected)
                    {
                        selectedDeckButton = selectDeckButtons[i];
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
            }
        }
    }
}