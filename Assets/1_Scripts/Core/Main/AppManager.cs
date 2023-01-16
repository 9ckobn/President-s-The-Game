using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Thirdweb;
using UI;
using UnityEngine;

namespace Core
{
    public class AppManager : Singleton<AppManager>
    {
        [BoxGroup("Managers")]
        [SerializeField] private LobbyManager lobbyManager;
        [BoxGroup("Parameters app")]
        [SerializeField] private bool isNeedLog;

        public bool IsTutorNow { get; set; }

        private ThirdwebSDK sdk;


        #region START_APP

        private void Start()
        {
            sdk = new ThirdwebSDK("binance");

            IsTutorNow = true;

            SceneControllers.OnInit += AfterControllersInit;
            SceneControllers.InitControllers();
        }

        public void AfterControllersInit()
        {
            SceneControllers.OnInit -= AfterControllersInit;

            LogManager.SetIsNeedLog = isNeedLog;
            LogManager.Log("After Controllers Init");

            UIManager.ShowWindow<ConnectMetamaskWindow>();
        }

        public async void MetamaskConnect()
        {
            UIManager.HideWindow<ConnectMetamaskWindow>();

            try
            {
                string address = await sdk.wallet.Connect(new WalletConnection()
                {
                    provider = WalletProvider.MetaMask,
                    chainId = 56 // Switch the wallet Goerli on connection
                });

                LogManager.Log("Connected as: " + address);
                OnConnectMetamask();
            }
            catch (System.Exception e)
            {
                LogManager.LogError("Error (see console): " + e.Message);
            }
        }

        public async void OnConnectMetamask()
        {
            LogManager.Log("On connect to metamask");

            CurrencyValue balance = await sdk.wallet.GetBalance();
            Debug.Log($"Balance = {balance}");


            //var contract = sdk.GetContract("0x2e01763fA0e15e07294D74B63cE4b526B321E389"); // NFT Drop
            //int count = 0;
            //NFT result = await contract.ERC721.Get(count.ToString());
            //Debug.Log("GetERC721   " + result.metadata.name + "\nowned by " + result.owner.Substring(0, 6) + "...");
            //Debug.Log($"RESULT   =   {result}");


            DataBaseManager.OnInit += AfterInitDataBase;
            DataBaseManager.Instance.Initialize();
        }

        private void AfterInitDataBase()
        {
            DataBaseManager.OnInit -= AfterInitDataBase;

            if (IsTutorNow)
            {
                LoadSceneManager.instance.LoadFightScene();
            }
            else
            {
                LoadSceneManager.instance.LoadDeckBuildScene();
            }
        }

        #endregion

        public void FailedConnect()
        {
            LogManager.LogError($"Failed connect to moralis!");
        }

        public void ConnectToMetaverse()
        {
            lobbyManager.Connect();
        }

        public void CancelConnectMetaverse()
        {

        }

        public void GoToMainPageMetaverse()
        {
            //Application.OpenURL(URL_Data.RARITIGRAM_URL);
        }
    }
}