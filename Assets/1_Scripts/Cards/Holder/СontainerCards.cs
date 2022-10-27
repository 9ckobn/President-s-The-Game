using UnityEngine;

namespace Cards.�ontainer
{
    public class �ontainerCards : MonoBehaviour
    {
        protected int maxCards, countCards = 0;

        [SerializeField] protected GameObject[] positions;

        public void SetData(int maxCards)
        {
            this.maxCards = maxCards;
        }
    }
}
