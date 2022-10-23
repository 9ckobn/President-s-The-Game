using Cards.Data;
using Cards.Type;
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

                for (int c = 0; c < 6; c++)
                {
                    CardPresidentData cardData = new CardPresidentData(c.ToString());
                    deckData.AddPresidentCard(cardData);
                }

                for (int c = 0; c < 12; c++)
                {
                    CardFightData cardData = new CardFightData(c.ToString());
                    deckData.AddFightCard(cardData);
                }
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