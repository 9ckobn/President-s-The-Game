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
        protected bool isSelected = false, isUse;
        protected bool isBlocked = false;

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

        public void OnMouseEnter()
        {
            if (CheckMouseEnter())
            {
                isSelected = true;

                MouseEnter();
            }
        }

        public void OnMouseExit()
        {
            if (CheckMouseExit())
            {
                isSelected = false;

                MouseExit();
            }
        }

        public void OnMouseDown()
        {
            if (CheckMouseDown())
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

        protected virtual bool CheckMouseEnter()
        {
            return !isSelected && !isBlocked && cardController.CanSelectedCard;
        }

        protected virtual bool CheckMouseExit()
        {
            return isSelected && !isUse;
        }

        protected virtual bool CheckMouseDown()
        {
            return isSelected && !isBlocked;
        }

        protected abstract void MouseEnter();
        protected abstract void MouseExit();
        protected abstract void UseCard();
        protected abstract void StopUseCard();
    }
}