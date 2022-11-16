using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class DeckButton : MyButton
    {
        [SerializeField] private Text nameDeckText;
        [SerializeField] private InputField inputField;

        private Image buttonImage;

        public string SetNameDeck { set => nameDeckText.text = value; }

        [HideInInspector]
        public int IdDeck;

        protected override void AfterAwake()
        {
            buttonImage = GetComponent<Image>();
        }

        protected override void OnClickButton()
        {
            UIManager.Instance.GetWindow<DeckBuildWindow>().ClickDeckButton(this);
        }

        public void SetSpriteButton(Sprite sprite)
        {
            buttonImage.sprite = sprite;
        }

        public void RenameDeck()
        {
            inputField.gameObject.SetActive(true);
            inputField.ActivateInputField();
            nameDeckText.gameObject.SetActive(false);
        }

        public string EndRenameDeck()
        {
            inputField.gameObject.SetActive(false);
            nameDeckText.gameObject.SetActive(true);
            nameDeckText.text = inputField.text;

            return inputField.text;
        }

        private IEnumerator CoWaitIput()
        {
        }
    }
}