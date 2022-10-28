using Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public abstract class CardModel : CardBase
    {
        [SerializeField] private float startScele = 1f;
        [SerializeField] private GameObject parentModel;

        protected GameObject model;
        private bool pointerEnter = false, isSelected;

        public GameObject SetMode
        {
            set
            {
                model = value;
                model.transform.SetParent(parentModel.transform);
                model.transform.position = parentModel.transform.position;
                transform.localScale = new Vector3(startScele, startScele, startScele);
            }
        }

        protected override void AfterAwake() { }

        private void OnMouseEnter()
        {
            if (!pointerEnter)
            {
                pointerEnter = true;

                MouseEnter();
            }
        }

        private void OnMouseExit()
        {
            if (pointerEnter && !isSelected)
            {
                pointerEnter = false;

                MouseExit();
            }
        }

        private void OnMouseDown()
        {
            isSelected = true;

            MouseDown();
        }

        protected abstract void MouseEnter();
        protected abstract void MouseExit();
        protected abstract void MouseDown();
    }
}