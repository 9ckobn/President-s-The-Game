using UnityEngine;

namespace Cards.Container
{
    public class ContainerCards : MonoBehaviour
    {
        protected int maxCards, countCards = 0;

        [SerializeField] protected GameObject[] positions;

        public void SetData(int maxCards)
        {
            this.maxCards = maxCards;
        }
    }
}
