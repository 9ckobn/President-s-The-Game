using UI;
using UnityEngine;

namespace Core
{
    public class SceneControllers : Singleton<SceneControllers>
    {
        [SerializeField] private BaseController[] Controllers;

        private bool isInit;
        public bool GetIsInit { get => isInit; }

        public void InitControllers()
        {
            BoxController.OnInit += InitUI;
            BoxController.Init(Controllers);

            isInit = true;
        }

        private void InitUI()
        {
            BoxController.OnInit -= InitUI;

            UIManager.Instance.OnInitialize();
            UIManager.Instance.OnStart();
        }
    }
}