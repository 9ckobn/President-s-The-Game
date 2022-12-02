using MoralisUnity;
using MoralisUnity.Kits.AuthenticationKit;
using MoralisUnity.Platform.Objects;
using NaughtyAttributes;
using System.Collections;
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

        [BoxGroup("Moralis")]
        [SerializeField] private AuthenticationKit authenticationKit;
        [BoxGroup("Moralis")]
        [SerializeField] private bool isUseMoralis, isQrStartMoralis = false;
        [BoxGroup("Moralis")]
        [SerializeField] private GameObject qrObject, buttonObject;

        public bool IsTutorNow { get; set; }

        #region START_APP

        private void Start()
        {
            // TODO: Load from base Moralis
            IsTutorNow = true;

            DataBaseManager.Instance.SetIsUseMoralis = isUseMoralis;
            SceneControllers.OnInit += AfterControllersInit;
            SceneControllers.InitControllers();
        }

        public async void AfterControllersInit()
        {
            SceneControllers.OnInit -= AfterControllersInit;

            LogManager.SetIsNeedLog = isNeedLog;
            LogManager.Log("AfterControllersInit");

            if (isUseMoralis)
            {
                LogManager.Log("Before InitializeAsync");

                await authenticationKit.InitializeAsync();

                LogManager.Log("After InitializeAsync");

                if (isQrStartMoralis)
                {
                    qrObject.SetActive(true);
                    buttonObject.SetActive(false);
                }
                else
                {
                    qrObject.SetActive(false);
                    buttonObject.SetActive(true);
                }

                UIManager.GetWindow<ConnectMetamaskWindow>().SetMethodConnect(isQrStartMoralis);
            }
            else
            {
                authenticationKit.gameObject.SetActive(false);

                UIManager.GetWindow<ConnectMetamaskWindow>().SetMethodConnect(false);
            }

            UIManager.ShowWindow<ConnectMetamaskWindow>();
        }

        public void MetamaskConnect()
        {
            UIManager.HideWindow<ConnectMetamaskWindow>();

            if (isUseMoralis)
            {
                authenticationKit.Connect();
            }
            else
            {
                DataBaseManager.OnInit += AfterInitDataBase;
                DataBaseManager.Instance.SetMoralisUser = null;
                DataBaseManager.Instance.Initialize();
            }
        }

        public async void OnConnectMoralis()
        {
            LogManager.Log("On connect to moralis");

            MoralisUser moralisUser = await Moralis.GetUserAsync();

            if (moralisUser != null)
            {
                DataBaseManager.OnInit += AfterInitDataBase;
                DataBaseManager.Instance.SetMoralisUser = moralisUser;
                DataBaseManager.Instance.Initialize();
            }
            else
            {
                LogManager.LogError($"User not connect to moralis!");
            }
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