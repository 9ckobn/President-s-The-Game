using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class ScrollViewCards : MonoBehaviour
    {
        [SerializeField] private LineCard lineCardPrefab;
        [SerializeField] private GameObject content;

        private List<LineCard> lines = new List<LineCard>();

        public void DisposeCards(GameObject[] cards)
        {
            int countLines = CountLines(cards.Length);

            if(lines.Count == 0)
            {

            }
        }

        private int CountLines(int countCards)
        {
            return 0;
        }
    }
}