using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class DeckButton : MyButton
    {
        [SerializeField] private Text nameDeckText;
        [SerializeField] private InputField inputField;

        private Image buttonImage;
        private string prevName;

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
            inputField.characterLimit = MainData.MAC_LENGTH_DECK_NAME;
            inputField.gameObject.SetActive(true);
            inputField.ActivateInputField();
            inputField.onEndEdit.AddListener(delegate { EndRenameDeck(); });
            nameDeckText.gameObject.SetActive(false);
        }

        private void EndRenameDeck()
        {
            inputField.gameObject.SetActive(false);
            nameDeckText.gameObject.SetActive(true);

            if (inputField.text.Length > 0)
            {
                nameDeckText.text = inputField.text;
                UIManager.Instance.GetWindow<DeckBuildWindow>().EndRenameDeck(IdDeck, inputField.text);
            }
            else
            {
                nameDeckText.text = prevName;
            }
        }
    }
}