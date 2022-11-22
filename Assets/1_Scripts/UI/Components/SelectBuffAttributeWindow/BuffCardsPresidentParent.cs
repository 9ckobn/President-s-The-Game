using Cards;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class BuffCardsPresidentParent : MonoBehaviour
    {
        private const int OFFSET_CARD = 150;

        private List<BuffAttributeCardPresidentUI> cards = new List<BuffAttributeCardPresidentUI>();
        private Vector3[] positions = new Vector3[3];

        public void AddCardsInStart(List<BuffAttributeCardPresidentUI> cards)
        {
            this.cards = cards;

            CountPositions();

            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].transform.SetParent(transform);
                cards[i].transform.DOMove(positions[i], 0.15f);
            }
        }

        public void AddCard(BuffAttributeCardPresidentUI card)
        {
            cards.Add(card);
            card.transform.SetParent(transform);

            CountPositions();

            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].transform.DOMove(positions[i], 0.3f);
            }
        }

        public void RemoveCard(BuffAttributeCardPresidentUI card)
        {
            cards.Remove(card);

            BuffAttributeCardPresidentUI[] sortArray = new BuffAttributeCardPresidentUI[cards.Count];

            foreach (var currentCard in cards)
            {
                sortArray[currentCard.transform.GetSiblingIndex()] = currentCard;
            }

            cards = new List<BuffAttributeCardPresidentUI>(sortArray);
        }

        private void CountPositions()
        {
            if (cards.Count == 1)
            {
                positions[0] = transform.position;
            }
            else if (cards.Count == 2)
            {
                Vector3 position = transform.position;
                position.x -= OFFSET_CARD;
                positions[0] = position;

                position = transform.position;
                position.x += OFFSET_CARD;
                positions[1] = position;
            }
            else if (cards.Count == 3)
            {
                Vector3 position = transform.position;
                position.x -= OFFSET_CARD * 0.7f;
                positions[0] = position;

                position = transform.position;
                positions[1] = position;

                position.x += OFFSET_CARD * 0.7f;
                positions[2] = position;
            }            
        }
    }
}