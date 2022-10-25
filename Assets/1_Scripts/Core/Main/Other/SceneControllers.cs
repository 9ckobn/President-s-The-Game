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
            BoxController.Init(Controllers);

            isInit = true;
        }
    }
}