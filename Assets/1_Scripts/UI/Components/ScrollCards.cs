using Cards.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class ScrollCards : MonoBehaviour
    {
        private const int CARD_ID_LINE = 3;

        [SerializeField] private LineCard prefabLineCard;

        private List<LineCard> linesCard = new List<LineCard>();

        public void SetCards(List<GameObject> cardsData)
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