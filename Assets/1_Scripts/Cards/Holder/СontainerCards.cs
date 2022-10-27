using UnityEngine;

namespace Cards.Ñontainer
{
    public class ÑontainerCards : MonoBehaviour
    {
        protected int maxCards, countCards = 0;

        [SerializeField] protected GameObject[] positions;

        public void SetData(int maxCards)
        {
            this.maxCards = maxCards;
        }
    }
}
