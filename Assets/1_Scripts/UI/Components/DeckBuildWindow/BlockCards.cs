using UnityEngine;

namespace UI.Components
{
    public class BlockCards : MonoBehaviour
    {
        [SerializeField] private GameObject[] lines;

        private int counterPositions;

        public void AddCard(GameObject card)
        {
            if (counterPositions < 3)
            {
                card.transform.SetParent(lines[0].transform);
            }
            else
            {
                card.transform.SetParent(lines[1].transform);
            }

            counterPositions++;
        }
    }
}