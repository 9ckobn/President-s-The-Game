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
                    SelectDeck(deck);
                    return;
                }
            }
        }

        public void CreateDeck()
        {
            int prevId = 0;

            foreach (var deck in Decks)
            {
                if(deck.Id >= prevId)
                {
                    prevId = deck.Id + 1;
                }
            }
            Debug.Log("id = " + prevId);
            DeckData newDeck = new DeckData(prevId, DEFAULT_DECK_NAME, false, true, new List<string>(), new List<string>());
            Decks.Add(newDeck);
            SelectDeck(newDeck);
        }

        public void AddCardInDeck(CardPresidentData card)
        {
            SelectedDeck.AddPresidentCard(card.Id);

            DataBaseManager.Instance.SaveDecksData();
        }

        public void AddCardInDeck(CardFightData card)
        {
            SelectedDeck.AddFightCard(card.Id);

            DataBaseManager.Instance.SaveDecksData();
        }

        public void RemoveCardInDeck(CardPresidentData card)
        {
            SelectedDeck.RemovePresidentCard(card.Id);

            DataBaseManager.Instance.SaveDecksData();
        }

        public void RemoveCardInDeck(CardFightData card)
        {
            SelectedDeck.RemoveFightCard(card.Id);

            DataBaseManager.Instance.SaveDecksData();
        }

        private void SelectDeck(DeckData selectDeck)
        {
            foreach (var deck in Decks)
            {
                deck.IsSelected = deck == selectDeck;
            }

            SelectedDeck = selectDeck;
            DataBaseManager.Instance.SaveDecksData();
        }
    }
}