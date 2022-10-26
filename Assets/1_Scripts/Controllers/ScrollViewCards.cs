using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public class ScrollViewCards : MonoBehaviour
    {
        [SerializeField] private BlockCards lineCardPrefab;
        [SerializeField] private GameObject content;

        private List<BlockCards> lines = new List<BlockCards>();

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