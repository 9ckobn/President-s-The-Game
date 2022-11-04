using Core;
using Gameplay;
using NaughtyAttributes;
using UnityEngine;

namespace Cards
{
    public abstract class CardModel : CardBase
    {
        [BoxGroup("Start scale")]
        [SerializeField] protected float startScale = 1f;
        [BoxGroup("Parent model")]
        [SerializeField] private GameObject parentModel;
        [BoxGroup("Highlight")]
        [SerializeField] private GameObject highlight;

        protected GameObject model;
        private bool isSelected = false, isUse;
        protected bool isCanInteraction = true;

        protected CardsController cardController;

        public GameObject SetMode
        {
            set
            {
                model = value;
                model.transform.SetParent(parentModel.transform);
                model.transform.position = parentModel.transform.position;
                transform.localScale = new Vector3(startScale, startScale, startScale);
            }
        }

        protected override void AfterAwake()
        {
            cardController = BoxController.GetController<CardsController>();
        }

        private void OnMouseEnter()
        {
            if (!isSelected && isCanInteraction && cardController.CanSelectedCard)
            {
                isSelected = true;

                MouseEnter();
            }
        }

        private void OnMouseExit()
        {
            if (isSelected && !isUse)
            {
                isSelected = false;

                MouseExit();
            }
        }

        private void OnMouseDown()
        {
            if (isSelected && isCanInteraction)
            {
                if (isUse)
                {
                    isUse = false;
                    ChangeHighlight(false);

                    StopUseCard();
                }
                else
                {
                    isUse = true;
                    ChangeHighlight(true);

                    UseCard();
                }
            }
        }

        private void OnDisable()
        {
            isSelected = false;
            isUse = false;
        }

        public void ChangeHighlight(bool isActive)
        {
            highlight.gameObject.SetActive(isActive);
        }

        protected abstract void MouseEnter();
        protected abstract void MouseExit();
        protected abstract void UseCard();
        protected abstract void StopUseCard();
    }
}