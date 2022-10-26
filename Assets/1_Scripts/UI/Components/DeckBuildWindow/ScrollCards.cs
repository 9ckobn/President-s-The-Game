using Cards;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class ScrollCards : MonoBehaviour
    {
        private const int CARDS_IN_BLOCK = 6, WIDTH_BLOCK = 990;

        [SerializeField] private BlockCards prefabLineCard;
        [SerializeField] private GameObject contentLinesParent;

        private List<BlockCards> linesCard = new List<BlockCards>();
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
            linesCard = new List<BlockCards>();

            int countCards = 0;
            BlockCards line = null;

            for (int c = 0; c < cards.Count; c++)
            {
                if (countCards == 0)
                {
                    line = Instantiate(prefabLineCard, contentLinesParent.transform);
                    linesCard.Add(line);

                    float prevWidth = contentLinesParent.GetComponent<RectTransform>().rect.width;
                    contentLinesParent.GetComponent<RectTransform>().sizeDelta = new Vector2(prevWidth + WIDTH_BLOCK, 0);
                    cards[c].gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.1f, 1.1f);
                }

                line.AddCard(cards[c].gameObject);

                countCards++;
                if (countCards >= CARDS_IN_BLOCK)
                {
                    countCards = 0;
                }
            }
        }
    }
}