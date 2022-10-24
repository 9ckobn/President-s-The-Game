using UnityEngine;

namespace UI.Components
{
    public class CurrentDeckUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] positions;
        [SerializeField] private GameObject parentCards;

        private int currentPosition;

        public void AddCard(GameObject card)
        {
            card.transform.parent = parentCards.transform;
            card.transform.position = positions[currentPosition].transform.position;

            currentPosition++;
        }
    }
}