using UnityEngine;

namespace UI.Components
{
    public class CurrentDeckUI : MonoBehaviour
    {
        [SerializeField] private GameObject parentCards;

        public void AddCard(GameObject card)
        {
            card.transform.SetParent(parentCards.transform);

            // TODO: Sorting cards
        }
    }
}