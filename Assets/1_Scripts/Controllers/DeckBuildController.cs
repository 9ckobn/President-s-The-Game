using Cards;
using Cards.Data;
using Core;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "DeckBuildController", menuName = "Controllers/Gameplay/DeckBuildController")]
    public class DeckBuildController : BaseController
    {
        private List<DeckData> decks = new List<DeckData>();
        private DeckData selectedDeck;
        private TypeClimate typeClimate;

        public List<DeckData> GetAllDecks { get => decks; }
        public DeckData GetSelectedDeck { get => selectedDeck; }
        public TypeClimate GetTypeClimate { get => typeClimate; }

        public override void OnInitialize()
        {
            // TODO: load decks

            for (int i = 0; i < 4; i++)
            {
                DeckData deckData = new DeckData(i, null, null);
                decks.Add(deckData);
            }

            selectedDeck = decks[0];
        }

        public override void OnStart()
        {
            UIManager.Instance.OnInitialize();
            UIManager.Instance.OnStart();

            UIManager.Instance.ShowWindow<DeckBuildWindow>();
        }


        public DeckData GetDeckData(int idDeck)
        {
            return decks[idDeck];
        }
    }
}