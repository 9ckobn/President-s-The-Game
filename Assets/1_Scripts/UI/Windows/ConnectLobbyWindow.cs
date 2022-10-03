using Core;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ConnectLobbyWindow : Window
    {
        [SerializeField] private Button buttonMainPage, cancelButton;
        [SerializeField] private Slider connectSlider;

        protected override void AfterInitialization()
        {
            cancelButton.onClick.AddListener(ClickCancelButton);
            buttonMainPage.onClick.AddListener(ClickMainPageButton);
        }

        protected override void AfterShow()
        {
            StartCoroutine(CoFakeSlider());
        }

        private void ClickMainPageButton()
        {
            AppManager.Instance.GoToMainPageMetaverse();
        }

        private void ClickCancelButton()
        {
            AppManager.Instance.CancelConnectMetaverse();
        }

        private IEnumerator CoFakeSlider()
        {
            while (connectSlider.value != 100)
            {
                yield return new WaitForSeconds(0.025f);
                connectSlider.value += 1;
            }
        }
    }
}