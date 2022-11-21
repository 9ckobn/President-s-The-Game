using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(Button))]
    public abstract class CardUI : CardBase, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Image cardImage;
        [SerializeField] private GameObject blockCardObject;

        private bool isCanSelected = true;
        private bool inDeck = false, isPreview;

        public bool SetInDeck { set => inDeck = value; }
        public bool SetIsCanSelected { set => isCanSelected = value; }

        protected override void AfterAwake()
        {
            GetComponent<Button>().onClick.AddListener(ClickCard);
        }

        protected void ClickCard()
        {
            if (isCanSelected)
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
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inDeck)
            {
                isPreview = true;

                UIManager.GetWindow<DeckBuildWindow>().PointerEnterOnCard(this);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (inDeck && isPreview)
            {
                isPreview = false;

                UIManager.GetWindow<DeckBuildWindow>().DeletePreviewCard();
            }
        }

        public void ChangeState(bool isCanSelected)
        {
            this.isCanSelected = isCanSelected;
            blockCardObject.SetActive(!isCanSelected);
        }

        protected abstract void SelectCard();
        protected abstract void DeSelectCard();
    }
}