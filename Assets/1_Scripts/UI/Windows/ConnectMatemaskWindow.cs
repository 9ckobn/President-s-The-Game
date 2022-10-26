using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ConnectMatemaskWindow : Window
    {
        [SerializeField] private Button connectButton;
        [SerializeField] private GameObject qrImage;

        protected override void AfterInitialization()
        {
            connectButton.onClick.AddListener(ClickConnectButton);
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

        private void ClickConnectButton()
        {
            AppManager.Instance.MetamaskConnect();
        }
    }
}