using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ConnectMetamaskWindow : Window
    {
        [SerializeField] private Button connectButton;

        protected override void AfterInitialization()
        {
            connectButton.onClick.AddListener(() =>
            {
                AppManager.Instance.MetamaskConnect();
            });
        }
    }
}