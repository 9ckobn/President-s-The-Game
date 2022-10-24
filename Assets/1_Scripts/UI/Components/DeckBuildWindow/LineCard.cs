using UnityEngine;

namespace UI.Components
{
    public class LineCard : MonoBehaviour
    {
        [SerializeField] private GameObject[] positionsCards;

        private int counterPositions;

        public void AddCard(GameObject cards)
        {
            cards.transform.parent = this.transform;
            cards.transform.position = positionsCards[counterPositions].transform.position;
            counterPositions++;
        }
    }
}