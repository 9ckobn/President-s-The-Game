using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class EditDeckButton : DeckButton
    {
        [SerializeField] protected InputField inputField;

        private string prevName;          

        protected override void OnClickButton()
        {
            UIManager.Instance.GetWindow<DeckBuildWindow>().ClickDeckButton(this);
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