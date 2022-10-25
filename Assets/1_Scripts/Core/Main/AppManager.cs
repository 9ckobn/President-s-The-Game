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

        #region START_APP

        private IEnumerator Start()
        {
            DataBaseManager.Instance.SetIsUseMoralis = isUseMoralis;

            if (!SceneControllers.instance.GetIsInit)
            {
                while (!SceneControllers.instance.GetIsInit)
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }

            UIManager.Instance.OnInitialize();
            UIManager.Instance.OnStart();

            AfterControllersInit();
        }

        public async void AfterControllersInit()
        {
            BoxController.GetController<LogController>().SetIsNeedLog = isNeedLog;
            BoxController.GetController<LogController>().Log("AfterControllersInit");

            if (isUseMoralis)
            {
                BoxController.GetController<LogController>().Log("Before InitializeAsync");

                await authenticationKit.InitializeAsync();

                BoxController.GetController<LogController>().Log("After InitializeAsync");

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

                UIManager.instance.GetWindow<ConnectMatemaskWindow>().SetMethodConnect(isQrStartMoralis);
            }
            else
            {
                authenticationKit.gameObject.SetActive(false);

                UIManager.instance.GetWindow<ConnectMatemaskWindow>().SetMethodConnect(false);
            }

            UIManager.instance.ShowWindow<ConnectMatemaskWindow>();
        }

        public void MetamaskConnect()
        {
            UIManager.instance.HideWindow<ConnectMatemaskWindow>();

            if (isUseMoralis)
            {
                authenticationKit.Connect();
            }
            else
            {
                DataBaseManager.Instance.OnInit.AddListener(AfterInitDataBase);
                DataBaseManager.Instance.SetMoralisUser = null;
                DataBaseManager.Instance.Initialize();
            }
        }

        public async void OnConnectMoralis()
        {
            BoxController.GetController<LogController>().Log("On connect to moralis");

            MoralisUser moralisUser = await Moralis.GetUserAsync();

            if (moralisUser != null)
            {
                DataBaseManager.Instance.OnInit.AddListener(AfterInitDataBase);
                DataBaseManager.Instance.SetMoralisUser = moralisUser;
                DataBaseManager.Instance.Initialize();
            }
            else
            {
                BoxController.GetController<LogController>().LogError($"User not connect to moralis!");
            }
        }

        private void AfterInitDataBase()
        {
            DataBaseManager.Instance.OnInit.RemoveListener(AfterInitDataBase);

            LoadSceneManager.instance.LoadDeckBuildScene();
        }

        #endregion

        public void FailedConnect()
        {
            BoxController.GetController<LogController>().LogError($"Failed connect to moralis!");
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