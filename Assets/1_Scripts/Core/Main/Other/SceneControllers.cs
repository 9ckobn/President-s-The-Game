using System;
using UI;
using UnityEngine;

namespace Core
{
    public class SceneControllers : Singleton<SceneControllers>
    {
        public static event Action OnInit;

        [SerializeField] private BaseController[] Controllers;

        public static void InitControllers()
        {
            BoxController.OnInit += instance.InitUI;
            BoxController.Init(instance.Controllers);
        }

        private void InitUI()
        { 
            BoxController.OnInit -= InitUI;

            UIManager.OnInit += EndInitialize;
            UIManager.Instance.OnInitialize();
            UIManager.Instance.OnStart();
        }

        private void EndInitialize()
        {
            UIManager.OnInit -= EndInitialize;

            if (OnInit != null)
            {
                OnInit();
            }
        }
    }
}