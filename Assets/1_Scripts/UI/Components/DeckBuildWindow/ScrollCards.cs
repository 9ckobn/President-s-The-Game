using Cards;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class ScrollCards : MonoBehaviour
    {
        private const int CARDS_IN_LINE = 3, HEIGHT_LINE = 300;

        [SerializeField] private LineCard prefabLineCard;
        [SerializeField] private GameObject contentLinesParent;

        private List<LineCard> linesCard = new List<LineCard>();
        private List<GameObject> cards;

        public void ClearLines()
        {
            for (int i = linesCard.Count - 1; i >= 0; i--)
            {
                Destroy(linesCard[i].gameObject);
            }

            linesCard.Clear();
            contentLinesParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        }

        public void AddCards(List<CardPresidentUI> cardsPresident)
        {
            cards = new List<GameObject>();

            foreach (var card in cardsPresident)
            {
                cards.Add(card.gameObject);
            }

            AddCards();
        }

        public void AddCards(List<CardFightUI> cardsFight)
        {
            cards = new List<GameObject>();

            foreach (var card in cardsFight)
            {
                cards.Add(card.gameObject);
            }

            AddCards();
        }

        private void AddCards()
        {
            linesCard = new List<LineCard>();

            int countCards = 0;
            LineCard line = null;

            for (int c = 0; c < cards.Count; c++)
            {
                if (countCards == 0)
                {
                    line = Instantiate(prefabLineCard, contentLinesParent.transform);
                    linesCard.Add(line);

                    float prevHeight = contentLinesParent.GetComponent<RectTransform>().rect.height;
                    contentLinesParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, prevHeight + HEIGHT_LINE);
                }

                line.AddCard(cards[c].gameObject);

                countCards++;
                if (countCards >= CARDS_IN_LINE)
                {
                    countCards = 0;
                }
            }
        }
    }
}