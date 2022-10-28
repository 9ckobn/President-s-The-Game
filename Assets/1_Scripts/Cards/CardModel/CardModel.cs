using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public abstract class CardModel : CardBase, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float startScele = 1f;

        [SerializeField] private GameObject parentModel;

        protected GameObject model;
        private bool pointerEnter = false;

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

        public void OnPointerEnter(PointerEventData eventData)
        {
            pointerEnter = true;

            PointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (pointerEnter)
            {
                pointerEnter = false;

                PointerExit();
            }
        }

        protected abstract void PointerEnter();
        protected abstract void PointerExit();
    }
}