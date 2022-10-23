using Cards.Type;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class AttributePresidentUI : MonoBehaviour
    {
        [SerializeField] private Color upColor, downColor, defaultColor;

        private Text numberText;
        private Image image;

        private void Awake()
        {
            numberText = GetComponent<Text>();
            image = GetComponentInChildren<Image>();
        }

        public void SetData(int value, TypeStateAttribute state)
        {
            numberText.text = value.ToString();

            if (state == TypeStateAttribute.Default)
            {
                image.color = defaultColor;
            }
            else if (state == TypeStateAttribute.Up)
            {
                image.color = upColor;
            }
            else if (state == TypeStateAttribute.Down)
            {
                image.color = downColor;
            }
        }
    }
}