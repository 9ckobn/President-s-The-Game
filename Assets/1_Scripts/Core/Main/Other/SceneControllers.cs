using UnityEngine;

namespace Core
{
    public class SceneControllers : Singleton<SceneControllers>
    {
        [SerializeField] private BaseController[] Controllers;

        private bool isInit;
        public bool GetIsInit { get => isInit; }

        protected override void AfterAwaik()
        {
            BoxController.Init(Controllers);

            isInit = true;
        }
    }
}