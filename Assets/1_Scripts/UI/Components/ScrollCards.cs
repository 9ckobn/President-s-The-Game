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

        public void SetCards(List<CardBase> cardsData)
        {
            int countCards = 0;
            LineCard line = null;

            for (int c = 0; c < cardsData.Count; c++)
            {
                if (countCards == 0)
                {
                    line = Instantiate(prefabLineCard, contentLinesParent.transform);
                    linesCard.Add(line);

                    float prevHeight = contentLinesParent.GetComponent<RectTransform>().rect.height;
                    contentLinesParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, prevHeight + HEIGHT_LINE);
                }

                line.AddCard(cardsData[c].gameObject);

                countCards++;
                if (countCards >= 3)
                {
                    countCards = 0;
                }
            }
        }
    }
}