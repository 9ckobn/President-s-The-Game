using Cards;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class ScrollCards : MonoBehaviour
    {
        private const int CARD_ID_LINE = 3;

        [SerializeField] private LineCard prefabLineCard;

        private List<LineCard> linesCard = new List<LineCard>();

        public void SetCards(List<CardFightUI> cardsData)
        {
            int countLines = CountLines(cardsData.Count);

            Debug.Log($"countLines = {countLines}");
        }

        public void SetCards(List<CardPresidentUI> cardsData)
        {
            int countLines = CountLines(cardsData.Count);

            Debug.Log($"countLines = {countLines}");
        }

        private int CountLines(int countCards)
        {
            int count = countCards / CARD_ID_LINE;

            if(countCards % CARD_ID_LINE != 0)
            {
                count++;
            }

            return count;
        }
    }
}