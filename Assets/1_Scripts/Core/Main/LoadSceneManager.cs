using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class LoadSceneManager : Singleton<LoadSceneManager>
    {
        [SerializeField] private GameObject loadCanvas;

        public void LoadDeckBuildScene()
        {
            StartCoroutine(CoLoadScene(1));
        }

        public void LoadFightScene()
        {
            StartCoroutine(CoLoadScene(2));
        }

        private IEnumerator CoLoadScene(int numberScene)
        {
            Debug.Log($"DEBUG Start load scene {numberScene}");

            loadCanvas.SetActive(true);

            AsyncOperation loadScene = SceneManager.LoadSceneAsync(numberScene);

            while (!loadScene.isDone)
            {
                yield return null;
            }

            loadCanvas.SetActive(false);

            Debug.Log($"DEBUG End load scene");

            StarterScene.instance.StartScene();
        }
    }
}