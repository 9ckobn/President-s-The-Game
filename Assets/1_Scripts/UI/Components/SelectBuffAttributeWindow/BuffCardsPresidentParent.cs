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
        private Sequence mySequence;

        //public void AddCardsOnStart(List<BuffAttributeCardPresidentUI> cards)
        //{
        //    this.cards = cards;


        //}

        public void AddCard(BuffAttributeCardPresidentUI card)
        {
            if (cards.Count == 0)
            {
                Vector3 position = transform.position;
                mySequence = DOTween.Sequence();

                mySequence.AppendCallback(() =>
                {
                    card.transform.DOMove(transform.position, 0.15f);
                });
            }
            else if (cards.Count == 1)
            {
                Vector3 position = transform.position;
                position.x -= OFFSET_CARD;

                mySequence = DOTween.Sequence();

                mySequence.AppendCallback(() =>
                {
                    cards[0].transform.DOMove(transform.position, 0.15f);
                });

                position = transform.position;
                position.x += OFFSET_CARD;

                mySequence.AppendCallback(() =>
                {
                    card.transform.DOMove(transform.position, 0.15f);
                });
            }
            else if (cards.Count == 2)
            {
                Vector3 position = transform.position;
                position.x -= OFFSET_CARD * 0.7f;

                mySequence = DOTween.Sequence();

                mySequence.AppendCallback(() =>
                {
                    cards[0].transform.DOMove(transform.position, 0.15f);
                });


                position = transform.position;

                mySequence = DOTween.Sequence();

                mySequence.AppendCallback(() =>
                {
                    cards[1].transform.DOMove(transform.position, 0.15f);
                });


                position = transform.position;
                position.x += OFFSET_CARD * 0.7f;

                mySequence = DOTween.Sequence();

                mySequence.AppendCallback(() =>
                {
                    card.transform.DOMove(transform.position, 0.15f);
                });
            }

            cards.Add(card);
        }

        public void RemoveCard(BuffAttributeCardPresidentUI card)
        {

        }

        private void CountPositions()
        {
            if(cards.Count == 3)
            {

            }
        }
    }
}