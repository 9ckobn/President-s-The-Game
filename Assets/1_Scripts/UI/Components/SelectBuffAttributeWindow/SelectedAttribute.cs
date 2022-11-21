using EffectSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Components
{
    public class SelectedAttribute : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Text valueText;
        [SerializeField] private GameObject blockImage;
        
        public bool IsEmpty { get; private set; }
        public TypeAttribute TypeAttribute { get; private set; }

        public void Init(Sprite sprite, TypeAttribute typeAttribute)
        {
            IsEmpty = true;
            TypeAttribute = typeAttribute;
            iconImage.sprite = sprite;
        }

        public void ShowValue(int value)
        {
            valueText.text = "+" + value;
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

        public void OnPointerEnter(PointerEventData eventData)
        {

        }
    }
}