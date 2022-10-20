using UnityEngine;

namespace UI.Components
{
    public class LineCard : MonoBehaviour
    {
        [SerializeField] private GameObject[] positionsCards;

        public void DisposeCards(GameObject[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[0].transform.parent = this.transform;
                cards[0].transform.position = positionsCards[0].transform.position;
            }
        }
    }
}