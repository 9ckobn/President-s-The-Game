using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ConnectMatemaskWindow : Window
    {
        [SerializeField] private Button connectButton, mainPageButton;
        [SerializeField] private GameObject qrImage;

        protected override void AfterInitialization()
        {
            connectButton.onClick.AddListener(ClickConnectButton);
            mainPageButton.onClick.AddListener(ClickMainPageButton);
        }

        public void SetMethodConnect(bool qrMethod)
        {
            if (qrMethod)
            {
                connectButton.gameObject.SetActive(false);
                qrImage.gameObject.SetActive(true);
            }
            else
            {
                connectButton.gameObject.SetActive(true);
                qrImage.gameObject.SetActive(false);
            }
        }

        private void ClickMainPageButton()
        {
            AppManager.Instance.GoToMainPageMetaverse();
        }

        private void ClickConnectButton()
        {
            AppManager.Instance.MetamaskConnect();
        }
    }
}