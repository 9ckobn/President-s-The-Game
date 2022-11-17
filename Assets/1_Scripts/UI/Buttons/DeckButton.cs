using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class DeckButton : MyButton
    {
        [SerializeField] protected Text nameDeckText;
        [SerializeField] protected InputField inputField;

        protected Image buttonImage;

        public string SetNameDeck { set => nameDeckText.text = value; }

        [HideInInspector]
        public int IdDeck;

        protected override void AfterAwake()
        {
            buttonImage = GetComponent<Image>();
        }

        public void SetSpriteButton(Sprite sprite)
        {
            buttonImage.sprite = sprite;
        }
    }
}