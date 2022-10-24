using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public abstract class CardUI : CardBase
    {
        [SerializeField] protected Image cardImage;

        protected bool inDeck = false;
        public bool SetInDeck { set => inDeck = value; }

        protected override void AfterAwake()
        {
            GetComponent<Button>().onClick.AddListener(ClickCard);
        }

        protected void ClickCard()
        {
            if (inDeck)
            {
                DeSelectCard();
            }
            else
            {
                SelectCard();
            }

        }

        protected abstract void SelectCard();
        protected abstract void DeSelectCard();
    }
}