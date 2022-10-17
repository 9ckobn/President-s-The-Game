using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BoxController : MonoBehaviour
    {
        private static BaseController[] controllers;

        private static Dictionary<Type, object> data = new Dictionary<Type, object>();

        #region INIT

        public static void Init(BaseController[] controllers)
        {
            data.Clear();

            BoxController.controllers = controllers;

            Coroutines.StartRoutine(InitGameRoutine());
        }

        private static IEnumerator InitGameRoutine()
        {
            CreateControllers();
            yield return null;

            InitControllers();
            yield return null;

            StartControllers();
            yield return null;
        }

        private static void CreateControllers()
        {
            foreach (var manager in controllers)
            {
                var add = Instantiate(manager);

                data.Add(add.GetType(), add);
            }
        }

        private static void InitControllers()
        {
            foreach (var manager in data.Values)
            {
                (manager as BaseController).OnInitialize();
            }
        }

        private static void StartControllers()
        {
            foreach (var manager in data.Values)
            {
                (manager as BaseController).OnStart();
            }
        }

        #endregion

        public static T GetController<T>()
        {
            object manager;
            data.TryGetValue(typeof(T), out manager);
            return (T)manager;
        }
    }
}