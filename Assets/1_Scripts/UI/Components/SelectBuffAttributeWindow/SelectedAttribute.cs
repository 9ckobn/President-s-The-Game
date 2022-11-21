using Cards;
using Core;
using EffectSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Components
{
    public class SelectedAttribute : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Text valueText;
        [SerializeField] private GameObject blockImage, highlightImage;

        public BuffAttributeCardPresidentUI CardPresident { get; private set; }
        
        public bool IsEmpty { get; private set; }
        public TypeAttribute TypeAttribute { get; private set; }

        public void Init(Sprite sprite, TypeAttribute typeAttribute)
        {
            CardPresident = null;
            IsEmpty = true;
            TypeAttribute = typeAttribute;
            iconImage.sprite = sprite;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            BoxController.GetController<BuffAttributePresidentController>().SeletAttribute(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            BoxController.GetController<BuffAttributePresidentController>().DeselectAttribute(this);
        }

        public void PutCardInAttribute(BuffAttributeCardPresidentUI card)
        {
            CardPresident = card;
            card.transform.SetParent(transform.parent);
            card.transform.position = transform.position;
        }

        public void RemoveCard()
        {
            CardPresident = null;
        }

        public void ShowValue(int value)
        {
            valueText.text = "+" + value;
            valueText.gameObject.SetActive(true);
            iconImage.gameObject.SetActive(true);
        }

        public void ShowBlock()
        {
            iconImage.gameObject.SetActive(false);
            valueText.gameObject.SetActive(false);
            blockImage.SetActive(true);
        }

        public void HideInfo()
        {
            iconImage.gameObject.SetActive(false);
            valueText.gameObject.SetActive(false);
            blockImage.gameObject.SetActive(false);
        }

        public void EnableHighlight()
        {
            highlightImage.SetActive(true);
        }

        public void DisableHighlight()
        {
            highlightImage.SetActive(false);
        }
    }
}