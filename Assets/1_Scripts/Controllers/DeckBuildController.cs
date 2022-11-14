using Cards.Data;
using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.DeckBuild
{
    [CreateAssetMenu(fileName = "DeckBuildController", menuName = "Controllers/DeckBuild/DeckBuildController")]
    public class DeckBuildController : BaseController
    {
        public List<DeckData> Decks { get; private set; }
        public DeckData SelectedDeck { get; private set; }

        public override void OnInitialize()
        {
            Decks = DataBaseManager.Instance.DecksData;

            if (Decks.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    DeckData deckData = new DeckData(i, "deck", null, null);
                    Decks.Add(deckData);
                }
            }

            SelectedDeck = Decks[0];
        }

        public bool CanAddCard(CardPresidentData card)
        {
            return SelectedDeck.CanAddPresidentData();
        }

        public bool CanAddCard(CardFightData card)
        {
            return SelectedDeck.CanAddFightData();
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
    }
}