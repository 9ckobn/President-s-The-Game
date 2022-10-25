using Cards;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class CurrentDeckUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] positions;
        [SerializeField] private GameObject parentCards;

        private int countPositions = 0;

        private List<CardUI> cardsInDeck = new List<CardUI>();

        public void AddCard(CardUI card)
        {
            card.transform.SetParent(parentCards.transform);
            card.transform.position = positions[countPositions].transform.position;

            countPositions++;
            cardsInDeck.Add(card);

            // TODO: Sorting cards
        }

        public void RemoveCard(CardUI card)
        {
            cardsInDeck.Remove(card);
            Destroy(card.gameObject);

            countPositions = 0;

            for (int i = 0; i < cardsInDeck.Count; i++)
            {
                cardsInDeck[i].transform.position = positions[countPositions].transform.position;
                countPositions++;
            }
        }

        public void RemoveAllCards()
        {
            countPositions = 0;

            foreach (var card in cardsInDeck)
            {
                Destroy(card.gameObject);
            }

            cardsInDeck.Clear();
        }
    }
}