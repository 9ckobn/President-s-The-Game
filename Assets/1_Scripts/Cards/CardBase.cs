using Cards.Data;
using UnityEngine;

namespace Cards
{
    public abstract class CardBase : MonoBehaviour
    {
        protected CardDataBase data;
        protected CardViewBase view;

        private void Awake()
        {
            view = GetComponent<CardViewBase>();
        }
    }
}