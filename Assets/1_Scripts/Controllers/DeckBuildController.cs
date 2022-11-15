using Cards.Data;
using Core;
using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.DeckBuild
{
    [CreateAssetMenu(fileName = "DeckBuildController", menuName = "Controllers/DeckBuild/DeckBuildController")]
    public class DeckBuildController : BaseController
    {
        private const string DEFAULT_DECK_NAME = "Deck";

        public List<DeckData> Decks { get; private set; }
        public DeckData SelectedDeck { get; private set; }

        public override void OnInitialize()
        {
            Decks = DataBaseManager.Instance.DecksData;

            if (Decks.Count > 0)
            {
                foreach (var deck in Decks)
                {
                    if (deck.IsSelected)
                    {
                        SelectedDeck = deck;
                    }
                }
            }
        }

        public int GetCountPresidentCards { get => SelectedDeck.PresidentsId.Count; }
        public int GetCountFightCards { get => SelectedDeck.FightsId.Count; }

        public bool CanAddPresidentCard { get => SelectedDeck.CanAddPresidentData(); }
        public bool CanAddFightCard { get => SelectedDeck.CanAddFightData(); }
        public bool CanCreateDeck { get => Decks.Count < MainData.MAX_DECKS; }

        public void SelectDeck(int idDeck)
        {
            foreach (var deck in Decks)
            {
                if(deck.Id == idDeck)
                {
                    SelectedDeck = deck;
                    return;
                }
            }
        }

        public void CreateDeck()
        {
            DeckData newDeck = new DeckData(Decks.Count, DEFAULT_DECK_NAME, false, true, new List<string>(), new List<string>());
            Decks.Add(newDeck);
            SelectDeck(newDeck);
        }

        public void AddCardInDeck(CardPresidentData card)
        {
            SelectedDeck.AddPresidentCard(card.Id);
        }

        public void AddCardInDeck(CardFightData card)
        {
            SelectedDeck.AddFightCard(card.Id);
        }

        public void RemoveCardInDeck(CardPresidentData card)
        {
            SelectedDeck.RemovePresidentCard(card.Id);
        }

        public void RemoveCardInDeck(CardFightData card)
        {
            SelectedDeck.RemoveFightCard(card.Id);
        }

        private void SelectDeck(DeckData selectDeck)
        {
            foreach (var deck in Decks)
            {
                deck.IsSelected = deck == selectDeck;
            }

            SelectedDeck = selectDeck;
        }
    }
}