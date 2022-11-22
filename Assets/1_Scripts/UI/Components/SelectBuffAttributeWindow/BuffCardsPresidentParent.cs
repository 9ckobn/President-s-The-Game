using Cards;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class BuffCardsPresidentParent : MonoBehaviour
    {
        private List<BuffAttributeCardPresidentUI> cards = new List<BuffAttributeCardPresidentUI>();
        private Vector3[] positions = new Vector3[3];

        private float offsetCard;

        public void AddCardsInStart(List<BuffAttributeCardPresidentUI> cards)
        {
            this.cards =  new List<BuffAttributeCardPresidentUI>(cards);
            offsetCard = cards[0].GetComponent<RectTransform>().sizeDelta.x;

            foreach (var card in this.cards)
            {
                card.transform.SetParent(transform);
            }

            ChangeCardsPositions();           
        }

        public void AddCard(BuffAttributeCardPresidentUI card)
        {
            cards.Add(card);
            card.transform.SetParent(transform);

            ChangeCardsPositions();
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

            ChangeCardsPositions();
        }

        private void ChangeCardsPositions()
        {
            if (cards.Count == 1)
            {
                positions[0] = transform.position;
            }
            else if (cards.Count == 2)
            {
                Vector3 position = transform.position;
                position.x -= offsetCard * 0.8f;
                positions[0] = position;

                position = transform.position;
                position.x += offsetCard * 0.8f;
                positions[1] = position;
            }
            else if (cards.Count == 3)
            {
                Vector3 position = transform.position;
                position.x -= offsetCard * 1.3f;
                positions[0] = position;

                position = transform.position;
                positions[1] = position;

                position.x += offsetCard * 1.3f;
                positions[2] = position;
            }

            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].transform.DOMove(positions[i], 0.15f);
            }
        }
    }
}