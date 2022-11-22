using Cards.Data;
using Cards.View;
using Core;
using DG.Tweening;
using UI.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cards
{
    [RequireComponent(typeof(CardPresidentView))]
    public class BuffAttributeCardPresidentUI : CardBase, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] protected Image cardImage;

        private const float START_SCALE = 1f, SCALE_SELECTED = 1.45f;

        private RectTransform rectTransform;
        private Canvas canvas;
        private Sequence mySequence;

        public SelectedAttribute Attribute { get; set; }

        public CardPresidentData SetCardData
        {
            set
            {
                data = value;

                (view as CardPresidentView).SetData(data as CardPresidentData);

                cardImage.sprite = data.Sprite;
            }
        }

        protected override void AfterAwake()
        {
            Attribute = null;

            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(Attribute != null)
            {
                Attribute.RemoveCard();
            }

            mySequence = DOTween.Sequence();

            mySequence.AppendCallback(() =>
            {
                transform.DOScale(new Vector3(START_SCALE * SCALE_SELECTED, START_SCALE * SCALE_SELECTED, START_SCALE * SCALE_SELECTED), 0.15f);
            });

            cardImage.raycastTarget = false;
            BoxController.GetController<BuffAttributePresidentController>().SelectCard(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            BoxController.GetController<BuffAttributePresidentController>().DeselecCard(this);

            cardImage.raycastTarget = true;
        }

        public void ReturnStartScale()
        {
            mySequence = DOTween.Sequence();

            mySequence.AppendCallback(() =>
            {
                transform.DOScale(new Vector3(START_SCALE, START_SCALE, START_SCALE), 0.15f);
            });
        }
    }
}