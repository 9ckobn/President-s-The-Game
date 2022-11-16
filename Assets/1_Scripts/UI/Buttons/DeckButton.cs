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

        public void EndRenameDeck()
        {
            inputField.gameObject.SetActive(false);
            nameDeckText.gameObject.SetActive(true);
            nameDeckText.text = inputField.text;

            UIManager.Instance.GetWindow<DeckBuildWindow>().EndRenameDeck(inputField.text);
        }
    }
}