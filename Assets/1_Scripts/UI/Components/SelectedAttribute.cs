using EffectSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class SelectedAttribute : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Text valueText;
        [SerializeField] private GameObject blockImage;
        
        public TypeAttribute TypeAttribute { get; private set; }

        public void Init(Sprite sprite, TypeAttribute typeAttribute)
        {
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
    }
}