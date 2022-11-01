using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RandomWindow : Window
    {
        [SerializeField] private GameObject substrate;
        [SerializeField] private Text text;
        [SerializeField] private Button closeButton;

        protected override void AfterInitialization()
        {
            closeButton.onClick.AddListener(ClickCloseButton);
        }

        public void ShowText(string text)
        {
            Show();

            this.text.text = text;
            substrate.SetActive(true);
        }

        private void ClickCloseButton()
        {
            substrate.SetActive(false);

            Hide();
        }
    }
}