using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cards
{
    public abstract class CardUI : CardBase, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Image cardImage;

        private bool inDeck = false, isPreview;
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inDeck)
            {
                isPreview = true;

                UIManager.Instance.GetWindow<DeckBuildWindow>().PointerEnterOnCard(this);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (inDeck && isPreview)
            {
                isPreview = false;

                UIManager.Instance.GetWindow<DeckBuildWindow>().DeletePreviewCard();
            }
        }

        protected abstract void SelectCard();
        protected abstract void DeSelectCard();
    }
}