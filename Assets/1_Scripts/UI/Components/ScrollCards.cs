using Cards;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class ScrollCards : MonoBehaviour
    {
        private const int CARD_ID_LINE = 3;

        [SerializeField] private LineCard prefabLineCard;
        [SerializeField] private GameObject linesParent;

        private List<LineCard> linesCard = new List<LineCard>();

        public void SetCards(List<CardBase> cardsData)
        {
            int countCards = 0;
            LineCard line = null;

            for (int c = 0; c < cardsData.Count; c++)
            {
                if (countCards == 0)
                {
                    line = Instantiate(prefabLineCard, linesParent.transform);
                    linesCard.Add(line);
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