using System.Collections;
using UnityEngine;

namespace Core
{
    public class Coroutines : MonoBehaviour
    {
        private static Coroutines Instance => GetInstance();
        private static Coroutines instance;

        private static Coroutines GetInstance()
        {
            if (instance == null)
            {
                CreateCoroutines();
            }

            return instance;
        }

        private static Coroutines CreateCoroutines()
        {
            // TODO: Where delete coroutine?

            instance = new GameObject(NamesData.COROUTINES_NAME).AddComponent<Coroutines>();
            instance.hideFlags = HideFlags.HideAndDontSave;
            instance.transform.SetParent(instance.gameObject.transform);
            return instance;
        }

        public static Coroutine StartRoutine(IEnumerator enumerator)
        {
            return Instance.StartCoroutine(enumerator);
        }
    }
}