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
            loadCanvas.SetActive(true);

            AsyncOperation loadScene = SceneManager.LoadSceneAsync(numberScene);

            while (!loadScene.isDone)
            {
                yield return null;
            }

            loadCanvas.SetActive(false);

            StarterScene.instance.StartScene();
        }
    }
}