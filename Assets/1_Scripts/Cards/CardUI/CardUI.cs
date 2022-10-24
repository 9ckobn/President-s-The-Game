using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public abstract class CardUI : CardBase
    {
        [SerializeField] protected Image cardImage;

        private bool isSelectCard = false;

        protected override void AfterAwake()
        {
            GetComponent<Button>().onClick.AddListener(SelectCard);
        }

        protected void ClickCard()
        {
            isSelectCard = !isSelectCard;

            if (isSelectCard)
            {
                SelectCard();
            }
            else
            {
                DeSelectCard();
            }

        }

        protected abstract void SelectCard();
        protected abstract void DeSelectCard();
    }
}